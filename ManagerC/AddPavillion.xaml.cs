using PavilionsApp.Model;
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
using System.Windows.Shapes;

namespace PavilionsApp.ManagerC
{
    /// <summary>
    /// Логика взаимодействия для AddPavillion.xaml
    /// </summary>
    public partial class AddPavillion : Window
    {
        public AddPavillion()
        {
            InitializeComponent();
            var db = new PAVILIONSEntities();
            AddSCID.ItemsSource = db.shoppingCenters.ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AllPavs allPavs = new AllPavs();
            allPavs.Show();
            this.Close();
        }


        private void AddPavillionBut_Click(object sender, RoutedEventArgs e)
        {
            var db = new PAVILIONSEntities();
            var b = new pavilion
            {

            };
            db.pavilions
                .Add(b);
            db.SaveChanges();
        }
    }
}
