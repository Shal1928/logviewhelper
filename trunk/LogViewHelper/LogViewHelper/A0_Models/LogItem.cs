using System;
using System.Collections.Generic;
using LogViewHelper.C0_Helpers;
using UseAbilities.Extensions.EnumerableExt;
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

        public LogItem(string log, IReadOnlyDictionary<string, string> idGuidDic)
        {
            DateTime = log.GetLogDateTime();
            Level = log.GetLogLevel();
            Id = log.GetLogId();
            Message = log.GetLogMessage().CleanEnd();
            if (idGuidDic.IsNullOrEmpty() || !idGuidDic.ContainsKey(Id)) return;
            GUID = idGuidDic[Id];
        }
        

        [Display("Время", 0, DisplayColumnType.SizeToCells, "ddd dd.MM hh:mm:ss,fff")]
        public DateTime DateTime { get; set; }
        
        [Display("Тип", 1, DisplayColumnType.SizeToCells)]
        public LogItemType Level { get; set; }
        //public string ThreadName { get; set; }

        [Display("Id", 2, DisplayColumnType.SizeToCells)]
        public string Id { get; set; }

        public string GUID { get; set; }

        [Display("Сообщение", 3, 1)]
        public string Message { get; set; }

        #region Overriden Methods
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Level;
                hashCode = (hashCode * 397) ^ (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GUID != null ? GUID.GetHashCode() : 0);
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
            return DateTime.Equals(other.DateTime) && Level == other.Level && string.Equals(Id, other.Id) && string.Equals(GUID, other.GUID) && string.Equals(Message, other.Message);
        }
        #endregion
    }
}
