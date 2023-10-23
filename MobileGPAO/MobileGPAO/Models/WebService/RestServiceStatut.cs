using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Xml.Serialization;

namespace GPAOnGo.WEBSERVICE
{

    public enum EnumRestService
    {
        [XmlEnum(Name = "-2")]
        STATUT_APP_ERROR_FATAL = -2,
        [XmlEnum(Name = "-1")]
        STATUT_APP_ERROR_AND_BACK = -1,
        [XmlEnum(Name = "0")]
        STATUT_APP_ERROR = 0,
        [XmlEnum(Name = "1")]
        STATUT_APP_OK = 1,
        [XmlEnum(Name = "2")]
        STATUT_APP_QUESTION_CONTAINERDEJAAPP = 2,
        [XmlEnum(Name = "3")]
        STATUT_APP_QUESTION_USERCHANGEPASSWORD = 3
    }

    [XmlRoot(App.XML_STATUT)]
    public class RestServiceStatut
    {
        const string XML_STATUT_VAL = "statut_val";
        const string XML_STATUT_AVECDESC = "statut_avecdesc";
        const string XML_STATUT_DESC = "statut_desc";

        public RestServiceStatut(EnumRestService pEtat,bool pAvecDesccripton, string pDescription)
        {
            this.Etat = pEtat;
            this.AvecDesccripton = pAvecDesccripton;
            this.Description = pDescription;

        }

        public RestServiceStatut():this(0, false, "")
        {
        }

        EnumRestService Etat { get; set; }

        bool AvecDesccripton { get; set; }

        string Description { get; set; }

        // Accès aux variables
        [XmlElement(XML_STATUT_VAL)]
        public EnumRestService etat
        {
            get { return Etat; }
            set { Etat = value; }
        }

        [XmlElement(XML_STATUT_AVECDESC)]
        public bool avecDesccripton
        {
            get { return AvecDesccripton; }
            set { AvecDesccripton = value; }
        }

        [XmlElement(XML_STATUT_DESC)]
        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        public async Task<bool> TraitementStatut(INavigation pNavigation)
        {
            Page oCurrentPage = pNavigation.NavigationStack.LastOrDefault();
            switch (this.Etat)
            {
                case EnumRestService.STATUT_APP_ERROR_FATAL:
                    //on ferme l'application
                    if (AvecDesccripton)
                    {
                        await oCurrentPage.DisplayAlert("ERROR_FATAL", Description + Environment.NewLine + "Merci de contacter le service informatique CAWE", "Ok");
                    }
                    //System.Diagnostics.Process.GetCurrentProcess().Kill();
                    break;

                case EnumRestService.STATUT_APP_ERROR_AND_BACK:
                    if (avecDesccripton)
                    {
                        await oCurrentPage.DisplayAlert(null, Description, "OK");
                    }
                    if(IsModal(oCurrentPage))
                    {

                        await oCurrentPage.Navigation.PopModalAsync();
                    }
                    else
                    {
                        await oCurrentPage.Navigation.PopAsync();
                    }
                    break;

                case EnumRestService.STATUT_APP_ERROR:
                    if (AvecDesccripton)
                    {

                        App._toastManager.LongMessage(Description);
                    }
                    break;

                case EnumRestService.STATUT_APP_OK:
                    if (AvecDesccripton)
                    {
                        App._toastManager.LongMessage(Description);
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        private bool IsModal(Page page)
        {
            bool retVal = false;

            for (int i = 0; i < page.Navigation.ModalStack.Count; i++)
            {
                if (page == page.Navigation.ModalStack[i])
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }
    }
}
