using System.Collections.Generic;
using System.ComponentModel;

namespace GPAOnGo.RFID
{
    public class TagListe : List<Tag>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public TagListe()
        {
            this.isConfigured = false;
        }

        public TagListe(List<Tag> ptagListe) : base(ptagListe)
        {
            this.isConfigured = true;
        }

        public TagListe(TagListe ptagListe) : base(ptagListe)
        {
            this.isConfigured = true;
        }

        bool isConfigured { get; set; }

        public bool IsConfigured
        {
            get { return this.isConfigured; }
        }

        public void Add(Tag tagToAdd, TagListe ptagListeNegatif)
        {
            bool vAdd = true;
            foreach (Tag tagIn in this)
            {
                if (tagIn != null && tagIn.ID == tagToAdd.ID)
                {
                    vAdd = false;
                }
            }

            if (vAdd)
            {
                foreach (Tag tagNeg in ptagListeNegatif)
                {
                    if (tagNeg.ID == tagToAdd.ID)
                    {
                        vAdd = false;
                    }
                }
            }

            if (vAdd)
            {
                base.Add(tagToAdd);
            }
        }

        public new int Count
        {
            get { return base.Count; }
        }

        public string toXML()
        {
            string vtoXML = string.Empty;

            foreach (Tag tag in this)
            {
                vtoXML += "<tag><id>" + tag.ID + "</id><param1>" + tag.Param1 + "</param1><param2>" + tag.Param2 + "</param2></tag>";
            }

            return vtoXML;
        }

        public new void Clear()
        {
            base.Clear();
            this.isConfigured = false;
        }


    }


}
