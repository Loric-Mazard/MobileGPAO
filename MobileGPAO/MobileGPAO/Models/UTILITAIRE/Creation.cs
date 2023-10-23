
using Xamarin.Forms;
using GPAOnGo.NGO;

namespace GPAOnGo.UTILITAIRE
{
    public class Creation : ContentView
    {
        private string nomObject = "";
        private string typeObject = "";
        private string ordreObject = "";

        private string action = "";
        private string fonction = "1";
        private string nomPageTarget = "";
        private string ordre = "1";
        private string titre = "";
        private string colonne = "";

        private string nomPage = "";

        private string nomUnivers = "";
        private string rowidUnivers = "";

        private string nomTable = "";
        private string tableRowid = "";

        private string rowidChamps = "";
        private string rowidPage = "";
        private string rowidObject = "";

        public void setCodeObject(string c)
        {
            this.nomObject = c;
        }
        public void setAction(string n)
        {
            this.action = n;
        }

        public void setFonction(string n)
        {
            this.fonction = n;
        }

        public void setNomPageTarget(string n)
        {
            this.nomPageTarget = n;
        }
        public void setOrdre(string n)
        {
            this.ordre = n;
        }
        public void setOrdreObject(string n)
        {
            this.ordreObject = n;
        }
        public void setNomPage(string n)
        {
            this.nomPage = n;
        }
        public void setNomObject(string n)
        {
            this.nomObject = n;
        }
        public void setTableRowid(string n)
        {
            this.tableRowid = n;
        }
        public void setNomUnivers(string n)
        {
            this.nomUnivers = n;
        }
        public void setNomTable(string n)
        {
            this.nomTable = n;
        }
        public void setRowidUnivers(string s)
        {
            this.rowidUnivers = s;
        }
        public void setChampsRowid(string s)
        {
            this.rowidChamps = s;
        }
        public void setPageRowid(string n)
        {
            this.rowidPage = n;
        }
        public void setObjectRowid(string s)
        {
            this.rowidObject = s;
        }
        public void setColonne(string s)
        {
            this.colonne = s;
        }
        public void setTitre(string s)
        {
            this.titre = s;
        }

        public void setTypeObject(string s)
        {
            this.typeObject = s;
        }

        public NGOCheck ajouterChamps ()
        {
            NGOCheck check = new NGOCheck();
            check = SetNewChamps();
            return check;
        }

        public NGOCheck AjouterObject()
        {
            NGOCheck check = new NGOCheck();
            if (nomObject  == "")
            {
                check.Resultat = "0";
                check.Erreur = "le nom de l'object est vide";
            }
            else
            {
                check =  SetNewObject();
            }
            return check;
        }

        public NGOCheck AjouterPage()
        {
            NGOCheck check = new NGOCheck();
            if (nomPage == "")
            {
                check.Resultat = "0";
                check.Erreur = "le nom de la page est vide";
            }
            else
            {
                check = SetNewPage();
            }
            return check;
        }

        public NGOCheck SetNewTable()
        {
            return  App._restService.SetNewTable(tableRowid, nomTable);
        }
        public NGOCheck SetNewUnivers()
        {
            NGOCheck check = new NGOCheck();
            check =  App._restService.SetNewUnivers(nomUnivers);
            if (check.Resultat == "1")
            {
                check = App._restService.SetNewTablePrinc(nomTable, nomUnivers);
            }
            return check;
        }

        public NGOCheck SetNewChamps()
        {
            return App._restService.SetNewChamps(tableRowid, colonne, rowidObject, fonction, nomPageTarget, action, ordre, titre);
        }

        public NGOCheck SetNewObject()
        {
            NGOCheck check = new NGOCheck();
            check =  App._restService.SetNewObject(nomObject, rowidPage, typeObject);
            if (check.Resultat == "1")
            {
                this.rowidObject = check.Rowid;
            }
            return check;
        }

        public NGOCheck SetNewPage()
        {
            NGOCheck check = new NGOCheck();
            check = App._restService.SetNewPage(tableRowid, nomPage);
            if (check.Resultat == "1")
            {
                this.rowidPage = check.Rowid;
            }
            return check;
        }

        public NGOCheck ModifTitre(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if(titre == null)
            {
                check.Resultat = "0";
                check.Erreur = "Titre vide";
            }
            check = App._restService.ModifTitre(rowid,titre);
            return check;
        }

        public NGOCheck ModifFonction(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (titre == null)
            {
                check.Resultat = "0";
                check.Erreur = "Action vide";
            }
            check = App._restService.ModifFonction(rowid, fonction);
            return check;
        }

        public NGOCheck ModifOrdre(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (titre == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifOrdre(rowid, ordre);
            return check;
        }

        public NGOCheck ModifNomObject(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (nomObject == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifNomObject(rowid, nomObject);
            return check;
        }

        public NGOCheck ModifNomPage(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (nomPage == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifNomPage(rowid, nomPage);
            return check;
        }

        public NGOCheck ModifOrdreObject(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (ordreObject == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifOrdreObject(rowid, ordreObject);
            return check;
        }

        public NGOCheck ModifTypeObject(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (typeObject == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifTypeObject(rowid, typeObject);
            return check;
        }

        public NGOCheck  ModifTargetChamps(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (nomPageTarget == null)
            {
                check.Resultat = "0";
                check.Erreur = "Ordre vide";
            }
            check = App._restService.ModifTargetChamps(rowid, nomPageTarget);
            return check;
        }

        public NGOCheck ModifIdChamps(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (colonne == null)
            {
                check.Resultat = "0";
                check.Erreur = "Colonne vide";
            }
            check = App._restService.ModifIdChamps(rowid, colonne);
            return check;
        }

        public NGOCheck ModifActionChamps(string rowid)
        {
            NGOCheck check = new NGOCheck();
            if (action == null)
            {
                check.Resultat = "0";
                check.Erreur = "Action vide";
            }
            check = App._restService.ModifActionChamps(rowid, action);
            return check;
        }

        public NGOCheck RemoveUnivers()
        {
            return  App._restService.RemoveUnivers(rowidUnivers);
        }

        public NGOCheck RemoveTable()
        {
            return  App._restService.RemoveTable(tableRowid);
        }

        public NGOCheck RemoveChamps()
        {
            return App._restService.RemoveChamps(rowidChamps);
        }

        public NGOCheck RemovePage()
        {
            return  App._restService.RemovePage(rowidPage);
        }
        public NGOCheck RemoveObject()
        {
            return  App._restService.RemoveObject(rowidObject);
        }
    }
}
