using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ArcMapUI;
using WorldfileGenerator.Classes;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System;
using ESRI.ArcGIS.DataSourcesRaster;

namespace WorldfileGenerator.Forms
{
    public partial class WorldfileGen_Form : Form
    {

        #region Fields

        public IApplication _application;
        private IMxDocument _mxdocument;
        private IMap _map;
        private IActiveView _activeView;
        private IProgressDialog2 _progressdialog;
        private Utilities _utilities;

        #endregion

        #region Constructor

        public WorldfileGen_Form(IApplication application)
        {
            _application = application;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void WorldfileGen_Form_Load(object sender, System.EventArgs e)
        {
            this.lblVersion.Text = "Version: " + this.ProductVersion;

            _mxdocument = (IMxDocument)_application.Document;
            _map = _mxdocument.FocusMap;
            _activeView = _mxdocument.ActiveView;

            if (_utilities == null) _utilities = new Utilities(_map);

            this.cboPolygonLayers.Items.AddRange(_utilities.PolygonLayers().ToArray());
            if (this.cboPolygonLayers.Items.Count > 0) this.cboPolygonLayers.SelectedIndex = 0;

        }

        private void btnBrowseTemplateRaster_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.tbxTemplateRaster.Text = this.openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cboPolygonLayers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cboNameField.Items.Count > 0) this.cboNameField.Items.Clear();
            this.cboNameField.Items.AddRange(_utilities.TextFields(this.cboPolygonLayers.Text).ToArray());
            if (this.cboNameField.Items.Count > 0) this.cboNameField.SelectedIndex = 0;

            ILayer polygonlayer = _utilities.Layer(this.cboPolygonLayers.Text);
            IFeatureLayer polygonfeaturelayer = polygonlayer as IFeatureLayer;
            IFeatureSelection polygonfeatureselection = polygonfeaturelayer as IFeatureSelection;
            if (polygonfeatureselection.SelectionSet.Count > 0)
            {
                this.chkUseSelected.Text = "Use selection (" + polygonfeatureselection.SelectionSet.Count.ToString() + ")";
                this.chkUseSelected.Checked = true;
                this.chkUseSelected.Enabled = true;
            }
            else
            {
                this.chkUseSelected.Text = "Use selection";
                this.chkUseSelected.Checked = false;
                this.chkUseSelected.Enabled = false;
            }
        }

        private void btnBrowseFolders_Click(object sender, System.EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.tbxOutputLocation.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnRun_Click(object sender, System.EventArgs e)
        {

            //Field checks!!!!!!!
            if (this.cboPolygonLayers.Text.Length == 0) return;
            if (this.cboNameField.Text.Length == 0) return;
            if (this.tbxTemplateRaster.Text.Length == 0) return;
            if (!System.IO.File.Exists(this.tbxTemplateRaster.Text)) return;
            if (!System.IO.Directory.Exists(this.tbxOutputLocation.Text)) return;

            //Check raster
            IRasterDataset rasterdataset = OpenRasterFileAsRasterDatset(System.IO.Path.GetDirectoryName(this.tbxTemplateRaster.Text),
                System.IO.Path.GetFileName(this.tbxTemplateRaster.Text));
            if (rasterdataset == null) return;
            //IRaster raster = rasterdataset.CreateDefaultRaster();
            //IRasterProps rasterprops = raster as IRasterProps;

            IWorldFileExport worldfileexport = rasterdataset as IWorldFileExport;
            worldfileexport.Write();

            string pixelsizex = string.Empty;
            string pixelsizey = string.Empty;
            string rotationx = string.Empty;
            string rotationy = string.Empty;

            string worldfile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.tbxTemplateRaster.Text), System.IO.Path.GetFileNameWithoutExtension(this.tbxTemplateRaster.Text));
            worldfile += ".tfw";
            FileInfo fi = new FileInfo(worldfile);
            if (fi.Exists)
            {
                using (StreamReader sr = fi.OpenText())
                {
                    string s = "";
                    int linenumber = 1;
                    while ((s = sr.ReadLine()) != null)
                    {
                        switch (linenumber)
                        {
                            case 1:
                                pixelsizex = s;
                                break;
                            case 2:
                                rotationy = s;
                                break;
                            case 3:
                                rotationx = s;
                                break;
                            case 4:
                                pixelsizey = s;
                                break;
                        }
                        linenumber++;
                    }
                }
            }
            else return;

