using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Qazi.EncodingSchemeManager
{
    public class EncodingFormateConvertor
    {
        public static DataTable HashToDataTable(Dictionary<string, string> htEncoding, DataTable dt)
        {
            DataRow row;

            foreach (string item in htEncoding.Keys)
            {
                if (item != "<IsBinaryString>")
                {
                    row = dt.NewRow();
                    row[0] = item;
                    row[1] = htEncoding[item];
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }


        public static DataTable HashToDataTable(Dictionary<string, string> htEncoding)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Items");
            dt.Columns.Add("Code");
            HashToDataTable(htEncoding, dt);
            return dt;
        }
    }
}
