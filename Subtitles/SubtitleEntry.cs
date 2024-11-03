using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subtitles
{
    internal class SubtitleEntry {
        private int mSeq;
        private string mStart;
        private string mEnd;
        private List<string> mLines;

        public string xSeqStr {
            get {
                return mSeq.ToString();
            }
        }

        public string xStart {
            get {
                return mStart;
            }
        }

        public string xEnd {
            get {
                return mEnd;
            }
        }

        public string xTitleStr {
            get {
                StringBuilder lBuilder;
                int lIndex;
                bool lFirst;

                lBuilder = new StringBuilder();
                lFirst = true;
                for (lIndex = 2; lIndex < mLines.Count; lIndex++) {
                    if (lFirst) {
                        lFirst = false;
                    } else {
                        lBuilder.Append('\n');
                    }
                    lBuilder.Append(mLines[lIndex]);
                }
                return lBuilder.ToString();
            }
        }

        internal SubtitleEntry(int pSeq, string pStart, string pEnd) {
            mSeq = pSeq;
            mStart = pStart;
            mEnd = pEnd;
            mLines = new List<string>();
        }

        internal void xAddLine(string pLine) {
            mLines.Add(pLine);
        }
    }
}
