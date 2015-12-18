using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace WorldfileGenerator.Classes
{
    public class Generator
    {
        internal static void CreateWorldfile(ESRI.ArcGIS.Geodatabase.IFeature feature, string outputlocation, string namefield, 
            string pixelsizex, string pixelsizey, string rotationx, string rotationy)
        {

            int namefieldindex = FindField(feature, namefield);
            if (namefieldindex > -1)
            {
                object obj = feature.get_Value(namefieldindex);
                if (obj != DBNull.Value)
                {
                    string worldfilename = obj.ToString();
                    if (worldfilename.Length > 0)
                    {
                        string outputworldfile = Path.Combine(outputlocation, worldfilename + ".tfw");

                        double d = Convert.ToDouble(pixelsizex);
                        pixelsizex = Math.Round(d, 3).ToString();
                        d = Convert.ToDouble(pixelsizey);
                        pixelsizey = Math.Round(d, 3).ToString();
                        d = Convert.ToDouble(rotationx);
                        rotationx = Math.Round(d, 3).ToString();
                        d = Convert.ToDouble(rotationy);
                        rotationy = Math.Round(d, 3).ToString();

                        using (StreamWriter sw = new StreamWriter(outputworldfile))
                        {
                            sw.WriteLine(pixelsizex);
                            sw.WriteLine(rotationy);
                            sw.WriteLine(rotationx);
                            sw.WriteLine(pixelsizey);
                            sw.WriteLine(feature.Extent.XMin.ToString());
                            sw.WriteLine(feature.Extent.YMax.ToString());
                        }
                    }
                }
            }
        }

        public static int FindField(object o, string FieldName)
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
                throw new Exception(ex.Message);
            }
        }

    }
}
