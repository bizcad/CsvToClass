using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CsvClassGenerator
{
    public class CsvColumnTyper
    {
        public List<PropertySpecification> ParseColumns(ref IList<string> rows)
        {
            // pick off the heading line
            string line0 = rows[0];
            var headings = rows[0].Split(',');
            rows.Remove(line0);

            List<PropertySpecification> propertySpecs = new List<PropertySpecification>();


            int rownum = 0;
            foreach (var line in rows)
            {
                if (line.Trim().Length == 0)
                    continue;
                var row = SplitLine(line);
                for (var columnid = 0; columnid < headings.Length; columnid++)
                {
                    try
                    {
                        string contents = row[columnid].Replace("\"", "");
                        var deduced = InferType(ColumnTypeCreate(contents));
                        PropertySpecification spec = propertySpecs.FirstOrDefault(s => s.Id == columnid);
                        if (spec == null)
                        {
                            propertySpecs.Add(new PropertySpecification
                            {
                                ColumnName = headings[columnid].Replace('"', ' ').Replace("/", string.Empty),
                                Id = columnid,
                                ColumnType = deduced,
                                Nullable = CheckForNullable(contents, deduced)
                            });
                        }
                        else
                        {
                            // elevate the type
                            if (spec.ColumnType < deduced && spec.ColumnType != CTEnum.String)
                            {
                                spec.ColumnType = deduced;
                            }
                            if (!spec.Nullable)
                            {
                                if (deduced != CTEnum.String && contents.Length == 0)
                                    Debug.WriteLine("Should be true");
                                spec.Nullable = CheckForNullable(contents, deduced);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("{0} - Row {1} - Column {2} - Value {3}", e.Message, line, rownum, row[columnid]));
                    }
                }
                rownum++;
                if (rownum == 57)
                    System.Diagnostics.Debug.WriteLine("56");
            }

            return propertySpecs;
        }

        public CTEnum InferType(CsvColumnType t)
        {
            if (t.IsTime)
                return CTEnum.DateTime;
            if (t.IsDate)
            {
                return CTEnum.DateTime;
            }
            if (t.IsInt32)
            {
                return CTEnum.Int32;
            }
            if (t.IsInt64)
            {
                return CTEnum.Int64;
            }
            if (t.IsDecimal)
            {
                return CTEnum.Decimal;
            }
            if (t.IsDouble)
            {
                return CTEnum.Double;
            }
            if (t.IsString)
            {
                return CTEnum.String;
            }

            throw new InvalidDataException("Unknown data type in column");
        }

        public static CsvColumnType ColumnTypeCreate(string s)
        {

            try
            {
                var t = new CsvColumnType
                {
                    IsDate = CheckDate(s),
                    IsTime = CheckTime(s),
                    IsDecimal = CheckDecimal(s),
                    IsDouble = CheckDouble(s),
                    IsInt32 = CheckInt32(s),
                    IsInt64 = CheckInt64(s),
                    IsNull = CheckForNull(s)
                };
                t.IsString = !(t.IsDate || t.IsDecimal || t.IsInt32 || t.IsInt64 || t.IsTime);

                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public static bool CheckForNull(string s)
        {
            return s.Length == 0;
        }
        public static bool CheckForNullable(string s, CTEnum deduced)
        {
            if (deduced == CTEnum.String)
                return false;
            return s.Length == 0;
        }
        public static bool CheckInt32(string s)
        {
            int d = 0;
            //int c = 0;
            //var ret = int.TryParse(s, out c);
            return int.TryParse(s, out d);
        }

        public static bool CheckInt64(string s)
        {
            Int64 d = 0;
            //Int64 c = 0;
            //var ret = Int64.TryParse(s, out c);
            return Int64.TryParse(s, out d);
        }

        public static bool CheckDecimal(string s)
        {
            decimal d = 0;
            //decimal c = 0;
            //var ret = decimal.TryParse(s, out c);
            return decimal.TryParse(s, out d);
        }

        public static bool CheckDouble(string s)
        {
            Double d = 0;
            //Double c = 0;
            //var ret = Double.TryParse(s, out c);
            return Double.TryParse(s, out d);
        }

        public static bool CheckTime(string s)
        {
            return TimeChecker.IsValidTime(s);
        }

        public static bool CheckDate(string s)
        {
            return DateChecker.IsValidDate(s);
        }
        public static string[] SplitLine(string line)
        {
            string filtered = FilterCommasInFields(line);

            return filtered.Split(',');

        }

        private static string FilterCommasInFields(string line)
        {
            string dq = "\"";
            while (line.Contains(dq))
            {
                int start = line.IndexOf(dq);
                int end = line.IndexOf(dq, start + 1);
                string partline = line.Substring(start, end - start + 1);
                string newpart = partline.Replace(",", string.Empty).Replace(dq, string.Empty);
                line = line.Replace(partline, newpart);                
            }
            return line;
        }

        public static string CheckForEndingDoubleQuote(string l)
        {
            return l.EndsWith("\"") ? l.Substring(0, l.Length - 1) : l;
        }

        public static string CheckForStartingDoubleQuote(string l)
        {
            return l.StartsWith("\"") ? l.Substring(1) : l;
        }
    }
}
