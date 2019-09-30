using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoffeeShopCRUD
{
    public partial class FrmCustomer : Form
    {
        
        public FrmCustomer()
        {
            InitializeComponent();
        }

        public void InsertCustomer()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Insert into Customer(CustName,Contact,Address) values
                                   ('" + txtCname.Text + "','" + txtContact.Text + "','" + txtAddress.Text + "')";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                int isExecuted = sqlcommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Entry Saved");
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Insertion Error");
            }
                
        }

        private void ShowCustomer()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Select * from Customer";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);
                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcommand);
                DataTable datatable = new DataTable();

                sqladapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    dataGridView.DataSource = datatable;
                }
                else
                {
                    dataGridView.DataSource = null;
                    MessageBox.Show("No data is stored in Customer Table ");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Error on Show all Customer");
            }
            

        }

        private void DeleteCustomer()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Delete from Customer where CustName='" + txtSearch.Text + "'";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                int isDeleted = sqlcommand.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    MessageBox.Show("Delete Succsecfully");
                }
                else
                {
                    MessageBox.Show("Delete Failed");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error on Deletion");
            }
            
        }

        private void SearchCustomer()
        {
            try
            {
                txtCname.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";

                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"select * from Customer where CustName = '" + txtSearch.Text + "'";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcommand);
                DataTable datatable = new DataTable();
                sqladapter.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {
                    dataGridView.DataSource = datatable;
                }
                else
                {
                    MessageBox.Show("Search Failed:Data Not Found");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error on Search Customer");
            }
           
        }

        private bool IsCustomerInDB(string nm)
        {

            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string command = @"select * from Customer where CustName = '" + nm + "'";
                SqlCommand sqlcommand = new SqlCommand(command, sqlconnection);
                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcommand);
                DataTable datatable = new DataTable();
                sqladapter.Fill(datatable);

                sqlconnection.Close();

                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        private void UpdateCustomer()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                
                sqlconnection.Open();

                string CommandString = @"Update Customer set CustName ='" + txtCname.Text + "', Contact = '" + txtContact.Text +
                                       "', Address = '" + txtAddress.Text + "' where CustName = '" + txtSearch.Text + "'";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                int isUpdated = sqlcommand.ExecuteNonQuery();

                if (isUpdated > 0)
                {
                    MessageBox.Show("Update Succsecfull");
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }

                sqlconnection.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error on Update");
            }
          
        }

        private void ClearControls()
        {
            txtCname.Text="";
            txtContact.Text="";
            txtAddress.Text = "";
            txtSearch.Text = "";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCname.Text))
            {
                MessageBox.Show("name field can not be empty");
                return;
            }

            bool isCus=IsCustomerInDB(txtCname.Text);

            if (isCus == true)
            {
                MessageBox.Show("name already exsists");
                return;
            }
            else
            {
                InsertCustomer();
            }
           
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowCustomer();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
            ClearControls();
        }

        
                                
      }
}
