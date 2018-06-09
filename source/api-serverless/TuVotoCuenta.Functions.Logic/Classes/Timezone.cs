using System;

namespace TuVotoCuenta.Functions.Logic.Classes
{
    public class Timezone
    {
        public static string GetCustomTimeZone()
        {
            TimeZoneInfo setTimeZoneInfo;
            DateTime currentDateTime;

            //Set the time zone information to US Mountain Standard Time
            setTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

            //Get date and time in US Mountain Standard Time
            currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, setTimeZoneInfo);

            return currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}