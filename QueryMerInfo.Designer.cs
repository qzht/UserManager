namespace UserManager
{
    partial class QueryMerInfo
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
            this.cbbfour = new System.Windows.Forms.ComboBox();
            this.cbbthree = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbbtwo = new System.Windows.Forms.ComboBox();
            this.cbbone = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.querypanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.querypanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbfour
            // 
            this.cbbfour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbbfour.FormattingEnabled = true;
            this.cbbfour.Location = new System.Drawing.Point(314, 32);
            this.cbbfour.Name = "cbbfour";
            this.cbbfour.Size = new System.Drawing.Size(60, 20);
            this.cbbfour.TabIndex = 27;
            // 
            // cbbthree
            // 
            this.cbbthree.FormattingEnabled = true;
            this.cbbthree.Location = new System.Drawing.Point(248, 32);
            this.cbbthree.Name = "cbbthree";
            this.cbbthree.Size = new System.Drawing.Size(60, 20);
            this.cbbthree.TabIndex = 26;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 84);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(660, 335);
            this.dataGridView1.TabIndex = 4;
            // 
            // cbbtwo
            // 
            this.cbbtwo.FormattingEnabled = true;
            this.cbbtwo.Location = new System.Drawing.Point(182, 32);
            this.cbbtwo.Name = "cbbtwo";
            this.cbbtwo.Size = new System.Drawing.Size(60, 20);
            this.cbbtwo.TabIndex = 25;
            // 
            // cbbone
            // 
            this.cbbone.FormattingEnabled = true;
            this.cbbone.Location = new System.Drawing.Point(75, 33);
            this.cbbone.Name = "cbbone";
            this.cbbone.Size = new System.Drawing.Size(100, 20);
            this.cbbone.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(428, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // querypanel
            // 
            this.querypanel.Controls.Add(this.cbbfour);
            this.querypanel.Controls.Add(this.cbbthree);
            this.querypanel.Controls.Add(this.cbbtwo);
            this.querypanel.Controls.Add(this.cbbone);
            this.querypanel.Controls.Add(this.button1);
            this.querypanel.Controls.Add(this.label4);
            this.querypanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.querypanel.Location = new System.Drawing.Point(0, 0);
            this.querypanel.Name = "querypanel";
            this.querypanel.Size = new System.Drawing.Size(660, 84);
            this.querypanel.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "商户:";
            // 
            // QueryMerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 419);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.querypanel);
            this.Name = "QueryMerInfo";
            this.Text = "商户查询";
            this.Load += new System.EventHandler(this.QueryMerInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.querypanel.ResumeLayout(false);
            this.querypanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbfour;
        private System.Windows.Forms.ComboBox cbbthree;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbbtwo;
        private System.Windows.Forms.ComboBox cbbone;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel querypanel;
        private System.Windows.Forms.Label label4;
    }
}