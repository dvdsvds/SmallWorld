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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            Username.GotFocus += Username_GotFocus;
            Username.LostFocus += Username_LostFocus;
            Email.GotFocus += Email_GotFocus;
            Email.LostFocus += Email_LostFocus;
            VerificationCode.GotFocus += VerificationCode_GotFocus;
            VerificationCode.LostFocus += VerificationCode_LostFocus;
            Password.GotFocus += Password_GotFocus;
            Password.LostFocus += Password_LostFocus;
            ConfirmPassword.GotFocus += ConfirmPassword_GotFocus;
            ConfirmPassword.LostFocus += ConfirmPassword_LostFocus;
        }

        private void Username_GotFocus(object sender, EventArgs e)
        {
            if(Username.Text == "Username")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
        }

        private void Username_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Username.Text))
            {
                Username.Text = "Username";
                Username.ForeColor = Color.Gray;
            }
        }

        private void Email_GotFocus(object sender, EventArgs e)
        {
            if(Email.Text == "Email")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        }

        private void Email_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Email.Text))
            {
                Email.Text = "Email";
                Email.ForeColor = Color.Gray;
            }
        }

        private void VerificationCode_GotFocus(object sender, EventArgs e)
        {
            if(VerificationCode.Text == "Verification Code")
            {
                VerificationCode.Text = "";
                VerificationCode.ForeColor = Color.Black;
            }
        }

        private void VerificationCode_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(VerificationCode.Text))
            {
                VerificationCode.Text = "Verification Code";
                VerificationCode.ForeColor = Color.Gray;
            }
        }

        private void Password_GotFocus(object sender, EventArgs e)
        {
            if(Password.Text == "Password")
            {
                Password.Text = "";
                Password.ForeColor = Color.Black;
            }
        }

        private void Password_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Password.Text))
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Gray;
            }
        }
        private void ConfirmPassword_GotFocus(object sender, EventArgs e)
        {
            if(ConfirmPassword.Text == "Confirm Password")
            {
                ConfirmPassword.Text = "";
                ConfirmPassword.ForeColor = Color.Black;
            }
        }

        private void ConfirmPassword_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                ConfirmPassword.Text = "Confirm Password";
                ConfirmPassword.ForeColor = Color.Gray;
            }
        }
    }
}
