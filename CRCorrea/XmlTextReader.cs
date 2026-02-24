using System;
using System.IO;

namespace CRCorrea
{
    internal class XmlTextReader
    {
        private TextReader textReader;

        public XmlTextReader(TextReader textReader)
        {
            this.textReader = textReader;
        }

        internal void Read()
        {
            throw new NotImplementedException();
        }
    }
}