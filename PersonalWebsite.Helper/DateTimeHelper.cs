using System;

namespace PersonalWebsite.Helper
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 日期转人性化字符串
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime dateTime)
        {
            DateTime nowDateTime = DateTime.Now;
            int hours = (int)(nowDateTime - dateTime).TotalHours;
            if (hours >= 24 * 4)
            {
                return dateTime.ToString("yyyy-M-d");
            }
            else
            {
                if (hours >= 24 * 3)
                {
                    return dateTime.ToString("3天前");
                }
                if (hours >= 24 * 2)
                {
                    return dateTime.ToString("2天前");
                }
                if (hours >= 24 * 1)
                {
                    return dateTime.ToString("1天前");
                }
                else
                {
                    if (hours > 0)
                    {
                        return $"{hours}小时前";
                    }
                    else
                    {
                        return "刚刚";
                    }

                }
            }

        }
        /// <summary>
        /// 星期转换成数字
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string WeekToNumber(DayOfWeek dayOfWeek)
        {
            string result = "";
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: result = "1"; break;
                case DayOfWeek.Tuesday: result = "2"; break;
                case DayOfWeek.Wednesday: result = "3"; break;
                case DayOfWeek.Thursday: result = "4"; break;
                case DayOfWeek.Friday: result = "5"; break;
                case DayOfWeek.Saturday: result = "6"; break;
                case DayOfWeek.Sunday: result = "0"; break;
            }
            return result;
        }
    }
}
