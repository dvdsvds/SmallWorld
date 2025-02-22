using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Client
{
    class Socket
    {
        private string IP;       
        private int PORT = 8080;

        public Socket()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "s.json");
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filePath)
                .Build();

            IP = config["ServerSetting:IP"];


            if (string.IsNullOrEmpty(IP))
            {
                MessageBox.Show("IP 오류");
            }
        }
        public async Task<string> SendMessageAsync(string message)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(IP, PORT);
                    NetworkStream stream = client.GetStream();

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string res = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    return res;
                }
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
