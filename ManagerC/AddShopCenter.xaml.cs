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

namespace PavilionsApp
{

    public partial class AddShopCenter : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");
        private static readonly Regex onlyNumbersAndComma = new Regex("[^0-9,]+");
        public AddShopCenter()
        {
            InitializeComponent();
            var db = new PAVILIONSEntities();
            AddSCStatus.ItemsSource = db.shoppingCentersStatuses.Where(a => a.shoppingCenterStatusID != 4).ToList();
        }

        private void ToPav_Click(object sender, RoutedEventArgs e)
        {
            AllPavs allPavs = new AllPavs();
            allPavs.Show();
            this.Close();
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

            var db = new PAVILIONSEntities();
            var a = new shoppingCenter
            {
                shoppingCenterName = AddShopCenName.Text,
                shoppingCenterStatusID = (AddSCStatus.SelectedItem as shoppingCentersStatus).shoppingCenterStatusID,
                numberOfPavilions = Convert.ToInt32(AddNumOfPavs.Text),
                city = AddCity.Text,
                cost = Convert.ToDecimal(AddCost.Text),
                coefficientOfAddedCost = Convert.ToDecimal(AddCoeffAddCost.Text),
                numberOfFloors = Convert.ToInt32(AddFloors.Text)
            };
            db.shoppingCenters
                .Add(a);
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
    }
}
