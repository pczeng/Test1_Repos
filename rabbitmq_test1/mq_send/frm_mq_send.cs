using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;

namespace mq_send
{
    public partial class frm_mq_send : Form
    {
        public frm_mq_send()
        {
            InitializeComponent();
        }

        private void frm_mq_send_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "192.168.10.166";
                factory.UserName = "guest";
                factory.Password = "guest";


                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("um_Queue", false, false, false, null);//创建一个名称为kibaqueue的消息队列
                        var properties = channel.CreateBasicProperties();
                        properties.DeliveryMode = 1;
                        string message = txt_msg.Text; //传递的消息内容
                        channel.BasicPublish("", "um_Queue", properties, Encoding.UTF8.GetBytes(message)); //生产消息
                        Console.WriteLine($"Send:{message}");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }
    }
}
