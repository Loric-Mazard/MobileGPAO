using RFID_Android_XAML.RFID;
using RFID_Android_XAML.UTILITAIRE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RFID_Android_XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanRFIDContainer : ContentPage
	{
        private ModeAppairage modeAppairage;
        private Container container;

        public  ScanRFIDContainer()

        {
			InitializeComponent ();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            modeAppairage = (ModeAppairage)((List<Object>)this.BindingContext).ElementAt(0);
            container = (Container)((List<Object>)this.BindingContext).ElementAt(1);

            // Update affichage
            PageScanRFIDContainer.Title = modeAppairage.Description;
            lblNumCarton.Text = "Numéro de carton : " + container.Identificateur;
            lblNbAttendu.Text = "Nombre de pièces attendues : " + container.QttAttendue.ToString();


            MessagingCenter.Subscribe<String>(this, App.Subscribe_NbTag, (pNbPieceScannees) =>
            {
                lblNbTag.Text = "Nombre de pièces scannées : " + pNbPieceScannees;
            });

            MessagingCenter.Subscribe<String>(this, App.Subscribe_Statut, (pStatut) =>
            {
                lblStatut.Text = pStatut;
            });

            MessagingCenter.Subscribe<string>(this, App.Subscribe_ScanRFIDStart, (pDuree) =>
            {
                Task.Run(async () => Progress(1, Convert.ToUInt16(pDuree)));

            });

            MessagingCenter.Subscribe<string>(this, App.Subscribe_ScanRFIDEnd, (pNull) =>
            {
                Task.Run(async () => Progress(0, 0));
            });

            MessagingCenter.Subscribe<TagListe>(this, App.Subscribe_ScanRFID_TagListe, ptagListe =>
            {
                container.TagListe = null;
                container.TagListe = ptagListe;
            });

            App._pistolet.ModeRFID();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //your code here;
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_NbTag);
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_Statut);
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_ScanRFIDStart);
            MessagingCenter.Unsubscribe<String>(this, App.Subscribe_ScanRFIDEnd);
            MessagingCenter.Unsubscribe<String, String>(this, App.Subscribe_ScanCodeBarre_Tag);
            MessagingCenter.Unsubscribe<TagListe>(this, App.Subscribe_ScanRFID_TagListe);
        }

        private  void Progress(Int32 pTo, uint pDuree)
        {
            pgbScan.Progress = 0;
            pgbScan.ProgressTo(pTo, pDuree, Easing.Linear);
        }

        private void onScannerClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeRFID(App._utilisateur, true);
        }
    }
}