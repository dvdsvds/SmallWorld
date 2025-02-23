namespace Client
{
    partial class Login
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
            this.Email = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FindAccountBtn = new System.Windows.Forms.Button();
            this.RegisterBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.RememberEmail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Email
            // 
            this.Email.ForeColor = System.Drawing.Color.Gray;
            this.Email.Location = new System.Drawing.Point(12, 32);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(226, 21);
            this.Email.TabIndex = 0;
            this.Email.Text = "Email";
            // 
            // Password
            // 
            this.Password.ForeColor = System.Drawing.Color.Gray;
            this.Password.Location = new System.Drawing.Point(12, 59);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(226, 21);
            this.Password.TabIndex = 1;
            this.Password.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "LOGIN";
            // 
            // FindAccountBtn
            // 
            this.FindAccountBtn.Location = new System.Drawing.Point(12, 103);
            this.FindAccountBtn.Name = "FindAccountBtn";
            this.FindAccountBtn.Size = new System.Drawing.Size(110, 23);
            this.FindAccountBtn.TabIndex = 3;
            this.FindAccountBtn.Text = "Find My Account";
            this.FindAccountBtn.UseVisualStyleBackColor = true;
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.Location = new System.Drawing.Point(128, 103);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(110, 23);
            this.RegisterBtn.TabIndex = 4;
            this.RegisterBtn.Text = "Register";
            this.RegisterBtn.UseVisualStyleBackColor = true;
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(12, 132);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(226, 23);
            this.LoginBtn.TabIndex = 5;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // RememberEmail
            // 
            this.RememberEmail.AutoSize = true;
            this.RememberEmail.Location = new System.Drawing.Point(14, 85);
            this.RememberEmail.Name = "RememberEmail";
            this.RememberEmail.Size = new System.Drawing.Size(148, 16);
            this.RememberEmail.TabIndex = 6;
            this.RememberEmail.Text = "Remember Username";
            this.RememberEmail.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 167);
            this.Controls.Add(this.RememberEmail);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.RegisterBtn);
            this.Controls.Add(this.FindAccountBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Email);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FindAccountBtn;
        private System.Windows.Forms.Button RegisterBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.CheckBox RememberEmail;
    }
}