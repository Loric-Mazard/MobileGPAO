using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RFID_Android_XAML.RFID
{
    public class TagListe

    {
        public TagListe()
        {
            this.isConfigured = false;
            this.collection = new List<Tag>();
        }

        public TagListe(List<Tag> ptagListe)
        {
            this.isConfigured = true;
            this.collection = ptagListe;
        }

        public TagListe(ObservableCollection<Tag> ptagListe)
        {
            List<Tag> ListTag =new List<Tag>();

            foreach(Tag tag in ptagListe)
            {
                ListTag.Add(tag);
            }
            this.isConfigured = true;
            this.collection = ListTag;
        }

        List<Tag> collection = null;
        bool isConfigured { get; set; }

        public bool IsConfigured
        {
            get { return this.isConfigured; }
        }

        public List<Tag> Collection

        {
            get { return this.collection; }
            set { this.collection =value; }
        }

        public void Clear()
        {
            this.collection.Clear();
        }

        public void Add(Tag tag, TagListe ptagListeNegatif)
        {
            this.collection.Add(tag);
        }

        public int Count()
        {
            return this.collection.Count;
        }
    }

  
}
