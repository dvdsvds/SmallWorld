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
        private Socket s = new Socket();
        private string message;
        private string res;
        private dynamic resObj;
        public Login()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            Email.GotFocus += Username_GotFocus;
            Email.LostFocus += Username_LostFocus;
            Password.GotFocus += Password_GotFocus;
            Password.LostFocus += Password_LostFocus;
        }
        private async Task HandleMsgAsync(string message)
        {
            try
            {
                res = await s.SendMessageAsync(message);
                resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending request : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;
            if (string.IsNullOrEmpty(email) || email == "Email" || string.IsNullOrEmpty(password) || password == "Password")
            {
                MessageBox.Show("아이디 또는 비밀번호를 입력하지 않았습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            message = $"LOGIN {email} {password}";
            await HandleMsgAsync(message);

            if(resObj != null && resObj.status == "success" && resObj.message == "login done")
            {
                MessageBox.Show("로그인 되었습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(resObj != null && resObj.status == "error")
            {
                if (resObj.message == "missing credentials")
                {
                    MessageBox.Show("아이디 또는 비밀번호를 입력해주세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.message == "user does not exist")
                {
                    MessageBox.Show("아이디가 틀렸습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.message == "invalid password")
                {
                    MessageBox.Show("비밀번호가 다릅니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Username_GotFocus(object sender, EventArgs e)
        {
            if(Email.Text == "Email")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        }

        private void Username_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                Email.Text = "Email";
                Email.ForeColor= Color.Gray;
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

    }
}
