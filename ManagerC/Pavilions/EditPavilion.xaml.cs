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

namespace PavilionsApp.ManagerC.Pavilions
{

    public partial class EditPavilion : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");
        private static readonly Regex onlyNumbersAndComma = new Regex("[^0-9,]+");

        private pavilion _pavilion;
        public EditPavilion(pavilion pavilion)
        {
            _pavilion = pavilion;
            
            InitializeComponent();
            var db = new PAVILIONSEntities();
            EditPavStatus.ItemsSource = db.pavilionsStatuses.ToList();

            EditPavNum.Text = _pavilion.pavilionNumber;
            EditFloorNumPav.Text = _pavilion.floor.ToString();
            EditSquare.Text = _pavilion.square.ToString();
            EditPavStatus.Text = _pavilion.pavilionsStatuses.pavilionStatusName;
            EditCoeffAddCostPav.Text = _pavilion.coefficientOfAddedCost.ToString();
            EditCostPerMeter.Text = _pavilion.costForMetere.ToString();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AllPavs allPavs = new AllPavs(_pavilion.shoppingCenterID);
            allPavs.Show();
            this.Close();
        }

        private void EditFloorNumPav_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void EditSquare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void EditCoeffAddCostPav_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void EditCostPerMeter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void EditPavillionBut_Click(object sender, RoutedEventArgs e)
        {
            var db = new PAVILIONSEntities();
            var pav = db.pavilions.Find(_pavilion.pavilionID);
            pav.pavilionNumber = EditPavNum.Text;
            pav.floor = Convert.ToInt32(EditFloorNumPav.Text);
            pav.pavilionStatusID = EditPavStatus.SelectedItem != null? (EditPavStatus.SelectedItem as pavilionStatus).pavilionStatusID : _pavilion.pavilionStatusID;
            pav.square = Convert.ToDecimal(EditSquare.Text);
            pav.costForMetere = Convert.ToDecimal(EditCostPerMeter.Text);
            pav.coefficientOfAddedCost = Convert.ToDecimal(EditCoeffAddCostPav.Text);
            db.Entry(pav).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
