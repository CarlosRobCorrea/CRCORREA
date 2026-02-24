using System;
using System.Collections.Generic;
using System.Text;

namespace CRCorreaBarCod.Symbologies
{
    class Blank: BarcodeCommon, IBarcode
    {
        
        #region IBarcode Members

        public string Encoded_Value
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
