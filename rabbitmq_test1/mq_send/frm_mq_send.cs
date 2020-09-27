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
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;

namespace mq_send
{
    public partial class frm_mq_send : Form
    {

        // rpc client
        IConnection m_connection = null;
        IModel m_channel = null;
        string m_replyQueueName;
        BlockingCollection<string> m_respQueue = new BlockingCollection<string>();
        IBasicProperties m_props;

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

        private void btn_send1_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "192.168.10.166";
                factory.UserName = "test1";
                factory.Password = "test1";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "um_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                        string message = this.txt_msg.Text;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "", routingKey: "um_queue", basicProperties: null, body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }

        private void btn_send3_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "um_msg", type: ExchangeType.Fanout);
                        string message = this.txt_msg.Text;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "um_msg", routingKey: "", basicProperties: null, body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }

        private void btn_send4_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "um_msg_4", type: ExchangeType.Direct);
                        string message = this.txt_msg.Text;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "um_msg_4", routingKey: "warning", basicProperties: null, body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }

        private void btn_send5_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "topic_prd", type: ExchangeType.Topic);
                        string message = this.txt_msg.Text;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "topic_prd", routingKey: "um_prd.310079", basicProperties: null, body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }

        private void btn_rpc_client_ready_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();
                m_replyQueueName = m_channel.QueueDeclare().QueueName;
                var consumer = new EventingBasicConsumer(m_channel);

                m_channel.BasicConsume(consumer: consumer, queue: m_replyQueueName, autoAck: true);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    if (ea.BasicProperties.CorrelationId == m_props.CorrelationId)
                    {
                        m_respQueue.Add(response);
                    }
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }

        public string Call(string message)
        {
            m_props = m_channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            m_props.CorrelationId = correlationId;
            m_props.ReplyTo = m_replyQueueName;

            var messageBytes = Encoding.UTF8.GetBytes(message);
            m_channel.BasicPublish(exchange: "",routingKey: "rpc_queue", basicProperties: m_props, body: messageBytes);

            return m_respQueue.Take();
        }

        private void btn_rpc_client_request_Click(object sender, EventArgs e)
        {
            try
            {
                var response = Call(txt_request.Text);
                txt_response.Text = response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, this.Text);
            }
        }
    }
}
