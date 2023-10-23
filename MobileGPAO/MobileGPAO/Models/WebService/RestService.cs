using System;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using GPAOnGo.NGO;
using GPAOnGo.RFID;
using GPAOnGo.UTILITAIRE;
using System.IO;

namespace GPAOnGo.WEBSERVICE
{
    public class RestService
    {
        string _base_test = "https://DORACLE02.btb.fr:8020/ords/mobile_gpao";
        string _base_prod = "https://PORACLE03.btb.fr:8020/ords/mobile_gpao";

        string _base_xml_webservice_api = "xml_webservice_api/";

        const string XML_RFID_ENTETE = "cawe_rfid_app";

        const string ORACLE_USER = "api_user";
        const string ORACLE_PASSWORD_TEST = "test";
        const string ORACLE_PASSWORD_PROD = "GpaoApiUser2023";

        HttpClient oHttpClient;

        public RestService()
        {
        }

        public string _base()
        {
            switch (Xamarin.Essentials.Preferences.Get("MODE_PRODUCTION", App.CONNEXION_BASE_PROD))
            {
                case App.CONNEXION_BASE_PROD:
                    oHttpClient = RestServiceHelper.GetClient(ORACLE_USER, ORACLE_PASSWORD_PROD);
                    return _base_prod;
                case App.CONNEXION_BASE_TEST:
                    oHttpClient = RestServiceHelper.GetClient(ORACLE_USER, ORACLE_PASSWORD_TEST);
                    return _base_test;
                default:
                    oHttpClient = RestServiceHelper.GetClient(ORACLE_USER, ORACLE_PASSWORD_PROD);
                    return _base_prod;
            }
        }

        public AppGPAOnGo GetGPAOnGo()
        {
            try
            {
                // Construction url
                string URL = ConstuireBase(_base_xml_webservice_api, "GetGPAOnGo");

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new AppGPAOnGo(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_PARA_GPAOnGo))
                    {
                        return new AppGPAOnGo(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElementParaGPAOnGo = vXElemententete.Element(App.XML_PARA_GPAOnGo);

                    XmlSerializer serializer = new XmlSerializer(typeof(AppGPAOnGo));
                    return (AppGPAOnGo)serializer.Deserialize(vXElementParaGPAOnGo.CreateReader());
                }

                return new AppGPAOnGo(new RestServiceStatut(0, true, "Accès à la base impossible"));

            }
            catch (Exception e)
            {
                return new AppGPAOnGo(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public Utilisateur GetUser(Utilisateur pUtilisateur)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "GetUser")
                     + ConstuireParametre("pUSER_LOGIN_WINDOWS", pUtilisateur.Identification)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());
                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new Utilisateur(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_PARA_USER))
                    {
                        return new Utilisateur(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementParaUser = vXElemententete.Element(App.XML_PARA_USER);

                    XmlSerializer serializerUtilisateur = new XmlSerializer(typeof(Utilisateur));
                    return (Utilisateur)serializerUtilisateur.Deserialize(xElementParaUser.CreateReader());

                }
                return new Utilisateur(new RestServiceStatut(0, true, "Accès à la base impossible"));
            }
            catch (Exception e)
            {
                return new Utilisateur(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }

        }

