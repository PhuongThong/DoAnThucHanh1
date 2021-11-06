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
    public partial class ThemHoaDon : Form
    {
        public ThemHoaDon()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DELL-PC;Initial Catalog=QLBH;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();


        
        private void themhd_Click(object sender, EventArgs e)
        {
            if(connection == null)
            {
                connection = new SqlConnection(str);
            }
            if(connection.State == ConnectionState.Closed)
            {
            connection.Open();
            }
            try
            {
                DateTime date = DateTime.Now;
                command = connection.CreateCommand();
                command.CommandText = "insert into hoadon(MaHD, MaKH, NgayLap,TongTien) values('" + mahd.Text + "','" + makh.Text + "',@date,0)";
                command.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;

                adapter.SelectCommand = command;
                command.Connection = connection;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Thêm Thất bại");
                }
            }
            catch
            {
                MessageBox.Show("Thêm thất bại","Thêm hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void ThemHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
