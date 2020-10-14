using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ControlDemoApp
{
    public partial class New : Form
    {
        public New()
        {
            InitializeComponent();
        }
        public static string passingText1 = null;
        public static string passingText2 = null;
        public static string passingText3;
        public static string passingText4;
        public static string passingText5;
        public static string passingText6;
        public static string passingText7;
        public static string passingText8;
        public static string passingText9 = null;
        public delegate void Refresh();

        public event Refresh myRefresh;

        private void New_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            BackBtn.Enabled = false;
            NextBtn.Enabled = false;
            FinishBtn.Enabled = false;

            label4.Font = new Font(label4.Font, FontStyle.Bold);

            this.ActiveControl = textBox1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBox2.Text = fbd.SelectedPath;
            textBox3.Text = textBox2.Text + "\\" + textBox1.Text;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
                textBox3.Text = textBox2.Text + "\\" + textBox1.Text;
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl1.SelectedIndex = 1;
                label4.Font = new Font(label4.Font, FontStyle.Regular);
                label5.Font = new Font(label5.Font, FontStyle.Bold);
                BackBtn.Enabled = true;
            }

            else if (tabControl1.SelectedIndex == 1)
            {
                label5.Font = new Font(label5.Font, FontStyle.Regular);
                label18.Font = new Font(label18.Font, FontStyle.Bold);
                if (radioButton1.Checked)
                    tabControl1.SelectedIndex = 2;
                else tabControl1.SelectedIndex = 3;
                NextBtn.Enabled = false;
                FinishBtn.Enabled = false;
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                label4.Font = new Font(label4.Font, FontStyle.Bold);
                label5.Font = new Font(label5.Font, FontStyle.Regular);
                tabControl1.SelectedIndex = 0;
                BackBtn.Enabled = false;
            }

            else if ((tabControl1.SelectedIndex == 2) || (tabControl1.SelectedIndex == 3))
            {
                label5.Font = new Font(label5.Font, FontStyle.Bold);
                label18.Font = new Font(label18.Font, FontStyle.Regular);
                tabControl1.SelectedIndex = 1;
                NextBtn.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "database files, SQLite files | *.db; *.db3; *.db2";
            openFileDialog1.FileOk += delegate (object s, CancelEventArgs ev)
            {
                if (!openFileDialog1.FileName.EndsWith("wa.db"))
                {
                    MessageBox.Show("wa.db shoould be selected.");
                    ev.Cancel = true;
                }
            };
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strdb1 = openFileDialog1.FileName;
                textBox8.Text = strdb1;
                if ((!string.IsNullOrEmpty(textBox8.Text)) && (!string.IsNullOrEmpty(textBox9.Text)))
                {
                    FinishBtn.Enabled = true;
                }
            }
            passingText1 = textBox8.Text;
        }

        protected void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "database files, SQLite files | *.db; *.db3; *.db2";
            openFileDialog1.FileOk += delegate (object s, CancelEventArgs ev)
            {
                if (!openFileDialog1.FileName.EndsWith("msgstore.db"))
                {
                    MessageBox.Show("msgstore.db shoould be selected.");
                    ev.Cancel = true;
                }
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strdb2 = openFileDialog1.FileName;
                textBox9.Text = strdb2;
                if ((!string.IsNullOrEmpty(textBox8.Text)) && (!string.IsNullOrEmpty(textBox9.Text)))
                {
                    FinishBtn.Enabled = true;
                }
            }
            passingText2 = textBox9.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "database files, SQLite files | *.db; *.db3; *.db2";
            openFileDialog1.FileOk += delegate (object s, CancelEventArgs ev)
            {
                if (!openFileDialog1.FileName.EndsWith("naver_line.db"))
                {
                    MessageBox.Show("naver_line.db shoould be selected.");
                    ev.Cancel = true;
                }
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strdb3 = openFileDialog1.FileName;
                textBox10.Text = strdb3;
                FinishBtn.Enabled = true;
            }
            passingText9 = textBox10.Text;
            passingText1 = null;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text))
                NextBtn.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text))
                NextBtn.Enabled = true;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FinishBtn_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(textBox3.Text);           
            passingText3 = textBox4.Text;
            passingText4 = textBox5.Text;
            passingText5 = textBox6.Text;
            passingText6 = textBox7.Text;
            passingText7 = textBox1.Text;
            passingText8 = textBox3.Text;            
            this.Close();
            myRefresh();

        }
    }
}
