namespace crm_test_httpclient
{
    partial class frm_test_httpclient
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
            this.btn_test_get = new System.Windows.Forms.Button();
            this.btn_test_post = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_test_get
            // 
            this.btn_test_get.Location = new System.Drawing.Point(546, 34);
            this.btn_test_get.Name = "btn_test_get";
            this.btn_test_get.Size = new System.Drawing.Size(138, 44);
            this.btn_test_get.TabIndex = 0;
            this.btn_test_get.Text = "test Get";
            this.btn_test_get.UseVisualStyleBackColor = true;
            this.btn_test_get.Click += new System.EventHandler(this.btn_test_get_Click);
            // 
            // btn_test_post
            // 
            this.btn_test_post.Location = new System.Drawing.Point(546, 108);
            this.btn_test_post.Name = "btn_test_post";
            this.btn_test_post.Size = new System.Drawing.Size(138, 44);
            this.btn_test_post.TabIndex = 1;
            this.btn_test_post.Text = "test Post";
            this.btn_test_post.UseVisualStyleBackColor = true;
            this.btn_test_post.Click += new System.EventHandler(this.btn_test_post_Click);
            // 
            // frm_test_httpclient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_test_post);
            this.Controls.Add(this.btn_test_get);
            this.Name = "frm_test_httpclient";
            this.Text = "test httpClient";
            this.Load += new System.EventHandler(this.frm_test_httpclient_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_test_get;
        private System.Windows.Forms.Button btn_test_post;
    }
}

