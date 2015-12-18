using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Collections;
using System.Windows.Forms;

namespace WorldfileGenerator.Classes
{
    public class Utilities
    {

        #region Fields

        IMap m_pMap = null;
        private bool m_suppressmessage = false;

        #endregion

        #region Properties

        /// <summary>
        /// <para> FocusMap property </para>
        /// <para> Initialize this class with a Map object to use the majority of its methods </para>
        /// </summary>
        private IMap FocusMap
        {
            get
            {
                return m_pMap;
            }
        }

        /// <summary>
        /// Set true to suppress the appearance of a message box on error otherwise set to false.
        /// </summary>
        public bool SupressMessaging
        {
            get { return m_suppressmessage; }
            set { m_suppressmessage = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor overloded class
        /// </summary>
        public Utilities()
        {
        }

        /// <summary> Constructor overloaded class </summary>
        /// <param name="pMapControl"> ESRI's IMap object (IMapControl4) </param>
        public Utilities(IMap pMap)
        {
            m_pMap = pMap;
        }

        #endregion

        #region Methods

        /// <summary> The layers in current map </summary>
        /// <returns> Returns a list of all the layers in the map </returns>
        public ArrayList AllLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;


            try
            {
                for (int i = 0; i < FocusMap.LayerCount; i++)
                {
                    pList.Add(FocusMap.get_Layer(i).Name);
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The feature layers in current map </summary>
        /// <returns> Function to retrieve a list of all feature layers in current map </returns>
        public ArrayList FeatureLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (pLayer is IFeatureLayer)
                        pList.Add(pLayer.Name);
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> Returns a list of feature layers based on the geometry type </summary>
        /// <param name="geometryType">Geometry type.  Expected values "Point", "Line" or "Polygon".</param>
        /// <returns>Arraylist</returns>
        public ArrayList FeatureLayers(string geometryType)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)pLayer;
                        if (string.Compare(geometryType, "Point", true) == 0)
                        {
                            if ((featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint) || (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint))
                                pList.Add(pLayer.Name);
                        }
                        if (string.Compare(geometryType, "Line", true) == 0)
                        {
                            if ((featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline) || (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine))
                                pList.Add(pLayer.Name);
                        }
                        if (string.Compare(geometryType, "Polygon", true) == 0)
                        {
                            if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                                pList.Add(pLayer.Name);
                        }
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The selectable layers in current map </summary>
        /// <returns> Function to retrieve a list of all selectable layers in current map </returns>
        public ArrayList SelectableLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.Selectable == true)
                        pList.Add(pLayer.Name);
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The point feature layers in current map </summary>
        /// <returns> Function to retrieve a list of all point feature layers in current map </returns>
        public ArrayList PointLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryMultipoint || pFLayer.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Point Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The polyline feature layers in current map </summary>
        /// <returns> Function to retrieve a list of polyline feature layers in current map </returns>
        public ArrayList PolylineLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryLine || pFLayer.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Polyline Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The polygon feature layers in current map </summary>
        /// <returns> Function to retrieve a list of polygon feature layers in current map </returns>
        public ArrayList PolygonLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Polygon Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The list of all raster layers in current map </summary>
        /// <returns> Function to retrieve a list of all raster layers in current map </returns>
        public ArrayList RasterLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"; //DataLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (pLayer is IRasterLayer)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The selection count of the specified layer </summary>
        /// <param name="Layername"> The name of layer in the map to fetch </param>
        /// <returns> Function to retrieve selection count for a given layer </returns>
        public int SelectionCount(string Layername)
        {
            int cnt = 0;
            if (FocusMap == null)
                return cnt;
            if (FocusMap.LayerCount == 0)
                return cnt;

            try
            {
                ILayer pLayer = Layer(Layername);
                if (pLayer == null)
                    return cnt;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFLayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFLayer;
                    ISelectionSet pSelectionSet = pDisplayTable.DisplaySelectionSet;
                    cnt = pSelectionSet.Count;
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Selection Count - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return cnt;
        }

        ///<summary> The selection count of the specified layer </summary>
        /// <param name="pLayer"> The layer from which a selection count is to be fetched </param>
        ///<returns> Function to retrieve selection count for a given layer </returns>
        public int SelectionCount(ILayer pLayer)
        {
            int cnt = 0;
            if (pLayer == null)
                return cnt;
            try
            {
                if (pLayer == null)
                    return cnt;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFLayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFLayer;
                    ISelectionSet pSelectionSet = pDisplayTable.DisplaySelectionSet;
                    cnt = pSelectionSet.Count;
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Selection Count - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return cnt;
        }

        ///<summary> The fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all fields in a given layer </returns>
        public ArrayList AllFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        pList.Add(pFields.get_Field(i).AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all fields in a given layer </returns>
        public ArrayList AllFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        pList.Add(pFields.get_Field(i).AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The text fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of text fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all text fields in a given layer </returns>
        public ArrayList TextFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeString)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Text Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The text fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of text fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all text fields in a given layer </returns>
        public ArrayList TextFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeString)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Text Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The number fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of number fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all number fields in a given layer </returns>
        public ArrayList NumberFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDouble || pField.Type == esriFieldType.esriFieldTypeInteger || pField.Type == esriFieldType.esriFieldTypeSingle || pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Number Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The number fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of number fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all number fields in a given layer </returns>
        public ArrayList NumberFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDouble || pField.Type == esriFieldType.esriFieldTypeInteger || pField.Type == esriFieldType.esriFieldTypeSingle || pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Number Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The date fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of date fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all data fields in a given layer </returns>
        public ArrayList DateFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDate)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Date Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The date fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of date fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all data fields in a given layer </returns>
        public ArrayList DateFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDate)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Date Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The specified layer </summary>
        /// <param name="LayerName"> The layer be retrieved </param>
        ///<returns> Function to retrieve a layer by name </returns>
        public ILayer Layer(string LayerName)
        {
            if (FocusMap == null)
                return null;
            if (FocusMap.LayerCount == 0)
                return null;
            try
            {
                UID pID = new UIDClass();
                pID.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (string.Compare(pLayer.Name, LayerName, true) == 0)
                        return pLayer;
                    pLayer = pEnumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return null;
        }

        /// <summary>Returns the index of a particular field given the Name or Alias of a given field.</summary>
        /// <param name="o">Object that could either be a layer, featurelayer, featureclass, feature, row, table, standalone table or tablefields</param>
        /// <param name="FieldName"> Field name to search</param>
        /// <returns>Integer denoting the field index</returns>
        public int FindField(object o, string FieldName)
        {
            try
            {
                if (o is ILayer)
                {
                    ILayer layer = (ILayer)o;
                    if (!(layer == null))
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)layer;
                        if (!(featurelayer == null))
                        {
                            int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                            if (i > -1)
                                return i;
                            else
                            {
                                for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                                {
                                    if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                        return j;
                                }
                            }
                        }
                    }
                }
                if (o is IFeatureLayer)
                {
                    IFeatureLayer featurelayer = (IFeatureLayer)o;
                    if (!(featurelayer == null))
                    {
                        int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IFeatureClass)
                {
                    IFeatureClass featureclass = (IFeatureClass)o;
                    if (!(featureclass == null))
                    {
                        int i = featureclass.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < featureclass.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(featureclass.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IFeature)
                {
                    IFeature feature = (IFeature)o;
                    if (!(feature == null))
                    {
                        int i = feature.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < feature.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(feature.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IRow)
                {
                    IRow row = (IRow)o;
                    if (!(row == null))
                    {
                        int i = row.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < row.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(row.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is ITable)
                {
                    ITable table = (ITable)o;
                    if (!(table == null))
                    {
                        int i = table.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < table.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(table.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IStandaloneTable)
                {
                    IStandaloneTable standalonetable = (IStandaloneTable)o;
                    int i = standalonetable.Table.FindField(FieldName);
                    if (i > -1)
                        return i;
                    else
                    {
                        for (int j = 0; j < standalonetable.Table.Fields.FieldCount; j++)
                        {
                            if (FieldName.CompareTo(standalonetable.Table.Fields.get_Field(j).AliasName) == 0)
                                return j;
                        }
                    }
                }
                if (o is ITableFields)
                {
                    ITableFields tablefields = (ITableFields)o;
                    int i = tablefields.FindField(FieldName);
                    if (i > -1)
                        return i;
                    else
                    {
                        for (int j = 0; j < tablefields.FieldCount; j++)
                        {
                            if (FieldName.CompareTo(tablefields.get_FieldInfo(j).Alias) == 0)
                                return j;
                        }
                    }
                }
                if (o is IGeoFeatureLayer)
                {
                    IGeoFeatureLayer geofeaturelayer = (IGeoFeatureLayer)o;
                    if (!(geofeaturelayer == null))
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)geofeaturelayer;
                        if (!(featurelayer == null))
                        {
                            int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                            if (i > -1)
                                return i;
                            else
                            {
                                for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                                {
                                    if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                        return j;
                                }
                            }
                        }
                    }
                }
                if (o is IFields)
                {
                    IFields fields = (IFields)o;
                    if (!(fields == null))
                    {
                        int i = fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Release COM Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return -1;
        }

        ///<summary> Manually release COM references </summary>
        /// <param name="comObj"> The object to be released </param>
        ///<returns> Function to release COM references </returns>
        public void Release(object comObj)
        {
            try
            {
                int refsLeft = 0;
                do
                {
                    refsLeft = System.Runtime.InteropServices.Marshal.ReleaseComObject(comObj);
                } while (refsLeft > 0);
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Release COM Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ///<summary> Rendering line styles </summary>
        ///<returns> Returns a list of Line Styles </returns>
        public ArrayList LineStyles()
        {
            ArrayList pList = new ArrayList();
            pList.AddRange(new object[] { "Solid",
                                              "Dash",
                                              "Dash Dot",
                                              "Dash Dot Dot",
                                              "Dot",
                                              "Inside Frame"});
            return pList;
        }

        ///<summary> Rendering fill styles </summary>
        ///<returns> Returns a list of Fill Styles </returns>
        public ArrayList FillStyles()
        {
            ArrayList pList = new ArrayList();
            pList.AddRange(new object[] { "Hollow", 
                                              "Solid",
                                              "Empty",
                                              "Horizontal Hatch",
                                              "Vertical Hatch",
                                              "Backward Diagonal",
                                              "Forward Diagonal",
                                              "Crosshatch",
                                              "Diagonal Cross"});

            return pList;
        }

        ///<summary> Rendering line styles</summary>
        ///<returns> Returns Line Style literal </returns>
        public string LineStyleLiteral(esriSimpleLineStyle pStyle)
        {
            string linestyle = "Solid";
            switch (pStyle)
            {
                case esriSimpleLineStyle.esriSLSDash:
                    linestyle = "Dash";
                    break;
                case esriSimpleLineStyle.esriSLSDashDot:
                    linestyle = "Dash Dot";
                    break;
                case esriSimpleLineStyle.esriSLSDashDotDot:
                    linestyle = "Dash Dot Dot";
                    break;
                case esriSimpleLineStyle.esriSLSDot:
                    linestyle = "Dot";
                    break;
                case esriSimpleLineStyle.esriSLSInsideFrame:
                    linestyle = "Inside Frame";
                    break;
                case esriSimpleLineStyle.esriSLSNull:
                    linestyle = "";
                    break;
                case esriSimpleLineStyle.esriSLSSolid:
                    linestyle = "Solid";
                    break;
            }
            return linestyle;
        }

        ///<summary> Rendering line styles</summary>
        /// <param name="pStyle"> ESRI Style </param>
        ///<returns> Returns Line Style </returns>
        public esriSimpleLineStyle LineStyle(string pStyle)
        {
            esriSimpleLineStyle linestyle = esriSimpleLineStyle.esriSLSSolid;
            switch (pStyle)
            {
                case "Solid":
                    linestyle = esriSimpleLineStyle.esriSLSSolid;
                    break;
                case "Dash":
                    linestyle = esriSimpleLineStyle.esriSLSDash;
                    break;
                case "Dash Dot":
                    linestyle = esriSimpleLineStyle.esriSLSDashDot;
                    break;
                case "Dash Dot Dot":
                    linestyle = esriSimpleLineStyle.esriSLSDashDotDot;
                    break;
                case "Dot":
                    linestyle = esriSimpleLineStyle.esriSLSDot;
                    break;
                case "Inside Frame":
                    linestyle = esriSimpleLineStyle.esriSLSInsideFrame;
                    break;
            }
            return linestyle;
        }

        ///<summary> Rendering fill styles </summary>
        /// <param name="pStyle"> ESRI Style </param>
        ///<returns> Returns retrieve Fill Style literal </returns>
        public string FillStyleLiteral(esriSimpleFillStyle pStyle)
        {
            string fillstyle = "Solid";
            switch (pStyle)
            {
                case esriSimpleFillStyle.esriSFSBackwardDiagonal:
                    fillstyle = "Backward Diagonal";
                    break;
                case esriSimpleFillStyle.esriSFSCross:
                    fillstyle = "Crosshatch";
                    break;
                case esriSimpleFillStyle.esriSFSDiagonalCross:
                    fillstyle = "Diagonal Cross";
                    break;
                case esriSimpleFillStyle.esriSFSForwardDiagonal:
                    fillstyle = "Forward Diagonal";
                    break;
                case esriSimpleFillStyle.esriSFSHollow:
                    fillstyle = "Hollow";
                    break;
                case esriSimpleFillStyle.esriSFSHorizontal:
                    fillstyle = "Horizontal Hatch";
                    break;
                case esriSimpleFillStyle.esriSFSSolid:
                    fillstyle = "Solid";
                    break;
                case esriSimpleFillStyle.esriSFSVertical:
                    fillstyle = "Vertical Hatch";
                    break;
            }
            return fillstyle;
        }

        ///<summary> Rendering fill styles </summary>
        /// <param name="pStyle"> ESRI Style </param>
        ///<returns> Returns Fill Style  </returns>
        public esriSimpleFillStyle FillStyle(string pStyle)
        {
            esriSimpleFillStyle fillstyle = esriSimpleFillStyle.esriSFSHollow;
            switch (pStyle)
            {
                case "Hollow":
                    fillstyle = esriSimpleFillStyle.esriSFSHollow;
                    break;
                case "Solid":
                    fillstyle = esriSimpleFillStyle.esriSFSSolid;
                    break;
                case "Empty":
                    fillstyle = esriSimpleFillStyle.esriSFSNull;
                    break;
                case "Horizontal Hatch":
                    fillstyle = esriSimpleFillStyle.esriSFSHorizontal;
                    break;
                case "Vertical Hatch":
                    fillstyle = esriSimpleFillStyle.esriSFSVertical;
                    break;
                case "Backward Diagonal":
                    fillstyle = esriSimpleFillStyle.esriSFSBackwardDiagonal;
                    break;
                case "Forward Diagonal":
                    fillstyle = esriSimpleFillStyle.esriSFSForwardDiagonal;
                    break;
                case "Crosshatch":
                    fillstyle = esriSimpleFillStyle.esriSFSCross;
                    break;
                case "Diagonal Cross":
                    fillstyle = esriSimpleFillStyle.esriSFSDiagonalCross;
                    break;
            }
            return fillstyle;
        }

        ///<summary> ESRI measurement units </summary>
        ///<returns> Returns a list of esri Units </returns>
        public ArrayList Units()
        {
            ArrayList pList = new ArrayList();
            pList.AddRange(new object[] { "Inches",
                                              "Feet",
                                              "Yards",
                                              "Miles",
                                              "Nautical Miles",
                                              "Millimeters",
                                              "Centimeters",
                                              "Meters",
                                              "Kilometers",
                                              "Decimal degrees",
                                              "Decimeters",});
            return pList;
        }

        ///<summary> ESRI measurement units </summary>
        /// <param name="sUnits"> ESRI Units </param>
        ///<returns> Function to retrieve esriUnits </returns>
        public esriUnits Unit(string sUnits)
        {
            esriUnits pUnits = esriUnits.esriFeet;
            switch (sUnits)
            {
                case "Inches":
                    pUnits = esriUnits.esriInches;
                    break;
                case "Feet":
                    pUnits = esriUnits.esriFeet;
                    break;
                case "Yards":
                    pUnits = esriUnits.esriYards;
                    break;
                case "Miles":
                    pUnits = esriUnits.esriMiles;
                    break;
                case "Nautical Miles":
                    pUnits = esriUnits.esriNauticalMiles;
                    break;
                case "Millimeters":
                    pUnits = esriUnits.esriMillimeters;
                    break;
                case "Centimeters":
                    pUnits = esriUnits.esriCentimeters;
                    break;
                case "Meters":
                    pUnits = esriUnits.esriMeters;
                    break;
                case "Kilometers":
                    pUnits = esriUnits.esriKilometers;
                    break;
                case "Decimal degrees":
                    pUnits = esriUnits.esriDecimalDegrees;
                    break;
                case "Decimeters":
                    pUnits = esriUnits.esriDecimeters;
                    break;
            }
            return pUnits;
        }

        ///<summary> ESRI measurement units </summary>
        /// <param name="pUnits"> ESRI Units </param>
        ///<returns> Function to retrieve esriUnits literal </returns>
        public string UnitsLiteral(esriUnits pUnits)
        {
            string sUnits = "Feet";
            switch (pUnits)
            {
                case esriUnits.esriCentimeters:
                    sUnits = "Centimeters";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sUnits = "Decimal degrees";
                    break;
                case esriUnits.esriDecimeters:
                    sUnits = "Decimeters";
                    break;
                case esriUnits.esriFeet:
                    sUnits = "Feet";
                    break;
                case esriUnits.esriInches:
                    sUnits = "Inches";
                    break;
                case esriUnits.esriKilometers:
                    sUnits = "Kilometers";
                    break;
                case esriUnits.esriMeters:
                    sUnits = "Meters";
                    break;
                case esriUnits.esriMiles:
                    sUnits = "Miles";
                    break;
                case esriUnits.esriMillimeters:
                    sUnits = "Millimeters";
                    break;
                case esriUnits.esriNauticalMiles:
                    sUnits = "Nautical Miles";
                    break;
                case esriUnits.esriYards:
                    sUnits = "Yards";
                    break;
            }
            return sUnits;
        }

        /// <summary>
        /// Returns a single integer value for a color given the integer values of the individual component values for red, green and blue.
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">Green</param>
        /// <param name="B">Blue</param>
        /// <returns>Integer</returns>
        public int GetRGBinteger(int R, int G, int B)
        {
            return R + (256 * G) + (65536 * B);
        }

        /// <summary>
        /// Returns a one dimensional byte array containing the byte values of the individual components given the integer value for the color.
        /// </summary>
        /// <param name="intRGB">The RGB integer value</param>
        /// <returns>A byte array</returns>
        public byte[] GetRGBbytes(int intRGB)
        {
            byte[] bytArray = new byte[3];
            bytArray[0] = System.Convert.ToByte(intRGB % 256);
            bytArray[1] = System.Convert.ToByte((intRGB / 256) % 256);
            bytArray[2] = System.Convert.ToByte((intRGB / 65536) % 256);
            return bytArray;
        }

        #endregion

        #region Destructor
        ///<summary> Destroy </summary>
        ~Utilities()
        {
        }

        #endregion

    }
}
