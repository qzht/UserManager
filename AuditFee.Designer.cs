namespace UserManager
{
    partial class AuditFee
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
            this.cbbtype = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.querypanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.querypanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(632, 381);
            this.dataGridView1.TabIndex = 6;
            // 
            // cbbtype
            // 
            this.cbbtype.FormattingEnabled = true;
            this.cbbtype.Location = new System.Drawing.Point(75, 33);
            this.cbbtype.Name = "cbbtype";
            this.cbbtype.Size = new System.Drawing.Size(60, 20);
            this.cbbtype.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 33);
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
            this.querypanel.Controls.Add(this.nudCount);
            this.querypanel.Controls.Add(this.label1);
            this.querypanel.Controls.Add(this.cbbtype);
            this.querypanel.Controls.Add(this.button1);
            this.querypanel.Controls.Add(this.label4);
            this.querypanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.querypanel.Location = new System.Drawing.Point(0, 0);
            this.querypanel.Name = "querypanel";
            this.querypanel.Size = new System.Drawing.Size(632, 84);
            this.querypanel.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(453, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 48;
            this.button2.Text = "导出(Excel)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(222, 34);
            this.nudCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(49, 21);
            this.nudCount.TabIndex = 47;
            this.nudCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nudCount_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "未交月数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "类型:";
            // 
            // AuditFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 465);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.querypanel);
            this.Name = "AuditFee";
            this.Text = "水电稽查";
            this.Load += new System.EventHandler(this.AuditFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.querypanel.ResumeLayout(false);
            this.querypanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbbtype;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel querypanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Button button2;
    }
}