using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace mq_receive
{
    public partial class frm_mq_receive : Form
    {
        IConnection m_connection = null;
        IModel m_channel = null;

        public frm_mq_receive()
        {
            InitializeComponent();
        }

        private void frm_mq_receive_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void frm_mq_receive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_connection != null)
                {
                    m_connection.Close();
                }

                if (m_channel != null)
                {
                    m_channel.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_receive_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "192.168.10.166";
                factory.UserName = "guest";
                factory.Password = "guest";

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("um_Queue", false, false, false, null);

                        /* 这里定义了一个消费者，用于消费服务器接受的消息
                         * C#开发需要注意下这里，在一些非面向对象和面向对象比较差的语言中，是非常重视这种设计模式的。
                         * 比如RabbitMQ使用了生产者与消费者模式，然后很多相关的使用文章都在拿这个生产者和消费者来表述。
                         * 但是，在C#里，生产者与消费者对我们而言，根本算不上一种设计模式，他就是一种最基础的代码编写规则。
                         * 所以，大家不要复杂的名词吓到，其实，并没那么复杂。
                         * 这里，其实就是定义一个EventingBasicConsumer类型的对象，然后该对象有个Received事件，
                         * 该事件会在服务接收到数据时触发。
                         */
                        var consumer = new EventingBasicConsumer(channel);//消费者
                        channel.BasicConsume("um_Queue", true, consumer);//消费消息
                        consumer.Received += (model, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body.ToArray());

                            //Console.WriteLine($"Receive:{message}");
                            Debug.WriteLine("Receive:" + message);

                            this.Invoke(new Action<string>(this.show_receive_msg), message);
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void show_receive_msg(string msg)
        {
            try
            {
                txt_msg.Text += "\r\n" + msg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "192.168.10.166";
                factory.UserName = "guest";
                factory.Password = "guest";

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.QueueDeclare("um_Queue", false, false, false, null);


                var consumer = new EventingBasicConsumer(m_channel);//消费者
                m_channel.BasicConsume("um_Queue", true, consumer);//消费消息
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    //Console.WriteLine($"Receive:{message}");
                    Debug.WriteLine("Receive:" + message);

                    this.Invoke(new Action<string>(this.show_receive_msg), message);
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

    }
}
