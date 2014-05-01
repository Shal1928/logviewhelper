using System;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.StringExt;

namespace LogViewHelper.C0_Helpers
{
    public static class LogHeaderParserExt
    {
        //         10        20        30        40        50        60        70        80        90
        //012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345
        //[30.04.2014 11:34:06,971] INFO  [QueueItemExecutor_OUTBOX_Worker background task processing4709]

        public static DateTime GetLogDateTime(this string value, int beginIndex = 1, int endIndex = 19)
        {
            return DateTime.Parse(value.Copy(beginIndex, endIndex));
        }

        public static int GetLogMSeconds(this string value, int beginIndex = 21, int endIndex = 23)
        {
            return value.IsNullOrEmptyOrSpaces() ? 0 : int.Parse(value.Copy(beginIndex, endIndex));
        }

        public static LogLevel GetLogLevel(this string value)
        {
            var indexOfBkt = value.IndexOfNext("]");
            value = value.Remove(0, indexOfBkt);
            indexOfBkt = value.IndexOfPreview("[");
            value = value.Remove(indexOfBkt, value.Length - indexOfBkt);

            return (LogLevel)Enum.Parse(typeof(LogLevel), value.Trim());
        }

        public static int GetLogId(this string value, string anchor = "processing", bool removeLast = true)
        {
            var indexOfAnchor = value.IndexOf(anchor, StringComparison.Ordinal);
            value = value.Remove(0, indexOfAnchor + anchor.Length);
            if (removeLast) value = value.RemoveLast();

            return value.IsNullOrEmptyOrSpaces() ? 0 : int.Parse(value);
        }

        public static string GetLogMessage(this string value, string anchor = "\n")
        {
            return value.GetAfterNewLine();
        }
    }
}
