﻿using GUI.USER.DatBan;
using GUI.USER.LichSuDat;
using GUI.USER.TaiKhoan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.CompilerServices;
using FontAwesome.Sharp;
using System.Windows.Media;
using Color = System.Drawing.Color;
using BusinessLayer;
using DataLayer;
namespace GUI
{
    public partial class MainViewCustomer : Form
    {
        string _id;
      
        Account _customer;
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public MainViewCustomer(string user = null)
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            _id = user;
            UserSession.UserID = user;
            _customer = new Account();
            LoadToView();
        }
        private struct RGBColors
        {
  public static Color color1 = Color.FromArgb(172, 126, 241);  
  public static Color color2 = Color.FromArgb(249, 118, 176); 
  public static Color color3 = Color.FromArgb(253, 138, 114);  
  public static Color color4 = Color.FromArgb(95, 77, 221);  
  public static Color color5 = Color.FromArgb(249, 88, 155);   
  public static Color color6 = Color.FromArgb(24, 161, 251);  
        }

       public void ActivateButton (object sender, Color color)
        {
            if(sender != null)
            {
                DisableButton();
                currentBtn  = (IconButton)sender;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                iconcurrentChildHome.IconChar = currentBtn.IconChar;
                iconcurrentChildHome.IconColor = color;

            }
        }
        private void LoadToView()
        {
            List<tb_Customer> uu = _customer.GetAccountsFromTable("tb_Customer");
            foreach(var i in uu)
            {
                if(i.CustomerID.Trim() == _id.Trim())
                {

                 lblName.Text = i.Name;
                    break;
                }
            }
        }
        private void DisableButton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31,30,68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            OpenChildForm(new OrderForm());

        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new AccountForm1());
        }

    

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new HistoryForm());

        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new AccountForm1(_id));

            ActivateButton(sender, RGBColors.color1);

        }

        private void btnHome_Click(object sender, EventArgs e)
        {

            currentChildForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconcurrentChildHome.IconChar = IconChar.Home;
            iconcurrentChildHome.IconColor = Color.MediumPurple;
            lblHome.Text = "Home";
        }
        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();
        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hhWnd, int wMsg, int wParam, int lParam);
        //private void panel1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    ReleaseCapture();
        //    SendMessage(this.Handle, 0x112, 0xf012, 0);
        //}

        private void MainViewCustomer_Load(object sender, EventArgs e)
        {

        }

        public void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            // Cập nhật tiêu đề của lblHome
            lblHome.Text = childForm.Text;
        }

    }
}
