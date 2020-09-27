using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;

namespace mq_receive
{
    public partial class frm_mq_receive : Form
    {
        IConnection m_connection = null;
        IModel m_channel = null;

        private bool m_b_quit = false;

        public frm_mq_receive()
        {
            InitializeComponent();
        }

        private void frm_mq_receive_Load(object sender, EventArgs e)
        {
            try
            {
                //test_async();

                //BlockingCollection<string> in_out_text = new BlockingCollection<string>();
                //in_out_text.Add("aaa");
                //Debug.WriteLine(in_out_text.Take());
                //CancellationTokenSource cts = new CancellationTokenSource(new TimeSpan(0, 0, 5));
                //cts.Token.Register(() => { Console.WriteLine("cts canceling!"); });
                //Debug.WriteLine("...");
                //string str2 = in_out_text.Take(cts.Token);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        //private async Task test_async()
        //{
        //    string str = "";
        //    str = await task_async_string();
        //    Debug.WriteLine(str);
        //}

        //private async Task<string> task_async_string()
        //{
        //    string str_ret = await Task.Run(() => {
        //        Thread.Sleep(2000);
        //        return "ABC";
        //    });

        //    return str_ret;
        //}

        private void frm_mq_receive_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_b_quit = true;

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

        private void btn_ready1_Click(object sender, EventArgs e)
        {
            try
            {
                release_old_connection();

                var factory = new ConnectionFactory();
                factory.HostName = "192.168.10.166";
                factory.UserName = "test1";
                factory.Password = "test1";

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.QueueDeclare(queue: "um_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(m_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    this.Invoke(new Action<string>(this.show_receive_msg), message);

                    m_channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);

                };
                m_channel.BasicConsume(queue: "um_queue", autoAck: false, consumer: consumer);

                //m_channel.ExchangeDeclare(exchange: "logs", type: "fanout");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void release_old_connection()
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

        private void btn_ready3_Click(object sender, EventArgs e)
        {
            try
            {
                release_old_connection();

                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.ExchangeDeclare(exchange: "um_msg", type: ExchangeType.Fanout);
                string que_name = m_channel.QueueDeclare().QueueName;
                m_channel.QueueBind(queue: que_name, exchange: "um_msg", routingKey: "");

                var consumer = new EventingBasicConsumer(m_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    this.Invoke(new Action<string>(this.show_receive_msg), message);
                };
                m_channel.BasicConsume(queue: que_name, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_ready4_Click(object sender, EventArgs e)
        {
            try
            {
                release_old_connection();

                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.ExchangeDeclare(exchange: "um_msg_4", type: ExchangeType.Direct);
                string que_name = m_channel.QueueDeclare().QueueName;
                m_channel.QueueBind(queue: que_name, exchange: "um_msg_4", routingKey: "info");
                m_channel.QueueBind(queue: que_name, exchange: "um_msg_4", routingKey: "warning");
                m_channel.QueueBind(queue: que_name, exchange: "um_msg_4", routingKey: "error");

                var consumer = new EventingBasicConsumer(m_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    this.Invoke(new Action<string>(this.show_receive_msg), message);
                };
                m_channel.BasicConsume(queue: que_name, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_ready5_Click(object sender, EventArgs e)
        {
            try
            {
                release_old_connection();

                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.ExchangeDeclare(exchange: "topic_prd", type: ExchangeType.Topic);
                string que_name = m_channel.QueueDeclare().QueueName;
                m_channel.QueueBind(queue: que_name, exchange: "topic_prd", routingKey: "um_prd.*");
                m_channel.QueueBind(queue: que_name, exchange: "topic_prd", routingKey: "um_prd2.#");

                var consumer = new EventingBasicConsumer(m_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    this.Invoke(new Action<string>(this.show_receive_msg), message);
                };
                m_channel.BasicConsume(queue: que_name, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void btn_rpc_server_Click(object sender, EventArgs e)
        {
            try
            {
                release_old_connection();

                var factory = new ConnectionFactory() { HostName = "192.168.10.166", UserName = "test1", Password = "test1" };

                m_connection = factory.CreateConnection();
                m_channel = m_connection.CreateModel();

                m_channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                m_channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                var consumer = new EventingBasicConsumer(m_channel);
                m_channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var props = ea.BasicProperties;
                    var replyProps = m_channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    this.Invoke(new Action<string>(this.show_receive_msg), message);

                    try
                    {
                        int n = int.Parse(message);
                        response = fib(n).ToString();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(" [.] " + ex.Message);
                        this.Invoke(new Action<string>(this.show_receive_msg), ex.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        m_channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);

                        m_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };

                this.Invoke(new Action<string>(this.show_receive_msg), " [x] Awaiting RPC requests");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private static int fib(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }

            return fib(n - 1) + fib(n - 2);
        }
    }
}
