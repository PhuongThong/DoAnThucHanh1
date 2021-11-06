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

namespace QuanLyHoaDon
{
    public partial class DoanhThu : Form
    {
        public DoanhThu()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DELL-PC;Initial Catalog=QLBH;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void LoadData()
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command = connection.CreateCommand();
            command.CommandText = "select* from hoadon";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        void XemDoanhThu()
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command = connection.CreateCommand();
            command.CommandText = "select* from hoadon where NgayLap >= @StartDate and NgayLap <= @EndDate order by NgayLap";
            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = dateTimePicker1.Value.Date;
            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = dateTimePicker2.Value.Date;
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            //tinh tong doanh thu
            long tongtien = 0;
            for (int i =0; i< table.Rows.Count; i++)
            {
                long giatri = Convert.ToInt64(table.Rows[i]["TongTien"]);
                tongtien += giatri;
            }
            tongdt.Text = tongtien.ToString();
        }
        private void Xem_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            XemDoanhThu();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DoanhThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
