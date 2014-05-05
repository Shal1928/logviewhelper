using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

            var processedSource = Split(source, @"\[\b\d{2}[.]?\d{2}[.]?\d{4}[ ]?\d{2}[:]?\d{2}[:]?\d{2}[,]?\d{1,3}\b\][ ]{0,3}\w{3,5}[ ]{0,3}\[\w{1,100}[ ]{0,3}\w{1,100}[ ]{0,3}\w{1,100}[ ]{0,3}\w{1,100}\]");

            var thatsAllFolks = false;
            while (!thatsAllFolks)
            {
                //@"\[\b\d{2}[.]?\d{2}[.]?\d{4}[ ]?\d{2}[:]?\d{2}[:]?\d{2}[,]?\d{3}\b\]"
                //\[\b\d{2}[.]?\d{2}[.]?\d{4}[ ]?\d{2}[:]?\d{2}[:]?\d{2}[,]?\d{1,3}\b\][ ]{0,3}\w{3,5}[ ]{0,3}\[\w{1,100}[ ]{0,3}\w{1,100}[ ]{0,3}\w{1,100}[ ]{0,3}\w{1,100}\]
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

        private static List<string> Split(string source, string regExp)
        {
            var result = new List<string>();
            var matches = Regex.Matches(source, regExp);

            var i = 0;
            foreach (var match in matches)
            {
                var nextIndex = i + 1;
                var beginIndex = source.IndexOf(match.ToString(), StringComparison.OrdinalIgnoreCase);
                var endIndex = nextIndex <= matches.LastIndex()
                             ? source.IndexOfPreview(matches[nextIndex].ToString(), StringComparison.OrdinalIgnoreCase)
                             : source.IndexOfLast();

                if (endIndex <= beginIndex && nextIndex <= matches.LastIndex())
                {
                    var tempSource = source.Replace(match.ToString(), StringBaseExt.GenerateSymbols(" ", match.ToString().Length));
                    endIndex = source.ReplaceFirst(match.ToString(), StringBaseExt.GenerateSymbols(" ", match.ToString().Length)).IndexOfPreview(matches[nextIndex].ToString(), StringComparison.OrdinalIgnoreCase);
                    //endIndex = source.Remove(beginIndex, match.ToString().Length).IndexOfPreview(matches[nextIndex].ToString(), StringComparison.OrdinalIgnoreCase);
                }
                    
                var part = source.Copy(beginIndex, endIndex);
                result.Add(part);
                source = source.Remove(beginIndex, part.Length);
                i++;
            }

            return result;
        } 
    }
}
