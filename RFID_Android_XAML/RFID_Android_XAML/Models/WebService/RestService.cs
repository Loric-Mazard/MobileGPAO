using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;
using RFID_Android_XAML.WEBSERVICE;
using RFID_Android_XAML.UTILITAIRE;
using RFID_Android_XAML.GPAO;
using RFID_Android_XAML.RFID;


namespace RFID_Android_XAML.WEBSERVICE
{
    public class RestService
    {
        HttpClient _client;
        string _base = "http://10.33.200.12:8010/xml_demo/xml_api.";
        IToast toast = App._toastManager;

        const string _check_user = "CheckUser?";
        const string _check_box = "CheckBox?";
        const string _do_rfid = "DoRFID?";
        const string _user_login = "pUSER_LOGIN_WINDOWS=";
        const string _mode_val = "pUSER_MODE_VAL=";
        const string _box_id = "pBOX_ID=";
        string _force_rfid = "pDoRFIDAppInForce=0";


        const string XML_MODE_APP = "mode_app";
        const string XML_MODE_VAL = "mode_val";
        const string XML_MODE_DESC = "mode_desc";
        const string XML_MODE_TYPE = "mode_type";

        const string XML_PARA_RFID = "rfid_para";
        const string XML_PARA_RFID_LIBRE = "rfid_para_libre";
        const string XML_PARA_RFID_OCCUR = "rfid_para_occur";
        const string XML_PARA_RFID_DUREE = "rfid_para_duree";
        

