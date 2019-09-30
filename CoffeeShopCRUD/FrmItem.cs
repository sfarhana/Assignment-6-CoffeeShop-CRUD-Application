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
    public partial class FrmItem : Form
    {
        
        public FrmItem()
        {
            InitializeComponent();
        }
        
        private void InsertItem()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Insert into Item(ItemName,ItemPrice) values
                                  ('" + txtIname.Text + "'," + txtIprice.Text + ")";
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

        private bool IsItemInDB(string itm)
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string command = @"select * from Item where ItemName='" + itm + "'";
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

        private void ShowItem()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Select * from Item";
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
                    MessageBox.Show("No data is stored in Item Table ");
                }

                sqlconnection.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Error on Show all Items");
            }
           
        }

        private void DeleteItem()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Delete from Item where ItemName='" + txtSearch.Text + "'";
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

        private void SearchItem()
        {
            try
            {
                txtIname.Text = "";
                txtIprice.Text = "";

                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"select * from Item where ItemName='" + txtSearch.Text + "'";
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
                    MessageBox.Show("Search Failed:Data Not Found");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Error on Search Item");
            }
            
        }

        private void UpdateItem()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True"; 
                SqlConnection sqlconnection = new SqlConnection(ConnectionString); 
                sqlconnection.Open();

                string CommandString = @"Update Item set ItemName='" + txtIname.Text +
                                       "',ItemPrice=" + txtIprice.Text + "where ItemName='" + txtSearch.Text + "'";

                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);

                int isUpdated = sqlcommand.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    MessageBox.Show("Update Succsecfully");
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
            txtIname.Text = "";
            txtIprice.Text = "";
            txtSearch.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIname.Text))
            {
                MessageBox.Show("name field can not be empty");
                return;
            }

            bool isItem = IsItemInDB(txtIname.Text);

            if (isItem == true)
            {
                MessageBox.Show("name already exsists");
                return;
            }
            else
            {
                InsertItem();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowItem();
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            UpdateItem();
            ClearControls();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }


    }
}
