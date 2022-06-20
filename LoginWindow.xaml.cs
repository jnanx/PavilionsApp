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

namespace PavilionsApp
{
    
    public partial class LoginWindow : Window
    {
        int checkForCaptcha = 0;
        public LoginWindow()
        {
            InitializeComponent();
        }


        private void AfterLoginButton_Click(object sender, RoutedEventArgs e)
        {
            checkForCaptcha ++;
            if (checkForCaptcha >3)
            {
                MessageBox.Show("попався");
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            string[] checkforlog1 = LoginBox.Text.Split('@');
            if (checkforlog1.Length != 2)
            {
                MessageBox.Show("Не верный формат логина");
                return;
            }
            string[] checkforlog2 = checkforlog1[1].Split('.');
            if (checkforlog2.Length != 2)
            {
                MessageBox.Show("Не верный формат логина");
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль");
                return;
            }
           
            var db = new PAVILIONSEntities();
            var employee = db.employees.Where(emp => emp.login == LoginBox.Text && emp.password == PasswordBox.Password).FirstOrDefault();
            if(employee != null)
            {
                if(employee.roleID != 3)
                {
                    MessageBox.Show("Вы не являетесь Менеджером типа С. Вход запрещен.");
                    return;
                }
                MainForManagC mainForManagC = new MainForManagC();
                mainForManagC.Show();
                this.Close();
                checkForCaptcha = 0;
            }
            else
            {
                MessageBox.Show("Неверные почта или пароль");
            }
            //if (!string.IsNullOrWhiteSpace(LoginBox.Text))
            //{
            //    string[] checkForLog1 = LoginBox.Text.Split('@');
            //    if (checkForLog1.Length == 2)
            //    {
            //        string[] checkForLog2 = checkForLog1[1].Split('.');
            //        if (checkForLog2.Length == 2)
            //        {

            //            if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
            //            {
            //                SqlConnection forLogin = new SqlConnection(@"Data Source=LAPTOP-BFCVFHEM\SQLEXPRESS;Initial Catalog=PAVILIONS;Integrated Security=True");
            //                forLogin.Open();
            //                int i = 0;
            //                SqlCommand forLogComm = new SqlCommand("SELECT login, password FROM employees WHERE login = @login AND password = @password", forLogin);
            //                forLogComm.Parameters.AddWithValue("@login", LoginBox.Text);
            //                forLogComm.Parameters.AddWithValue("@password", PasswordBox.Password);
            //                forLogComm.ExecuteNonQuery();
            //                DataTable dataTable = new DataTable();
            //                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(forLogComm);
            //                sqlDataAdapter.Fill(dataTable);
            //                i = Convert.ToInt32(dataTable.Rows.Count.ToString());

            //                if (i == 0)
            //                {
            //                    MessageBox.Show("Неверный логин или пароль");
            //                }
            //                else
            //                {
            //                    SqlCommand getRoleComm = new SqlCommand("SELECT roleID FROM employees WHERE login = @login AND password = @password", forLogin);
            //                    getRoleComm.Parameters.AddWithValue("@login", LoginBox.Text);
            //                    getRoleComm.Parameters.AddWithValue("@password", PasswordBox.Password);
            //                    var role = getRoleComm.ExecuteScalar();
            //                    var r = Convert.ToInt32(role.ToString());
            //                    if (r == 3)
            //                    {
            //                        MainForManagC mainForManagC = new MainForManagC();
            //                        mainForManagC.Show();
            //                        this.Close();
            //                    } else
            //                    {
            //                        MessageBox.Show("Вы не являетесь Менеджером типа С. Вход запрещен.");
            //                    }

            //                }
                            
            //            }
            //            else
            //            {
            //                MessageBox.Show("Введите пароль");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Не корректный формат логина");
            //        }
            //    } 
            //    else
            //    {
            //        MessageBox.Show("Не корректный формат логина");
            //    }
            //} else
            //{
            //    MessageBox.Show("Введите логин");
            //}
        }

        private void LoginBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                PasswordBox.Focus();
            }
        }


        private void NoSpace(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
