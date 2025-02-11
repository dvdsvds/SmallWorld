using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Client
{
    public partial class Chat : Form
    {
        private const int BUFFER_SIZE = 1024;
        private  TcpClient _client;
        private NetworkStream _stream;
        private byte[] _buffer = new byte[BUFFER_SIZE];
        private string _username;
        public Chat()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            IPAddress.GotFocus += IPAddress_GotFocus;
            IPAddress.LostFocus += IPAddress_LostFocus;
            Port.GotFocus += Port_GotFocus;
            Port.LostFocus += Port_LostFocus;

            SendBox.KeyDown += SendBox_Enter;
            ConnectBtn.Click += ConnectBtn_Click;

        }

       
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void ConnectToServer()
        {
            try
            {
                string IP = IPAddress.Text;
                int PORT = int.Parse(Port.Text);

                _client = new TcpClient();
                _client.Connect(IP, PORT);


                MessageBox.Show("서버 연결 성공");
                _stream = _client.GetStream();


                _stream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(ReceivedMessage), null);
            } 
            catch(Exception e)
            {
                MessageBox.Show("서버 연결 실패 : " + e.Message);

            }
        }

               private void SendBox_Enter(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string message = SendBox.Text;

                if(_client != null && _client.Connected)
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    _stream.Write(messageBytes, 0, messageBytes.Length);
                    Viwer.AppendText( "You : " + message + Environment.NewLine);

                    SendBox.Clear();
                }

                e.SuppressKeyPress = true;
            }
        }

        private void ReceivedMessage(IAsyncResult ar)
        {
            try
            {
                int bytesRead = _stream.EndRead(ar);

                if (bytesRead > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(_buffer, 0, bytesRead);
                    this.Invoke(new Action(() => Viwer.AppendText(receivedMessage + Environment.NewLine)));
                }
                _stream.BeginRead(_buffer, 0, _buffer.Length, new AsyncCallback(ReceivedMessage), null);
            }
            catch (Exception e)
            {
                MessageBox.Show("메시지 수신 실패 : " + e.Message);
            }
        }

        // Focus 관련 함수
        private void IPAddress_GotFocus(Object sender, EventArgs e)
        {
            if(IPAddress.Text == "IP")
            {
                IPAddress.Text = "";
                IPAddress.ForeColor = Color.Black;
            }
        }

        private void IPAddress_LostFocus(Object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(IPAddress.Text))
            {
                IPAddress.Text = "IP";
                IPAddress.ForeColor = Color.Gray;
            }
            else
            {
                IPAddress.ForeColor= Color.Black;
            }
        }

        private void Port_GotFocus(Object sender, EventArgs e)
        {
            if(Port.Text == "Port")
            {
                Port.Text = "";
                Port.ForeColor = Color.Black;
            }
        }

        private void Port_LostFocus(Object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Port.Text))
            {
                Port.Text = "Port";
                Port.ForeColor = Color.Gray;
            }
            else
            {
                Port.ForeColor= Color.Black;
            }
        }


    }
}
