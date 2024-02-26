﻿using FlexCel.Report;
using Inventec.Common.FileFolder;
using Inventec.Common.Logging;
using Inventec.Fss.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MRS.MANAGER.Core.MrsReport.Lib
{
    public class GetCellValue
    {
        public static string Get(string reportTemplateUrl ,int row,int column)
        {
            string result = "";
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                int xf = 0;
                var st=xls.GetCellValue(1,row, column,ref xf);
                if (st != null)
                {
                    result = st.ToString();
                }

                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static string GetBySheetIndex(string reportTemplateUrl, int row, int column, int sheetIndex)
        {
            string result = "";
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                int xf = 0;
                var st = xls.GetCellValue(sheetIndex, row, column, ref xf);
                if (st != null)
                {
                    result = st.ToString();
                }

                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<string> GetBySheetIndex(string reportTemplateUrl, int x, int y, int with, int height, int sheetIndex)
        {
            List<string> result = new List<string>();
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                for (int i = x; i < x+with; i++)
                {
                    for (int j = y; j < y+height; j++)
                    {
                        int xf = 0;
                        var st = xls.GetCellValue(sheetIndex, i, j, ref xf);
                        if (st != null)
                        {
                            result.Add(st.ToString());
                        }
                        else
                        {
                            result.Add("");
                        }
                    }
                }


                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static string GetBySheetName(string reportTemplateUrl, int row, int column, string sheetName)
        {
            string result = "";
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                int xf = 0;
                int sheetIndex = xls.GetSheetIndex(sheetName);
                var st = xls.GetCellValue(sheetIndex, row, column, ref xf);
                if (st != null)
                {
                    result = st.ToString();
                }

                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<string> GetBySheetIndex(string reportTemplateUrl, int x, int y, int with, int height, string sheetName)
        {
            List<string> result = new List<string>();
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                int sheetIndex = xls.GetSheetIndex(sheetName);
                for (int i = x; i < x + with; i++)
                {
                    for (int j = y; j < y + height; j++)
                    {
                        int xf = 0;
                        var st = xls.GetCellValue(sheetIndex, i, j, ref xf);
                        if (st != null)
                        {
                            result.Add(st.ToString());
                        }
                        else
                        {
                            result.Add("");
                        }
                    }
                }


                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<string> GetBySheetIndex(byte[] reportTemplateData, int x, int y, int with, int height, int sheetIndex)
        {
            List<string> result = new List<string>();
            try
            {
                using ( var templateClientStream = new MemoryStream())
                {
                    templateClientStream.Write(reportTemplateData, 0, (int)reportTemplateData.Length);
                    templateClientStream.Position = 0;
                    FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                    xls.Open(templateClientStream);
                    for (int i = x; i < x + with; i++)
                    {
                        for (int j = y; j < y + height; j++)
                        {
                            int xf = 0;
                            var st = xls.GetCellValue(sheetIndex, i, j, ref xf);
                            if (st != null)
                            {
                                result.Add(st.ToString());
                            }
                            else
                            {
                                result.Add("");
                            }
                        }
                    }


                    xls.Save(templateClientStream);
                } 
               
                
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<string> GetBySheetIndex(string reportTemplateUrl, int x, int y, int with, int height, string sheetName, Dictionary<string, object> filter)
        {
            List<string> result = new List<string>();
            try
            {
                ResultFile templateFile = JsonConvert.DeserializeObject<ResultFile>(reportTemplateUrl);
                var template = FileDownload.GetFile(templateFile.URL);
                MemoryStream TemplateStream = new MemoryStream();
                template.CopyTo(TemplateStream);
                TemplateStream.Position = 0;
                FlexCel.XlsAdapter.XlsFile xls = new FlexCel.XlsAdapter.XlsFile(true);
                xls.Open(TemplateStream);
                int sheetIndex = xls.GetSheetIndex(sheetName);
                for (int i = x; i < x + with; i++)
                {
                    for (int j = y; j < y + height; j++)
                    {
                        int xf = 0;
                        var st = xls.GetCellValue(sheetIndex, i, j, ref xf);
                        if (st != null)
                        {
                            string s = st.ToString();
                            new ManagerSql().ReplaceFilter(filter,ref s);
                            result.Add(s);
                        }
                        else
                        {
                            result.Add("");
                        }
                    }
                }


                xls.Save(TemplateStream);
                //resultStream = result;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
