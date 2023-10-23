using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Xml.Serialization;
using GPAOnGo.WEBSERVICE;

namespace GPAOnGo.NGO
{
    [XmlRoot("NGOPage")]
    public class NGOPage
    {
        const string CodePage = "CodePage";
        const string NomPage = "NomPage";
        const string TypePage = "TypePage";
        const string RowidOrigine = "Rowid";
        const string NomTableRowidOrigine = "NomTableRowid";
        const string CodeChampsOrigine = "CodeChampsOrigine";

        const string ListeNGOObject = "ListeNGOObject";
        const string NGOObject = "NGOObject";

        const string NGOStatut = "NGOStatut";

        public NGOPage()
        {
            this.A_num = string.Empty;
            this.A_nom = string.Empty;
            this.A_type = string.Empty;
            this.A_rowid = string.Empty;
            this.A_nomTableRowid = string.Empty;
            this.A_vues = new ListeNGOObject();
            this.A_statut = new NGOStatutOF();
            this.A_PK = string.Empty;
            this.A_codePageRecherche = string.Empty;
            this.A_codeChampsOrigine = string.Empty;
            this.oRestServiceStatut = new RestServiceStatut();
        }

        public NGOPage(RestServiceStatut pRestServiceStatut) : this()
        {
            this.oRestServiceStatut = pRestServiceStatut;
        }

        string A_num { get; set; }
        string A_nom { get; set; }
        string A_type { get; set; }
        string A_rowid { get; set; }
        string A_nomTableRowid { get; set; }
        ListeNGOObject A_vues { get; set; }
        string A_PK { get; set; }
        string A_codePageRecherche { get; set; }
        string A_codeChampsOrigine { get; set; }
        NGOStatutOF A_statut { get; set; }
        RestServiceStatut oRestServiceStatut { get; set; }

        [XmlElement(CodePage)]
        public string Num
        {
            get { return A_num; }
            set { A_num = value; }
        }

        [XmlElement(NomPage)]
        public string Nom
        {
            get { return A_nom; }
            set { A_nom = value; }
        }

        [XmlElement(TypePage)]
        public string Type
        {
            get { return A_type; }
            set { A_type = value; }
        }

        [XmlElement(RowidOrigine)]
        public string Rowid
        {
            get { return A_rowid; }
            set { A_rowid = value; }
        }

        [XmlElement(NomTableRowidOrigine)]
        public string NomTableRowid
        {
            get { return A_nomTableRowid; }
            set { A_nomTableRowid = value; }
        }

        [XmlArray(ListeNGOObject)]
        [XmlArrayItem(NGOObject, Type = typeof(NGOObject))]
        public ListeNGOObject Vues_liste
        {
            get { return A_vues; }
            set { A_vues = value;}
        }

        public string PK
        {
            get { return A_PK; }
            set { A_PK = value;}
        }

        [XmlElement(NGOStatut)]
        public NGOStatutOF StatutNGO
        {
            get { return A_statut; }
            set { A_statut = value; }
        }

        public string CodePageRecherche
        {
            get { return A_codePageRecherche; }
            set { A_codePageRecherche = value; }
        }

        [XmlElement(CodeChampsOrigine)]
        public string CodeOrigine
        {
            get { return A_codeChampsOrigine; }
            set { A_codeChampsOrigine = value; }
        }

        public NGOPage GetObjectERPForGroupe(string codePage, NGOChamps champs)
        {
            try
            {
                return App._restService.GetNGOForGroupe(codePage, champs);
            }
            catch (Exception e)
            {
                return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public NGOPage GetObjectERPFromPK(Utilisateur pUtilisateur,string pNomTable)
        {
            try
            {
                return App._restService.GetObjectERPFromPK(this,pUtilisateur, pNomTable);
            }
            catch (Exception e)
            {
                return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }

        public ListeNGOObject Recherche(string vStringRecherche)
        {
            ListeNGOObject l = new ListeNGOObject();
            foreach(NGOObject n in this.Vues_liste)
            {
                l.Add(n.Recherche(vStringRecherche));
            }
            return l;
        }

        public async Task<bool> TraitementStatut(INavigation pNavigation)
        {
            try
            {
                Page oCurrentPage = pNavigation.NavigationStack.LastOrDefault();
                if (IsModal(oCurrentPage))
                {
                    await oCurrentPage.Navigation.PopModalAsync();
                }
                else
                {
                    await oCurrentPage.Navigation.PopAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
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

        public NGOPage RefreshPage()
        {
            try
            {
                NGOChamps c = new NGOChamps();
                c.Rowid = Rowid;
                c.NomTable = NomTableRowid;
                c.Code = CodeOrigine;
                return App._restService.GetNGOForGroupe(Num, c);
            }
            catch (Exception e)
            {
                return new NGOPage(new RestServiceStatut(EnumRestService.STATUT_APP_ERROR_FATAL, true, "Merci de contacter le service informatique CAWE \nImpossible de se connecter à l'API\n" + e.Message));
            }
        }
    }
}
