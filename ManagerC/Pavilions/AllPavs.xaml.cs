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

        private List<pavilion> _pavilions;
        
        PAVILIONSEntities db;
        public AllPavs(int shoppingCenterID)
        {
            db = new PAVILIONSEntities();
            _shoppingCenterID = shoppingCenterID;
            _pavilions = db.pavilions.Where(p => p.shoppingCenterID == _shoppingCenterID).ToList();
            InitializeComponent();
            AllPavillions.ItemsSource = _pavilions;
            List<int> floors = new List<int>();
            for (int i = 1; i <= db.shoppingCenters.Find(_shoppingCenterID).numberOfFloors; i++) floors.Add(i);
           
            ChooseFloor.ItemsSource = floors;
            ChooseStatus.ItemsSource = db.pavilionsStatuses.ToList();
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
            var selecteditemStatus = ChooseStatus.SelectedItem as pavilionStatus;
            if(selecteditemFloor != null)
            {
                List<pavilion> pavilions = new List<pavilion>();
                pavilions = _pavilions.Where(p => p.floor == selecteditemFloor).ToList();
                if (selecteditemStatus != null && StartSquare.Text != "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => p.pavilionStatusID == selecteditemStatus.pavilionStatusID && 
                                                      p.square > Convert.ToDecimal(StartSquare.Text) && 
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList();
                }
                if (selecteditemStatus != null && StartSquare.Text != "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p => p.pavilionStatusID == selecteditemStatus.pavilionStatusID &&
                                                      p.square > Convert.ToDecimal(StartSquare.Text)
                                                      ).ToList();
                }
                if (selecteditemStatus != null && StartSquare.Text == "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => p.pavilionStatusID == selecteditemStatus.pavilionStatusID &&
                                                      
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList(); 
                }
                if (selecteditemStatus == null && StartSquare.Text != "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => 
                                                      p.square > Convert.ToDecimal(StartSquare.Text) &&
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList(); 
                }
                if (selecteditemStatus != null && StartSquare.Text == "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p => p.pavilionStatusID == selecteditemStatus.pavilionStatusID 
                                                      ).ToList(); 
                }
                if (selecteditemStatus == null && StartSquare.Text == "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => 
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList(); 
                }
                if (selecteditemStatus == null && StartSquare.Text != "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p =>
                                                      p.square > Convert.ToDecimal(StartSquare.Text)).ToList(); 
                }

                AllPavillions.ItemsSource = pavilions;
            }
        }


        private void ChooseStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboboxFloor = sender as ComboBox;
            var selecteditemStatus = comboboxFloor.SelectedItem as pavilionStatus;
            var selecteditemFloor = ChooseFloor.SelectedItem as int?;
            if (selecteditemStatus != null)
            {
                List<pavilion> pavilions = new List<pavilion>();
                pavilions = _pavilions.Where(p => p.pavilionsStatuses == selecteditemStatus).ToList();
                if (selecteditemFloor != null && StartSquare.Text != "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => p.floor == selecteditemFloor &&
                                                      p.square > Convert.ToDecimal(StartSquare.Text) &&
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList();
                }
                if (selecteditemFloor != null && StartSquare.Text != "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p => p.floor == selecteditemFloor &&
                                                      p.square > Convert.ToDecimal(StartSquare.Text)
                                                      ).ToList();
                }
                if (selecteditemFloor != null && StartSquare.Text == "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p => p.floor == selecteditemFloor &&

                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList();
                }
                if (selecteditemFloor == null && StartSquare.Text != "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p =>
                                                      p.square > Convert.ToDecimal(StartSquare.Text) &&
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList();
                }
                if (selecteditemFloor != null && StartSquare.Text == "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p => p.floor == selecteditemFloor
                                                      ).ToList();
                }
                if (selecteditemFloor == null && StartSquare.Text == "" && EndSquare.Text != "")
                {
                    pavilions = pavilions.Where(p =>
                                                      p.square < Convert.ToDecimal(EndSquare.Text)).ToList();
                }
                if (selecteditemFloor == null && StartSquare.Text != "" && EndSquare.Text == "")
                {
                    pavilions = pavilions.Where(p =>
                                                      p.square > Convert.ToDecimal(StartSquare.Text)).ToList();
                }

                AllPavillions.ItemsSource = pavilions;
            }
        }

        private void ToReset_Click(object sender, RoutedEventArgs e)
        {
            AllPavillions.ItemsSource = db.pavilions.Where(p => p.shoppingCenterID == _shoppingCenterID).ToList();
        }

        private void EndSquare_TextChanged(object sender, TextChangedEventArgs e)
        {

            
        }
    }
}
