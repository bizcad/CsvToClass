namespace CsvClassGenerator
{
    public static class TimeChecker
    {
        public static bool IsValidTime(string s)
        {
            if (s.Contains("-"))
            {
                if (s.Contains(":"))
                    return true;
            }

            string[] hms = s.Split(':');

            if (hms.Length != 3)
                return false;
            if (hms[0].Length == 0 || hms[1].Length == 0 || hms[2].Length == 0)
                return false;
            int h;
            int m;
            int sec;
            bool result = int.TryParse(hms[0], out h);
            if (!result)
                return false;
            if (h > 23 || h < 0)
                return false;
            result = int.TryParse(hms[1], out m);
            if (!result)
                return false;
            if (m > 60 || m < 0)
                return false;
            result = int.TryParse(hms[2], out sec);
            if (!result)
                return false;
            if (sec > 60 || sec < 0)
                return false;
            return true;
        }
        //public static bool IsValidTime(string s)
        //{
        //    if (s.Contains("-"))
        //    {
        //        if (s.Contains(":"))
        //            return true;
        //    }

        //    string[] hms = s.Split(':');
        //    //var a = hms.Length == 3;
        //    //var b = int.Parse(hms[0]) < 24;
        //    //var c = int.Parse(hms[1]) < 60;
        //    //var d = int.Parse(hms[2]) < 60;
        //    //return (a && b && c && d);
        //    return hms.Length == 3
        //           && int.Parse(hms[0]) < 24
        //           && int.Parse(hms[1]) < 60
        //           && int.Parse(hms[2]) < 60;
        //}
    }
}
