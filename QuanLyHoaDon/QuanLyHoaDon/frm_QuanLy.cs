using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace QuanLyHoaDon
{
    public partial class frm_QuanLy : Form
    {
        public frm_QuanLy()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DELL-PC;Initial Catalog=QLBH;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public static string mahd;
        void LoadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "select* from hoadon";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);    
            dataGridView1.DataSource = table;
        }

        private void frm_QuanLy_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            LoadData();
        }
        void TimKiem()
        {
            command = connection.CreateCommand();
            command.CommandText = "select* from hoadon where MaHD like '%"+textBox1.Text+ "%' or MaKH like '%" + textBox1.Text + "%' ";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        void fill()
        {
            command = connection.CreateCommand();
            command.CommandText = "select* from hoadon";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            TimKiem();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoanhThu dt = new DoanhThu();
            dt.ShowDialog();
        }

        private void them_Click(object sender, EventArgs e)
        {
            ThemHoaDon thd = new ThemHoaDon();
            thd.ShowDialog();
        }
        
        private void xoa_Click(object sender, EventArgs e)
        {
            
            if ((MessageBox.Show("Bạn Muốn Xóa Thông Tin Hóa Đơn", "Xóa Thông Tin Hóa Đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {


                try
                {
                    if (connection == null)
                    {
                        connection = new SqlConnection(str);
                    }
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    Xoact_hoadon();
                    Xoahoadon();
                    MessageBox.Show("Đã Xóa Thông Tin Hóa Đơn", "Xóa Thông Tin Hóa Đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch
                {
                    MessageBox.Show("Xóa Thất Bại", "Xóa Thông Tin Hóa Đơn ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Xóa Thất Bại", "Xóa Thông Tin Hóa Đơn ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            fill();
        }
        void Xoahoadon()
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
            command.CommandText = "delete from hoadon where MaHD = @mahd";
            command.Parameters.Add("@mahd", mahd);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
        }
        void Xoact_hoadon()
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
            command.CommandText = "delete from ct_hoadon where MaHD = @mahd";
            command.Parameters.Add("@mahd", mahd);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            mahd = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThemCT_HD cthd = new ThemCT_HD();
            cthd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            fill();
        }

        private void frm_QuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(connection != null)
            {
                connection.Close();
            }
        }
    }
}
