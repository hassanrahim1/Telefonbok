using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace telefonbok
{
    public partial class Form1 : Form
    {
        private OleDbConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elev\\Documents\\telefonbok.accdb";
            conn.Open();
            MessageBox.Show("Connected");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbCommand comm = new OleDbCommand())
            {
                comm.CommandText = "insert into Tabell1 (Namn,Email,Nummer,Klass) values(@Namn, @Email, @Nummer, @Klass)";

                if (conn == null)
                {
                    conn = new OleDbConnection();
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elev\\Documents\\telefonbok.accdb";
                    conn.Open();
                }

                comm.Connection = conn;
                comm.Parameters.AddWithValue("@Namn", textBox1.Text);
                comm.Parameters.AddWithValue("@Email", textBox2.Text);
                comm.Parameters.AddWithValue("@Nummer", textBox3.Text);
                comm.Parameters.AddWithValue("@Klass", textBox4.Text);

                comm.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                MessageBox.Show("Data Inserted");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection deleteConn = new OleDbConnection())
            {
                deleteConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elev\\Documents\\telefonbok.accdb";

                try
                {
                    deleteConn.Open();
                    OleDbCommand deleteCmd = new OleDbCommand();
                    deleteCmd.Connection = deleteConn;
                    string deleteQuery = "delete from Tabell1 where Namn=@Namn";
                    deleteCmd.CommandText = deleteQuery;
                    deleteCmd.Parameters.AddWithValue("@Namn", textBox1.Text);
                    MessageBox.Show(deleteQuery);
                    deleteCmd.ExecuteNonQuery();
                    MessageBox.Show("Data Deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    deleteConn.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection loadConn = new OleDbConnection())
            {
                loadConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Elev\\Documents\\telefonbok.accdb";

                try
                {
                    loadConn.Open();
                    OleDbCommand loadCmd = new OleDbCommand();
                    loadCmd.Connection = loadConn;
                    string loadQuery = "SELECT * FROM Tabell1";
                    loadCmd.CommandText = loadQuery;
                    OleDbDataAdapter da = new OleDbDataAdapter(loadCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Data Loaded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    loadConn.Close();
                }
            }
        }
    }
}
