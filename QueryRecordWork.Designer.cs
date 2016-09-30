namespace UserManager
{
    partial class QueryRecordWork
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbbone = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.querypanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpstart = new System.Windows.Forms.DateTimePicker();
            this.dtpend = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.querypanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(850, 384);
            this.dataGridView1.TabIndex = 4;
            // 
            // cbbone
            // 
            this.cbbone.FormattingEnabled = true;
            this.cbbone.Location = new System.Drawing.Point(94, 32);
            this.cbbone.Name = "cbbone";
            this.cbbone.Size = new System.Drawing.Size(60, 20);
            this.cbbone.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(606, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // querypanel
            // 
            this.querypanel.Controls.Add(this.button2);
            this.querypanel.Controls.Add(this.label2);
            this.querypanel.Controls.Add(this.label3);
            this.querypanel.Controls.Add(this.dtpstart);
            this.querypanel.Controls.Add(this.dtpend);
            this.querypanel.Controls.Add(this.cbbone);
            this.querypanel.Controls.Add(this.button1);
            this.querypanel.Controls.Add(this.label4);
            this.querypanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.querypanel.Location = new System.Drawing.Point(0, 0);
            this.querypanel.Name = "querypanel";
            this.querypanel.Size = new System.Drawing.Size(850, 81);
            this.querypanel.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(705, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "导出(Excel)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "时间段：";
            // 
            // dtpstart
            // 
            this.dtpstart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpstart.Location = new System.Drawing.Point(222, 32);
            this.dtpstart.Name = "dtpstart";
            this.dtpstart.Size = new System.Drawing.Size(155, 21);
            this.dtpstart.TabIndex = 31;
            // 
            // dtpend
            // 
            this.dtpend.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpend.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpend.Location = new System.Drawing.Point(404, 32);
            this.dtpend.Name = "dtpend";
            this.dtpend.Size = new System.Drawing.Size(155, 21);
            this.dtpend.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "收费单位：";
            // 
            // QueryRecordWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 465);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.querypanel);
            this.Name = "QueryRecordWork";
            this.Text = "交接班记录查询";
            this.Load += new System.EventHandler(this.QueryRecordWork_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.querypanel.ResumeLayout(false);
            this.querypanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbbone;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel querypanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpstart;
        private System.Windows.Forms.DateTimePicker dtpend;
        private System.Windows.Forms.Button button2;
    }
}