using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace CobaDBMS
{
    public partial class Form1 : Form
    {
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=db;SslMode=none;";
        int currentId = 0;
        int currentRow = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void showBtn_1061_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM mahasiswa";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 600;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                // Success, now list 

                // If there are available rows
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Example to save in the listView1 :
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3) };
                        var listViewItem = new ListViewItem(row);
                        listview_1061.Items.Add(listViewItem);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveBtn_1061_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO mahasiswa(`First Name`, `Last Name`, `Address`) VALUES ('" + 
                firstNameTB_1061.Text + "', '" + lastNameTB_1061.Text + "', '" + addressBT_1061.Text + "')";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Student successfully added");

                this.showBtn_1061_Click(sender, e);

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_1061_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = listview_1061.SelectedItems[0];

            firstNameTB_1061.Text = lvi.SubItems[1].Text;
            lastNameTB_1061.Text = lvi.SubItems[2].Text;
            addressBT_1061.Text = lvi.SubItems[3].Text;
            currentId = int.Parse(lvi.SubItems[0].Text);
            currentRow = lvi.Index;
        }

        private void updateBtn_1061_Click(object sender, EventArgs e)
        {
            string query = "UPDATE mahasiswa SET `First Name` = '" + firstNameTB_1061.Text + 
                "', `Last Name` = '" + lastNameTB_1061.Text + "', `Address` = '" + addressBT_1061.Text + 
                "' WHERE `Id` = " + currentId + ";";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Student successfully updated");
               
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteBtn_1061_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM mahasiswa WHERE `Id` = " + currentId + ";";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                MessageBox.Show("Student successfully deleted");

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }
    }
}
