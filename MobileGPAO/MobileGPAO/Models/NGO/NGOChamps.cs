using System.Xml.Serialization;
using Xamarin.Forms;
using System.Threading.Tasks;
using GPAOnGo.IHM.ERP;
using GPAOnGo.RFID;

namespace GPAOnGo.NGO
{
    public class NGOChamps
    {
        const string ListeNGOContenu = "ListeNGOContenu";
        const string NGOContenu = "NGOContenu";

        const string CodeChamps = "CodeChamps";
        const string PageTarget = "PageTarget";
        const string TypeTarget = "TypeTarget";
        const string CodePageRecherche = "PageRecherche";
        const string ChampsRowid = "Rowid";
        const string NomTableRowid = "NomTableRowid";

        const string FROMDescription = "FROMDescription";

        public NGOChamps()
        {
            this.A_contenu = new ListeNGOContenu();
            this.A_code = string.Empty;
            this.A_target = string.Empty;
            this.A_type_target = string.Empty;
            this.A_codePageRecherche = string.Empty;
            this.A_rowid = string.Empty;
            this.A_nomTableRowid = string.Empty;
            this.A_FROMDescription = string.Empty;
        }

        ListeNGOContenu A_contenu { get; set; }
        string A_code { get; set; }
        string A_target { get; set; }
        string A_type_target { get; set; }
        string A_codePageRecherche { get; set; }
        string A_rowid { get; set; }
        string A_nomTableRowid { get; set; }
        string A_FROMDescription { get; set; }

        [XmlArray(ListeNGOContenu)]
        [XmlArrayItem(NGOContenu, Type = typeof(NGOContenu))]
        public ListeNGOContenu Contenu_liste
        {
            get { return A_contenu; }
            set { A_contenu = value; }
        }

        [XmlElement(CodeChamps)]
        public string Code
        {
            get { return A_code; }
            set { A_code = value; }
        }

        [XmlElement(PageTarget)]
        public string Target
        {
            get { return A_target; }
            set { A_target = value; }
        }

        [XmlElement(TypeTarget)]
        public string TypeTargetPage
        {
            get { return A_type_target; }
            set { A_type_target = value; }
        }

        [XmlElement(CodePageRecherche)]
        public string RecherchePage
        {
            get { return A_codePageRecherche; }
            set { A_codePageRecherche = value; }
        }

        [XmlElement(ChampsRowid)]
        public string Rowid
        {
            get { return A_rowid; }
            set { A_rowid = value; }
        }

        [XmlElement(NomTableRowid)]
        public string NomTable
        {
            get { return A_nomTableRowid; }
            set { A_nomTableRowid = value; }
        }

        [XmlElement(FROMDescription)]
        public string FROMDesc
        {
            get { return A_FROMDescription; }
            set { A_FROMDescription = value; }
        }

        public int TORFID_Mode
        {
            get { return 0; }
        }

