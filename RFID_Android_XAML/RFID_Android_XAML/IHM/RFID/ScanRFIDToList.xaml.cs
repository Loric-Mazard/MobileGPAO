using RFID_Android_XAML.RFID;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RFID_Android_XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanRFIDToList : ContentPage
	{

        private ModeAppairage modeAppairage;
        private TagListe tagliste = null;

        private static Label lNbPieceScannees = null;

        public ScanRFIDToList()
		{
			InitializeComponent ();

            lNbPieceScannees = lblNbTag;
        }

        private void Progress(Int32 pTo, uint pDuree)
        {
            pgbScan.Progress = 0;
            pgbScan.ProgressTo(pTo, pDuree, Easing.Linear);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            modeAppairage = (ModeAppairage)this.BindingContext;

            //your code here;
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
                this.tagliste = null;
                this.tagliste = ptagListe;
            });

            App._pistolet.ModeRFID(App._utilisateur, false);
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

        private void onStartClicked(object sender, EventArgs e)
        {
            App._pistolet.ModeRFID(App._utilisateur, true);
        }

        private void onFinClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(tagliste, App.Subscribe_ScanRFID_TagListe);
            Navigation.PopAsync();
        }
    }
}