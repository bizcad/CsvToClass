namespace CsvClassGenerator
{
    public class CsvColumnType
    {
        public bool IsNull { get; set; }
        public bool IsInt32 { get; set; }
        public bool IsInt64 { get; set; }
        public bool IsDecimal { get; set; }
        public bool IsDouble { get; set; }
        public bool IsDate { get; set; }
        public bool IsTime { get; set; }
        public bool IsString { get; set; }
    }
}