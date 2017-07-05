using Articles.Core;
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
        public int PageFrom { get; set; }
        public int PageTo { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createDB_Click(object sender, RoutedEventArgs e)
        {
            XmlDB DB = new XmlDB(dbAddress.Text);

            PageFrom = Convert.ToInt32(pageFrom.Text);
            PageTo = Convert.ToInt32(pageTo.Text);

            DB.FillUp(Address, PageFrom, PageTo);
        }
    }
}