        public RestService()
        {
            _client = new HttpClient();

            // Login pour accéder au webservice
            var authData = string.Format("{0}:{1}", "test", "test");
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<Utilisateur> CreerUtilisateur(string pIdentification)
        {
            try
            {
                // Construction url
                string URL = _base + _check_user + _user_login + pIdentification;
                var uri = string.Format(URL, string.Empty);

                // Requête
                HttpResponseMessage response = await _client.GetAsync(uri);

                // Traitement de la réponse
                if (response.IsSuccessStatusCode)
                {
                    // Récupère le contenu de la réponse
                    string content = await response.Content.ReadAsStringAsync();
                    
                    
                    XDocument doc = XDocument.Parse(content);
                    List<ModeAppairage> listeModes =await CreerlisteModes(doc.Descendants(XML_MODE_APP));
                    Scan scanParametre = await CreerScanParametre(doc.Descendants(XML_PARA_RFID));

                    return new Utilisateur(pIdentification, await new RestServiceStatut(content).traitementStatut()==1, ref listeModes,ref scanParametre);
                }
                // Problème avec les logins pour accéder à l'API
                toast.LongMessage("Accès à la base impossible");

                return new Utilisateur();
            }
            catch (System.Net.WebException e)
            {
                var actionPage = App.Current.MainPage;
                if (actionPage.Navigation != null)
                    actionPage = actionPage.Navigation.NavigationStack.Last();

                await actionPage.DisplayAlert("ERROR_FATAL", "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API", "Ok");
                return new Utilisateur();
            }
        }

        private async Task<List<ModeAppairage>> CreerlisteModes(IEnumerable<XElement> pXML)
        {
            try
            {
                List<ModeAppairage> listeModes = new List<ModeAppairage>();
                foreach (var item in pXML)
                {
                    int val = int.Parse(item.Element(XML_MODE_VAL).Value);
                    string desc = item.Element(XML_MODE_DESC).Value;
                    int type = int.Parse(item.Element(XML_MODE_TYPE).Value);

                    listeModes.Add(new ModeAppairage(val, desc, type));
                }

                return listeModes;
            }
            catch (System.Net.WebException e)
            {
                var actionPage = App.Current.MainPage;
                if (actionPage.Navigation != null)
                    actionPage = actionPage.Navigation.NavigationStack.Last();

                await actionPage.DisplayAlert("ERROR_FATAL", "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API", "Ok");
                return new List<ModeAppairage>();
            }
        }

        private async Task<Scan> CreerScanParametre(IEnumerable<XElement> pXML)
        {
            try
            {
                bool scan_libre = false;
                int occur = 0;
                int duree = 0;

                foreach (var item in pXML)
                {
                    scan_libre = (int.Parse(item.Element(XML_PARA_RFID_LIBRE).Value)==1);
                    occur = int.Parse(item.Element(XML_PARA_RFID_OCCUR).Value);
                    duree = int.Parse(item.Element(XML_PARA_RFID_DUREE).Value);
                }


                return new Scan(scan_libre, duree, occur);
            }
            catch (System.Net.WebException e)
            {
                var actionPage = App.Current.MainPage;
                if (actionPage.Navigation != null)
                    actionPage = actionPage.Navigation.NavigationStack.Last();

                await actionPage.DisplayAlert("ERROR_FATAL", "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API", "Ok");
                return new Scan();
            }
        }

        public async Task<Container> CreerContainer( Utilisateur putilisateur,  ModeAppairage pmodeAppairage, string pContainerID)
        {
            try
            {
                // Contruction url
                string URL = _base + _check_box + _user_login + putilisateur.Identification + "&" + _mode_val + pmodeAppairage.Valeur + "&" + _box_id + pContainerID;
                var uri = string.Format(URL, string.Empty);

                // Requête
                var response = await _client.GetAsync(uri);

                // Traitement de la réponse
                if (response.IsSuccessStatusCode)
                {
                    // Récupère le contenu de la réponse
                    string content = await response.Content.ReadAsStringAsync();

                    // Gestion du statut

                    XDocument doc = XDocument.Parse(content);

                    int box_qty = 0;
                    foreach (var item in doc.Descendants("box_para"))
                    {
                        box_qty = int.Parse(item.Element("box_qty").Value);
                    }

                    if (box_qty == 0)
                    {
                        toast.LongMessage("Quantité à zéro");
                    }

                    return new Container(pContainerID, (await new RestServiceStatut(content).traitementStatut()==1), null, box_qty);
                }
                toast.LongMessage("Accès à la base impossible");

                return new Container();
            }
            catch (System.Net.WebException e)
            {
                var actionPage = App.Current.MainPage;
                if (actionPage.Navigation != null)
                    actionPage = actionPage.Navigation.NavigationStack.Last();

                await actionPage.DisplayAlert("ERROR_FATAL", "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API", "Ok");
                return new Container();
            }
        }

        public async Task<bool> PostContainer(ModeAppairage pmodeAppairage, Container pcontainer)
        {
            try
            {
                string allTags = null;

                // Construction data à Post
                foreach (Tag t in pcontainer.TagListe.Collection)
                {
                    allTags += "<tag><id>" + t.ID + "</id><param1>" + t.Param1 + "</param1><param2>" + t.Param2 + "</param2></tag>";
                }

                string content = "<cawe_rfid_app><nb_scan>" + pcontainer.TagListe.Collection.Count + "</nb_scan><data>" + allTags + "</data></cawe_rfid_app>";

                // Construction url
                string URL = _base + _do_rfid + _user_login + App._utilisateur.Identification + "&" + _mode_val + pmodeAppairage.Valeur + "&" + _box_id + pcontainer.Identificateur + "&" + _force_rfid + "&pXMLString=" + content;
                var uri = string.Format(URL, string.Empty);

                // Requête
                var response = await _client.GetAsync(uri);

                // Traitement de la réponse
                if (response.IsSuccessStatusCode)
                {
                    // Récupère le contenu de la réponse
                    string data = await response.Content.ReadAsStringAsync();

                    // Gestion du statut
                    RestServiceStatut restServiceStatut = new RestServiceStatut(data);

                    await restServiceStatut.traitementStatut();

                    if (restServiceStatut.Valeur == 1)
                        return true;
                }

                return false;
            }
            catch (System.Net.WebException e)
            {
                var actionPage = App.Current.MainPage;
                if (actionPage.Navigation != null)
                    actionPage = actionPage.Navigation.NavigationStack.Last();

                await actionPage.DisplayAlert("ERROR_FATAL", "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API", "Ok");
                return false;
            }

        }
    }
}