        public RestServiceStatut ChangeUserPassword(Utilisateur pUtilisateur, string pNewPassword)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ChangeUserPassword")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pOLDPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pNEWPASSWORD", pNewPassword);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_PARA_USER))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElementParaUser = vXElemententete.Element(App.XML_PARA_USER);

                    if (!checkOneNodeOnly(vXElementParaUser, App.XML_STATUT))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement xElementStatut = vXElementParaUser.Element(App.XML_STATUT);

                    XmlSerializer serializer = new XmlSerializer(typeof(RestServiceStatut));
                    return (RestServiceStatut)serializer.Deserialize(xElementStatut.CreateReader());
                }
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR, true, "Accès à la base impossible");
            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message);
            }
        }

        public Container GetContainer(Container pContainer, int pAppairage_Mode, string pAppairage_Desc, Utilisateur pUtilisateur)
        {
            try
            {
                // Contruction url
                string URL = ConstuireBase(_base_xml_webservice_api, "GetBox")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pUSER_MODE_VAL", pAppairage_Mode.ToString())
                     + ConstuireParametre("pUSER_MODE_DESC", pAppairage_Desc)
                     + ConstuireParametre("pBOX_ID", pContainer.Identification)
                     + ConstuireParametre("pDoRFIDAppInForce", Convert.ToInt16(pContainer.Appairage_Mode_DejaEffectue_Forcer).ToString());

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new Container(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_RFID_PARA_BOX))
                    {
                        return new Container(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementRFIDBoxPara = vXElemententete.Element(App.XML_RFID_PARA_BOX);

                    XmlSerializer serializerContainer = new XmlSerializer(typeof(Container));
                    return (Container)serializerContainer.Deserialize(xElementRFIDBoxPara.CreateReader());
                }
                return new Container("");
            }
            catch (Exception e)
            {
                return new Container(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public RestServiceStatut PostContainer(Container pcontainer, Utilisateur pUtilisateur)
        {
            try
            {
                string XMLtagListe = DebutXML(XML_RFID_ENTETE) + DebutXML(App.XML_RFID_NBTAG) + pcontainer.TagListe.Count + FinXML(App.XML_RFID_NBTAG) + DebutXML(App.XML_RFID_TAGS) + pcontainer.TagListe.toXML() + FinXML(App.XML_RFID_TAGS) + FinXML(XML_RFID_ENTETE);
                // Constructions url
                string URL = ConstuireBase(_base_xml_webservice_api, "DoRFID")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pUSER_MODE_VAL", pcontainer.Appairage_Mode.ToString())
                     + ConstuireParametre("pBOX_ID", pcontainer.Identification)
                     + ConstuireParametre("pDoRFIDAppEcart", Convert.ToInt16(pcontainer.Appairage_Mode_EcartOK_Forcer).ToString())
                     + ConstuireParametre("pTagListeXML", XMLtagListe);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_RFID_BOX_DORFID))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElementRFIDBoxDoRFID = vXElemententete.Element(App.XML_RFID_BOX_DORFID);

                    if (!checkOneNodeOnly(vXElementRFIDBoxDoRFID, App.XML_STATUT))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement xElementStatut = vXElementRFIDBoxDoRFID.Element(App.XML_STATUT);

                    XmlSerializer serializer = new XmlSerializer(typeof(RestServiceStatut));
                    return (RestServiceStatut)serializer.Deserialize(xElementStatut.CreateReader());
                }

                return new RestServiceStatut(0, true, "Accès à la base impossible");

            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message);
            }
        }

        public NGOPage GetObjectERPFromPK(NGOPage pNGOPage, Utilisateur pUtilisateur,string pNomTable)
        {
            try
            {
                // Contruction url
                string URL = ConstuireBase(_base_xml_webservice_api, "GetNGOFromPK")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pCODEPAGE", pNGOPage.Num)
                     + ConstuireParametre("pPK", pNGOPage.PK)
                     + ConstuireParametre("pNomTableRowid", pNomTable);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, "NGOPage"))
                    {
                        return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementObjectERPGroupe = vXElemententete.Element("NGOPage");

                    XmlSerializer serializerUtilisateur = new XmlSerializer(typeof(NGOPage));
                    return (NGOPage)serializerUtilisateur.Deserialize(xElementObjectERPGroupe.CreateReader());
                }
                return new NGOPage(new RestServiceStatut(0, true, "Accès à la base impossible"));
            }
            catch (Exception e)
            {
                return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        /*public ObjectERP GetNGOProfil(ObjectERP pObjectERP, Utilisateur pUtilisateur)
        {
            try
            {
                // Contruction url
                string URL = ConstuireBase(_base_xml_webservice_api, "GetNGOProfil")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pFROMORIGINE", pObjectERP.FROMOrigine)
                     + ConstuireParametre("pORIGINE", pObjectERP.Origine)
                     + ConstuireParametre("pROWID", pObjectERP.ROWID);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new ObjectERP(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_OBJECTERP_PARA))
                    {
                        return new ObjectERP(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementOFPara = vXElemententete.Element(App.XML_OBJECTERP_PARA);

                    XmlSerializer serializerOFPara = new XmlSerializer(typeof(ObjectERP));
                    return (ObjectERP)serializerOFPara.Deserialize(xElementOFPara.CreateReader());
                }
                return new ObjectERP(new RestServiceStatut(0, true, "Accès à la base impossible"));
            }
            catch (Exception e)
            {
                return new ObjectERP(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }*/

        public RestServiceStatut PostNGOStatutChoix(NGOStatutChoix pERPStatutChoix, Utilisateur pUtilisateur)
        {
            try
            {
                // Construction url
                string URL = ConstuireBase(_base_xml_webservice_api, "DoNGOStatutChoix")
                     + ConstuireParametre("pUSER_CODE", pUtilisateur.CodeGPAO)
                     + ConstuireParametre("pPASSWORD", pUtilisateur.Password)
                     + ConstuireParametre("pORIGINE", pERPStatutChoix.Origine)
                     + ConstuireParametre("pROWID", pERPStatutChoix.ROWID)
                     + ConstuireParametre("pPOSITION", pERPStatutChoix.Position.ToString())
                     + ConstuireParametre("pDATA", pERPStatutChoix.Valeur);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());

                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);


                    if (!checkOneNodeOnly(vXElemententete, App.XML_ERPStatutChoix))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement vXElementERPStatutChoix = vXElemententete.Element(App.XML_ERPStatutChoix);


                    if (!checkOneNodeOnly(vXElementERPStatutChoix, App.XML_STATUT))
                    {
                        return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML");
                    }
                    XElement xElementStatut = vXElementERPStatutChoix.Element(App.XML_STATUT);

                    XmlSerializer serializer = new XmlSerializer(typeof(RestServiceStatut));
                    return (RestServiceStatut)serializer.Deserialize(xElementStatut.CreateReader());
                }

                return new RestServiceStatut(0, true, "Accès à la base impossible");

            }
            catch (Exception e)
            {
                return new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message);
            }
        }

        public NGOStatutOF GetNGOStatut(NGOStatutOF pObjectERPStatut)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "GetNGOStatut")
                     + ConstuireParametre("pORIGINE", "OFPXXX")
                     + ConstuireParametre("pROWID", pObjectERPStatut.ROWID);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());
                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new NGOStatutOF(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, App.XML_OBJECTERPSTATUT_PARA))
                    {
                        return new NGOStatutOF(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementParaObjectERPStatut = vXElemententete.Element(App.XML_OBJECTERPSTATUT_PARA);

                    XmlSerializer serializerUtilisateur = new XmlSerializer(typeof(NGOStatutOF));
                    return (NGOStatutOF)serializerUtilisateur.Deserialize(xElementParaObjectERPStatut.CreateReader());

                }
                return new NGOStatutOF(new RestServiceStatut(0, true, "Accès à la base impossible"));
            }
            catch (Exception e)
            {
                return new NGOStatutOF(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOPage GetNGOForGroupe(string codePage,NGOChamps champs)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "GetNGOForGroupe") + ConstuireParametre("pCodeChampsOrigine", champs.Code) + ConstuireParametre("pCodePage", codePage)
                    + ConstuireParametre("pRowid", champs.Rowid) + ConstuireParametre("pNomTableRowid", champs.NomTable);

                var uri = new Uri(string.Format(URL, string.Empty));

                HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(stream);
                    XDocument xdocument = XDocument.Parse(reader.ReadToEnd());
                    if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                    {
                        return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                    if (!checkOneNodeOnly(vXElemententete, "NGOPage"))
                    {
                        return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                    }
                    XElement xElementObjectERPGroupe = vXElemententete.Element("NGOPage");

                    XmlSerializer serializerUtilisateur = new XmlSerializer(typeof(NGOPage));
                    return (NGOPage)serializerUtilisateur.Deserialize(xElementObjectERPGroupe.CreateReader());

                }
                return new NGOPage(new RestServiceStatut(0, true, "Accès à la base impossible"));
            }
            catch (Exception e)
            {
                return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck SetNewChamps(string rowidTable, string nameColonne, string rowidObject, string typeFonction, string nomPageTarget, string action, string ordre, string titre)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewChamps") + ConstuireParametre("pRowidTable", rowidTable)
                    + ConstuireParametre("pColonneName", nameColonne) + ConstuireParametre("pRowidObject", rowidObject)
                    + ConstuireParametre("pTypeFonction", typeFonction.ToString()) + ConstuireParametre("pNomPageTarget", nomPageTarget) + ConstuireParametre("pAction", action)
                    + ConstuireParametre("pOrdre", ordre.ToString()) + ConstuireParametre("pTitre", titre);

                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }
        
        public NGOCheck SetNewObject(string nomObject,string rowidPage,string typeObject)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewObject") + ConstuireParametre("pNomObject", nomObject)
                    + ConstuireParametre("pRowidPage", rowidPage) + ConstuireParametre("pTypeObject", typeObject);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck SetNewPage(string table, string nomPage)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewPage") + ConstuireParametre("pRowid", table) + ConstuireParametre("pNomPage", nomPage);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck SetNewTable(string rowid, string nomTable)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewTable") + ConstuireParametre("pRowid", rowid) + ConstuireParametre("pNomTable", nomTable);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck SetNewUnivers(string nomUnivers)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewUnivers") + ConstuireParametre("pNomUnivers", nomUnivers);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck SetNewTablePrinc(string nomTable, string nomUnivers)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SetNewTablePrinc") + ConstuireParametre("pNomTable", nomTable)
                    + ConstuireParametre("pNomUnivers", nomUnivers);

                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck RemoveUnivers(string rowidUnivers)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SupprimerUnivers") + ConstuireParametre("pRowidUnivers", rowidUnivers);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck RemoveTable(string rowidTable)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SupprimerTable") + ConstuireParametre("pRowidTable", rowidTable);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck RemoveChamps(string rowidChamps)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SupprimerChamps") + ConstuireParametre("pRowidChamps", rowidChamps);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck RemovePage(string rowidPage)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SupprimerPage") + ConstuireParametre("pRowidPage", rowidPage);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck RemoveObject(string rowidObject)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "SupprimerObject") + ConstuireParametre("pRowidObject", rowidObject);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifTitre(string rowidChamps, string titre)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifTitreChamps") + ConstuireParametre("pRowidChamps", rowidChamps) + ConstuireParametre("pTitre", titre);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifFonction(string rowidChamps, string fonction)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifFonctionChamps") + ConstuireParametre("pRowidChamps", rowidChamps) + ConstuireParametre("pAction", fonction);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifOrdre(string rowidOrdreChamps, string ordre)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifOrdreChamps") + ConstuireParametre("pRowidOrdreChamps", rowidOrdreChamps);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifNomPage(string rowidPage, string nomPage)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifNomPage") + ConstuireParametre("pRowidPage", rowidPage) + ConstuireParametre("pNomPage", nomPage);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifOrdreObject(string rowidObject, string ordre)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifOrdreObject") + ConstuireParametre("pRowidOrdreObject", rowidObject) + ConstuireParametre("pOrdre", ordre);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifNomObject(string rowidObject, string nomObject)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifNomObject") + ConstuireParametre("pRowidObject", rowidObject) + ConstuireParametre("pNomObject", nomObject);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifTypeObject(string rowidObject, string typeObject)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifTypeObject") + ConstuireParametre("pRowidOrdreObject", rowidObject) + ConstuireParametre("pTypeObject", typeObject);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifTargetChamps(string rowidChamps, string target)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifTargetChamps") + ConstuireParametre("pRowidOrdreChamps", rowidChamps) + ConstuireParametre("pTarget", target);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifActionChamps(string rowidChamps, string action)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifActionChamps") + ConstuireParametre("pRowidOrdreChamps", rowidChamps) + ConstuireParametre("pAction", action);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOCheck ModifIdChamps(string rowidChamps, string colonne)
        {
            try
            {
                string URL = ConstuireBase(_base_xml_webservice_api, "ModifIdChamps") + ConstuireParametre("pRowidChamps", rowidChamps) + ConstuireParametre("pColonne", colonne);
                return Checking(URL);
            }
            catch (Exception e)
            {
                return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        private NGOCheck Checking(string URL)
        {
            var uri = new Uri(string.Format(URL, string.Empty));

            HttpResponseMessage response = oHttpClient.GetAsync(uri).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var stream = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(stream);
                XDocument xdocument = XDocument.Parse(reader.ReadToEnd());
                if (!checkOneNodeOnly(xdocument, App.XML_androidGPAO_ENTETE))
                {
                    return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                }
                XElement vXElemententete = xdocument.Element(App.XML_androidGPAO_ENTETE);

                if (!checkOneNodeOnly(vXElemententete, "NGOCheck"))
                {
                    return new NGOCheck(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Erreur Structure XML"));
                }
                XElement xElementObjectERPGroupe = vXElemententete.Element("NGOCheck");

                XmlSerializer serializerUtilisateur = new XmlSerializer(typeof(NGOCheck));
                return (NGOCheck)serializerUtilisateur.Deserialize(xElementObjectERPGroupe.CreateReader());

            }
            return new NGOCheck(new RestServiceStatut(0, true, "Accès à la base impossible"));
        }

        private string ConstuireBase(string pExtention, string pAPI)
        {
            return _base().Trim() + "/" + pExtention.Trim() + pAPI.Trim() + "?p1=1";
        }
        private string ConstuireParametre(string pParametre, string pValue)
        {
            return "&" + pParametre.Trim() + "=" + CaractereInterditURL(pValue.Trim());
        }
        private string CaractereInterditURL(string pString)
        {
            return pString.Replace("+", "%2B");
        }
        private string DebutXML(string pString)
        {
            return "<" + pString.Trim() + ">";
        }
        private string FinXML(string pString)
        {
            return "</" + pString.Trim() + ">";
        }

        private bool checkOneNodeOnly(XDocument pXDocument, String pNode)
        {
            return (pXDocument.Elements(pNode).Count() == 1);
        }

        private bool checkOneNodeOnly(XElement pXElement, String pNode)
        {
            return (pXElement.Elements(pNode).Count() == 1);
        }
    }
}
