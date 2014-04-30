using System;
using LogViewHelper.A0_Models;

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
            return int.Parse(value.Copy(beginIndex, endIndex));
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

            return String.IsNullOrEmpty(value) ? 0 : int.Parse(value);
        }

        public static string GetLogMessage(this string value, string anchor = "\n")
        {
            return value.GetAfterNewLine();
        }






        public static string Copy(this string value, int beginIndex, int endIndex)
        {
            return value.Substring(beginIndex, endIndex - beginIndex + 1);
        }

        public static string RemoveLast(this string value)
        {
            return value.Remove(value.IndexOfLast());
        }

        public static string GetAfterNewLine(this string value)
        {
            var newLineIndex = value.IndexOfNext(NEW_LINE);
            return value.Copy(newLineIndex, value.IndexOfLast());
        }

        public static int IndexOfNext(this string value, string item, StringComparison comparison = StringComparison.Ordinal)
        {
            return value.IndexOf(item, comparison) + 1;
        }

        public static int IndexOfPreview(this string value, string item, StringComparison comparison = StringComparison.Ordinal)
        {
            return value.IndexOf(item, comparison) - 1;
        }

        public static int IndexOfLast(this string value)
        {
            return value.Length - 1;
        }

        public static readonly string NEW_LINE = "\n";
    }
}
