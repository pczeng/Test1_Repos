namespace crm_test_entity
{
    partial class frm_test_entity
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
            this.btn_test_entity1 = new System.Windows.Forms.Button();
            this.btn_test_fetchXML = new System.Windows.Forms.Button();
            this.btn_test_fetchXML2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_test_entity1
            // 
            this.btn_test_entity1.Location = new System.Drawing.Point(530, 24);
            this.btn_test_entity1.Name = "btn_test_entity1";
            this.btn_test_entity1.Size = new System.Drawing.Size(114, 43);
            this.btn_test_entity1.TabIndex = 0;
            this.btn_test_entity1.Text = "test entity1";
            this.btn_test_entity1.UseVisualStyleBackColor = true;
            this.btn_test_entity1.Click += new System.EventHandler(this.btn_test_entity1_Click);
            // 
            // btn_test_fetchXML
            // 
            this.btn_test_fetchXML.Location = new System.Drawing.Point(530, 98);
            this.btn_test_fetchXML.Name = "btn_test_fetchXML";
            this.btn_test_fetchXML.Size = new System.Drawing.Size(114, 43);
            this.btn_test_fetchXML.TabIndex = 1;
            this.btn_test_fetchXML.Text = "test fetchXML1";
            this.btn_test_fetchXML.UseVisualStyleBackColor = true;
            this.btn_test_fetchXML.Click += new System.EventHandler(this.btn_test_fetchXML_Click);
            // 
            // btn_test_fetchXML2
            // 
            this.btn_test_fetchXML2.Location = new System.Drawing.Point(530, 166);
            this.btn_test_fetchXML2.Name = "btn_test_fetchXML2";
            this.btn_test_fetchXML2.Size = new System.Drawing.Size(114, 43);
            this.btn_test_fetchXML2.TabIndex = 2;
            this.btn_test_fetchXML2.Text = "test fetchXML2";
            this.btn_test_fetchXML2.UseVisualStyleBackColor = true;
            this.btn_test_fetchXML2.Click += new System.EventHandler(this.btn_test_fetchXML2_Click);
            // 
            // frm_test_entity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_test_fetchXML2);
            this.Controls.Add(this.btn_test_fetchXML);
            this.Controls.Add(this.btn_test_entity1);
            this.Name = "frm_test_entity";
            this.Text = "test entity";
            this.Load += new System.EventHandler(this.frm_test_entity_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_test_entity1;
        private System.Windows.Forms.Button btn_test_fetchXML;
        private System.Windows.Forms.Button btn_test_fetchXML2;
    }
}

