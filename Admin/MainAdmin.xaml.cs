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

namespace PavilionsApp.Admin
{

    public partial class MainAdmin : Window
    {
        PAVILIONSEntities db;
        public MainAdmin()
        {
            db = new PAVILIONSEntities();
            InitializeComponent();
            AllEmployee.ItemsSource = db.employees.ToList();

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void AllEmployee_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var datagrid = sender as DataGrid;
            var emp = datagrid.SelectedItem as employee;
            if (datagrid.SelectedItem != null)
            {
                db.employees.Find(emp.employeeID).roleID = 4;
            }
        }

        private void SaveEmp_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(FindEmployee.Text != null)
            {
                AllEmployee.ItemsSource = db.employees.Where(emp => emp.employeeSurname == FindEmployee.Text).ToList();
            }
        }


        private void AddEmpBut_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.Show();
            this.Close();
        }
    }
}
