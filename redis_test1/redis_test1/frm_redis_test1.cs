using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redis_test1
{
    public partial class frm_redis_test1 : Form
    {
        public frm_redis_test1()
        {
            InitializeComponent();
        }

        private void frm_redis_test1_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message);
            }
        }
    }
}
