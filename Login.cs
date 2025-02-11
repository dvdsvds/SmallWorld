using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            Username.GotFocus += Username_GotFocus;
            Username.LostFocus += Username_LostFocus;
            Password.GotFocus += Password_GotFocus;
            Password.LostFocus += Password_LostFocus;
        }

        private void Username_GotFocus(object sender, EventArgs e)
        {
            if(Username.Text == "Username or Email")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
        }

        private void Username_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text))
            {
                Username.Text = "Username or Email";
                Username.ForeColor= Color.Gray;
            }
        }

        private void Password_GotFocus(Object sender, EventArgs e)
        {
            if (Password.Text == "Password")
            {
                Password.Text = "";
                Password.ForeColor = Color.Black;
            }
        }

        private void Password_LostFocus(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Password.Text))
            {
                Password.Text = "Password";
                Password.ForeColor= Color.Gray;
            }
        }

    }
}
