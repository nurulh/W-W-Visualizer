using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Gma.CodeCloud.Controls.Geometry;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist.En;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;
using Gma.CodeCloud.Controls.TextAnalyses.Stemmers;
using System.Data;
using System.Data.SQLite;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ControlDemoApp
{
    public partial class MainForm
    {
        public int a;
        private const string s_BlacklistTxtFileName = "blacklist.txt";
        public iTextSharp.text.Image Chart_Image;
        public iTextSharp.text.Image Chart_Image1;
        public iTextSharp.text.Image Chart_Image2;
        public Bitmap bmp;
        public static double passing_text1;
        public static double passing_text2;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }        

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New objCF = new New();
            
            objCF.myRefresh += new New.Refresh(RefreshControl);
            objCF.Owner = this;
            objCF.ShowDialog();
        }

        private void RefreshControl()
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                a = 1;
                button1.Text = "wa.db";
                button1.Visible = true;
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select name From sqlite_master where type='table' order by name;";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView1.DataSource = dt;
                }

                SQLiteCommand cmd1 = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select * from android_metadata";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView2.DataSource = dt;
                }

                string cmd2 = "Select display_name, jid from wa_contacts where jid like '%-%'";
                SQLiteDataAdapter sdr2 = new SQLiteDataAdapter(cmd2,myconnection);
                DataTable ds2 = new DataTable();
                sdr2.Fill(ds2);
                foreach (DataRow dr in ds2.Rows)
                {
                    string jid = dr[0].ToString();
                    string jid1 = dr[1].ToString();                     
                    if (string.IsNullOrEmpty(jid))
                    dr[0] = dr[1].ToString();
                    string sName = dr[0].ToString();
                    comboBox1.Items.Add(sName);
                    comboBox1.Visible = true;
                }
                comboBox1.Visible = true;
                comboBox2.Visible = true;
            }
            if (!string.IsNullOrEmpty(New.passingText2))
            {
                button2.Text = "msgstore.db";
                button2.Visible = true;
            }

            if (!string.IsNullOrEmpty(New.passingText9))
            {
                a = 1;
                button1.Text = "naver_line.db";
                button1.Visible = true;
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select name From sqlite_master where type='table' order by name;";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView1.DataSource = dt;
                }

                SQLiteCommand cmd1 = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select * from android_metadata";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView2.DataSource = dt;
                }

                string cmd2 = "Select name from groups";
                SQLiteDataAdapter sdr2 = new SQLiteDataAdapter(cmd2, myconnection);
                DataTable ds2 = new DataTable();
                sdr2.Fill(ds2);
                foreach (DataRow dr in ds2.Rows)
                {                    
                    string sName = dr[0].ToString();
                    comboBox1.Items.Add(sName);                    
                }
                comboBox1.Visible = true;
                comboBox3.Visible = true;
            }
            Statistics();
            label15.Visible = true;
            label16.Visible = true;
            label23.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (a == 1)
            {
                if (!string.IsNullOrEmpty(New.passingText1))
                {
                    dataGridView1.CurrentRow.Selected = true;
                    string g = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                    SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3");
                    myconnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = myconnection;
                    cmd.CommandText = "Select * from " + g + "";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }
                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }
                else if (!string.IsNullOrEmpty(New.passingText9))
                {
                    dataGridView1.CurrentRow.Selected = true;
                    string g = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                    SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3");
                    myconnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = myconnection;
                    cmd.CommandText = "Select * from " + g + "";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }
                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }


            }
            else if (a == 2)
            {
                dataGridView1.CurrentRow.Selected = true;
                string g = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select * from " + g + "";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView2.DataSource = dt;
                }
                if (dataGridView2.Columns[0].HeaderText == "Show")
                    dataGridView2.Columns[0].Visible = false;
            }            
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                a = 1;
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select name From sqlite_master where type='table' order by name;";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView1.DataSource = dt;
                }
            }
            else if (!string.IsNullOrEmpty(New.passingText9))
            {
                a = 1;
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = myconnection;
                cmd.CommandText = "Select name From sqlite_master where type='table' order by name;";
                using (SQLiteDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    sdr.Close();
                    dataGridView1.DataSource = dt;
                }
            }
        }
        
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            a = 2;
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3");
            myconnection.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = myconnection;
            cmd.CommandText = "Select name From sqlite_master where type='table' order by name;";
            using (SQLiteDataReader sdr = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(sdr);
                sdr.Close();
                dataGridView1.DataSource = dt;
            }
        }

        public void Statistics()
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                myconnection.Open();
                SQLiteConnection myconnection1 = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                myconnection1.Open();
                SQLiteCommand cmd = new SQLiteCommand("Select count (*) from messages", myconnection1);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                label10.Text = count.ToString();
                SQLiteCommand cmd1 = new SQLiteCommand("Select count (*) from wa_contacts", myconnection);
                Int32 count1 = Convert.ToInt32(cmd1.ExecuteScalar());
                label11.Text = count1.ToString();
                SQLiteCommand cmd2 = new SQLiteCommand("Select count (*) from messages where key_from_me = 0", myconnection1);
                Int32 count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                label12.Text = count2.ToString();
                SQLiteCommand cmd3 = new SQLiteCommand("Select count (*) from messages where key_from_me = 1", myconnection1);
                Int32 count3 = Convert.ToInt32(cmd3.ExecuteScalar());
                label13.Text = count3.ToString();
                SQLiteCommand cmd4 = new SQLiteCommand("Select count (*) from messages where media_wa_type = 1", myconnection1);
                Int32 count4 = Convert.ToInt32(cmd4.ExecuteScalar());
                label18.Text = count4.ToString();
                SQLiteCommand cmd5 = new SQLiteCommand("Select count (*) from messages where media_wa_type = 3", myconnection1);
                Int32 count5 = Convert.ToInt32(cmd5.ExecuteScalar());
                label20.Text = count5.ToString();
                SQLiteCommand cmd6 = new SQLiteCommand("Select count (*) from messages where media_wa_type = 2", myconnection1);
                Int32 count6 = Convert.ToInt32(cmd6.ExecuteScalar());
                label22.Text = count6.ToString();
            }
            else if (!string.IsNullOrEmpty(New.passingText9))
            {
                SQLiteConnection myconnection = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                myconnection.Open();
                SQLiteCommand cmd = new SQLiteCommand("Select count (*) from chat_history", myconnection);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                label10.Text = count.ToString();
                SQLiteCommand cmd1 = new SQLiteCommand("Select count (*) from contacts", myconnection);
                Int32 count1 = Convert.ToInt32(cmd1.ExecuteScalar());
                label11.Text = count1.ToString();
                SQLiteCommand cmd2 = new SQLiteCommand("Select count (*) from chat_history where from_mid is not null", myconnection);
                Int32 count2 = Convert.ToInt32(cmd2.ExecuteScalar());
                label12.Text = count2.ToString();
                SQLiteCommand cmd3 = new SQLiteCommand("Select count (*) from chat_history where from_mid is null", myconnection);
                Int32 count3 = Convert.ToInt32(cmd3.ExecuteScalar());
                label13.Text = count3.ToString();
                label17.Visible = false;
                label19.Visible = false;
                label21.Visible = false;
            }
        }
        
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3; new=False;");
                SQLiteConnection sqlite_conn1 = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                sqlite_conn.Open();
                sqlite_conn1.Open();

                StringBuilder builder = new StringBuilder();

                string sqlite_cmd = "SELECT remote_resource,key_remote_jid as jid, pm, gc, (pm + gc) as 'total' FROM (SELECT remote_resource, COUNT(remote_resource) AS 'pm' FROM messages WHERE remote_resource IS NOT NULL GROUP BY remote_resource) t1 JOIN (SELECT key_remote_jid, COUNT(key_remote_jid) AS 'gc' FROM messages WHERE key_remote_jid NOT LIKE '%-%' GROUP BY key_remote_jid) t2 where t1.remote_resource = t2.key_remote_jid ORDER BY total DESC LIMIT 5";
                SQLiteDataAdapter myCommand = new SQLiteDataAdapter(sqlite_cmd, sqlite_conn1);
                DataTable ds = new DataTable();
                myCommand.Fill(ds);

                string sqlite_cmd1 = "SELECT wa_name, jid from wa_contacts";
                SQLiteDataAdapter myCommand1 = new SQLiteDataAdapter(sqlite_cmd1, sqlite_conn);
                DataTable ds1 = new DataTable();
                myCommand1.Fill(ds1);
                ds.Columns.Add("wa_name", typeof(string));
                foreach (DataRow dr in ds.Rows)
                {
                    string jid = dr[1].ToString();
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        string jid1 = dr1[1].ToString();
                        if (jid1 == jid)
                            dr["wa_name"] = dr1[0].ToString();
                    }
                }

                chart1.DataSource = ds;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[0].XValueMember = "wa_name";
                chart1.Series[0].YValueMembers = "total";
                chart1.Titles.Clear();
                chart1.Titles.Add("Top 5 Most Frequent User");
                chart1.DataBind();
                chart1.Series[0].IsValueShownAsLabel = true;
                if (chart1.Visible == false)
                {
                    chart1.Visible = true;
                    chart1.BringToFront();
                }
                var chartimage = new MemoryStream();
                chart1.SaveImage(chartimage, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                Chart_Image = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
            }
            else if (!string.IsNullOrEmpty(New.passingText9))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3; new=False;");
                sqlite_conn.Open();
                string sqlite_cmd = "SELECT from_mid, count(from_mid) from chat_history group by from_mid order by count(from_mid) DESC LIMIT 5 ";
                SQLiteDataAdapter myCommand = new SQLiteDataAdapter(sqlite_cmd, sqlite_conn);
                DataTable ds = new DataTable();
                myCommand.Fill(ds);

                string sqlite_cmd1 = "SELECT m_id, name from contacts";
                SQLiteDataAdapter myCommand1 = new SQLiteDataAdapter(sqlite_cmd1, sqlite_conn);
                DataTable ds1 = new DataTable();
                myCommand1.Fill(ds1);
                ds.Columns.Add("name", typeof(string));
                foreach (DataRow dr in ds.Rows)
                {
                    string id = dr[0].ToString();
                    foreach (DataRow dr1 in ds1.Rows)
                    {
                        string id1 = dr1[0].ToString();
                        if (id1 == id)
                            dr["name"] = dr1[1].ToString();
                    }
                }

                chart1.DataSource = ds;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[0].XValueMember = "name";
                chart1.Series[0].YValueMembers = "count(from_mid)";
                chart1.Titles.Clear();
                chart1.Titles.Add("Top 5 Most Frequent User");
                chart1.DataBind();
                chart1.Series[0].IsValueShownAsLabel = true;
                if (chart1.Visible == false)
                {
                    chart1.Visible = true;
                    chart1.BringToFront();
                }
                var chartimage = new MemoryStream();
                chart1.SaveImage(chartimage, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                Chart_Image = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3; new=False;");
                sqlite_conn.Open();
                SQLiteConnection sqlite_conn1 = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                sqlite_conn1.Open();
                SQLiteCommand sqlite_cmd1 = new SQLiteCommand("Select jid from wa_contacts where display_name = '" + comboBox1.SelectedItem.ToString() + "' or jid = '" + comboBox1.SelectedItem.ToString() + "'", sqlite_conn1);
                DataTable ds = new DataTable();
                using (SQLiteDataReader sdr = sqlite_cmd1.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        string groupid = sdr[0].ToString();
                        string sqlite_cmd = "Select remote_resource, count(remote_resource) from messages where key_remote_jid = '" + groupid + "' and remote_resource IS NOT NULL GROUP BY remote_resource ORDER BY count(*)";
                        SQLiteDataAdapter myCommand = new SQLiteDataAdapter(sqlite_cmd, sqlite_conn);
                        myCommand.Fill(ds);
                    }
                }

                string sqlite_cmd2 = "SELECT wa_name, jid from wa_contacts";
                SQLiteDataAdapter myCommand2 = new SQLiteDataAdapter(sqlite_cmd2, sqlite_conn1);
                DataTable ds2 = new DataTable();
                myCommand2.Fill(ds2);
                ds.Columns.Add("wa_name", typeof(string));
                foreach (DataRow dr in ds.Rows)
                {
                    string jid = dr[0].ToString();
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        string jid1 = dr1[1].ToString();
                        string name = dr1[0].ToString();
                        if (jid1 == jid)
                        {
                            if (string.IsNullOrEmpty(name))
                                dr["wa_name"] = dr1[1].ToString();
                            else
                                dr["wa_name"] = dr1[0].ToString();
                        }
                    }
                }

                DataTable source = ds;
                chart1.DataSource = source;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0].XValueMember = "wa_name";
                chart1.Series[0].YValueMembers = "count(remote_resource)";
                chart1.DataBind();
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Titles.Clear();
                chart1.Titles.Add("Frequency of User in Group Chat");
                chart1.Visible = true;
                cloudControl.Visible = false;
                var chartimage = new MemoryStream();
                chart1.SaveImage(chartimage, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                Chart_Image1 = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
            }
            else if (!string.IsNullOrEmpty(New.passingText9))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3; new=False;");
                sqlite_conn.Open();
                SQLiteCommand sqlite_cmd1 = new SQLiteCommand("Select id from groups where name = '" + comboBox1.SelectedItem.ToString() + "'", sqlite_conn);
                DataTable ds = new DataTable();
                using (SQLiteDataReader sdr = sqlite_cmd1.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        string groupid = sdr[0].ToString();
                        string sqlite_cmd = "Select from_mid, count(from_mid) from chat_history where chat_id = '" + groupid + "' GROUP BY from_mid ORDER BY count(*)";
                        SQLiteDataAdapter myCommand = new SQLiteDataAdapter(sqlite_cmd, sqlite_conn);
                        myCommand.Fill(ds);
                    }
                }

                string sqlite_cmd2 = "SELECT m_id, name from contacts";
                SQLiteDataAdapter myCommand2 = new SQLiteDataAdapter(sqlite_cmd2, sqlite_conn);
                DataTable ds2 = new DataTable();
                myCommand2.Fill(ds2);
                ds.Columns.Add("name", typeof(string));
                foreach (DataRow dr in ds.Rows)
                {
                    string id = dr[0].ToString();
                    foreach (DataRow dr1 in ds2.Rows)
                    {
                        string id1 = dr1[0].ToString();
                        if (id1 == id)
                        {
                            dr["name"] = dr1[1].ToString();
                        }
                    }
                }

                DataTable source = ds;
                chart1.DataSource = source;
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0].XValueMember = "name";
                chart1.Series[0].YValueMembers = "count(from_mid)";
                chart1.DataBind();
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Titles.Clear();
                chart1.Titles.Add("Frequency of User in Group Chat");
                chart1.Visible = true;
                cloudControl.Visible = false;
                var chartimage = new MemoryStream();
                chart1.SaveImage(chartimage, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                Chart_Image1 = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProcessText();
            cloudControl.Visible = true;
            chart1.Visible = false;
        }

        private void ProcessText()
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText2 + "; Version=3; new=False;");
                sqlite_conn.Open();

                StringBuilder builder = new StringBuilder();

                SQLiteCommand sqlite_cmd = new SQLiteCommand("Select data from messages where key_remote_jid like '60165489131%'", sqlite_conn);
                using (SQLiteDataReader sdr = sqlite_cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        builder.Append(sdr[0]);
                        builder.Append(" ");
                    }
                    textBox.Text = builder.ToString();
                }

                IBlacklist customBlacklist = CommonBlacklist.CreateFromTextFile(s_BlacklistTxtFileName);

                InputType inputType = ComponentFactory.DetectInputType(textBox.Text);
                IProgressIndicator progress = ComponentFactory.CreateProgressBar(inputType, progressBar);
                IEnumerable<string> terms = ComponentFactory.CreateExtractor(inputType, textBox.Text, progress);

                IEnumerable<IWord> words = terms
                    .Filter(customBlacklist)
                    .CountOccurences();

                cloudControl.WeightedWords =
                    words
                        .SortByOccurences()
                        .Cast<IWord>();


                bmp = new Bitmap(cloudControl.Width, cloudControl.Height);

                cloudControl.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, cloudControl.Width, cloudControl.Height));
            }
            else if(!string.IsNullOrEmpty(New.passingText9))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText9 + "; Version=3; new=False;");
                sqlite_conn.Open();

                StringBuilder builder = new StringBuilder();

                SQLiteCommand sqlite_cmd = new SQLiteCommand("Select content from chat_history", sqlite_conn);
                using (SQLiteDataReader sdr = sqlite_cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        builder.Append(sdr[0]);
                        builder.Append(" ");
                    }
                    textBox.Text = builder.ToString();
                }

                IBlacklist customBlacklist = CommonBlacklist.CreateFromTextFile(s_BlacklistTxtFileName);

                InputType inputType = ComponentFactory.DetectInputType(textBox.Text);
                IProgressIndicator progress = ComponentFactory.CreateProgressBar(inputType, progressBar);
                IEnumerable<string> terms = ComponentFactory.CreateExtractor(inputType, textBox.Text, progress);

                IEnumerable<IWord> words = terms
                    .Filter(customBlacklist)
                    .CountOccurences();

                cloudControl.WeightedWords =
                    words
                        .SortByOccurences()
                        .Cast<IWord>();


                bmp = new Bitmap(cloudControl.Width, cloudControl.Height);

                cloudControl.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, cloudControl.Width, cloudControl.Height));
            }
            
        }

        private void CloudControlClick(object sender, EventArgs e)
        {
            LayoutItem itemUderMouse;
            Point mousePositionRelativeToControl = cloudControl.PointToClient(new Point(MousePosition.X, MousePosition.Y));
            if (!cloudControl.TryGetItemAtLocation(mousePositionRelativeToControl, out itemUderMouse))
            {
                return;
            }

            MessageBox.Show(
                itemUderMouse.Word.GetCaption(),
                string.Format("Statistics for word [{0}]", itemUderMouse.Word.Text));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(New.passingText1))
            {
                SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=" + New.passingText2 + ";Version=3; new=False;");
                sqlite_conn.Open();
                SQLiteConnection sqlite_conn1 = new SQLiteConnection("Data Source=" + New.passingText1 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                sqlite_conn1.Open();

                if (comboBox2.SelectedItem.ToString() == "Keyword")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn;
                    cmd.CommandText = "Select * from messages where data like '%" + textBox2.Text + "' or data like '%" + textBox2.Text + "%' or data like '" + textBox2.Text + "%'";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }
                else if (comboBox2.SelectedItem.ToString() == "Phone Number")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn;
                    cmd.CommandText = "Select * from messages where key_remote_jid like '%" + textBox2.Text + "%' or key_remote_jid like '" + textBox2.Text + "%' or key_remote_jid like '%" + textBox2.Text + "' or remote_resource like '%" + textBox2.Text + "%' or remote_resource like '" + textBox2.Text + "%' or remote_resource like '%" + textBox2.Text + "'";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }
                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }
                else if (comboBox2.SelectedItem.ToString() == "Date")
                {
                    DateTime foo = DateTime.Parse(dateTimePicker1.Value.ToString("yyyy,MM,dd"));
                    long unixTime = ((DateTimeOffset)foo).ToUnixTimeMilliseconds();
                    long unixTime1 = ((DateTimeOffset)foo).ToUnixTimeMilliseconds() + 86399000;


                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn;
                    cmd.CommandText = "Select * from messages where timestamp between '" + unixTime + "' and '" + unixTime1 + "'";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }

                else if (comboBox2.SelectedItem.ToString() == "Location Message")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn;
                    cmd.CommandText = "Select _id, key_remote_jid, key_from_me, key_id, status, timestamp, media_url, media_name, remote_resource, received_timestamp, latitude, longitude from messages where media_wa_type = 5";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    var ShowButton = new DataGridViewButtonColumn();
                    ShowButton.Name = "dataGridViewShowButton";
                    ShowButton.HeaderText = "Show";
                    ShowButton.Text = "Show";
                    ShowButton.UseColumnTextForButtonValue = true;
                    if (dataGridView2.Columns[0].HeaderText != "Show")
                        dataGridView2.Columns.Insert(0, ShowButton);
                    else
                        dataGridView2.Columns[0].Visible = true;
                }
            }
            else if (!string.IsNullOrEmpty(New.passingText9))
            {
                SQLiteConnection sqlite_conn2 = new SQLiteConnection("Data Source=" + New.passingText9 + ";Version=3; new=False;datetimeformat = CurrentCulture");
                sqlite_conn2.Open();

                if (comboBox3.SelectedItem.ToString() == "Keyword")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn2;
                    cmd.CommandText = "Select * from chat_history where content like '%" + textBox2.Text + "' or content like '%" + textBox2.Text + "%' or content like '" + textBox2.Text + "%'";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }
                else if (comboBox3.SelectedItem.ToString() == "Name")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn2;
                    cmd.CommandText = "select * from (select * from chat_history) t1 join (select m_id from contacts where name like '%" + textBox2.Text + "' or name like '%" + textBox2.Text + "%' or name like '" + textBox2.Text + "%')t2 where t1.from_mid = t2.m_id";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }
                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }
                else if (comboBox3.SelectedItem.ToString() == "Date")
                {
                    DateTime foo = DateTime.Parse(dateTimePicker1.Value.ToString("yyyy,MM,dd"));
                    long unixTime = ((DateTimeOffset)foo).ToUnixTimeMilliseconds();
                    long unixTime1 = ((DateTimeOffset)foo).ToUnixTimeMilliseconds() + 86399000;


                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn2;
                    cmd.CommandText = "Select * from chat_history where created_time between '" + unixTime + "' and '" + unixTime1 + "'";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    if (dataGridView2.Columns[0].HeaderText == "Show")
                        dataGridView2.Columns[0].Visible = false;
                }

                else if (comboBox3.SelectedItem.ToString() == "Location Message")
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqlite_conn2;
                    cmd.CommandText = "Select * from chat_history where location_name is not null";
                    using (SQLiteDataReader sdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(sdr);
                        sdr.Close();
                        dataGridView2.DataSource = dt;
                    }

                    var ShowButton = new DataGridViewButtonColumn();
                    ShowButton.Name = "dataGridViewShowButton";
                    ShowButton.HeaderText = "Show";
                    ShowButton.Text = "Show";
                    ShowButton.UseColumnTextForButtonValue = true;
                    if (dataGridView2.Columns[0].HeaderText != "Show")
                        dataGridView2.Columns.Insert(0, ShowButton);
                    else
                        dataGridView2.Columns[0].Visible = true;
                }
            }            
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString()=="Date")
            {
                dateTimePicker1.Visible = true;
                textBox2.Visible = false;
            }
            else if (comboBox2.SelectedItem.ToString() == "Keyword" || comboBox2.SelectedItem.ToString() == "Phone Number")
            {
                dateTimePicker1.Visible = false;
                textBox2.Visible = true;
            }
            else if (comboBox2.SelectedItem.ToString() == "Location Message")
            {
                dateTimePicker1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Value.ToString("yyyy,MM,dd");
        }

        private void exportAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4, 20, 20, 42, 35);
            PdfWriter w = PdfWriter.GetInstance(doc, new FileStream (" "+New.passingText8+"\\Report.pdf",FileMode.Create));
            doc.Open();
            Paragraph p1 = new Paragraph("Examiner Details");
            Paragraph p2 = new Paragraph("Name:" + New.passingText3);
            Paragraph p3 = new Paragraph("Phone:" + New.passingText4);
            Paragraph p4 = new Paragraph("Email: " + New.passingText5);
            Paragraph p5 = new Paragraph("Notes: " + New.passingText6);
            iTextSharp.text.Image i = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Bmp);
            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 32);
            Paragraph title;
            title = new Paragraph("Report of Case " +New.passingText7+"", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            doc.AddAuthor("a");
            doc.AddCreator("Visual Studio");
            doc.AddSubject("PDF file");
            doc.Add(title);
            doc.Add(p1);
            doc.Add(p2);
            doc.Add(p3);
            doc.Add(p4);
            doc.Add(p5);
            doc.Add(Chart_Image);
            doc.Add(Chart_Image1);
            doc.Add(i);
            doc.Close();
            MessageBox.Show("Export Successfully.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && !string.IsNullOrEmpty(New.passingText1))
            {
                passing_text1 = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[11].FormattedValue.ToString());
                passing_text2 = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[12].FormattedValue.ToString());
                Map objCF = new Map();
                objCF.Owner = this;
                objCF.ShowDialog();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && !string.IsNullOrEmpty(New.passingText9))
            {
                passing_text1 = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[16].FormattedValue.ToString());
                passing_text2 = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[17].FormattedValue.ToString());
                Map objCF = new Map();
                objCF.Owner = this;
                objCF.ShowDialog();
            }
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Date")
            {
                dateTimePicker1.Visible = true;
                textBox2.Visible = false;
            }
            else if (comboBox3.SelectedItem.ToString() == "Keyword" || comboBox3.SelectedItem.ToString() == "Name")
            {
                dateTimePicker1.Visible = false;
                textBox2.Visible = true;
            }
            else if (comboBox3.SelectedItem.ToString() == "Location Message")
            {
                dateTimePicker1.Visible = false;
                textBox2.Visible = false;
            }
        }


    }
}