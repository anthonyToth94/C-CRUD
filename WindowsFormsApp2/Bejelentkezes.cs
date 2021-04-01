using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Bejelentkezes : Form
    {
        string felhasznalo = "";
        string jelszo = "";
        //adatbázis majd a query
        string kapcsString = "";
        SqlConnection conn;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable adattabla;

        public Bejelentkezes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                felhasznalo = textBox1.Text;
                jelszo = textBox2.Text;
                kapcsolodas();
                string query = "SELECT Nev, Jelszo FROM Felhasznalo WHERE Nev = '" + felhasznalo + "' AND Jelszo = '" + jelszo + "'";
                command = new SqlCommand(query, conn);
                adapter = new SqlDataAdapter(command);
                int row = adapter.Fill(adattabla = new DataTable());

                if (row == 1)
                {
                    this.Hide();
                    Form1 f = new Form1();
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Nincs ilyen felhasználó!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                kapcsolatLezaras();
            }
           
        }
        public void kapcsolodas()
        {
            try
            {
                kapcsString = @"Data Source=DESKTOP-5KK82CO;Initial Catalog=Valami;Integrated Security=True";
                conn = new SqlConnection(kapcsString);
                kapcsolatEllenorzes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void kapcsolatEllenorzes()
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public void kapcsolatLezaras()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
