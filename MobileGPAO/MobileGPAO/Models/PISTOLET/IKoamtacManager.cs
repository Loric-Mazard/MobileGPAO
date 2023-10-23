using GPAOnGo.RFID;
using GPAOnGo.NGO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GPAOnGo.PISTOLET
{
    public interface IKoamtacManager
    {
        void Parametre(ref Utilisateur pUtilisateur);

        bool Connection(Pistolet device);

        bool Deconnection();

        void ModeRFID();

        void ModeBarCode();

        void StartScanCodeBare();

        void StartScanRFIDAuto();

    }
}
