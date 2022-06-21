using PavilionsApp.ManagerC;
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

namespace PavilionsApp
{
    /// <summary>
    /// Логика взаимодействия для AllPavs.xaml
    /// </summary>
    public partial class AllPavs : Window
    {
        public AllPavs()
        {
            InitializeComponent();
            var db = new PAVILIONSEntities();
            AllPavillions.ItemsSource = db.pavilions
                .Select(s => new
                {
                    statusshopcen = s.shoppingCenters.shoppingCentersStatuses.shoppingCenterStatusName,
                    nameshopcen = s.shoppingCenters.shoppingCenterName,
                    floor = s.floor,
                    pavnum = s.pavilionNumber,
                    square = s.square,
                    pavstatus = s.pavilionsStatuses.pavilionStatusName,
                    coeffofaddcost = s.coefficientOfAddedCost,
                    costpermeter = s.costForMetere
                }).ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AddShopCenter addShopCenter = new AddShopCenter();
            addShopCenter.Show();
            this.Close();
        }

        private void ToAddPav_Click(object sender, RoutedEventArgs e)
        {
            AddPavillion addPavillion = new AddPavillion();
            addPavillion.Show();
            this.Close();
        }
    }
}
