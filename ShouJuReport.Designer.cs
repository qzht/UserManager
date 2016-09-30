namespace UserManager
{
    partial class ShouJuReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShouJuReport));
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.txtPayTime = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.txtsfy = new System.Windows.Forms.TextBox();
            this.txtPayMoenyUP = new System.Windows.Forms.TextBox();
            this.txtPayMoney2 = new System.Windows.Forms.TextBox();
            this.txtPayType = new System.Windows.Forms.TextBox();
            this.txtMerUserName = new System.Windows.Forms.TextBox();
            this.txtPayMoeny = new System.Windows.Forms.TextBox();
            this.txtMerName = new System.Windows.Forms.TextBox();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(415, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 30);
            this.button1.TabIndex = 90;
            this.button1.Text = "预 览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(537, 329);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 30);
            this.button5.TabIndex = 89;
            this.button5.Text = "打 印";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(97, 63);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(118, 21);
            this.txtCompany.TabIndex = 91;
            // 
            // txtPayTime
            // 
            this.txtPayTime.Location = new System.Drawing.Point(345, 63);
            this.txtPayTime.Name = "txtPayTime";
            this.txtPayTime.ReadOnly = true;
            this.txtPayTime.Size = new System.Drawing.Size(101, 21);
            this.txtPayTime.TabIndex = 92;
            // 
            // txtOrder
            // 
            this.txtOrder.Location = new System.Drawing.Point(494, 63);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.ReadOnly = true;
            this.txtOrder.Size = new System.Drawing.Size(159, 21);
            this.txtOrder.TabIndex = 93;
            // 
            // txtsfy
            // 
            this.txtsfy.Location = new System.Drawing.Point(94, 293);
            this.txtsfy.Name = "txtsfy";
            this.txtsfy.ReadOnly = true;
            this.txtsfy.Size = new System.Drawing.Size(84, 21);
            this.txtsfy.TabIndex = 94;
            // 
            // txtPayMoenyUP
            // 
            this.txtPayMoenyUP.Location = new System.Drawing.Point(351, 144);
            this.txtPayMoenyUP.Name = "txtPayMoenyUP";
            this.txtPayMoenyUP.ReadOnly = true;
            this.txtPayMoenyUP.Size = new System.Drawing.Size(301, 21);
            this.txtPayMoenyUP.TabIndex = 97;
            // 
            // txtPayMoney2
            // 
            this.txtPayMoney2.Location = new System.Drawing.Point(119, 144);
            this.txtPayMoney2.Name = "txtPayMoney2";
            this.txtPayMoney2.ReadOnly = true;
            this.txtPayMoney2.Size = new System.Drawing.Size(84, 21);
            this.txtPayMoney2.TabIndex = 98;
            // 
            // txtPayType
            // 
            this.txtPayType.Location = new System.Drawing.Point(119, 117);
            this.txtPayType.Name = "txtPayType";
            this.txtPayType.ReadOnly = true;
            this.txtPayType.Size = new System.Drawing.Size(84, 21);
            this.txtPayType.TabIndex = 99;
            // 
            // txtMerUserName
            // 
            this.txtMerUserName.Location = new System.Drawing.Point(119, 90);
            this.txtMerUserName.Name = "txtMerUserName";
            this.txtMerUserName.ReadOnly = true;
            this.txtMerUserName.Size = new System.Drawing.Size(84, 21);
            this.txtMerUserName.TabIndex = 100;
            // 
            // txtPayMoeny
            // 
            this.txtPayMoeny.Location = new System.Drawing.Point(349, 117);
            this.txtPayMoeny.Name = "txtPayMoeny";
            this.txtPayMoeny.ReadOnly = true;
            this.txtPayMoeny.Size = new System.Drawing.Size(184, 21);
            this.txtPayMoeny.TabIndex = 101;
            // 
            // txtMerName
            // 
            this.txtMerName.Location = new System.Drawing.Point(348, 90);
            this.txtMerName.Name = "txtMerName";
            this.txtMerName.ReadOnly = true;
            this.txtMerName.Size = new System.Drawing.Size(184, 21);
            this.txtMerName.TabIndex = 102;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(30, 175);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(623, 102);
            this.richTextBox1.TabIndex = 103;
            this.richTextBox1.Text = "";
            // 
            // ShouJuReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(693, 372);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.txtMerName);
            this.Controls.Add(this.txtPayMoeny);
            this.Controls.Add(this.txtMerUserName);
            this.Controls.Add(this.txtPayType);
            this.Controls.Add(this.txtPayMoney2);
            this.Controls.Add(this.txtPayMoenyUP);
            this.Controls.Add(this.txtsfy);
            this.Controls.Add(this.txtOrder);
            this.Controls.Add(this.txtPayTime);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Name = "ShouJuReport";
            this.Text = "收费收据";
            this.Load += new System.EventHandler(this.ShouJuReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TextBox txtPayTime;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.TextBox txtsfy;
        private System.Windows.Forms.TextBox txtPayMoenyUP;
        private System.Windows.Forms.TextBox txtPayMoney2;
        private System.Windows.Forms.TextBox txtPayType;
        private System.Windows.Forms.TextBox txtMerUserName;
        private System.Windows.Forms.TextBox txtPayMoeny;
        private System.Windows.Forms.TextBox txtMerName;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}