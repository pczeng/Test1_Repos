namespace mq_send
{
    partial class frm_mq_send
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
            this.btn_send = new System.Windows.Forms.Button();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.btn_send1 = new System.Windows.Forms.Button();
            this.btn_send3 = new System.Windows.Forms.Button();
            this.btn_send4 = new System.Windows.Forms.Button();
            this.btn_send5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_rpc_client_ready = new System.Windows.Forms.Button();
            this.btn_rpc_client_request = new System.Windows.Forms.Button();
            this.txt_request = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_response = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(300, 24);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(300, 66);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(438, 134);
            this.txt_msg.TabIndex = 1;
            this.txt_msg.Text = "I am um-data";
            // 
            // btn_send1
            // 
            this.btn_send1.Location = new System.Drawing.Point(410, 24);
            this.btn_send1.Name = "btn_send1";
            this.btn_send1.Size = new System.Drawing.Size(75, 23);
            this.btn_send1.TabIndex = 2;
            this.btn_send1.Text = "send 1";
            this.btn_send1.UseVisualStyleBackColor = true;
            this.btn_send1.Click += new System.EventHandler(this.btn_send1_Click);
            // 
            // btn_send3
            // 
            this.btn_send3.Location = new System.Drawing.Point(24, 94);
            this.btn_send3.Name = "btn_send3";
            this.btn_send3.Size = new System.Drawing.Size(75, 23);
            this.btn_send3.TabIndex = 3;
            this.btn_send3.Text = "send 3";
            this.btn_send3.UseVisualStyleBackColor = true;
            this.btn_send3.Click += new System.EventHandler(this.btn_send3_Click);
            // 
            // btn_send4
            // 
            this.btn_send4.Location = new System.Drawing.Point(24, 137);
            this.btn_send4.Name = "btn_send4";
            this.btn_send4.Size = new System.Drawing.Size(75, 23);
            this.btn_send4.TabIndex = 4;
            this.btn_send4.Text = "send 4";
            this.btn_send4.UseVisualStyleBackColor = true;
            this.btn_send4.Click += new System.EventHandler(this.btn_send4_Click);
            // 
            // btn_send5
            // 
            this.btn_send5.Location = new System.Drawing.Point(24, 177);
            this.btn_send5.Name = "btn_send5";
            this.btn_send5.Size = new System.Drawing.Size(75, 23);
            this.btn_send5.TabIndex = 5;
            this.btn_send5.Text = "send 5";
            this.btn_send5.UseVisualStyleBackColor = true;
            this.btn_send5.Click += new System.EventHandler(this.btn_send5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_response);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_request);
            this.groupBox1.Controls.Add(this.btn_rpc_client_request);
            this.groupBox1.Controls.Add(this.btn_rpc_client_ready);
            this.groupBox1.Location = new System.Drawing.Point(24, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 95);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "rpc client";
            // 
            // btn_rpc_client_ready
            // 
            this.btn_rpc_client_ready.Location = new System.Drawing.Point(6, 20);
            this.btn_rpc_client_ready.Name = "btn_rpc_client_ready";
            this.btn_rpc_client_ready.Size = new System.Drawing.Size(75, 23);
            this.btn_rpc_client_ready.TabIndex = 6;
            this.btn_rpc_client_ready.Text = "ready";
            this.btn_rpc_client_ready.UseVisualStyleBackColor = true;
            this.btn_rpc_client_ready.Click += new System.EventHandler(this.btn_rpc_client_ready_Click);
            // 
            // btn_rpc_client_request
            // 
            this.btn_rpc_client_request.Location = new System.Drawing.Point(6, 49);
            this.btn_rpc_client_request.Name = "btn_rpc_client_request";
            this.btn_rpc_client_request.Size = new System.Drawing.Size(75, 23);
            this.btn_rpc_client_request.TabIndex = 7;
            this.btn_rpc_client_request.Text = "request";
            this.btn_rpc_client_request.UseVisualStyleBackColor = true;
            this.btn_rpc_client_request.Click += new System.EventHandler(this.btn_rpc_client_request_Click);
            // 
            // txt_request
            // 
            this.txt_request.Location = new System.Drawing.Point(173, 20);
            this.txt_request.Name = "txt_request";
            this.txt_request.Size = new System.Drawing.Size(59, 21);
            this.txt_request.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "request:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "response:";
            // 
            // txt_response
            // 
            this.txt_response.Location = new System.Drawing.Point(173, 51);
            this.txt_response.Name = "txt_response";
            this.txt_response.Size = new System.Drawing.Size(59, 21);
            this.txt_response.TabIndex = 9;
            // 
            // frm_mq_send
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 322);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_send5);
            this.Controls.Add(this.btn_send4);
            this.Controls.Add(this.btn_send3);
            this.Controls.Add(this.btn_send1);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.btn_send);
            this.Name = "frm_mq_send";
            this.Text = "RabbitMQ-Send";
            this.Load += new System.EventHandler(this.frm_mq_send_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Button btn_send1;
        private System.Windows.Forms.Button btn_send3;
        private System.Windows.Forms.Button btn_send4;
        private System.Windows.Forms.Button btn_send5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_response;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_request;
        private System.Windows.Forms.Button btn_rpc_client_request;
        private System.Windows.Forms.Button btn_rpc_client_ready;
    }
}

