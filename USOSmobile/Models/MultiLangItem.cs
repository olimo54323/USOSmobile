namespace USOSmobile.Models
{
    internal class MultiLangItem
    {
        private string _Pl;
        private string _En;
        public string pl
        {
            get { return _Pl; }
            set { _Pl = value; }
        }
        public string en
        {
            get { return _En; }
            set { _En = value; }
        }

        public MultiLangItem()
        {
            _Pl = string.Empty;
            _En = string.Empty;

        }
    }
}
