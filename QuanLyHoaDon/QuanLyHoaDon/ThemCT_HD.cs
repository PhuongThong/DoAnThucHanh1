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
    public partial class ThemCT_HD : Form
    {
        public ThemCT_HD()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source=DELL-PC;Initial Catalog=QLBH;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        void Fill()  
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
            command = connection.CreateCommand();
            command.CommandText = "select* from ct_hoadon where MaHD = @mahd";
            command.Parameters.Add("@mahd", frm_QuanLy.mahd);
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Chưa chọn hóa đơn", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ThemCT_HD_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            Fill();
        }

        private string masp = null;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            masp = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
        }

        private void Them_Click(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            { 

                int soluong = Int16.Parse(soluongtext.Text);
                if(soluong > SoluongtonSP())
                {
                    MessageBox.Show("Không đủ số lượng", "Thêm sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                command = connection.CreateCommand();
                command.CommandText = "insert into ct_hoadon(MaHD, MaSP, SoLuong,GiaBan,GiaGiam,ThanhTien) values(@mahd,'" + masptext.Text + "',@soluong,0,0,0)";               
                command.Parameters.Add("@mahd", frm_QuanLy.mahd);
                command.Parameters.Add("@soluong", soluong);

                adapter.SelectCommand = command;
                command.Connection = connection;
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("Thêm thành công");
                    ThemGiaBan();
                    UpdateHoaDon();
                }
                else
                {
                MessageBox.Show("Thêm thất bại");
                }
            }
            catch
            {     
                MessageBox.Show("Thêm thất bại", "them", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
            Fill();
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            
           
            if(masp != null)
            {

                 if ((MessageBox.Show("Bạn Muốn Xóa sản phẩm", "Xóa sản phẩm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
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
                         Xoasanpham();
                         UpdateHoaDon();

                        MessageBox.Show("Đã Xóa sản phẩm", "Xóa sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Information);

                     }
                     catch
                     {
                         MessageBox.Show("Xóa Thất Bại", "Xóa sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }

                 }
                 else
                 {
                     MessageBox.Show("Xóa Thất Bại", "Xóa sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);

                 }
                 Fill();
            }
            else
            {
                MessageBox.Show("Chưa chọn sản phẩm", "Xóa sản phẩm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
        private void Xoasanpham()
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
            command.CommandText = "delete from ct_hoadon where MaHD = @mahd and MaSP = '" + masp + "'";
            command.Parameters.Add("@mahd", frm_QuanLy.mahd);
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
        }
        private int SoluongtonSP()
        {
            DataTable tableSP = new DataTable();
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command = connection.CreateCommand();
            command.CommandText = "select SoLuongTon from sanpham where MaSP = '"+masptext.Text+"'";
            adapter.SelectCommand = command;
            tableSP.Clear();
            adapter.Fill(tableSP);
            return Convert.ToInt16(tableSP.Rows[0]["SoLuongTon"]);
        }
        private void ThemGiaBan()
        {
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int giaban = TimGiaBan();
            int giagiam = giaban * 5 / 100;
            int thanhtien = giaban - giagiam;

            command = connection.CreateCommand();
            command.CommandText = "update ct_hoadon set GiaBan = @giaban, GiaGiam = @giagiam, ThanhTien = @thanhtien where MaHD = @mahd and MaSP = @masp";
            command.Parameters.Add("@giaban",giaban);
            command.Parameters.Add("@giagiam",giagiam);
            command.Parameters.Add("@thanhtien",thanhtien);
            command.Parameters.Add("@masp",masptext.Text);
            command.Parameters.Add("@mahd", frm_QuanLy.mahd);
            
            adapter.SelectCommand = command;
            command.ExecuteNonQuery();
        }
        private int TimGiaBan()
        {
            DataTable tableSP = new DataTable();
            if (connection == null)
            {
                connection = new SqlConnection(str);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            command = connection.CreateCommand();
            command.CommandText = "select Gia from sanpham where MaSP = '" + masptext.Text + "'";
            adapter.SelectCommand = command;
            tableSP.Clear();
            adapter.Fill(tableSP);
            return Convert.ToInt32(tableSP.Rows[0]["Gia"]);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            masp = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
        }
        private void UpdateHoaDon()
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
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "UpdateHoaDon";
            command.Parameters.Add(new SqlParameter("@MaHD", frm_QuanLy.mahd));
            command.ExecuteNonQuery();
        }
    }
}
