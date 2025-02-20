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
        private Socket s = new Socket();
        private string message;
        private string res;
        private dynamic resObj;
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

        private async Task HandleMsgAsync(string message)
        {
            try
            {
                res = await s.SendMessageAsync(message);
                resObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(res);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error sending request : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleResponse(string condition, string successMsg, string errorMsg)
        {
            if (resObj?.status == "success" && resObj?.message == condition)
            {
                MessageBox.Show(successMsg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resObj.status == "error")
            {
                MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("알 수 없는 오류가 발생했습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void CheckUsername_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            if (string.IsNullOrEmpty(username) || username == "Username")
            {
                MessageBox.Show("Username을 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            message = $"CHECK-USERNAME {username}";
            await HandleMsgAsync(message);

            HandleResponse("available", "사용 가능한 이름입니다.", "사용중인 이름입니다.");
        }

        private string email;
        private async void SendEmail_Click(object sender, EventArgs e)
        {

            email = Email.Text;
            if (string.IsNullOrEmpty(email) || email == "Email")
            {
                MessageBox.Show("이메일을 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            message = $"SEND-EMAIL {email}";
            await HandleMsgAsync(message);

            HandleResponse("send code to email", "이메일로 인증번호가 전송되었습니다.", "인증번호 전송에 실패했습니다.");
        }
        private async void Confirm_Click(object sender, EventArgs e)
        {
            string verification_code = VerificationCode.Text;
            if(string.IsNullOrEmpty(verification_code) || verification_code == "Verification Code")
            {
                MessageBox.Show("인증번호를 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            message = $"CHECK-CODE {email} {verification_code}";
            await HandleMsgAsync(message);

            HandleResponse("verified", "인증되었습니다.", "인증번호가 틀립니다.");
        }

        private void Username_GotFocus(object sender, EventArgs e)
        {
            if (Username.Text == "Username")
            {
                Username.Text = "";
                Username.ForeColor = Color.Black;
            }
        }
        private void Username_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text))
            {
                Username.Text = "Username";
                Username.ForeColor = Color.Gray;
            }
        }
        private void Email_GotFocus(object sender, EventArgs e)
        {
            if (Email.Text == "Email")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
            }
        }
        private void Email_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                Email.Text = "Email";
                Email.ForeColor = Color.Gray;
            }
        }
        private void VerificationCode_GotFocus(object sender, EventArgs e)
        {
            if (VerificationCode.Text == "Verification Code")
            {
                VerificationCode.Text = "";
                VerificationCode.ForeColor = Color.Black;
            }
        }
        private void VerificationCode_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(VerificationCode.Text))
            {
                VerificationCode.Text = "Verification Code";
                VerificationCode.ForeColor = Color.Gray;
            }
        }
        private void Password_GotFocus(object sender, EventArgs e)
        {
            if (Password.Text == "Password")
            {
                Password.Text = "";
                Password.ForeColor = Color.Black;
            }
        }
        private void Password_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Password.Text))
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Gray;
            }
        }
        private void ConfirmPassword_GotFocus(object sender, EventArgs e)
        {
            if (ConfirmPassword.Text == "Confirm Password")
            {
                ConfirmPassword.Text = "";
                ConfirmPassword.ForeColor = Color.Black;
            }
        }
        private void ConfirmPassword_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConfirmPassword.Text))
            {
                ConfirmPassword.Text = "Confirm Password";
                ConfirmPassword.ForeColor = Color.Gray;
            }
        }

    }
}
