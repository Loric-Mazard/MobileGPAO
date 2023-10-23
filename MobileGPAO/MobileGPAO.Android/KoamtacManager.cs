using System;
using System.Collections.Generic;
using Android.Bluetooth;
using Android.Widget;
using GPAOnGo.RFID;
using GPAOnGo.NGO;
using GPAOnGo.PISTOLET;
using Koamtac.Kdc.Sdk;
using Xamarin.Forms;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(GPAOnGo.Droid.KoamtacManager))]

namespace GPAOnGo.Droid
{

    public class KoamtacManager : Java.Lang.Object,
        IKoamtacManager,
        IKDCDataReceivedListener,
        IKDCBarcodeDataReceivedListener,
        IKDCGPSDataReceivedListener,
        IKDCMSRDataReceivedListener,
        IKDCNFCDataReceivedListener,
        IKDCConnectionListener

    {
        private const int CONST_DUREE_MAX_SEC_SCAN_LIBRE = 10;

        private KDCReader kdcReader;
        private Pistolet pistolet;

        private Scan scanParametre = new Scan();

        public KoamtacManager()
        {
        }

        public void Parametre(ref Utilisateur pUtilisateur)
        {

            scanParametre.Scan_libre = pUtilisateur.ScanParametre.Scan_libre;
            scanParametre.Duree = pUtilisateur.ScanParametre.Duree;
            scanParametre.Nb_occur = pUtilisateur.ScanParametre.Nb_occur;
            scanParametre.Level = pUtilisateur.ScanParametre.Level;

            if (!scanParametre.Scan_libre)
            {
                kdcReader.SetUHFReadingTimeout(1);
            }
            else
            {
                kdcReader.SetUHFReadingTimeout(CONST_DUREE_MAX_SEC_SCAN_LIBRE * 1000);
            }
        }

        public bool Connection(Pistolet ppistolet)
        {
            pistolet = ppistolet;

            kdcReader = new KDCReader((BluetoothDevice)pistolet.KoamtacBluetoothDevice.GetBluetoothDevice(), this, this, this, this, this, this, false);
            ConfigureKDCReader();
            ConfigureSyncOptions();

            if (!kdcReader.IsConnected)
            {
                kdcReader.Disconnect();
            }
            kdcReader.ConnectEx();
            
            if (kdcReader.IsConnected)
            {
                kdcReader.EnableBeepSound(false);
                kdcReader.EraseLastData();
                kdcReader.SetUHFReadTagMode(KDCConstants.UHFReadTagMode.Multiple);
                kdcReader.EnableUHFBurstMode();
                App._toastManager.ShortMessage("Connexion réussie : " + kdcReader.BatteryLevel + "%");
                return true;
            }
            else
                App._toastManager.ShortMessage("Echec de la connexion");
            return false;
        }

        private void ConfigureKDCReader()
        {
            kdcReader.SetContext(Android.App.Application.Context);
        }

        private void ConfigureSyncOptions()
        {
            kdcReader.EnableAttachType(true);
            kdcReader.EnableAttachSerialNumber(true);
            kdcReader.EnableAttachTimestamp(true);
            kdcReader.EnableAttachLocation(true);
            kdcReader.SetDataDelimiter(KDCConstants.DataDelimiter.Semicolon);
            kdcReader.SetRecordDelimiter(KDCConstants.RecordDelimiter.CRnLF);
            kdcReader.EnableBeepSound(true);
        }

        public bool Deconnection()
        {
            kdcReader.Disconnect();

            Toast.MakeText(Android.App.Application.Context, "Déconnexion réussie", ToastLength.Short).Show();

            return true;
        }

        public void ModeRFID()
        {
            if (kdcReader.IsConnected)
            {
                kdcReader.SetUHFReadMode(KDCConstants.UHFReadMode.NfcRfid);
                kdcReader.EnableUHFDuplicateCheck(true);

                KDCConstants.UHFPowerLevel lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level1;
                switch (scanParametre.Level)
                {
                    case 1:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level1;
                        break;
                    case 2:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level2;
                        break;
                    case 3:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level3;
                        break;
                    case 4:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level4;
                        break;
                    case 5:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level5;
                        break;
                    case 6:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level6;
                        break;
                    case 7:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level7;
                        break;
                    case 8:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level8;
                        break;
                    case 9:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level9;
                        break;
                    case 10:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level10;
                        break;
                    case 11:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level11;
                        break;
                    case 12:
                        lUHFPowerLevel = KDCConstants.UHFPowerLevel.Level12;
                        break;
                }
                kdcReader.SetUHFPowerLevel(lUHFPowerLevel);
            }
        }

        public void ModeBarCode()
        {
            if (kdcReader.IsConnected)
            {
                kdcReader.SetUHFReadMode(KDCConstants.UHFReadMode.Barcode);
                kdcReader.EnableBeepOnScan(true);
                kdcReader.EnableBeepSound(true);
            }
        }

        public void StartScanCodeBare()
        {
            if (kdcReader.IsConnected)
            {
                kdcReader.SoftwareTrigger();
            }
        }

        public void StartScanRFIDAuto()
        {
            if (kdcReader.IsConnected)
            {
                pistolet.Tagliste.Clear();
                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));

                IList<byte[]> taglistHexa;

                TagListe tagliste;
                List<TagListe> occurtagliste = new List<TagListe>();

                UHFStatus status = new UHFStatus();
                status.UHFDataFormat = KDCConstants.UHFDataFormat.Binary;

                int vNb_occur = scanParametre.Nb_occur;

                for (int vCompteurOccur = 0; vCompteurOccur < vNb_occur; vCompteurOccur++)
                {

                    taglistHexa = null;

                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_Start, App.Subscribe_ScanRFIDChange, Convert.ToUInt16(scanParametre.Duree * 1000)));
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Début du scan " + (vCompteurOccur + 1).ToString() + "/" + vNb_occur.ToString(), App.Subscribe_ScanRFID_Statut));
                    ShowScanQuantity("?");

