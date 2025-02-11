using System.Drawing;

namespace Client
{
    partial class Chat
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Viwer = new System.Windows.Forms.RichTextBox();
            this.SendBox = new System.Windows.Forms.TextBox();
            this.IPAddress = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.TextBox();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Viwer
            // 
            this.Viwer.CausesValidation = false;
            this.Viwer.Location = new System.Drawing.Point(12, 41);
            this.Viwer.Name = "Viwer";
            this.Viwer.ReadOnly = true;
            this.Viwer.Size = new System.Drawing.Size(417, 411);
            this.Viwer.TabIndex = 0;
            this.Viwer.Text = "";
            // 
            // SendBox
            // 
            this.SendBox.Location = new System.Drawing.Point(12, 459);
            this.SendBox.Name = "SendBox";
            this.SendBox.Size = new System.Drawing.Size(417, 21);
            this.SendBox.TabIndex = 1;
            // 
            // IPAddress
            // 
            this.IPAddress.ForeColor = System.Drawing.Color.Gray;
            this.IPAddress.Location = new System.Drawing.Point(12, 10);
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.Size = new System.Drawing.Size(126, 21);
            this.IPAddress.TabIndex = 2;
            this.IPAddress.Text = "IP";
            // 
            // Port
            // 
            this.Port.ForeColor = System.Drawing.Color.Gray;
            this.Port.Location = new System.Drawing.Point(144, 10);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(126, 21);
            this.Port.TabIndex = 3;
            this.Port.Text = "Port";
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(276, 10);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectBtn.TabIndex = 4;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(354, 10);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(75, 23);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 491);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IPAddress);
            this.Controls.Add(this.SendBox);
            this.Controls.Add(this.Viwer);
            this.Name = "Chat";
            this.Text = "Chat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Viwer;
        private System.Windows.Forms.TextBox SendBox;
        private System.Windows.Forms.TextBox IPAddress;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}

