using System;
using LogViewHelper.C0_Helpers;
using UseAbilities.Extensions.StringExt;
using UseAbilities.WPF.Attributes;

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
            Message = log.GetLogMessage().CleanEnd();
        }

        public DateTime DateTime { get; set; }
        public int MSeconds { get; set; }

        [Display("Дата", 0, 2)]
        public string DisplayDateTime { get { return String.Format("{0},{1}", DateTime.ToString("ddd dd.MM hh:mm:ss"), MSeconds); } }

        [Display("Тип", 1, 1)]
        public LogItemType Level { get; set; }
        //public string ThreadName { get; set; }

        [Display("Id", 2, 1)]
        public int Id { get; set; }

        [Display("Сообщение", 3, 7)]
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
