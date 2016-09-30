namespace UserManager
{
    partial class Recharge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recharge));
            this.nudyue = new System.Windows.Forms.NumericUpDown();
            this.button5 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtoldyue = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtcarid = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtxm = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtsfz = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtmerName = new System.Windows.Forms.TextBox();
            this.txtbuyDiancount = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.nudDianBuyMoney = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDianPrice = new System.Windows.Forms.TextBox();
            this.txtDianTotal = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtbuyWatercount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtWaterOrder = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.nudWaterBuyMoney = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtWaterTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWaterPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDianOrder = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtYueOrder = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudyue)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDianBuyMoney)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaterBuyMoney)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudyue
            // 
            this.nudyue.Location = new System.Drawing.Point(128, 110);
            this.nudyue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudyue.Name = "nudyue";
            this.nudyue.Size = new System.Drawing.Size(120, 21);
            this.nudyue.TabIndex = 39;
            this.nudyue.ValueChanged += new System.EventHandler(this.nudyue_ValueChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(343, 67);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 30);
            this.button5.TabIndex = 35;
            this.button5.Text = "查看余额";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "元";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(69, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 30;
            this.label11.Text = "原余额：";
            // 
            // txtoldyue
            // 
            this.txtoldyue.Enabled = false;
            this.txtoldyue.Location = new System.Drawing.Point(128, 67);
            this.txtoldyue.Name = "txtoldyue";
            this.txtoldyue.ReadOnly = true;
            this.txtoldyue.Size = new System.Drawing.Size(121, 21);
            this.txtoldyue.TabIndex = 29;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(343, 118);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 30);
            this.button6.TabIndex = 26;
            this.button6.Text = "交费";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(59, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 24;
            this.label12.Text = "充值金额：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(347, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 30);
            this.button2.TabIndex = 38;
            this.button2.Text = "读水卡";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtcarid
            // 
            this.txtcarid.Location = new System.Drawing.Point(64, 25);
            this.txtcarid.Name = "txtcarid";
            this.txtcarid.ReadOnly = true;
            this.txtcarid.Size = new System.Drawing.Size(120, 21);
            this.txtcarid.TabIndex = 37;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(21, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 36;
            this.label19.Text = "卡号：";
            // 
            // txtxm
            // 
            this.txtxm.Location = new System.Drawing.Point(64, 54);
            this.txtxm.Name = "txtxm";
            this.txtxm.ReadOnly = true;
            this.txtxm.Size = new System.Drawing.Size(120, 21);
            this.txtxm.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 40;
            this.label9.Text = "姓名：";
            // 
            // txtsfz
            // 
            this.txtsfz.Location = new System.Drawing.Point(64, 110);
            this.txtsfz.Name = "txtsfz";
            this.txtsfz.ReadOnly = true;
            this.txtsfz.Size = new System.Drawing.Size(259, 21);
            this.txtsfz.TabIndex = 43;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(-1, 115);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 42;
            this.label20.Text = "身份证号：";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button10);
            this.groupBox6.Controls.Add(this.txtsfz);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.txtmerName);
            this.groupBox6.Controls.Add(this.txtcarid);
            this.groupBox6.Controls.Add(this.txtxm);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Location = new System.Drawing.Point(28, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(469, 152);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "个人信息";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(347, 97);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(87, 30);
            this.button10.TabIndex = 44;
            this.button10.Text = "读电卡";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "商户：";
            // 
            // txtmerName
            // 
            this.txtmerName.Location = new System.Drawing.Point(64, 81);
            this.txtmerName.Name = "txtmerName";
            this.txtmerName.ReadOnly = true;
            this.txtmerName.Size = new System.Drawing.Size(259, 21);
            this.txtmerName.TabIndex = 37;
            // 
            // txtbuyDiancount
            // 
            this.txtbuyDiancount.Location = new System.Drawing.Point(142, 142);
            this.txtbuyDiancount.Name = "txtbuyDiancount";
            this.txtbuyDiancount.ReadOnly = true;
            this.txtbuyDiancount.Size = new System.Drawing.Size(121, 21);
            this.txtbuyDiancount.TabIndex = 54;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(78, 145);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(59, 12);
            this.label25.TabIndex = 53;
            this.label25.Text = "购买次数:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(260, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 12);
            this.label21.TabIndex = 47;
            this.label21.Text = "元";
            // 
            // nudDianBuyMoney
            // 
            this.nudDianBuyMoney.Location = new System.Drawing.Point(140, 34);
            this.nudDianBuyMoney.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudDianBuyMoney.Name = "nudDianBuyMoney";
            this.nudDianBuyMoney.Size = new System.Drawing.Size(120, 21);
            this.nudDianBuyMoney.TabIndex = 47;
            this.nudDianBuyMoney.ValueChanged += new System.EventHandler(this.nudDianBuyMoney_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 51;
            this.label5.Text = "元";
            // 
            // txtDianPrice
            // 
            this.txtDianPrice.Location = new System.Drawing.Point(140, 70);
            this.txtDianPrice.Name = "txtDianPrice";
            this.txtDianPrice.ReadOnly = true;
            this.txtDianPrice.Size = new System.Drawing.Size(121, 21);
            this.txtDianPrice.TabIndex = 48;
            // 
            // txtDianTotal
            // 
            this.txtDianTotal.Location = new System.Drawing.Point(141, 106);
            this.txtDianTotal.Name = "txtDianTotal";
            this.txtDianTotal.ReadOnly = true;
            this.txtDianTotal.Size = new System.Drawing.Size(121, 21);
            this.txtDianTotal.TabIndex = 50;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(102, 111);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(35, 12);
            this.label22.TabIndex = 49;
            this.label22.Text = "购电:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(65, 74);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 12);
            this.label23.TabIndex = 47;
            this.label23.Text = "一度电价格:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(78, 38);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 12);
            this.label24.TabIndex = 10;
            this.label24.Text = "充值金额:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "充值";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(28, 192);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(483, 245);
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtbuyWatercount);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtWaterOrder);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.nudWaterBuyMoney);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.txtWaterTotal);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.txtWaterPrice);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(475, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "水卡充值";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtbuyWatercount
            // 
            this.txtbuyWatercount.Location = new System.Drawing.Point(139, 144);
            this.txtbuyWatercount.Name = "txtbuyWatercount";
            this.txtbuyWatercount.ReadOnly = true;
            this.txtbuyWatercount.Size = new System.Drawing.Size(121, 21);
            this.txtbuyWatercount.TabIndex = 65;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(75, 147);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 64;
            this.label15.Text = "购买次数:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(85, 183);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 62;
            this.label13.Text = "流水号:";
            // 
            // txtWaterOrder
            // 
            this.txtWaterOrder.Location = new System.Drawing.Point(138, 178);
            this.txtWaterOrder.Name = "txtWaterOrder";
            this.txtWaterOrder.ReadOnly = true;
            this.txtWaterOrder.Size = new System.Drawing.Size(121, 21);
            this.txtWaterOrder.TabIndex = 63;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(343, 135);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 30);
            this.button3.TabIndex = 41;
            this.button3.Text = "预 览 打 印";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "一立方水价格：";
            // 
            // nudWaterBuyMoney
            // 
            this.nudWaterBuyMoney.Location = new System.Drawing.Point(139, 41);
            this.nudWaterBuyMoney.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudWaterBuyMoney.Name = "nudWaterBuyMoney";
            this.nudWaterBuyMoney.Size = new System.Drawing.Size(120, 21);
            this.nudWaterBuyMoney.TabIndex = 40;
            this.nudWaterBuyMoney.ValueChanged += new System.EventHandler(this.nudWaterBuyMoney_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(68, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "充值金额：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(267, 117);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 12);
            this.label18.TabIndex = 38;
            this.label18.Text = "立方米";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(343, 68);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 30);
            this.button4.TabIndex = 26;
            this.button4.Text = "充 值";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtWaterTotal
            // 
            this.txtWaterTotal.Location = new System.Drawing.Point(139, 113);
            this.txtWaterTotal.Name = "txtWaterTotal";
            this.txtWaterTotal.ReadOnly = true;
            this.txtWaterTotal.Size = new System.Drawing.Size(121, 21);
            this.txtWaterTotal.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "元";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(92, 116);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 36;
            this.label17.Text = "购水：";
            // 
            // txtWaterPrice
            // 
            this.txtWaterPrice.Location = new System.Drawing.Point(139, 77);
            this.txtWaterPrice.Name = "txtWaterPrice";
            this.txtWaterPrice.ReadOnly = true;
            this.txtWaterPrice.Size = new System.Drawing.Size(121, 21);
            this.txtWaterPrice.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "元";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtDianOrder);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.txtbuyDiancount);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.nudDianBuyMoney);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtDianTotal);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtDianPrice);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(475, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "电卡充值";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(87, 182);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 62;
            this.label14.Text = "流水号:";
            // 
            // txtDianOrder
            // 
            this.txtDianOrder.Location = new System.Drawing.Point(140, 177);
            this.txtDianOrder.Name = "txtDianOrder";
            this.txtDianOrder.ReadOnly = true;
            this.txtDianOrder.Size = new System.Drawing.Size(121, 21);
            this.txtDianOrder.TabIndex = 63;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(343, 135);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(87, 30);
            this.button7.TabIndex = 55;
            this.button7.Text = "预 览 打 印";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtYueOrder);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.nudyue);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.txtoldyue);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(475, 219);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "余额充值";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(126, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 12);
            this.label16.TabIndex = 67;
            this.label16.Text = "*点击读水卡";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 60;
            this.label6.Text = "流水号:";
            // 
            // txtYueOrder
            // 
            this.txtYueOrder.Location = new System.Drawing.Point(127, 149);
            this.txtYueOrder.Name = "txtYueOrder";
            this.txtYueOrder.ReadOnly = true;
            this.txtYueOrder.Size = new System.Drawing.Size(121, 21);
            this.txtYueOrder.TabIndex = 61;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(343, 164);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(87, 30);
            this.button8.TabIndex = 42;
            this.button8.Text = "打印";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Recharge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 454);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Recharge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "充值";
            this.Load += new System.EventHandler(this.Recharge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudyue)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDianBuyMoney)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaterBuyMoney)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtoldyue;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtcarid;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nudyue;
        private System.Windows.Forms.TextBox txtxm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtsfz;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown nudDianBuyMoney;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDianPrice;
        private System.Windows.Forms.TextBox txtDianTotal;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.TextBox txtbuyDiancount;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtmerName;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtYueOrder;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDianOrder;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtbuyWatercount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWaterOrder;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudWaterBuyMoney;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtWaterTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtWaterPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
    }
}