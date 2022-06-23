﻿using PavilionsApp.Model;
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

namespace PavilionsApp.ManagerC
{

    public partial class AddPavillion : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9]+");
        private static readonly Regex onlyNumbersAndComma = new Regex("[^0-9,]+");
        private int _shoppingCenterID;
        public AddPavillion(int shoppingCenterID)
        {
            _shoppingCenterID = shoppingCenterID;
            InitializeComponent();
            var db = new PAVILIONSEntities();
            AddPavStatus.ItemsSource = db.pavilionsStatuses.Where(b => b.pavilionStatusID != 4).ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AllPavs allPavs = new AllPavs(_shoppingCenterID);
            allPavs.Show();
            this.Close();
        }


        private void AddPavillionBut_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddPavNum.Text))
            {
                MessageBox.Show("Введите номер павильона");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddFloorNumPav.Text))
            {
                MessageBox.Show("Введите этаж");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddSquare.Text))
            {
                MessageBox.Show("Введите плоадь павильона");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddPavStatus.Text))
            {
                MessageBox.Show("Выберите статус павильона");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddCoeffAddCostPav.Text))
            {
                MessageBox.Show("Введите коэффициент добавочной стоимости");
                return;
            }

            if (string.IsNullOrWhiteSpace(AddCostPerMeter.Text))
            {
                MessageBox.Show("Введите стоимость за кв.метр");
                return;
            }

            var db = new PAVILIONSEntities();
            var b = new pavilion
            {
                shoppingCenterID = _shoppingCenterID,
                pavilionNumber = AddPavNum.Text,
                floor = Convert.ToInt32(AddFloorNumPav.Text),
                square = Convert.ToDecimal(AddSquare.Text),
                pavilionStatusID = (AddPavStatus.SelectedItem as pavilionStatus).pavilionStatusID,
                coefficientOfAddedCost = Convert.ToDecimal(AddCoeffAddCostPav.Text),
                costForMetere = Convert.ToDecimal(AddCostPerMeter.Text)               
            };
            db.pavilions
                .Add(b);
            db.SaveChanges();
        }

        private void AddFloorNumPav_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbers.IsMatch(e.Text);
        }

        private void AddSquare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void AddCoeffAddCostPav_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }

        private void AddCostPerMeter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersAndComma.IsMatch(e.Text);
        }
    }
}
