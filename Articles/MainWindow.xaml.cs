using Articles.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Articles
{
    public partial class MainWindow : Window
    {
        public string Address = @"http://old.mon.gov.ua/ua/about-ministry/normative/page";
        Dictionary<string, int> DictDB;
        Dictionary<string, int> DictCurrent;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createDB_Click(object sender, RoutedEventArgs e)
        {
            XmlDB DB = new XmlDB(dbAddress.Text);
            DB.DBCreator();

            int PageFrom = Convert.ToInt32(pageFrom.Text);
            int PageTo = Convert.ToInt32(pageTo.Text);

            DictDB = DB.FillUp(Address, PageFrom, PageTo);
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browse = new OpenFileDialog();
            browse.DefaultExt = ".xml";
            browse.Filter = "All Files (*.xml, *.xls, *.xlsx)|*.xml; *.xls; *.xlsx";
            Nullable<bool> result = browse.ShowDialog();

            if (result == true)
            {
                dbAddress.Text = browse.FileName;
            }
        }

        private void loadArticles_Click(object sender, RoutedEventArgs e)
        {
            int PageFrom = Convert.ToInt32(pageFrom.Text);
            int PageTo = Convert.ToInt32(pageTo.Text);
            XmlDB DB = new XmlDB(dbAddress.Text);

            if (DictDB == null)
            {
                DictDB = DB.DBDictFromXml();
            }

            PageParser parser = new PageParser();
            DictCurrent = parser.ParsePage(Address, PageFrom, PageTo);

            ICollection<string> curKeys = DictCurrent.Keys;
            foreach (string curKey in curKeys)
            {
                if (!DictDB.ContainsKey(curKey))
                {
                    textBox.Text += "Страница " + DictCurrent[curKey] + "\n";
                    textBox.Text += curKey + "\n";
                }
            }
            DictCurrent = null;
        }
    }
}
