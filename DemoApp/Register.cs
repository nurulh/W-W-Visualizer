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
using System.Configuration;
using System.Text.RegularExpressions;

namespace ControlDemoApp
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ong\\Desktop\\fyp\\DemoApp\\User.mdf;Integrated Security=True");
            con.Open();
            SqlCommand comm = new SqlCommand("select username from detail where username ='" + textBox1.Text + "'");
            comm.Connection = con;
            using (SqlDataReader sdr = comm.ExecuteReader())
            {
                if (sdr.Read())
                {
                    MessageBox.Show("Duplicate Username.Please enter a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sdr.Close();
                    if (textBox2.Text == textBox3.Text)
                    {
                        string encpass = Encryption.Encrypt(textBox2.Text);
                        string ins = "Insert into detail (username,password,email) values ('" + textBox1.Text + "','" + encpass + "','"+textBox4.Text+"')";
                        SqlCommand com = new SqlCommand(ins, con);
                        com.ExecuteNonQuery();
                        MessageBox.Show("Successfully registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.None);
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter the same password.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox2.Text)
            {
                errorProvider1.SetError(this.textBox3, "Password is not identical.");
                textBox3.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string pattern1 = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox4.Text, pattern1) == false)
            {
                textBox4.Focus();
                errorProvider1.SetError(this.textBox4, "Please provide valid email address.");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            if (Regex.IsMatch(textBox2.Text, pattern) == false)
            {
                textBox2.Focus();
                errorProvider1.SetError(this.textBox2, "Uppercase, Lowercase, Numbers, Special Characters");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
