using System;
using System.Collections.Generic;
using System.Text;

namespace GPAOnGo.UTILITAIRE
{
    public interface IToast
    {
        void LongMessage(string msg);
        void ShortMessage(string msg);
    }
}
