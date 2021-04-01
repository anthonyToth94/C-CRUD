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
    public partial class Form1 : Form
    {
        string felhasznalo = "";
        string jelszo = "";
        //adatbázis majd a query
        string kapcsString = "";
        SqlConnection conn;
        SqlCommand command;
        string neve;

         string nev;
        double ar;
        int darab;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Insert
        private void button1_Click(object sender, EventArgs e)
        {
           
       
            if(textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Töltsd ki a mezőket");
            }
            else
            {
                nev = textBox2.Text;
                ar = Convert.ToDouble(textBox3.Text);
                darab = Convert.ToInt32(textBox4.Text);

                try
                {
                    kapcsolodas();
                    string query = "INSERT INTO Aru(Nev, Ar, Darab) VALUES(@nev, @ar, @darab)";
                    command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@nev", nev);
                    command.Parameters.AddWithValue("@ar", ar);
                    command.Parameters.AddWithValue("@darab", darab);
                    int row = command.ExecuteNonQuery();

                    if (row == 1)
                    {
                        MessageBox.Show("Sikeres feltöltés");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült feltölteni");
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
           
        }
       
        //update
        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Töltse ki a mezőket");
            }
            else
            {
                int id2 = Convert.ToInt32(textBox1.Text);
                string nev2 = textBox2.Text;
                double ar2 = Convert.ToDouble(textBox3.Text);
                int darab2 = Convert.ToInt32(textBox4.Text);

                try
                {
                    kapcsolodas();
                    string query = "UPDATE Aru SET Nev = '" + @nev2 + "', Ar = '" + @ar2 + "', Darab = '" + @darab2 + "' WHERE id = '" + @id2 + "'";
                    command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@nev2", nev2);
                    command.Parameters.AddWithValue("@ar2", ar2);
                    command.Parameters.AddWithValue("@darab2", darab2);
                    int row = command.ExecuteNonQuery();

                    if (row == 1)
                    {
                        MessageBox.Show("Sikeres frissítés");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült frissíteni");
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
        }
        //serach
        private void button1_Click_1(object sender, EventArgs e)
        {
            neve = textBox5.Text;

            try
            {
                
                if(nev == "")
                {
                    MessageBox.Show("Írja be a termék nevét");
                }
                else
                {
                    kapcsolodas();
                    string query = "SELECT * FROM Aru WHERE Nev LIKE '%" + neve + "%'";
                    command = new SqlCommand(query, conn); SqlDataAdapter adapter;
                    adapter = new SqlDataAdapter(command); DataTable adattabla;
                    int row = adapter.Fill(adattabla = new DataTable());

                    if (row == 1)
                    {
                        textBox1.Text = adattabla.Rows[0]["Id"].ToString();
                        textBox2.Text = adattabla.Rows[0]["Nev"].ToString();
                        textBox3.Text = adattabla.Rows[0]["Ar"].ToString();
                        textBox4.Text = adattabla.Rows[0]["Darab"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Nincs ilyen Áru!");
                    }
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
        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Töltse ki a mezőket");
            }
            else
            {
                int id3 = Convert.ToInt32(textBox1.Text);
                string nev3 = textBox2.Text;
                double ar3 = Convert.ToDouble(textBox3.Text);
                int darab3 = Convert.ToInt32(textBox4.Text);

                try
                {
                    kapcsolodas();
                    string query = "DELETE FROM Aru WHERE id = '" + @id3 + "'";
                    command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@nev3", nev3);
                    int row = command.ExecuteNonQuery();

                    if (row == 1)
                    {
                        MessageBox.Show("Törölve");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nem sikerült törölni");
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
            if (conn.State != ConnectionState.Open)
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
