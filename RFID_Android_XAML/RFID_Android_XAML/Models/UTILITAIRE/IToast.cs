using System;
using System.Collections.Generic;
using System.Text;

namespace RFID_Android_XAML.UTILITAIRE
{
    public interface IToast
    {
        void LongMessage(string msg);
        void ShortMessage(string msg);
    }
}