        public async Task Action(INavigation pNavigation)
        {

            NGOPage p = new NGOPage();
            IHMERPList oIHMERPList = new IHMERPList();
            NGOCheck check = new NGOCheck();

            IHMNGOAjoutObject oIHMNGOAjoutObject = new IHMNGOAjoutObject();
            IHMNGOAjoutChamps oIHMNGOAjoutChamps = new IHMNGOAjoutChamps();
            IHMERPListRecherche oIHMERPListRecherche = new IHMERPListRecherche();
            IHMNGOAjoutUnivers oIHMNGOAjoutUnivers = new IHMNGOAjoutUnivers();
            IHMERPStatutListe pageIHMERPStatutListe = new IHMERPStatutListe();
            IHMNGOAjoutPage oIHMNGOAjoutPage = new IHMNGOAjoutPage();

            IHMNGOModifTitreChamps oIHMERPModifTitreChamps = new IHMNGOModifTitreChamps();
            IHMNGOModifFonctionChamps oIHMERPModifFonctionChamps = new IHMNGOModifFonctionChamps();
            IHMNGOModifOrdreChamps oIHMERPModifOrdreChamps = new IHMNGOModifOrdreChamps();
            IHMNGOModifTitreObject oIHMERPModifTitreObject = new IHMNGOModifTitreObject();
            IHMNGOModifTitrePage oIHMERPModifTitrePage = new IHMNGOModifTitrePage();
            IHMNGOModifOrdreObject oIHMERPModifOrdreObject = new IHMNGOModifOrdreObject();
            IHMNGOModifTypeObject oIHMERPModifTypeObject = new IHMNGOModifTypeObject();
            IHMNGOModifTargetChamps oIHMERPModifTargetChamps = new IHMNGOModifTargetChamps();
            IHMNGOModifActionChamps oIHMERPModifActionChamps = new IHMNGOModifActionChamps();
            IHMNGOModifIdChamps oIHMERPModifIdChamps = new IHMNGOModifIdChamps();

            if (A_codePageRecherche != "")
            {
                p.Num = A_target;
                p.Vues_liste.Add(new NGOObject());
                p.Vues_liste.Collection[0].ListeChamps.Add(new NGOChamps());
                p.Vues_liste.Collection[0].ListeChamps.Collection[0].NomTable = this.NomTable;
                p.Vues_liste.Collection[0].ListeChamps.Collection[0].Code = this.Code;
                p.CodePageRecherche = A_codePageRecherche;
                App.pageObjectERPScan.BindingContext = p;
                await pNavigation.PushAsync(App.pageObjectERPScan);
            }
            else if (A_target != "")
            {
                p = p.GetObjectERPForGroupe(A_target, this);
                switch (A_type_target)
                {
                    case "1":
                        oIHMERPList.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPList);
                        break;
                    case "2":
                        p = p.GetObjectERPForGroupe(A_target, this);
                        ListeNGOChamps l = new ListeNGOChamps();
                        ListeNGOChamps var = new ListeNGOChamps();
                        foreach (NGOChamps n in p.Vues_liste.Collection[0].ListeChamps.Collection)
                        {
                            if (n.Contenu_liste.Collection[0].Valeur.Length == 1)
                            {
                                var.Clear();
                                var.Add(n);
                                l.Collection.Add(n);
                            }
                            else
                            {
                                var[(n.Contenu_liste.Collection[0].Valeur.Length - 3) / 2].Contenu_liste.Collection[0].ListeChamps.Collection.Add(n);
                                if (var.Count < n.Contenu_liste.Collection[0].Valeur.Length - 1)
                                {
                                    var.Add(n);
                                }
                                else
                                {
                                    var[(n.Contenu_liste.Collection[0].Valeur.Length - 1) / 2] = n;
                                }
                            }
                            n.Contenu_liste.Collection.Remove(n.Contenu_liste.Collection[0]);
                        }
                        p.Vues_liste.Collection[0].ListeChamps = l;
                        oIHMERPList.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPList);
                        break;
                    case "3":
                        App.crea.setColonne(this.Contenu_liste.Collection[0].Valeur);

                        check = App.crea.ajouterChamps();
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "4":
                        App.crea.setObjectRowid(A_rowid);
                        oIHMERPList.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPList);
                        break;

