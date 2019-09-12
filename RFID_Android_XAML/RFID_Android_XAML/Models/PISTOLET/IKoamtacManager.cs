using RFID_Android_XAML.RFID;
using RFID_Android_XAML.GPAO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RFID_Android_XAML.PISTOLET
{
    public interface IKoamtacManager
    {
        void AffecterUtilisateur(Utilisateur putilisateur);
        bool Connection(Pistolet device);

        bool Deconnection();

        void ModeRFID(int plevel);

        void ModeBarCode();

        void StartScanCodeBare();

        void StartScanRFIDAuto();
    }
}
