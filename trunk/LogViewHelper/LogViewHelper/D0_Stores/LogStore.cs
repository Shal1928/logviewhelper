using System.Collections.Generic;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.BytesExt;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.StringExt;
using UseAbilities.IoC.Stores;
using UseAbilities.IoC.Stores.Impl;

namespace LogViewHelper.D0_Stores
{
    public class LogStore : IFileReadStore<IEnumerable<LogItem>>
    {
        private readonly BytesStore _bytesStore = new BytesStore();

         #region Singleton implementation

        private LogStore()
        {
            //
        }

        private static readonly LogStore SingleInstance = new LogStore();
        public static LogStore Instance { get { return SingleInstance; } }

        #endregion


        public string FileName { get; set; }

        public IEnumerable<LogItem> Load()
        {
            return Parse(_bytesStore.Load(FileName));
        }

        public IEnumerable<LogItem> Load(string key)
        {
            return Parse(_bytesStore.Load(key));
        }

        private static IEnumerable<LogItem> Parse(byte[] bytes)
        {
            var result = new List<LogItem>();
            if (bytes.IsNullOrEmpty()) return result;

            var source = bytes.GetStringUTF8();

            var thatsAllFolks = false;
            while (!thatsAllFolks)
            {
                var logPart = source.GetBetweenTabs();
                if (logPart.IsEmpty())
                {
                    logPart = source;
                    thatsAllFolks = true;
                }
                else source = source.Remove(0, logPart.Length);
                if(!logPart.IsNullOrEmptyOrSpaces()) result.Add(new LogItem(logPart));
            }

            return result;
        }
    }
}
