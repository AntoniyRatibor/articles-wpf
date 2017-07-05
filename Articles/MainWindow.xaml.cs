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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createDB_Click(object sender, RoutedEventArgs e)
        {
            XmlDB DB = new XmlDB(dbAddress.Text);

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
    }
}
