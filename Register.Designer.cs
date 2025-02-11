namespace Client
{
    partial class Register
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
            this.Username = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.ConfirmPassword = new System.Windows.Forms.TextBox();
            this.RegisterLabel = new System.Windows.Forms.Label();
            this.VerificationCode = new System.Windows.Forms.TextBox();
            this.SendEmail = new System.Windows.Forms.Button();
            this.ConfirmCode = new System.Windows.Forms.Button();
            this.ConfirmUsername = new System.Windows.Forms.Button();
            this.Done = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.ForeColor = System.Drawing.Color.Gray;
            this.Username.Location = new System.Drawing.Point(16, 32);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(174, 21);
            this.Username.TabIndex = 0;
            this.Username.Text = "Username";
            // 
            // Email
            // 
            this.Email.ForeColor = System.Drawing.Color.Gray;
            this.Email.Location = new System.Drawing.Point(16, 59);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(174, 21);
            this.Email.TabIndex = 1;
            this.Email.Text = "Email";
            // 
            // Password
            // 
            this.Password.ForeColor = System.Drawing.Color.Gray;
            this.Password.Location = new System.Drawing.Point(16, 113);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(174, 21);
            this.Password.TabIndex = 2;
            this.Password.Text = "Password";
            // 
            // ConfirmPassword
            // 
            this.ConfirmPassword.ForeColor = System.Drawing.Color.Gray;
            this.ConfirmPassword.Location = new System.Drawing.Point(16, 140);
            this.ConfirmPassword.Name = "ConfirmPassword";
            this.ConfirmPassword.Size = new System.Drawing.Size(174, 21);
            this.ConfirmPassword.TabIndex = 3;
            this.ConfirmPassword.Text = "Confirm Password";
            // 
            // RegisterLabel
            // 
            this.RegisterLabel.AutoSize = true;
            this.RegisterLabel.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F);
            this.RegisterLabel.Location = new System.Drawing.Point(12, 9);
            this.RegisterLabel.Name = "RegisterLabel";
            this.RegisterLabel.Size = new System.Drawing.Size(64, 20);
            this.RegisterLabel.TabIndex = 4;
            this.RegisterLabel.Text = "Register";
            // 
            // VerificationCode
            // 
            this.VerificationCode.ForeColor = System.Drawing.Color.Gray;
            this.VerificationCode.Location = new System.Drawing.Point(16, 86);
            this.VerificationCode.Name = "VerificationCode";
            this.VerificationCode.Size = new System.Drawing.Size(174, 21);
            this.VerificationCode.TabIndex = 5;
            this.VerificationCode.Text = "Verification Code";
            // 
            // SendEmail
            // 
            this.SendEmail.Location = new System.Drawing.Point(197, 58);
            this.SendEmail.Name = "SendEmail";
            this.SendEmail.Size = new System.Drawing.Size(75, 23);
            this.SendEmail.TabIndex = 6;
            this.SendEmail.Text = "Send";
            this.SendEmail.UseVisualStyleBackColor = true;
            // 
            // ConfirmCode
            // 
            this.ConfirmCode.Location = new System.Drawing.Point(197, 85);
            this.ConfirmCode.Name = "ConfirmCode";
            this.ConfirmCode.Size = new System.Drawing.Size(75, 23);
            this.ConfirmCode.TabIndex = 7;
            this.ConfirmCode.Text = "Confirm";
            this.ConfirmCode.UseVisualStyleBackColor = true;
            // 
            // ConfirmUsername
            // 
            this.ConfirmUsername.Location = new System.Drawing.Point(196, 31);
            this.ConfirmUsername.Name = "ConfirmUsername";
            this.ConfirmUsername.Size = new System.Drawing.Size(75, 23);
            this.ConfirmUsername.TabIndex = 8;
            this.ConfirmUsername.Text = "Confirm";
            this.ConfirmUsername.UseVisualStyleBackColor = true;
            // 
            // Done
            // 
            this.Done.Location = new System.Drawing.Point(16, 167);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(174, 23);
            this.Done.TabIndex = 9;
            this.Done.Text = "Done";
            this.Done.UseVisualStyleBackColor = true;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 202);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.ConfirmUsername);
            this.Controls.Add(this.ConfirmCode);
            this.Controls.Add(this.SendEmail);
            this.Controls.Add(this.VerificationCode);
            this.Controls.Add(this.RegisterLabel);
            this.Controls.Add(this.ConfirmPassword);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.Username);
            this.Name = "Register";
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox ConfirmPassword;
        private System.Windows.Forms.Label RegisterLabel;
        private System.Windows.Forms.TextBox VerificationCode;
        private System.Windows.Forms.Button SendEmail;
        private System.Windows.Forms.Button ConfirmCode;
        private System.Windows.Forms.Button ConfirmUsername;
        private System.Windows.Forms.Button Done;
    }
}