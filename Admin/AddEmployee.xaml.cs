using PavilionsApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainAdmin mainAdmin = new MainAdmin();
            mainAdmin.Show();
            this.Close();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    {
        //        var db = new PAVILIONSEntities();
        //        string[] files = Directory.GetFiles(@"C:\Users\Самар\Desktop\Практика 3 курс лето\Image Сотрудники", "*.jpg");
        //        foreach (string file in files)
        //        {
        //            string fullname = String.Join(null, System.IO.Path.GetFileNameWithoutExtension(file).Split(' '));
        //            employee _employee = db.employees.Where(emp => emp.employeeSurname + emp.employeeName + emp.employeeMiddlename == fullname).FirstOrDefault();
        //            if (_employee != null)
        //            {
        //                _employee.photo = ImageConvert.GetImageBytes(file);
        //            }
        //            fullname = null;
        //            _employee = null;
        //        }
        //        db.SaveChanges();
        //    }
        //}
    }
}
