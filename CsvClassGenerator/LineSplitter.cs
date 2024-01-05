namespace CsvClassGenerator
{
    public static class LineSplitter
    {
        public static string[] SplitFilterCommasInQuotedStrings(string line)
        {            
            return SimpleSplitOnCommas(FilterCommasInFields(line));            
        }
        public static string[] SimpleSplitOnCommas(string line)
        {
            return line.Split(',');            
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
    }
}
