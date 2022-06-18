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

namespace PavilionsApp
{
    
    public partial class MainForManagC : Window
    {
        public MainForManagC()
        {
            InitializeComponent();
        }

        private void ShowAllShopingCenters()
        {
            SqlConnection showAll = new SqlConnection(@"Data Source=LAPTOP-BFCVFHEM\SQLEXPRESS;Initial Catalog=PAVILIONS;Integrated Security=True");
            showAll.Open();
            SqlCommand fillData = new SqlCommand("SELECT dbo.shoppingCenters.shoppingCenterName, dbo.shoppingCentersStatuses.shoppingCenterStatusName, dbo.shoppingCenters.numberOfPavilions, dbo.shoppingCenters.city, dbo.shoppingCenters.cost, dbo.shoppingCenters.numberOfFloors, dbo.shoppingCenters.coefficientOfAddedCost FROM dbo.shoppingCenters INNER JOIN dbo.shoppingCentersStatuses ON dbo.shoppingCenters.shoppingCenterStatusID = dbo.shoppingCentersStatuses.shoppingCenterStatusID", showAll);
            fillData.ExecuteNonQuery();
            DataTable allShopCenters = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(fillData);
            sqlDataAdapter.Fill(allShopCenters);
            AllShopCenters.ItemsSource = allShopCenters.DefaultView;
            showAll.Close();
        }

        private void BACK_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
