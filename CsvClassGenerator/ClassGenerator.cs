using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvClassGenerator.Utility;


namespace CsvClassGenerator
{
    public class ClassGenerator
    {
        private string _inputFile;
        private string _destFile;
        public void GenerateClass(string defaultInputFile, string destinationFilePath,string name_space)
        {
            _inputFile = defaultInputFile;
            _destFile = destinationFilePath;
            string className = new FileInfo(destinationFilePath).Name.Replace(".cs", "");
            string classspec = FormatClass(name_space, className);
            using (var sw = new StreamWriter(_destFile, false))
            {
                sw.WriteLine(classspec);
                sw.Flush();
                sw.Close();
            }
        }
        public IList<string> ReadFileIntoList(string inputFile)
        {
            List<string> rows = new List<string>();
            using (var sr = new StreamReader(_inputFile))
            {
                while (!sr.EndOfStream)
                {
                    rows.Add(sr.ReadLine());
                }
                sr.Close();
            }

            return rows;
        }
        private string FormatClass(string nameSpace, string className)
        {
            var rows = ReadFileIntoList(_inputFile);

            StringBuilder sb = new StringBuilder();

            // write the class heading
            sb.AppendLine("using System;");
            sb.AppendLine();
            sb.Append("namespace ");
            sb.AppendLine(nameSpace);
            sb.AppendLine("{");
            sb.Append("    public class ");
            sb.AppendLine(className);
            sb.AppendLine("    {");
            sb.AppendLine("        public int Id { get; set; }");

            var propertySpecicationList = new CsvColumnTyper().ParseColumns(ref rows);
            foreach (var p in propertySpecicationList)
            {
                
                sb.Append(string.Format("        public {0}", p.ColumnType));
                if (p.Nullable)
                    sb.Append("?");
                sb.AppendLine(string.Format(" {0} {1} get; set; {2}", IllegalCharactersFilter.FilterIdentifier(p.ColumnName), "{", "}"));
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
