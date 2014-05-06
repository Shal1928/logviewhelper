using System;
using System.Text.RegularExpressions;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.StringExt;

namespace LogViewHelper.C0_Helpers
{
    public static class LogHeaderParserExt
    {
        public static DateTime GetLogDateTime(this string value)
        {
            var dateTime = Regex.Match(value, @"\d{1,2}[.]\d{1,2}[.]\d{2,4}[ ]\d{1,2}[:]\d{1,2}[:]\d{1,2}[.,]\d{0,3}").ToString();
            return dateTime.IsNullOrEmptyOrSpaces() ? new DateTime() : DateTime.Parse(dateTime.Replace(",", "."));
        }

        public static LogItemType GetLogLevel(this string value)
        {
            var level = Regex.Match(value, @"[ ][A-Z]{3,5}[ ]").ToString().Trim().FirstCapital();
            return (LogItemType)Enum.Parse(typeof(LogItemType), level);
        }

        public static string GetLogId(this string value, bool isFull = false)
        {
            var regExp = isFull ? @"[ ]\[.*\S.*\]" : @"\d{1,10}\]";
            var id = Regex.Match(value, regExp).ToString().Trim().ClearEdges("[","]");
            return id;
        }

        public static string GetLogMessage(this string value, string anchor = "\n")
        {
            return value.GetAfterNewLine();
        }
    }
}
