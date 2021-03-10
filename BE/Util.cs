using System;

namespace BE
{
    public class Util
    {
        public int convertDay(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case (DayOfWeek.Sunday): return -1;
                case (DayOfWeek.Monday): return -2;
                case (DayOfWeek.Tuesday): return -3;
                case (DayOfWeek.Wednesday): return -4;
                case (DayOfWeek.Thursday): return -5;
                case (DayOfWeek.Friday): return -6;
                default: return -7;
            }
        }

        public int convertDayPeriod(DateTime date)
        {
            int theHour = date.Hour;
            // Morning = -8.
            if (theHour >= 5 && theHour <= 10) { return -8; }
            // Noon = -9.
            if (theHour >= 11 && theHour <= 16) { return -9; }
            // Night = -10.
            return -10;
        }

        public string convertTimeIdToString(int timeId)
        {
            switch (timeId)
            {
                case (-1): return "Sunday";
                case (-2): return "Monday";
                case (-3): return "Tuesday";
                case (-4): return "Wednesday";
                case (-5): return "Thursday";
                case (-6): return "Friday";
                case (-7): return "Saturday";
                case (-8): return "Morning";
                case (-9): return "Noon";
                default: return "Night";
            }
        }
    }
}
