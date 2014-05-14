using System;
using System.Linq;
using System.Text.RegularExpressions;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.ObjectExt;
using UseAbilities.Extensions.StringExt;

namespace LogViewHelper.C0_Helpers
{
    public static class LogParserExt
    {
        public static readonly string GUID_REG_EXP = @"[{]\w{8}[-]\w{4}[-]\w{4}[-]\w{4}[-]\w{12}[}]";

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
            //var regExp = isFull ? @"[ ]\[.*\S.*\]" : @"\d{1,10}\]";
            var regExp = isFull ? @"[ ]\[.*\S.*\]" : @"[a-zA-Z](\d+)\]";
            var id = Regex.Match(value, regExp).Groups[1].ToString().Trim().ClearEdges("[","]");
            return id;
        }

        public static string GetLogMessage(this string value, string anchor = "\n")
        {
            return value.GetAfterNewLine();
        }

        //(На обработку поступил объект).*([{]\w{8}[-]\w{4}[-]\w{4}[-]\w{4}[-]\w{12}[}])
        public static string GetMainGUID(this string value, string anchor = "На обработку поступил объект")
        {
            var logHeader = Regex.Match(value, @"({0}).*({1})".Paste(anchor, GUID_REG_EXP)).ToString();
            return logHeader.IsNullOrEmpty() 
                 ? null 
                 : logHeader;
        }

        public static string GetProcessedObjectGUID(this string value)
        {
            return Regex.Match(value, GUID_REG_EXP).ToString();
        }
    }
}
