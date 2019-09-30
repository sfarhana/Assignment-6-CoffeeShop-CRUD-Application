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
    public partial class FrmOrder : Form
    {
        int UnitPrice =0;
        public FrmOrder()
        {
            InitializeComponent();
        }

        private void InsertOrder(int payment)
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string CommandString = @"Insert into OrderItem(CustName,ItemName,Quantity,Bill) 
                                   values('" + txtCname.Text + "','" + txtIname.Text + "',"
                                                 + txtQuantity.Text + ",'" + payment.ToString() + "')";
                SqlCommand sqlcommand = new SqlCommand(CommandString, sqlconnection);
                int isExecuted = sqlcommand.ExecuteNonQuery();
                if (isExecuted > 0)
                {
                    MessageBox.Show("Order Entry Saved");
                }
                else
                {
                    MessageBox.Show("Order Not Saved");
                }

                sqlconnection.Close();

            }
            catch (Exception)
            {
                
                throw;
            }
            

        }
        private void ShowAllOrder()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string CommandString = @"Select * from OrderItem";
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
                    MessageBox.Show("No data is stored in OrderItem Table ");
                }

                sqlconnection.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Error on Show all order");
            }
            


        }
        private void SearchOrder()
        {
            try
            {
                txtCname.Text = "";
                txtIname.Text = "";
                txtQuantity.Text = "";

                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string CommandString = @"select * from OrderItem where CustName='" + txtSearch.Text + "'";
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

                MessageBox.Show("Error on Search Order");
            }
            
        }
        private void DeleteOrder()
        {
            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string CommandString = @"Delete from OrderItem where CustName='" + txtSearch.Text + "'";
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
        private void UpdateOrder()
        {
            try
            {
                //IsItemInDB(txtIname.Text);
                string nm = txtCname.Text;
                string itm = txtIname.Text;
                string qnt = txtQuantity.Text;
                bool isItem = IsItemInDB(itm);
                bool isCustomer = IsCustomerInDB(nm);

                if (isItem == true && isCustomer == true)
                {
                    int Bill = Convert.ToInt16(qnt) * Convert.ToInt16(UnitPrice);
                    string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                    SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                    sqlconnection.Open();

                    string CommandString = @"Update OrderItem set CustName='" + txtCname.Text + "',ItemName='"
                                             + txtIname.Text + "',Quantity=" + txtQuantity.Text + ",Bill=" + Bill.ToString() + " where CustName= '" + txtSearch.Text + "'";
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
                else if (isItem == true && isCustomer == false)
                {
                    MessageBox.Show("Customer not in database");
                    return;
                }
                else if (isItem == false && isCustomer == true)
                {
                    MessageBox.Show("Item not in database");
                    return;
                }
                else if (isItem == false && isCustomer == false)
                {
                    MessageBox.Show("Item & Customer is not in database");
                    return;
                }
               
            }
            catch (Exception)
            {

                MessageBox.Show("Error on Update");
            }
           

        }
        
        private bool IsCustomerInDB(string nm)
        {

            try
            {
                string ConnectionString = @"server=FARHANAMOSTO-PC;Database=CoffeeShopCRUD;Integrated Security=True";
                SqlConnection sqlconnection = new SqlConnection(ConnectionString);
                sqlconnection.Open();

                string command = @"select * from Customer where CustName='" + nm + "'";
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
                    UnitPrice = Convert.ToInt16(datatable.Rows[0][1].ToString());
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
        private void AddOrder()
        {
            try
            {
                string nm = txtCname.Text;
                string itm = txtIname.Text;
                string qnt = txtQuantity.Text;
                bool isItem = IsItemInDB(itm);
                bool isCustomer = IsCustomerInDB(nm);

                if (isItem == true && isCustomer == true)
                {
                    int Bill = Convert.ToInt16(qnt) * Convert.ToInt16(UnitPrice);
                    InsertOrder(Bill);
                }
                else if (isItem == true && isCustomer == false)
                {
                    MessageBox.Show("Customer not in database");
                    return;
                }
                else if (isItem == false && isCustomer == true)
                {
                    MessageBox.Show("Item not in database");
                    return;
                }
                else if (isItem == false && isCustomer == false)
                {
                    MessageBox.Show("Item & Customer is not in database");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
        private void ClearControls()
        {
            txtCname.Text = "";
            txtIname.Text = "";
            txtQuantity.Text = "";
            txtSearch.Text = "";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrder();                         
            
        }
               
        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowAllOrder();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOrder();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteOrder();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateOrder();
            ClearControls();
        }

        

    }
}
