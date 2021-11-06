
namespace QuanLyHoaDon
{
    partial class ThemHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.makh = new System.Windows.Forms.TextBox();
            this.mahd = new System.Windows.Forms.TextBox();
            this.themhd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã hóa đơn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã khách hàng";
            // 
            // makh
            // 
            this.makh.Location = new System.Drawing.Point(124, 51);
            this.makh.Name = "makh";
            this.makh.Size = new System.Drawing.Size(126, 22);
            this.makh.TabIndex = 3;
            // 
            // mahd
            // 
            this.mahd.Location = new System.Drawing.Point(124, 99);
            this.mahd.Name = "mahd";
            this.mahd.Size = new System.Drawing.Size(126, 22);
            this.mahd.TabIndex = 4;
            // 
            // themhd
            // 
            this.themhd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.themhd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themhd.Location = new System.Drawing.Point(275, 56);
            this.themhd.Name = "themhd";
            this.themhd.Size = new System.Drawing.Size(97, 65);
            this.themhd.TabIndex = 5;
            this.themhd.Text = "Thêm hóa đơn";
            this.themhd.UseVisualStyleBackColor = true;
            this.themhd.Click += new System.EventHandler(this.themhd_Click);
            // 
            // ThemHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 184);
            this.Controls.Add(this.themhd);
            this.Controls.Add(this.mahd);
            this.Controls.Add(this.makh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "ThemHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThemHoaDon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThemHoaDon_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox makh;
        private System.Windows.Forms.TextBox mahd;
        private System.Windows.Forms.Button themhd;
    }
}