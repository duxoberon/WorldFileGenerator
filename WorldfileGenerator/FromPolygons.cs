using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WorldfileGenerator
{
    public class FromPolygons : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public FromPolygons()
        {
        }

        protected override void OnClick()
        {
            new Forms.WorldfileGen_Form(ArcMap.Application).ShowDialog();
            ArcMap.Application.CurrentTool = null;
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
