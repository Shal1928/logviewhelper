using System;
using LogViewHelper.C0_Helpers;

namespace LogViewHelper.A0_Models
{
    //[30.04.2014 11:34:06,971] INFO  [QueueItemExecutor_OUTBOX_Worker background task processing4709]
    public class LogItem
    {
        public LogItem()
        {
            //    
        }

        public LogItem(string log)
        {
            DateTime = log.GetLogDateTime();
            MSeconds = log.GetLogMSeconds();
            Level = log.GetLogLevel();
            Id = log.GetLogId();
            Message = log.GetLogMessage();
        }

        public DateTime DateTime { get; set; }
        public int MSeconds { get; set; }
        public LogLevel Level { get; set; }
        //public string ThreadName { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }


        #region Overriden Methods
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ MSeconds;
                hashCode = (hashCode * 397) ^ (int)Level;
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ (Message != null ? Message.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((LogItem) obj);
        }

        protected bool Equals(LogItem other)
        {
            return DateTime.Equals(other.DateTime) && MSeconds == other.MSeconds && Level == other.Level && Id == other.Id && string.Equals(Message, other.Message);
        }
        #endregion
    }
}
