using Microsoft.Win32;
using PavilionsApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PavilionsApp.ManagerC.ShoppingCenters
{
    public partial class EditShopCenter : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");
        private static readonly Regex onlyNumbersAndComma = new Regex("[^0-9,]+");

        private shoppingCenter _shoppingCenter;
        public EditShopCenter(shoppingCenter shoppingCenter)
        {
            _shoppingCenter = shoppingCenter;
            InitializeComponent();
            var db = new PAVILIONSEntities();

            var StatusesSC = db.shoppingCentersStatuses.ToList();
            EditSCStatus.ItemsSource = StatusesSC; 

            EditShopCenName.Text = _shoppingCenter.shoppingCenterName;
            EditSCStatus.SelectedItem = StatusesSC.Where(sc => sc.shoppingCenterStatusID == _shoppingCenter.shoppingCenterStatusID).FirstOrDefault();
            EditCoeffAddCost.Text = _shoppingCenter.coefficientOfAddedCost.ToString();
            EditCost.Text = _shoppingCenter.cost.ToString();
            EditCity.Text = _shoppingCenter.city;
            EditFloors.Text = _shoppingCenter.numberOfFloors.ToString();
            EditNumOfPavs.Text = _shoppingCenter.numberOfPavilions.ToString();
            if(shoppingCenter.picShoppingCenter != null)
            {
                SCImage.Source = ImageConvert.BytesToImage(_shoppingCenter.picShoppingCenter);
            }
            

        }

        
        private void EditCoeffAddCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void EditCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }
        private void EditFloors_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void EditNumOfPavs_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }


        private void EditShopCen_Click(object sender, RoutedEventArgs e)
        {
            decimal a, b;
            bool result_a = decimal.TryParse(EditCoeffAddCost.Text, out a);
            if (!result_a)
            {
                MessageBox.Show("Не верный формат записи коэффициента доб. стоимости");
                return;
            }

            bool result_b = decimal.TryParse(EditCost.Text, out b);
            if (!result_b)
            {
                MessageBox.Show("Не верный формат записи стоимости");
                return;
            }
            
            var db = new PAVILIONSEntities();
            var sc = db.shoppingCenters.Find(_shoppingCenter.shoppingCenterID);
            sc.shoppingCenterName = EditShopCenName.Text;
            sc.shoppingCenterStatusID = EditSCStatus.SelectedItem != null? (EditSCStatus.SelectedItem as shoppingCentersStatus).shoppingCenterStatusID : _shoppingCenter.shoppingCenterStatusID;
            sc.coefficientOfAddedCost = Convert.ToDecimal(EditCoeffAddCost.Text);
            sc.cost = Convert.ToDecimal(EditCost.Text);
            sc.city = EditCity.Text;
            sc.numberOfFloors = Convert.ToInt32(EditFloors.Text);
            sc.picShoppingCenter = _shoppingCenter.picShoppingCenter;
            sc.numberOfPavilions = Convert.ToInt32(EditNumOfPavs.Text);
            db.Entry(sc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainForManagC mainForManagC = new MainForManagC();
            mainForManagC.Show();
            this.Close();
        }

        private void EditShopCenPic_Click(object sender, RoutedEventArgs e)
        {
            var db = new PAVILIONSEntities();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Изображения |*.jpg;*.png";
            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                _shoppingCenter.picShoppingCenter = ImageConvert.GetImageBytes(fileDialog.FileName);

            }
            //var db = new PAVILIONSEntities();
            //string[] files = Directory.GetFiles(@"C:\Users\Самар\Desktop\Практика 3 курс лето\Image ТЦ", "*.jpg");
            //foreach (string file in files)
            //{
            //    string name = System.IO.Path.GetFileNameWithoutExtension(file);
            //    shoppingCenter SC = db.shoppingCenters.Where(sc => sc.shoppingCenterName == name).FirstOrDefault();
            //    if (SC != null)
            //    {
            //        SC.picShoppingCenter = ImageConvert.GetImageBytes(file);
            //    }
            //    name = null;
            //    SC = null;
            //}
            //db.SaveChanges();
        }
    }
}