                    taglistHexa = kdcReader.GetUHFTagList(scanParametre.Duree * 1000, status);

                    tagliste = null;
                    tagliste = new TagListe();
                    foreach (byte[] tabByte in taglistHexa)
                    {
                        tagliste.Add(new Tag(BitConverter.ToString(tabByte).Replace("-", ""), null, null), App._utilisateur.TagListeNegatif);
                    }

                    occurtagliste.Add(tagliste);
                }

                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_Stop, App.Subscribe_ScanRFIDChange, Convert.ToUInt16(0)));
                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Fin du Scan", App.Subscribe_ScanRFID_Statut));

                tagliste = null;
                tagliste = occurtagliste.ElementAt(0);

                if (scanParametre.Nb_occur > 1)
                {
                    TagListe comparetagliste;
                    for (int vCheckCompteur = 1; vCheckCompteur < occurtagliste.Count(); vCheckCompteur++)
                    {
                        comparetagliste = occurtagliste.ElementAt(vCheckCompteur);
                        string vErreur = string.Empty;
                        if (!CompareTagList(ref tagliste, ref comparetagliste, ref vErreur))
                        {
                            Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Scan Error" + vErreur, App.Subscribe_ScanRFID_Statut));
                            pistolet.Tagliste.Clear();
                            Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));
                        }
                    }
                }

                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Scan Success", App.Subscribe_ScanRFID_Statut));
                ShowScanQuantity(tagliste.Count.ToString());
                
                pistolet.Tagliste = null;
                pistolet.Tagliste = new TagListe(tagliste);

                App._utilisateur.TagListeNegatif = pistolet.Tagliste;

                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));
            }
            else
            {
                App._toastManager.LongMessage("GUN RFID NOT CONNECTED");
                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Aucun Scan", App.Subscribe_ScanRFID_Statut));
                pistolet.Tagliste.Clear();
                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));
            }
        }

        private void ShowScanQuantity(string quantity)
        {
            Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(quantity, App.Subscribe_ScanRFID_NbTag));
        }

        private bool CompareTagList(ref TagListe preftaglist, ref TagListe pcomparetaglist, ref string pErreur)
        {
            bool vExist = false;

            if (preftaglist.Count != pcomparetaglist.Count)
            {
                pErreur = "NB SCAN DIFFERENT";
                return false;
            }
            foreach (Tag reftag in preftaglist)
            {
                vExist = false;
                foreach (Tag comparetag in pcomparetaglist)
                {
                    if (reftag.ID == comparetag.ID)
                    {
                        vExist = true;
                        break;
                    }
                }
                if (!vExist)
                {
                    pErreur = "VAL SCAN DIFFERENT";
                    return false;
                }
            }
            return true;
        }


        public void BarcodeDataReceived(KDCData p0)
        {
            if (p0.Data != null)
            {
                string pType = string.Empty;
                string pData = string.Empty;
                try
                {
                    pType = p0.Data.Substring(0, p0.Data.IndexOf("-"));
                    pData = p0.Data.Substring(p0.Data.IndexOf("-") + 1);
                }
                catch (System.Exception)
                {
                    pType = App.Type_CAB_Unknow;
                    pData = "";
                }
                switch (pType)
                {
                    case App.Type_CAB_Utilisateur:
                    case App.Type_CAB_Container:
                    case App.Type_CAB_OF:
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(pType, App.Subscribe_ScanCodeBarre_Tag, pData));
                        break;
                    default:
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Type_CAB_Unknow, App.Subscribe_ScanCodeBarre_Tag, ""));
                        break;
                }

            }
        }

        public void DataReceived(KDCData p0)
        {
            string status = p0.Data.ToString();

            if (status == "UHF_START")
            {
                if (scanParametre.Scan_libre)
                {
                    pistolet.Tagliste.Clear();
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Début du Scan", App.Subscribe_ScanRFID_Statut));
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_Start, App.Subscribe_ScanRFIDChange, Convert.ToUInt16(CONST_DUREE_MAX_SEC_SCAN_LIBRE * 1000)));
                }
            }
            else if (status == "UHF_STOP")
            {
                if (scanParametre.Scan_libre)
                {
                    pistolet.Tagliste = new TagListe(pistolet.Tagliste);
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_Stop, App.Subscribe_ScanRFIDChange, Convert.ToUInt16(0)));
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send("Fin du Scan", App.Subscribe_ScanRFID_Statut));
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_NewTagListe, App.Subscribe_ScanRFIDChange, pistolet.Tagliste));
                    
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(App.Subscribe_ScanRFIDChange_SwitchToAuto, App.Subscribe_ScanRFIDChange));
                }
            }
        }

        public void GPSDataReceived(KDCData p0)
        {
            throw new NotImplementedException();
        }

        public void MSRDataReceived(KDCData p0)
        {
            throw new NotImplementedException();
        }

        public void NFCDataReceived(KDCData kdcData)
        {
            if (scanParametre.Scan_libre)
            {
                if (kdcData.NFCTagType == KDCConstants.NFCTag.Rfid)
                {
                    if (kdcData.Data != null)
                    {
                        pistolet.Tagliste.Add(new Tag(kdcData.NFCData, null, null), App._utilisateur.TagListeNegatif);
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(pistolet.Tagliste.Count.ToString(), App.Subscribe_ScanRFID_NbTag));
                    }
                }

                if (kdcData.DataType == KDCConstants.DataType.UhfList)
                {
                    IList<string> tagListString = kdcData.UHFList;
                    foreach (String tagString in tagListString)
                    {
                        pistolet.Tagliste.Add(new Tag(tagString, null, null), App._utilisateur.TagListeNegatif);
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(pistolet.Tagliste.Count.ToString(), App.Subscribe_ScanRFID_NbTag));
                    }
                }
            }
        }

        public void ConnectionChanged(BluetoothDevice device, int state)
        {
            //TO DO
        }

        /*public void Dispose()
        {
            throw new NotImplementedException();
        }*/
    }
}