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

            LoginBtn.Click += LoginBtn_Click;
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

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;

            if(string.IsNullOrEmpty(username))
            {
                MessageBox.Show("ID을 입력해주세요", "INPUT ERROR", MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Socket s = new Socket();
                string message = $"LOGIN {username} {password}";
                string res = await s.SendMessageAsync(message);

                var resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(res);
                if(resObj.status == "success" && resObj.message == "LS")
                {
                    MessageBox.Show("로그인 성공", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(resObj.status == "error" && resObj.message == "IP")
                {
                    MessageBox.Show("비밀번호가 틀립니다", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.status == "error" && resObj.message == "UDNE")
                {
                    MessageBox.Show("사용자가 존재하지 않습니다", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}
    }
}
