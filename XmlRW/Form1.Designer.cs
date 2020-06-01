namespace XmlRW
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnDosya = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnAktar = new System.Windows.Forms.Button();
            this.dgFile = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxRows = new System.Windows.Forms.TextBox();
            this.tbxQuantity = new System.Windows.Forms.TextBox();
            this.cbxExport = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxFisno = new System.Windows.Forms.TextBox();
            this.tbxOrderNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxExRefno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpTarih = new System.Windows.Forms.DateTimePicker();
            this.dtpFabCikis = new System.Windows.Forms.DateTimePicker();
            this.dtpTerTarih = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxSevkTip = new System.Windows.Forms.ComboBox();
            this.btnKontrol = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnIptal = new System.Windows.Forms.Button();
            this.rbKayit = new System.Windows.Forms.RadioButton();
            this.rbIptal = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDosya
            // 
            this.btnDosya.Location = new System.Drawing.Point(1072, 135);
            this.btnDosya.Name = "btnDosya";
            this.btnDosya.Size = new System.Drawing.Size(121, 38);
            this.btnDosya.TabIndex = 0;
            this.btnDosya.Text = "Dosya Seç";
            this.btnDosya.UseVisualStyleBackColor = true;
            this.btnDosya.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(40, 14);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(987, 20);
            this.txtFile.TabIndex = 1;
            // 
            // btnAktar
            // 
            this.btnAktar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAktar.Location = new System.Drawing.Point(1072, 179);
            this.btnAktar.Name = "btnAktar";
            this.btnAktar.Size = new System.Drawing.Size(121, 33);
            this.btnAktar.TabIndex = 47;
            this.btnAktar.Text = "Yeni Kayıt";
            this.btnAktar.UseVisualStyleBackColor = true;
            this.btnAktar.Click += new System.EventHandler(this.btnAktar_Click);
            // 
            // dgFile
            // 
            this.dgFile.AllowUserToAddRows = false;
            this.dgFile.AllowUserToDeleteRows = false;
            this.dgFile.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dgFile.Location = new System.Drawing.Point(12, 40);
            this.dgFile.Name = "dgFile";
            this.dgFile.ReadOnly = true;
            this.dgFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgFile.Size = new System.Drawing.Size(1015, 442);
            this.dgFile.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Top. Miktar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Satır Sayısı:";
            // 
            // tbxRows
            // 
            this.tbxRows.Location = new System.Drawing.Point(82, 18);
            this.tbxRows.Name = "tbxRows";
            this.tbxRows.Size = new System.Drawing.Size(39, 20);
            this.tbxRows.TabIndex = 51;
            // 
            // tbxQuantity
            // 
            this.tbxQuantity.Location = new System.Drawing.Point(206, 18);
            this.tbxQuantity.Name = "tbxQuantity";
            this.tbxQuantity.Size = new System.Drawing.Size(39, 20);
            this.tbxQuantity.TabIndex = 52;
            // 
            // cbxExport
            // 
            this.cbxExport.FormattingEnabled = true;
            this.cbxExport.Location = new System.Drawing.Point(80, 77);
            this.cbxExport.Name = "cbxExport";
            this.cbxExport.Size = new System.Drawing.Size(88, 21);
            this.cbxExport.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Export Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Gürmen Fişno:";
            // 
            // tbxFisno
            // 
            this.tbxFisno.Location = new System.Drawing.Point(80, 49);
            this.tbxFisno.Name = "tbxFisno";
            this.tbxFisno.Size = new System.Drawing.Size(88, 20);
            this.tbxFisno.TabIndex = 56;
            // 
            // tbxOrderNo
            // 
            this.tbxOrderNo.Location = new System.Drawing.Point(80, 131);
            this.tbxOrderNo.Name = "tbxOrderNo";
            this.tbxOrderNo.Size = new System.Drawing.Size(88, 20);
            this.tbxOrderNo.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Order No:";
            // 
            // tbxExRefno
            // 
            this.tbxExRefno.Location = new System.Drawing.Point(80, 104);
            this.tbxExRefno.Name = "tbxExRefno";
            this.tbxExRefno.Size = new System.Drawing.Size(88, 20);
            this.tbxExRefno.TabIndex = 60;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "Export RefNo:";
            // 
            // dtpTarih
            // 
            this.dtpTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTarih.Location = new System.Drawing.Point(82, 189);
            this.dtpTarih.MaxDate = new System.DateTime(2021, 2, 1, 0, 0, 0, 0);
            this.dtpTarih.MinDate = new System.DateTime(2019, 11, 1, 0, 0, 0, 0);
            this.dtpTarih.Name = "dtpTarih";
            this.dtpTarih.Size = new System.Drawing.Size(96, 20);
            this.dtpTarih.TabIndex = 61;
            // 
            // dtpFabCikis
            // 
            this.dtpFabCikis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFabCikis.Location = new System.Drawing.Point(82, 215);
            this.dtpFabCikis.MaxDate = new System.DateTime(2021, 2, 1, 0, 0, 0, 0);
            this.dtpFabCikis.MinDate = new System.DateTime(2019, 11, 1, 0, 0, 0, 0);
            this.dtpFabCikis.Name = "dtpFabCikis";
            this.dtpFabCikis.Size = new System.Drawing.Size(96, 20);
            this.dtpFabCikis.TabIndex = 62;
            // 
            // dtpTerTarih
            // 
            this.dtpTerTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTerTarih.Location = new System.Drawing.Point(82, 241);
            this.dtpTerTarih.MaxDate = new System.DateTime(2021, 2, 1, 0, 0, 0, 0);
            this.dtpTerTarih.MinDate = new System.DateTime(2019, 11, 1, 0, 0, 0, 0);
            this.dtpTerTarih.Name = "dtpTerTarih";
            this.dtpTerTarih.Size = new System.Drawing.Size(96, 20);
            this.dtpTerTarih.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Tarih:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Fabrika Çıkış:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 66;
            this.label9.Text = "Teslim Tarihi:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 68;
            this.label10.Text = "Sevk Tipi:";
            // 
            // cbxSevkTip
            // 
            this.cbxSevkTip.FormattingEnabled = true;
            this.cbxSevkTip.Items.AddRange(new object[] {
            "Tır",
            "Mega Tır",
            "Konteyner 20DC",
            "Konteyner 40DC",
            "Konteyner 40 High Cube"});
            this.cbxSevkTip.Location = new System.Drawing.Point(80, 157);
            this.cbxSevkTip.Name = "cbxSevkTip";
            this.cbxSevkTip.Size = new System.Drawing.Size(88, 21);
            this.cbxSevkTip.TabIndex = 67;
            // 
            // btnKontrol
            // 
            this.btnKontrol.Location = new System.Drawing.Point(1199, 135);
            this.btnKontrol.Name = "btnKontrol";
            this.btnKontrol.Size = new System.Drawing.Size(111, 38);
            this.btnKontrol.TabIndex = 69;
            this.btnKontrol.Text = "Kontrol Et";
            this.btnKontrol.UseVisualStyleBackColor = true;
            this.btnKontrol.Click += new System.EventHandler(this.btnKontrol_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::XmlRW.Properties.Resources.prolinelogo;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(1072, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(238, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            // 
            // btnIptal
            // 
            this.btnIptal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIptal.Location = new System.Drawing.Point(1199, 179);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(111, 33);
            this.btnIptal.TabIndex = 70;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // rbKayit
            // 
            this.rbKayit.AutoSize = true;
            this.rbKayit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbKayit.Location = new System.Drawing.Point(1080, 108);
            this.rbKayit.Name = "rbKayit";
            this.rbKayit.Size = new System.Drawing.Size(99, 21);
            this.rbKayit.TabIndex = 71;
            this.rbKayit.TabStop = true;
            this.rbKayit.Text = "Yeni Kayıt";
            this.rbKayit.UseVisualStyleBackColor = true;
            this.rbKayit.CheckedChanged += new System.EventHandler(this.rbKayit_CheckedChanged);
            // 
            // rbIptal
            // 
            this.rbIptal.AutoSize = true;
            this.rbIptal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIptal.Location = new System.Drawing.Point(1219, 108);
            this.rbIptal.Name = "rbIptal";
            this.rbIptal.Size = new System.Drawing.Size(57, 21);
            this.rbIptal.TabIndex = 72;
            this.rbIptal.TabStop = true;
            this.rbIptal.Text = "İptal";
            this.rbIptal.UseVisualStyleBackColor = true;
            this.rbIptal.CheckedChanged += new System.EventHandler(this.rbIptal_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxRows);
            this.groupBox1.Controls.Add(this.tbxQuantity);
            this.groupBox1.Controls.Add(this.cbxExport);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxSevkTip);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbxFisno);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbxOrderNo);
            this.groupBox1.Controls.Add(this.dtpTerTarih);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dtpFabCikis);
            this.groupBox1.Controls.Add(this.tbxExRefno);
            this.groupBox1.Controls.Add(this.dtpTarih);
            this.groupBox1.Location = new System.Drawing.Point(1051, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 264);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Üst Bilgiler";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 497);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rbIptal);
            this.Controls.Add(this.rbKayit);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKontrol);
            this.Controls.Add(this.dgFile);
            this.Controls.Add(this.btnAktar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnDosya);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Xml To Order 1.15";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDosya;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnAktar;
        private System.Windows.Forms.DataGridView dgFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxQuantity;
        private System.Windows.Forms.TextBox tbxRows;
        private System.Windows.Forms.ComboBox cbxExport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxFisno;
        private System.Windows.Forms.TextBox tbxOrderNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxExRefno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpTarih;
        private System.Windows.Forms.DateTimePicker dtpFabCikis;
        private System.Windows.Forms.DateTimePicker dtpTerTarih;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxSevkTip;
        private System.Windows.Forms.Button btnKontrol;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.RadioButton rbKayit;
        private System.Windows.Forms.RadioButton rbIptal;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

