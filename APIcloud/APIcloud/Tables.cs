using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace APIcloud
{
    internal class Tables
    {
        #region Tables
        public class Color
        {
            public int a { get; set; }
            public int r { get; set; }
            public int g { get; set; }
            public int b { get; set; }
        }
        public class EllipseElement
        {
            public Color color { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public int angle { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
        public class Font
        {
            public string fontFamily { get; set; }
            public int size { get; set; }
            public string fontStyle { get; set; }
        }
        public class MarkElement
        {
            public string text { get; set; }
            public Font font { get; set; }
            public Color color { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public int angle { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
        public class RectangleElement
        {
            public Color color { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public int angle { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
        public class RestaurantSection
        {
            public string id { get; set; }
            public string terminalGroupId { get; set; }
            public string name { get; set; }
            public List<Table> tables { get; set; }
            public Schema schema { get; set; }
        }
        public class GetTable
        {
            public string correlationId { get; set; }
            public List<RestaurantSection> restaurantSections { get; set; }
            public Int64 revision { get; set; }
        }
        public class Schema
        {
            public int width { get; set; }
            public int height { get; set; }
            public List<MarkElement> markElements { get; set; }
            public List<TableElement> tableElements { get; set; }
            public List<RectangleElement> rectangleElements { get; set; }
            public List<EllipseElement> ellipseElements { get; set; }
            public Int64 revision { get; set; }
            public bool isDeleted { get; set; }
        }
        public class Table
        {
            public string id { get; set; }
            public int number { get; set; }
            public string name { get; set; }
            public int seatingCapacity { get; set; }
            public Int64 revision { get; set; }
            public bool isDeleted { get; set; }
        }
        public class TableElement
        {
            public string tableId { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
           // public int angle { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }
        #endregion
        public static string Get(string TiD)
        {
            var url = "https://api-ru.iiko.services/api/1/reserve/available_restaurant_sections";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer "+ TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"terminalGroupIds\":[\"{TiD}\"], \"returnSchema\":true, \"revision\":0}}";

            using (StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