            ILayer polygonlayer = _utilities.Layer(this.cboPolygonLayers.Text);
            if (polygonlayer != null)
            {
                IFeatureLayer polygonfeaturelayer = polygonlayer as IFeatureLayer;
                IFeatureClass polygonfeatureclass = polygonfeaturelayer.FeatureClass;
                ITrackCancel trackcancel = new CancelTrackerClass();
                IProgressDialogFactory progressdialogfactory = new ProgressDialogFactoryClass();
                IStepProgressor stepprogressor = progressdialogfactory.Create(trackcancel, _application.hWnd);
                stepprogressor.MinRange = 0;
                stepprogressor.MaxRange = polygonfeatureclass.FeatureCount(null);
                stepprogressor.StepValue = 1;
                stepprogressor.Message = "Polygons";
                _progressdialog = (IProgressDialog2)stepprogressor; // Explict Cast
                _progressdialog.CancelEnabled = true;
                _progressdialog.Description = "Processing " + polygonfeatureclass.FeatureCount(null).ToString() + " polygons.";
                _progressdialog.Title = "Processing...";
                _progressdialog.Animation = esriProgressAnimationTypes.esriProgressSpiral;
                try
                {
                    int count = 1;
                    if (this.chkUseSelected.Checked)
                    {
                        IFeatureSelection featureselection = polygonfeaturelayer as IFeatureSelection;
                        int featurecount = featureselection.SelectionSet.Count;
                        stepprogressor.MaxRange = featureselection.SelectionSet.Count;
                        IEnumIDs enumIDs = featureselection.SelectionSet.IDs;
                        enumIDs.Reset();
                        int intOID = enumIDs.Next();
                        while (intOID != -1)
                        {
                            IFeature feature = polygonfeatureclass.GetFeature(intOID);
                            if (feature != null)
                            {
                                Generator.CreateWorldfile(feature, this.tbxOutputLocation.Text, this.cboNameField.Text, pixelsizex, pixelsizey, rotationx, rotationy);
                            }
                            intOID = enumIDs.Next();
                            bool cont = trackcancel.Continue();
                            if (!cont)
                                break;

                            stepprogressor.Message = "Processing polygon " + count.ToString() + " of " + featurecount.ToString();
                            stepprogressor.Step();
                            count++;
                        }
                    }
                    else
                    {
                        int featurecount = polygonfeatureclass.FeatureCount(null);
                        IFeatureCursor featurecursor = polygonfeatureclass.Search(null, false);
                        IFeature feature = null;
                        while ((feature = featurecursor.NextFeature()) != null)
                        {
                            Generator.CreateWorldfile(feature, this.tbxOutputLocation.Text, this.cboNameField.Text, pixelsizex, pixelsizey, rotationx, rotationy);
                            bool cont = trackcancel.Continue();
                            if (!cont)
                                break;

                            stepprogressor.Message = "Processing building " + count.ToString() + " of " + featurecount.ToString();
                            stepprogressor.Step();
                            count++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ABE Calculators", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    trackcancel = null;
                    stepprogressor = null;
                    _progressdialog.HideDialog();
                    _progressdialog = null;
                }
            }
        }

        #endregion

        #region Methods

        public IRasterDataset OpenRasterFileAsRasterDatset(System.String path, System.String name)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
                IRasterWorkspace rasterWorkspace = (IRasterWorkspace)(workspaceFactory.OpenFromFile(path, 0));
                IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset(name);
                return rasterDataset;
            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        #endregion

    }
}
