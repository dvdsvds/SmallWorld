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


        private string email;
        private string username;
        private string password;

        // Username Check
        private async void CheckUsername_Click(object sender, EventArgs e)
        {
            username = Username.Text;
            if (string.IsNullOrEmpty(username) || username == "Username")
            {
                MessageBox.Show("Username을 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            message = $"CHECK-USERNAME {username}";
            await HandleMsgAsync(message);

            if (resObj != null && resObj.status == "success" && resObj.message == "available")
            {
                MessageBox.Show("사용가능한 이름입니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resObj != null && resObj.status == "error")
            {
                if (resObj.message == "user required")
                {
                    MessageBox.Show("이름은 필수입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "taken")
                {
                    MessageBox.Show("사용중인 이름입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Send Email
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

            if (resObj.status == "success" && resObj.message == "send code to email")
            {
                MessageBox.Show("이메일로 인증번호를 전송했습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resObj.status == "error")
            {
                if (resObj.message == "please enter an email")
                {
                    MessageBox.Show("이메일은 필수입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "failed to send email")
                {
                    MessageBox.Show("이메일 전송에 실패했습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Confirm Code
        private async void Confirm_Click(object sender, EventArgs e)
        {
            string verification_code = VerificationCode.Text;
            if (string.IsNullOrEmpty(verification_code) || verification_code == "Verification Code")
            {
                MessageBox.Show("인증번호를 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            message = $"CHECK-CODE {email} {verification_code}";
            await HandleMsgAsync(message);

            if (resObj.status == "success" && resObj.message == "verified")
            {
                MessageBox.Show("인증 성공!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resObj.status == "error")
            {
                if (resObj.message == "code is missing from data")
                {
                    MessageBox.Show("인증 코드가 없습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "code not found or code expired")
                {
                    MessageBox.Show("인증 코드가 존재하지 않거나 만료되었습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "invalid code")
                {
                    MessageBox.Show("잘못된 인증 코드입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "password do not match")
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (resObj.message == "register failed")
                {
                    MessageBox.Show("회원 가입에 실패했습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("알 수 없는 오류가 발생했습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Register Done
        private async void RegisterDone_Click(object sender, EventArgs e)
        {
            password = Password.Text;
            string confirm_password = ConfirmPassword.Text;
            if (string.IsNullOrEmpty(password) || password == "Password" || string.IsNullOrEmpty(confirm_password) || confirm_password == "Confirm Password")
            {
                MessageBox.Show("비밀번호 또는 비밀번호 확인을 입력해주세요.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirm_password)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            message = $"DONE {username} {email} {password} {confirm_password}";
            await HandleMsgAsync(message);

            if(resObj.status == "success" && resObj.message == "register done")
            {
                MessageBox.Show("회원가입이 완료되었습니다.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(resObj.status == "error")
            {
                if(resObj.message == "username field is required")
                {
                    MessageBox.Show("이름은 필수입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.message == "email field is required")
                {
                    MessageBox.Show("이메일은 필수입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.message == "password field is required")
                {
                    MessageBox.Show("비밀번호는 필수입니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(resObj.message == "register failed")
                {
                    MessageBox.Show("회원가입을 실패했습니다.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
