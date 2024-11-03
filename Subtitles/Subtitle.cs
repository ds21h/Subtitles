using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Subtitles
{
    internal class Subtitle{
        private List<SubtitleEntry> mSubtitles;
        Regex mRegexTime;
        string mTitle;

        internal List<SubtitleEntry> xSubtitleEntries {
            get {
                return mSubtitles;
            }
        }

        internal string xTitle {
            get {
                return mTitle;
            }
        }

        internal Subtitle() {
            mRegexTime = new Regex(@"^\d{2}:\d{2}:\d{2},\d{3}$");
            mSubtitles = new List<SubtitleEntry>();
            mTitle = string.Empty;
        }

        internal void xLoadSubtitleFile(string pFile) {
            FileInfo lFile;
            StreamReader lStream;
            string lLine;
            SubtitleEntry lSubtitle;
            List<string> lLines;

            lLines = new List<string>();    
            lFile = new FileInfo(pFile);
            if (lFile.Exists) {
                mTitle = lFile.Name;
                lStream = new StreamReader(pFile);
                do {
                    lLine = lStream.ReadLine();
                    if (string.IsNullOrEmpty(lLine)) {
                        lSubtitle = sCreateSubTitle(lLines);
                        if (lSubtitle != null) {
                            mSubtitles.Add(lSubtitle);
                        }
                        if (lLine == null) {
                            break;
                        } else {
                            lLines.Clear();
                        }
                    } else {
                        lLines.Add(lLine);
                    }
                } while (true);
                lStream.Close();
            }
        }

        internal void xWriteSubtitleFile(string pFile) {
            using (StreamWriter lWriter = new StreamWriter(File.Open(pFile, FileMode.Create), Encoding.Latin1)) {
                foreach (SubtitleEntry bEntry in mSubtitles) {
                    lWriter.Write(bEntry.xSeqStr);
                    lWriter.Write('\n');
                    lWriter.Write(bEntry.xStart);
                    lWriter.Write(" --> ");
                    lWriter.Write(bEntry.xEnd);
                    lWriter.Write('\n');
                    lWriter.Write(bEntry.xTitleStr);
                    lWriter.Write('\n');
                    lWriter.Write('\n');
                }
            }
        }

        private SubtitleEntry sCreateSubTitle(List<string> pLines) {
            SubtitleEntry lSubtitle;
            int lSeq;
            string lStart;
            string lEnd;
            int lCount;

            lSubtitle = null;
            if (pLines.Count >= 2) {
                if (int.TryParse(pLines[0], out lSeq)) {
                    if (pLines[1].Length >= 29) {
                        lStart = pLines[1].Substring(0, 12);
                        if (mRegexTime.IsMatch(lStart)) {
                            if (pLines[1].Substring(12, 5) == " --> ") {
                                lEnd = pLines[1].Substring(17, 12);
                                if (mRegexTime.IsMatch(lEnd)) {
                                    lSubtitle = new SubtitleEntry(lSeq, lStart, lEnd);
                                    for (lCount = 0; lCount < pLines.Count; lCount++) {
                                        lSubtitle.xAddLine(pLines[lCount]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return lSubtitle;
        }
    }
}
