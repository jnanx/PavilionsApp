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
using PavilionsApp.ManagerC.ShoppingCenters;

namespace PavilionsApp
{
    
    public partial class MainForManagC : Window
    {
        PAVILIONSEntities db;

        public MainForManagC()
        {
            InitializeComponent();
            db = new PAVILIONSEntities();
            AllShopCenters.ItemsSource = db.shoppingCenters.OrderBy(sc => sc.city).ThenBy(sc => sc.shoppingCentersStatuses.shoppingCenterStatusID).ToList();
            ChooseStatus.ItemsSource = db.shoppingCentersStatuses.Where(sc => sc.shoppingCenterStatusID !=4).ToList();
            ChooseCity.ItemsSource = db.shoppingCenters.Select(sc => sc.city).Distinct().ToList();

            //.Select(s => new
            //{
            //    name = s.shoppingCenterName,
            //    statusName = s.shoppingCentersStatuses.shoppingCenterStatusName,
            //    numofpav = s.numberOfPavilions,
            //    city = s.city,
            //    cost = s.cost,
            //    numoffloors = s.numberOfFloors,
            //    coeffaddcost = s.coefficientOfAddedCost
            //})

        }

      

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ToAdd_Click(object sender, RoutedEventArgs e)
        {
            AddShopCenter addShopCenter = new AddShopCenter();
            addShopCenter.Show();
            this.Close();
        }

        private void ToSaveDeleted_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void AllShopCenters_KeyDown(object sender, KeyEventArgs e)
        {
            var datagridsc = sender as DataGrid;
            var sc = datagridsc.SelectedItem as shoppingCenter;
            if (datagridsc.SelectedItem != null)
            {
                if(e.Key == Key.Delete)
                {
                    db.shoppingCenters.Find(sc.shoppingCenterID).shoppingCenterStatusID = 4;
                }
                if(e.Key == Key.Enter)
                {
                    AllPavs allPavs = new AllPavs(sc.shoppingCenterID);
                    allPavs.Show();
                    this.Close();
                }
                if(e.Key == Key.Tab)
                {
                    EditShopCenter editShopCenter = new EditShopCenter(sc);
                    editShopCenter.Show();
                    this.Close();
                }
            }
        }


        private void ChooseStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            var selectedStatus = combobox.SelectedItem as shoppingCentersStatus;
            var selectedCity = ChooseCity.SelectedItem as string;

            if (selectedStatus != null)
            {
                AllShopCenters.ItemsSource = selectedCity == null ?
                    db.shoppingCenters.Where(sc => sc.shoppingCenterStatusID == selectedStatus.shoppingCenterStatusID).ToList() :
                    db.shoppingCenters.Where(sc => sc.shoppingCenterStatusID == selectedStatus.shoppingCenterStatusID && sc.city == selectedCity).ToList();
                   ;
            }
        }

        private void ChooseCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboboxCity = sender as ComboBox;
            var selecteditemCity = comboboxCity.SelectedItem as string;
            var selecteditemStatus = ChooseStatus.SelectedItem as shoppingCentersStatus;

            if (selecteditemCity != null)
            {
                AllShopCenters.ItemsSource = selecteditemStatus == null ?
                    db.shoppingCenters.Where(sc => sc.city == selecteditemCity).ToList() :
                    db.shoppingCenters.Where(sc => sc.city == selecteditemCity && sc.shoppingCenterStatusID == selecteditemStatus.shoppingCenterStatusID).ToList();
                    ;
            }
        }

        private void ToReset_Click(object sender, RoutedEventArgs e)
        {
            AllShopCenters.ItemsSource = db.shoppingCenters.ToList();
        }
    }
}
