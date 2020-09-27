namespace mq_receive
{
    partial class frm_mq_receive
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.btn_receive = new System.Windows.Forms.Button();
            this.btn_ready = new System.Windows.Forms.Button();
            this.btn_ready1 = new System.Windows.Forms.Button();
            this.btn_ready3 = new System.Windows.Forms.Button();
            this.btn_ready4 = new System.Windows.Forms.Button();
            this.btn_ready5 = new System.Windows.Forms.Button();
            this.btn_rpc_server = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(318, 65);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(438, 134);
            this.txt_msg.TabIndex = 3;
            // 
            // btn_receive
            // 
            this.btn_receive.Location = new System.Drawing.Point(610, 24);
            this.btn_receive.Name = "btn_receive";
            this.btn_receive.Size = new System.Drawing.Size(75, 23);
            this.btn_receive.TabIndex = 2;
            this.btn_receive.Text = "receive";
            this.btn_receive.UseVisualStyleBackColor = true;
            this.btn_receive.Click += new System.EventHandler(this.btn_receive_Click);
            // 
            // btn_ready
            // 
            this.btn_ready.Location = new System.Drawing.Point(318, 24);
            this.btn_ready.Name = "btn_ready";
            this.btn_ready.Size = new System.Drawing.Size(75, 23);
            this.btn_ready.TabIndex = 4;
            this.btn_ready.Text = "Ready";
            this.btn_ready.UseVisualStyleBackColor = true;
            this.btn_ready.Click += new System.EventHandler(this.btn_ready_Click);
            // 
            // btn_ready1
            // 
            this.btn_ready1.Location = new System.Drawing.Point(37, 24);
            this.btn_ready1.Name = "btn_ready1";
            this.btn_ready1.Size = new System.Drawing.Size(75, 23);
            this.btn_ready1.TabIndex = 5;
            this.btn_ready1.Text = "Ready 1";
            this.btn_ready1.UseVisualStyleBackColor = true;
            this.btn_ready1.Click += new System.EventHandler(this.btn_ready1_Click);
            // 
            // btn_ready3
            // 
            this.btn_ready3.Location = new System.Drawing.Point(37, 140);
            this.btn_ready3.Name = "btn_ready3";
            this.btn_ready3.Size = new System.Drawing.Size(75, 23);
            this.btn_ready3.TabIndex = 6;
            this.btn_ready3.Text = "Ready 3";
            this.btn_ready3.UseVisualStyleBackColor = true;
            this.btn_ready3.Click += new System.EventHandler(this.btn_ready3_Click);
            // 
            // btn_ready4
            // 
            this.btn_ready4.Location = new System.Drawing.Point(37, 186);
            this.btn_ready4.Name = "btn_ready4";
            this.btn_ready4.Size = new System.Drawing.Size(75, 23);
            this.btn_ready4.TabIndex = 7;
            this.btn_ready4.Text = "Ready 4";
            this.btn_ready4.UseVisualStyleBackColor = true;
            this.btn_ready4.Click += new System.EventHandler(this.btn_ready4_Click);
            // 
            // btn_ready5
            // 
            this.btn_ready5.Location = new System.Drawing.Point(37, 231);
            this.btn_ready5.Name = "btn_ready5";
            this.btn_ready5.Size = new System.Drawing.Size(75, 23);
            this.btn_ready5.TabIndex = 8;
            this.btn_ready5.Text = "Ready 5";
            this.btn_ready5.UseVisualStyleBackColor = true;
            this.btn_ready5.Click += new System.EventHandler(this.btn_ready5_Click);
            // 
            // btn_rpc_server
            // 
            this.btn_rpc_server.Location = new System.Drawing.Point(171, 231);
            this.btn_rpc_server.Name = "btn_rpc_server";
            this.btn_rpc_server.Size = new System.Drawing.Size(153, 23);
            this.btn_rpc_server.TabIndex = 9;
            this.btn_rpc_server.Text = "rpc server ready";
            this.btn_rpc_server.UseVisualStyleBackColor = true;
            this.btn_rpc_server.Click += new System.EventHandler(this.btn_rpc_server_Click);
            // 
            // frm_mq_receive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 307);
            this.Controls.Add(this.btn_rpc_server);
            this.Controls.Add(this.btn_ready5);
            this.Controls.Add(this.btn_ready4);
            this.Controls.Add(this.btn_ready3);
            this.Controls.Add(this.btn_ready1);
            this.Controls.Add(this.btn_ready);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.btn_receive);
            this.Name = "frm_mq_receive";
            this.Text = "RabbitMQ-Receive";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_mq_receive_FormClosing);
            this.Load += new System.EventHandler(this.frm_mq_receive_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Button btn_receive;
        private System.Windows.Forms.Button btn_ready;
        private System.Windows.Forms.Button btn_ready1;
        private System.Windows.Forms.Button btn_ready3;
        private System.Windows.Forms.Button btn_ready4;
        private System.Windows.Forms.Button btn_ready5;
        private System.Windows.Forms.Button btn_rpc_server;
    }
}

