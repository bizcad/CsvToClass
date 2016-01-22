using System;

namespace CsvClassGenerator
{
    public static class DateChecker
    {
        public static bool IsValidDate(string s)
        {
            DateTime date;
            if (s.Contains("-"))
            {
                if (s.Contains(":"))
                    return true;
                string[] ymd = s.Split('-');
                if (ymd.Length != 3)
                    return false;
                if (ymd[0].Length == 0 || ymd[1].Length == 0 || ymd[2].Length == 0)
                    return false;
                int year;
                int month;
                int day;
                bool result = int.TryParse(ymd[0], out year);
                if (!result)
                    return false;
                if (year <= 1970)
                    return false;
                result = int.TryParse(ymd[1], out month);
                if (!result)
                    return false;
                if (month > 12 || month < 1)
                    return false;
                result = int.TryParse(ymd[2], out day);
                if (!result)
                    return false;
                if (day > 31 || day < 1)
                    return false;
                return true;
            }

            return DateTime.TryParse(s, out date);
        }
        //public static bool IsValidDate(string s)
        //{
        //    DateTime date;
        //    if (s.Contains("-"))
        //    {
        //        if (s.Contains(":"))
        //            return true;
        //        string[] ymd = s.Split('-');
        //        if (ymd.Length != 3)
        //            return false;
        //        if (ymd[0].Length == 0 || ymd[1].Length == 0 || ymd[2].Length == 0)
        //            return false;
        //        return ymd.Length == 3
        //          && int.Parse(ymd[0]) > 1970
        //          && int.Parse(ymd[1]) <= 12
        //          && int.Parse(ymd[2]) <= 31;
        //    }

        //    return 
    }
}
