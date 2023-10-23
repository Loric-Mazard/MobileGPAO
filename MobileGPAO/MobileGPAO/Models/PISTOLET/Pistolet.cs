using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using GPAOnGo.UTILITAIRE;
using GPAOnGo.NGO;
using GPAOnGo.RFID;
using System.ComponentModel;

using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace GPAOnGo.PISTOLET
{
    public class Pistolet
    {

        private static IKoamtacManager KoamtacManager = DependencyService.Get<IKoamtacManager>();
        private static IBluetoothManager BluetoothManager = DependencyService.Get<IBluetoothManager>();
        private static IKoamtacBluetoothDevice koamtacBluetoothDevice = DependencyService.Get<IKoamtacBluetoothDevice>();
        private static IQrCodeScanningService iQrCodeScanningService = DependencyService.Get<IQrCodeScanningService>();

        public Pistolet()
        {
            this.A_Name = string.Empty;
            this.A_Address = string.Empty;
            this.A_IsConnected = false;
            this.A_IsInitialized = false;
            this.A_IsKDCReader = false;
            this.A_OnlyCamera = false;
            this.A_IsModeBarCode = false;
            this.A_IsModeRFID = false;
            this.OTagliste = new TagListe();
        }

        public Pistolet(string name, string address, bool ponlyCamera) : this()
        {
            this.A_Name = name;
            this.A_Address = address;
            this.A_OnlyCamera = ponlyCamera;
            this.A_IsInitialized = true;
        }
        public Pistolet(string name, string address, bool pisKDCReader, bool ponlyCamera, object pBluetoothDevice) : this()
        {
            this.A_Name = name;
            this.A_Address = address;
            this.A_IsKDCReader = pisKDCReader;
            this.A_OnlyCamera = ponlyCamera;
            this.A_IsInitialized = true;
            koamtacBluetoothDevice.SetBluetoothDevice(ref pBluetoothDevice);
        }

        string A_Name { get; set; }
        string A_Address { get; set; }
        bool A_IsConnected { get; set; }
        bool A_IsInitialized { get; set; }
        bool A_IsKDCReader { get; set; }
        bool A_OnlyCamera { get; set; }
        bool A_IsModeRFID { get; set; }
        bool A_IsModeBarCode { get; set; }
        TagListe OTagliste { get; set; }

        // Accès aux variables
        public string Name
        {
            get { return A_Name; }
            set { A_Name = value; }
        }

        public string Address
        {
            get { return A_Address; }
            set { A_Address = value; }
        }

        public bool IsConnected
        {
            get { return A_IsConnected; }
        }
        public bool IsInitialized
        {
            get { return A_IsInitialized; }
        }

        public bool IsKDCReader
        {
            get { return A_IsKDCReader; }
        }
        private bool OnlyCamera
        {
            get { return A_OnlyCamera; }
            set { A_OnlyCamera = value; }
        }
        private bool IsModeRFID
        {
            get { return A_IsModeRFID; }
        }
        private bool IsModeBarCode
        {
            get { return A_IsModeBarCode; }
        }
        public IKoamtacBluetoothDevice KoamtacBluetoothDevice
        {
            get { return koamtacBluetoothDevice; }
            set { koamtacBluetoothDevice = value; }
        }
        public TagListe Tagliste
        {
            get { return OTagliste; }
            set { OTagliste = value; }
        }

        public bool RFID
        {
            get { return A_IsKDCReader; }
        }

        // Fonctions


        public bool ModeIsSelected
        {
            get { return (this.A_IsInitialized && (this.A_IsKDCReader || this.A_OnlyCamera)); }
        }

        public void Parametre(ref Utilisateur putilisateur)
        {
            if(A_IsKDCReader)
            {
                KoamtacManager.Parametre(ref putilisateur);
            }
            
        }

        public bool Connect(ref string pErreur)
        {
            try
            {
                if (A_IsKDCReader)
                {
                    A_IsConnected = KoamtacManager.Connection(this);
                }
                else
                {
                    A_IsConnected = true;
                }
            }
            catch (System.Exception e)
            {
                pErreur = "Problème Connexion: \n" + e;
            }
            return A_IsConnected;
        }

        public bool Disconnect(ref string pErreur)
        {
            try
            {
                if (A_IsKDCReader)
                {
                    A_IsConnected = !KoamtacManager.Deconnection();
                }
                else
                {
                    A_IsConnected = false;
                }
            }
            catch (System.Exception e)
            {
                pErreur = "Problème DèConnexion: \n" + e;
            }
            return A_IsConnected;
        }

        public void ModeRFID()
        {
            if (ModeIsSelected)
            {
                if (!IsModeRFID)
                {
                    if (IsKDCReader)
                    {
                        KoamtacManager.ModeRFID();

                        A_IsModeRFID = true;
                        A_IsModeBarCode = false;
                    }
                }

            }
        }

        public void ModeRFID(Utilisateur putilisateur, bool pWithStart)
        {
            ModeRFID();

            if (pWithStart && IsModeRFID)
            {
                StartScanRFIDAuto();
            }

        }

        public void StartScanRFIDAuto()
        {
            if (IsKDCReader)
            {
                Task.Run(() => KoamtacManager.StartScanRFIDAuto());
            }
        }

        public void ModeBarCode()
        {
            if (ModeIsSelected)
            {
                if (!IsModeBarCode)
                {
                    if (IsKDCReader)
                    {
                        KoamtacManager.ModeBarCode();
                    }
                    A_IsModeRFID = false;
                    A_IsModeBarCode = true;
                }
            }
        }

        public void ModeBarCode(bool pWithStart)
        {
            if (ModeIsSelected)
            {
                if (!IsModeBarCode)
                {
                    if (IsKDCReader)
                    {
                        KoamtacManager.ModeBarCode();
                    }
                    
                    A_IsModeRFID = false;
                    A_IsModeBarCode = true;
                }

                if (pWithStart)
                {
                    if (IsKDCReader)
                    {
                        KoamtacManager.StartScanCodeBare();
                    }
                     else
                    {
                        iQrCodeScanningService.ScanAsync();
                       
                    }
                }
            }
        }

        private void StartScanCodeBare()
        {
            KoamtacManager.StartScanCodeBare();
        }

        public List<Pistolet> Bluetooth_getAllPairedKoamtacDevices()
        {
            return BluetoothManager.GetAllPairedKoamtacDevices();
        }

        public Pistolet autoPlug(string pAddress)
        {
            string vErreur= string.Empty;
            
            foreach (Pistolet pistolet in Bluetooth_getAllPairedKoamtacDevices())
            {

                if (pistolet.Address == pAddress)
                {
                    return pistolet;
                }
            }
            return new Pistolet();
        }
    }
}
