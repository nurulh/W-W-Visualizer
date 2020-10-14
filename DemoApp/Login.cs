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
using AesEncDec;

namespace ControlDemoApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register frm = new Register();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ong\\Desktop\\fyp\\DemoApp\\User.mdf;Integrated Security=True");
            con.Open();
            string username = "";
            string password = "";
            string decpass = "";
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select username,password from Detail where username = '" + textBox1.Text + "'";
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.Read())
                {
                    username = sdr["username"].ToString();
                    password = sdr["password"].ToString();

                    decpass = Encryption.Decrypt(password);

                    if (textBox2.Text == decpass)
                    {
                        MainForm frm = new MainForm();
                        frm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Password or Username is not correct.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}

