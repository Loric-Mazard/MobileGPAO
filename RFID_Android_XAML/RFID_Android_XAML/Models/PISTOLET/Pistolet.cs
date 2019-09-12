using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using RFID_Android_XAML.UTILITAIRE;
using RFID_Android_XAML.GPAO;
using RFID_Android_XAML.RFID;

namespace RFID_Android_XAML.PISTOLET
{
    public class Pistolet
    {
        private static IKoamtacManager KoamtacManager = DependencyService.Get<IKoamtacManager>();
        private static IBluetoothManager BluetoothManager = DependencyService.Get<IBluetoothManager>();
        private static IKoamtacBluetoothDevice koamtacBluetoothDevice = DependencyService.Get<IKoamtacBluetoothDevice>();

        public Pistolet()
        {
            this.name = "";
            this.address = "";
            this.isConnected = false;
            this.isInitialized = false;
            this.gunBlueTooth = false;
            this.onlyCamera = false;
            this.isModeBarCode = false;
            this.isModeRFID = false;
            this.tagliste = new TagListe();
        }

        public Pistolet(string name, string address, bool ponlyCamera):this()
        {
            this.name = name;
            this.address = address;
            this.onlyCamera = ponlyCamera;
            this.isInitialized = true;
        }
        public Pistolet(string name, string address, bool pgunBlueTooth, bool ponlyCamera,object pBluetoothDevice) : this()
        {
            this.name = name;
            this.address = address;
            this.gunBlueTooth = pgunBlueTooth;
            this.onlyCamera = ponlyCamera;
            this.isInitialized = true;
            koamtacBluetoothDevice.SetBluetoothDevice(ref pBluetoothDevice);
        }

        string name { get; set; }
        string address { get; set; }
        bool isConnected { get; set; }
        bool isInitialized { get; set; }
        bool gunBlueTooth { get; set;}
        bool onlyCamera { get; set; }
        bool isModeRFID { get; set; }
        bool isModeBarCode { get; set; }
        TagListe tagliste { get; set; }
        
        // Accès aux variables
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        public bool GunBlueTooth
        {
            get { return gunBlueTooth; }
        }
        private bool OnlyCamera
        {
            get { return onlyCamera; }
            set { onlyCamera = value; }
        }
        private bool IsModeRFID
        {
            get { return isModeRFID; }
        }
        private bool IsModeBarCode
        {
            get { return isModeBarCode; }
        }
        public IKoamtacBluetoothDevice KoamtacBluetoothDevice
        {
            get { return koamtacBluetoothDevice; }
            set { koamtacBluetoothDevice = value; }
        }
        public TagListe Tagliste
        {
            get { return tagliste; }
            set { tagliste = value; }
        }

        // Fonctions
        public bool ModeIsSelected()
        {
            return (this.isInitialized && (this.gunBlueTooth || this.onlyCamera)); 
        }

        public void AffecterUtilisateur(Utilisateur putilisateur)
        {
            KoamtacManager.AffecterUtilisateur(putilisateur);
        }

        public bool Connect(ref string pErreur)
        {
            bool vConnect = false;

            try
            {
                vConnect = KoamtacManager.Connection(this);
                this.isConnected = true;
                this.gunBlueTooth = true;
            }
            catch (System.Exception e)
            {
                pErreur="Problème Connexion: \n" + e;
                vConnect = false;
            }
            return vConnect;
        }

        public bool Disconnect(ref string pErreur)
        {
            bool vDisconnect = false;

            try
            {
                vDisconnect= KoamtacManager.Deconnection();
                this.isConnected = false;
                this.gunBlueTooth = false;
            }
            catch (System.Exception e)
            {
                pErreur = "Problème DèConnexion: \n" + e;
                vDisconnect= false;
            }
            return vDisconnect;
        }

        public void ModeRFID()
        {
            if (ModeIsSelected())
            {
                if (!IsModeRFID)
                {
                    KoamtacManager.ModeRFID(8);
                    this.isModeRFID = true;
                    this.isModeBarCode = false;
                }
                
            }
        }
        public void ModeRFID(Utilisateur putilisateur, bool pWithStart)
        {
            if (ModeIsSelected())
            {
                if (!IsModeRFID)
                {
                    KoamtacManager.ModeRFID(8);
                    this.isModeRFID = true;
                    this.isModeBarCode = false;
                }

                if (pWithStart)
                {
                    StartScanRFIDAuto();

                }
            }
        }

        private void StartScanRFIDAuto()
        {
            Task.Run(async () => KoamtacManager.StartScanRFIDAuto());
        }

        public void ModeBarCode()
        {
            if (ModeIsSelected())
            {
                if (!IsModeBarCode)
                {
                    if (gunBlueTooth)
                    {
                        KoamtacManager.ModeBarCode();
                    }
                    this.isModeRFID = false;
                    this.isModeBarCode = true;
                }
            }
        }

        public void ModeBarCode(bool pWithStart)
        {
            if (ModeIsSelected())
            {
                if (!IsModeBarCode)
                {
                    if (gunBlueTooth)
                    {
                        KoamtacManager.ModeBarCode();
                    }
                    this.isModeRFID = false;
                    this.isModeBarCode = true;
                }

                if (pWithStart)
                {
                    KoamtacManager.StartScanCodeBare();
                }
            }
        }

        private void StartScanCodeBare()
        {
            KoamtacManager.StartScanCodeBare();
        }
  
        public List<Pistolet> Bluetooth_getAllPairedKoamtacDevices()
        {
            return BluetoothManager.getAllPairedKoamtacDevices();
        }
    }
}
