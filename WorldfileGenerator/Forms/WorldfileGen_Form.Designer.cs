namespace WorldfileGenerator.Forms
{
    partial class WorldfileGen_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldfileGen_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.cboPolygonLayers = new System.Windows.Forms.ComboBox();
            this.tbxOutputLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseFolders = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cboNameField = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkUseSelected = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseTemplateRaster = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxTemplateRaster = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Polygon Layers";
            // 
            // cboPolygonLayers
            // 
            this.cboPolygonLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPolygonLayers.FormattingEnabled = true;
            this.cboPolygonLayers.Location = new System.Drawing.Point(23, 38);
            this.cboPolygonLayers.Name = "cboPolygonLayers";
            this.cboPolygonLayers.Size = new System.Drawing.Size(504, 23);
            this.cboPolygonLayers.TabIndex = 1;
            this.cboPolygonLayers.SelectedIndexChanged += new System.EventHandler(this.cboPolygonLayers_SelectedIndexChanged);
            // 
            // tbxOutputLocation
            // 
            this.tbxOutputLocation.Location = new System.Drawing.Point(23, 242);
            this.tbxOutputLocation.Name = "tbxOutputLocation";
            this.tbxOutputLocation.Size = new System.Drawing.Size(472, 21);
            this.tbxOutputLocation.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Location";
            // 
            // btnBrowseFolders
            // 
            this.btnBrowseFolders.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseFolders.Image")));
            this.btnBrowseFolders.Location = new System.Drawing.Point(501, 240);
            this.btnBrowseFolders.Name = "btnBrowseFolders";
            this.btnBrowseFolders.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseFolders.TabIndex = 4;
            this.btnBrowseFolders.UseVisualStyleBackColor = true;
            this.btnBrowseFolders.Click += new System.EventHandler(this.btnBrowseFolders_Click);
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.Color.Green;
            this.btnRun.Location = new System.Drawing.Point(449, 280);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 6;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(18, 284);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 15);
            this.lblVersion.TabIndex = 33;
            // 
            // cboNameField
            // 
            this.cboNameField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNameField.FormattingEnabled = true;
            this.cboNameField.Location = new System.Drawing.Point(23, 82);
            this.cboNameField.Name = "cboNameField";
            this.cboNameField.Size = new System.Drawing.Size(504, 23);
            this.cboNameField.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 34;
            this.label3.Text = "Name Field";
            // 
            // chkUseSelected
            // 
            this.chkUseSelected.AutoSize = true;
            this.chkUseSelected.Location = new System.Drawing.Point(29, 111);
            this.chkUseSelected.Name = "chkUseSelected";
            this.chkUseSelected.Size = new System.Drawing.Size(99, 19);
            this.chkUseSelected.TabIndex = 36;
            this.chkUseSelected.Text = "Use Selected";
            this.chkUseSelected.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(382, 15);
            this.label4.TabIndex = 37;
            this.label4.Text = "Provide a template raster (to acquire the pixel size and rotation value)";
            // 
            // btnBrowseTemplateRaster
            // 
            this.btnBrowseTemplateRaster.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowseTemplateRaster.Image")));
            this.btnBrowseTemplateRaster.Location = new System.Drawing.Point(501, 173);
            this.btnBrowseTemplateRaster.Name = "btnBrowseTemplateRaster";
            this.btnBrowseTemplateRaster.Size = new System.Drawing.Size(23, 23);
            this.btnBrowseTemplateRaster.TabIndex = 40;
            this.btnBrowseTemplateRaster.UseVisualStyleBackColor = true;
            this.btnBrowseTemplateRaster.Click += new System.EventHandler(this.btnBrowseTemplateRaster_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 15);
            this.label5.TabIndex = 39;
            this.label5.Text = "Template raster";
            // 
            // tbxTemplateRaster
            // 
            this.tbxTemplateRaster.Location = new System.Drawing.Point(23, 175);
            this.tbxTemplateRaster.Name = "tbxTemplateRaster";
            this.tbxTemplateRaster.Size = new System.Drawing.Size(472, 21);
            this.tbxTemplateRaster.TabIndex = 38;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // WorldfileGen_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 318);
            this.Controls.Add(this.btnBrowseTemplateRaster);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxTemplateRaster);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkUseSelected);
            this.Controls.Add(this.cboNameField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnBrowseFolders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxOutputLocation);
            this.Controls.Add(this.cboPolygonLayers);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorldfileGen_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Worldfile Generator";
            this.Load += new System.EventHandler(this.WorldfileGen_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPolygonLayers;
        private System.Windows.Forms.TextBox tbxOutputLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseFolders;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ComboBox cboNameField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox chkUseSelected;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseTemplateRaster;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxTemplateRaster;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}