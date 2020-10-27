using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StackExchange.Redis;
using System.Diagnostics;

namespace redis_test1
{
    public partial class frm_redis_test1 : Form
    {

        ConnectionMultiplexer _redis;
        IDatabase _db;

        public frm_redis_test1()
        {
            InitializeComponent();
        }

        private void frm_redis_test1_Load(object sender, EventArgs e)
        {
            try
            {
                _redis = ConnectionMultiplexer.Connect("192.168.10.166");
                _db = _redis.GetDatabase();




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //_db.StringSet("foo", "aaBBccEE");
                //String str = _db.StringGet("foo");
                //Debug.WriteLine(str);

                byte[] value = Encoding.UTF8.GetBytes("aaBBccDD_1122");
                _db.StringSet("foo", value);
                byte[] data = _db.StringGet("foo");
                Debug.WriteLine(Encoding.UTF8.GetString(data));

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }
    }
}
