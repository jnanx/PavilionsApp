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
using System.Data;
using System.Data.SqlClient;
using PavilionsApp.Model;

namespace PavilionsApp
{
    
    public partial class MainForManagC : Window
    {
        public MainForManagC()
        {
            InitializeComponent();
            var db = new PAVILIONSEntities();
            AllShopCenters.ItemsSource = db.shoppingCenters
                .Select(s => new 
                {
                    name = s.shoppingCenterName,
                    statusName = s.shoppingCentersStatuses.shoppingCenterStatusName,
                    numofpav = s.numberOfPavilions,
                    city = s.city,
                    cost = s.cost,
                    numoffloors = s.numberOfFloors,
                    coeffaddcost = s.coefficientOfAddedCost
                }).ToList();
        }

      

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
