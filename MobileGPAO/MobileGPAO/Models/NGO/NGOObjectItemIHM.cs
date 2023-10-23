using System.Xml.Serialization;

namespace GPAOnGo.NGO
{
      
    public class NGOObjectItemIHM
    {
        const string XML_OBJECTERP_ITEM_IHM_IMPORT = "important";
        const string XML_OBJECTERP_ITEM_IHM_VISIBLE = "visible";
        const string XML_OBJECTERP_ITEM_IHM_ROW = "row";
        const string XML_OBJECTERP_ITEM_IHM_COL = "col";
        const string XML_OBJECTERP_ITEM_IHM_Type = "type_presentation";

        public enum enumItemIHMType
        {
            [XmlEnum(Name = "0")]
            NA = 0,
            [XmlEnum(Name = "1")]
            Menu = 1,
            [XmlEnum(Name = "2")]
            Detail = 2,
            [XmlEnum(Name = "3")]
            SfChart_ColumnSeries = 3,
            [XmlEnum(Name = "4")]
            SfChart_LineSeries = 4,
            [XmlEnum(Name = "5")]
            TreeView = 5,
            [XmlEnum(Name = "6")]
            ComboBox = 6,
            [XmlEnum(Name = "7")]
            TextBox = 7
        }

        public NGOObjectItemIHM()
        {
            this.A_Important = false;
            this.A_Visible = false;
            this.A_Row = 0;
            this.A_Col = 0;
            this.A_Type = enumItemIHMType.NA;
        }


        bool A_Important { get; set; }
        bool A_Visible { get; set; }
        int A_Row { get; set; }
        int A_Col { get; set; }
        enumItemIHMType A_Type { get; set; }

        [XmlElement(XML_OBJECTERP_ITEM_IHM_IMPORT)]
        public bool Important
        {
            get { return A_Important; }
            set { A_Important = value; }
        }

        [XmlElement(XML_OBJECTERP_ITEM_IHM_VISIBLE)]
        public bool Visible
        {
            get { return A_Visible; }
            set { A_Visible = value; }
        }

        [XmlElement(XML_OBJECTERP_ITEM_IHM_ROW)]
        public int Row
        {
            get { return A_Row; }
            set { A_Row = value; }
        }

        [XmlElement(XML_OBJECTERP_ITEM_IHM_COL)]
        public int Col
        {
            get { return A_Col; }
            set { A_Col = value; }
        }

        [XmlElement(XML_OBJECTERP_ITEM_IHM_Type)]
        public enumItemIHMType Type
        {
            get { return A_Type; }
            set { A_Type = value; }
        }
    }
}
