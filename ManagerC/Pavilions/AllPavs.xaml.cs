using PavilionsApp.ManagerC;
using PavilionsApp.ManagerC.Pavilions;
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
    
    public partial class AllPavs : Window
    {
        private int _shoppingCenterID;
        
        PAVILIONSEntities db;
        public AllPavs(int shoppingCenterID)
        {
            db = new PAVILIONSEntities();
            _shoppingCenterID = shoppingCenterID;
            InitializeComponent();
            AllPavillions.ItemsSource = db.pavilions.Where(p => p.shoppingCenterID == _shoppingCenterID).ToList();
            ChooseFloor.ItemsSource = db.pavilions.Select(p => p.floor).Distinct().ToList();
            ChooseSquare.ItemsSource = db.pavilions.Select(p => p.square).Distinct().ToList();
            ChooseStatus.ItemsSource = db.pavilions.Select(p => p.pavilionsStatuses.pavilionStatusName).Distinct().ToList();
            //.Select(s => new
            //{
            //    statusshopcen = s.shoppingCenters.shoppingCentersStatuses.shoppingCenterStatusName,
            //    nameshopcen = s.shoppingCenters.shoppingCenterName,
            //    floor = s.floor,
            //    pavnum = s.pavilionNumber,
            //    square = s.square,
            //    pavstatus = s.pavilionsStatuses.pavilionStatusName,
            //    coeffofaddcost = s.coefficientOfAddedCost,
            //    costpermeter = s.costForMetere
            //}).ToList();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainForManagC mainForManagC = new MainForManagC();
            mainForManagC.Show();
            this.Close();
        }

        private void ToAddPav_Click(object sender, RoutedEventArgs e)
        {
            AddPavillion addPavillion = new AddPavillion(_shoppingCenterID);
            addPavillion.Show();
            this.Close();
        }

        private void SaveDeletedPavs_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void AllPavillions_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagridpav = sender as DataGrid;
            var pav = datagridpav.SelectedItem as pavilion;
            if (datagridpav.SelectedItem != null)
            {
                if(e.Key == Key.Delete)
                {
                    db.pavilions.Find(pav.pavilionID).pavilionStatusID = 4;
                }
                if(e.Key == Key.Tab)
                {
                    EditPavilion editPavilion = new EditPavilion(pav);
                    editPavilion.Show();
                    this.Close();
                }
            }
        }

        private void ChooseFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboboxFloor = sender as ComboBox;
            var selecteditemFloor = comboboxFloor.SelectedItem as int?;
            var selecteditemSquare = ChooseSquare.SelectedItem as int?;
            var selecteditemStatus = ChooseStatus.SelectedItem as shoppingCentersStatus;
            if(selecteditemFloor != null)
            {
                AllPavillions.ItemsSource = selecteditemFloor == null ?
                    db.pavilions.Where(p => p.floor == selecteditemFloor && p.shoppingCenterID == _shoppingCenterID).ToList() :
                    db.pavilions.Where(p => p.floor == selecteditemFloor && p.shoppingCenterID == _shoppingCenterID && p.square == selecteditemSquare && p.pavilionStatusID == selecteditemStatus.shoppingCenterStatusID).ToList();
                ;
            }
        }


        private void ChooseSquare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChooseStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToReset_Click(object sender, RoutedEventArgs e)
        {
            AllPavillions.ItemsSource = db.pavilions.Where(p => p.shoppingCenterID == _shoppingCenterID).ToList();
        }
    }
}
