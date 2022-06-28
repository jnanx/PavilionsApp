using PavilionsApp.Model;
using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.Win32;

namespace PavilionsApp
{

    public partial class AddShopCenter : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");
        private static readonly Regex onlyNumbersAndComma = new Regex("[^0-9,]+");

        shoppingCenter _shoppingCenter;
        PAVILIONSEntities db;
        public AddShopCenter()
        {
            InitializeComponent();
            db = new PAVILIONSEntities();
            _shoppingCenter = new shoppingCenter();
            AddSCStatus.ItemsSource = db.shoppingCentersStatuses.Where(a => a.shoppingCenterStatusID != 4).ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainForManagC mainForManagC = new MainForManagC();
            mainForManagC.Show();
            this.Close();
        }

        private void AddShopCen_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddShopCenName.Text))
            {
                MessageBox.Show("Введите название ТЦ");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddSCStatus.Text))
            {
                MessageBox.Show("Ввберите статус");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddCoeffAddCost.Text))
            {
                MessageBox.Show("Введите коэффициент добавочной стоимости");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddCost.Text))
            {
                MessageBox.Show("Введите затраты на строительсво");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddCity.Text))
            {
                MessageBox.Show("Введите город");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddFloors.Text))
            {
                MessageBox.Show("Введите этажность");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddNumOfPavs.Text))
            {
                MessageBox.Show("Введите кол-во павильонов");
                return;
            }

            decimal a, b;
            
            bool result_a = decimal.TryParse(AddCoeffAddCost.Text, out a);
            if (!result_a)
            {
                MessageBox.Show("Не верный формат записи коэффициента доб. стоимости");
                return;
            }

            bool result_b = decimal.TryParse(AddCost.Text, out b);
            if (!result_b)
            {
                MessageBox.Show("Не верный формат записи затрат");
                return;
            }


            var db = new PAVILIONSEntities();


            _shoppingCenter.shoppingCenterName = AddShopCenName.Text;
            _shoppingCenter.shoppingCenterStatusID = (AddSCStatus.SelectedItem as shoppingCentersStatus).shoppingCenterStatusID;
            _shoppingCenter.numberOfPavilions = Convert.ToInt32(AddNumOfPavs.Text);
            _shoppingCenter.city = AddCity.Text;
            _shoppingCenter.cost = Convert.ToDecimal(AddCost.Text);
            _shoppingCenter.coefficientOfAddedCost = Convert.ToDecimal(AddCoeffAddCost.Text);
            _shoppingCenter.numberOfFloors = Convert.ToInt32(AddFloors.Text);
            
            db.shoppingCenters
                .Add(_shoppingCenter);
            db.SaveChanges();
        }

        private void AddFloors_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void AddNumOfPavs_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void AddCoeffAddCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void AddCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void AddShopCenPic_Click(object sender, RoutedEventArgs e)
        {
            var db = new PAVILIONSEntities();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Изображения |*.jpg;*.png";
            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                _shoppingCenter.picShoppingCenter = ImageConvert.GetImageBytes(fileDialog.FileName);

            }

        }
    }
}