                    case "5":
                        check = App.crea.AjouterObject();
                        if (check.Resultat == "1")
                        {
                            this.A_rowid = check.Rowid;
                            p =  p.GetObjectERPForGroupe(A_target, this);

                            oIHMERPList.BindingContext = p;
                            await pNavigation.PushAsync(oIHMERPList);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "6":
                        check = App.crea.AjouterPage();
                        if (check.Resultat == "1")
                        {
                            this.A_rowid = check.Rowid;
                            p =  p.GetObjectERPForGroupe(A_target, this);

                            oIHMNGOAjoutObject.BindingContext = p;
                            await pNavigation.PushAsync(oIHMNGOAjoutObject);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "7":

                        oIHMERPListRecherche.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPListRecherche);
                        break;
                    case "8":
                        App.crea.setTableRowid(A_rowid);
                        App.crea.setNomTable(this.Contenu_liste.Collection[0].Valeur);
                        check = App.crea.SetNewTable();
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "9":
                        App.crea.setTableRowid(A_rowid);
                        App.crea.setNomTable(this.Contenu_liste.Collection[0].Valeur);
                        oIHMNGOAjoutUnivers.BindingContext = p;
                        await pNavigation.PushAsync(oIHMNGOAjoutUnivers);
                        break;
                    case "10":
                        check = App.crea.SetNewUnivers();
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "11":
                        App.crea.setRowidUnivers(A_rowid);
                        bool answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous supprimer cet univers ?", "Oui", "Non");
                        if (answer)
                        {
                            check = App.crea.RemoveUnivers();
                            if (check.Resultat == "1")
                            {
                                await p.TraitementStatut(pNavigation);
                                await pNavigation.PopModalAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                            }
                        }
                        break;
                    case "12":
                        App.crea.setTableRowid(A_rowid);
                        answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous supprimer cette table ?", "Oui", "Non");
                        if (answer)
                        {
                            check = App.crea.RemoveTable();
                            if (check.Resultat == "1")
                            {
                                await p.TraitementStatut(pNavigation);
                                await pNavigation.PopModalAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                            }
                        }
                        break;
                    case "13":
                        App.crea.setChampsRowid(A_rowid);
                        answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous supprimer ce champs ?", "Oui", "Non");
                        if (answer)
                        {
                            check = App.crea.RemoveChamps();
                            if (check.Resultat == "1")
                            {
                                await p.TraitementStatut(pNavigation);
                                await pNavigation.PopModalAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                            }
                        }
                        break;
                    case "14":
                        if (!App._utilisateur.TagListeNegatif.IsConfigured)
                        {
                            answer = await App.Current.MainPage.DisplayAlert("Attention", "Vous n'avez pas configuré la Liste de Tag Negatif. Voulez-vous la configurer ?", "Oui", "Non");
                            if (answer)
                            {
                                MessagingCenter.Subscribe<string, TagListe>(this, App.Subscribe_pageScanRFIDToList_NewTagListe, (pStatut, NewtagListe) =>
                                {
                                    App._utilisateur.TagListeNegatif = null;
                                    App._utilisateur.TagListeNegatif = NewtagListe;
                                });
                                App._utilisateur.TagListeNegatif.Clear();
                                App.pageScanRFIDToList.BindingContext = this;
                                await pNavigation.PushAsync(App.pageScanRFIDToList);
                                break;
                            }
                        }
                        App.pageCartonScan.BindingContext = this;
                        await pNavigation.PushAsync(App.pageCartonScan);
                        break;
                    case "15":
                        p.StatutNGO.ROWID = Rowid;
                        p.StatutNGO = p.StatutNGO.GetObjectNGOStatut();
                        bool traitementStatutGroupe = await p.StatutNGO.RestServiceStatut.TraitementStatut(pNavigation);
                        if (p.StatutNGO.AutorisationOK)
                        {
                            pageIHMERPStatutListe.BindingContext = p;
                            await pNavigation.PushAsync(pageIHMERPStatutListe);
                        }
                        break;
                    case "16":
                        oIHMERPModifTitreChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifTitreChamps);
                        break;
                    case "17":
                        check = App.crea.ModifTitre(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "18":
                        oIHMERPModifFonctionChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifFonctionChamps);
                        break;
                    case "19":
                        check = App.crea.ModifFonction(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "20":
                        oIHMERPModifOrdreChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifOrdreChamps);
                        break;
                    case "21":
                        check = App.crea.ModifOrdre(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "22":
                        App.crea.setTableRowid(A_rowid);
                        oIHMNGOAjoutPage.BindingContext = p;
                        await pNavigation.PushAsync(oIHMNGOAjoutPage);
                        break;
                    case "23":
                        App.crea.setPageRowid(A_rowid);
                        oIHMNGOAjoutObject.BindingContext = p;
                        await pNavigation.PushAsync(oIHMNGOAjoutObject);
                        break;
                    case "24":
                        App.crea.setPageRowid(A_rowid);
                        answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous supprimer cette page ?", "Oui", "Non");
                        if (answer)
                        {
                            check = App.crea.RemovePage();
                            if (check.Resultat == "1")
                            {
                                await p.TraitementStatut(pNavigation);
                                await pNavigation.PopModalAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                            }
                        }
                        break;
                    case "25":
                        App.crea.setObjectRowid(A_rowid);
                        answer = await App.Current.MainPage.DisplayAlert("Attention", "Voulez vous supprimer cet object ?", "Oui", "Non");
                        if (answer)
                        {
                            check = App.crea.RemoveObject();
                            if (check.Resultat == "1")
                            {
                                await p.TraitementStatut(pNavigation);
                                await pNavigation.PopModalAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                            }
                        }
                        break;

                    case "26":
                        App.crea.setTableRowid(A_rowid);
                        oIHMNGOAjoutChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMNGOAjoutChamps);
                        break;
                    case "27":
                        oIHMERPModifTitrePage.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifTitrePage);
                        break;
                    case "28":
                        check = App.crea.ModifNomPage(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "29":
                        oIHMERPModifTitreObject.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifTitreObject);
                        break;
                    case "30":
                        check = App.crea.ModifNomObject(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "31":
                        oIHMERPModifOrdreObject.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifOrdreObject);
                        break;
                    case "32":
                        check = App.crea.ModifOrdreObject(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "33":
                        oIHMERPModifTypeObject.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifTypeObject);
                        break;
                    case "34":
                        check = App.crea.ModifTypeObject(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "35":
                        oIHMERPModifTargetChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifTargetChamps);
                        break;
                    case "36":
                        check = App.crea.ModifTargetChamps(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "37":
                        oIHMERPModifActionChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifActionChamps);
                        break;
                    case "38":
                        check = App.crea.ModifActionChamps(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                    case "39":
                        oIHMERPModifIdChamps.BindingContext = p;
                        await pNavigation.PushAsync(oIHMERPModifIdChamps);
                        break;
                    case "40":
                        check = App.crea.ModifIdChamps(Rowid);
                        if (check.Resultat == "1")
                        {
                            await p.TraitementStatut(pNavigation);
                            await pNavigation.PopModalAsync();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Attention", check.Erreur, "Ok");
                        }
                        break;
                }
            }
        }

        public bool Contains(string pValue)
        {
            foreach (NGOContenu oERPObjectItem in this.Contenu_liste)
            {
                if (oERPObjectItem.Valeur.ToUpper().Contains(pValue.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
