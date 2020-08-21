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
            // frm_mq_receive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 307);
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
    }
}

