using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Edm;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Xml;
using Exportable.Engines;
using Exportable.Engines.Excel;
using XmlRW.Entities;
using Microsoft.VisualBasic;

namespace XmlRW
{
    public partial class Form1 : Form
    {

        private static string Database = "GURMENPVC2020";
        private static string ProDatabase = "PAZARLAMA2020";
        private static string ServerName = @"GURMENSRV02\GURMENSQLSERVER";
        private static string SqlUser = "sa";
        private static string SqlPass = "Grm20Sql19";
        public static string ConnStr = "server=" + ServerName + ";initial catalog=" +
                                       Database + ";integrated security=false;uid=" +
                                       SqlUser + ";password=" + SqlPass + ";";

        private static bool _error = false;
        private static bool _controlled = false;
        public Form1()
        {
            InitializeComponent();
        }
        List<Orders> orders = new List<Orders>();
        public void Dosya_Yukle(string pDosyaYolu)
        {
            string dosya = pDosyaYolu;
            XmlDocument xd = new XmlDocument();
            xd.Load(dosya);
            XmlNode xn = xd.SelectSingleNode("orderlist");
            if (xn == null)
            {
                MessageBox.Show("Orderlist etiketi bulunamadı. Doğru xml seçtiğinize emin olun!", "Hata");
                return;
            }
            XmlNodeList xns = xn.SelectNodes("order");
            if (xns == null)
            {
                MessageBox.Show("Order etiketi bulunamadı. Doğru xml seçtiğinize emin olun!", "Hata");
                return;
            }

            if (orders.Any())
            {
                orders.Clear();
                dgFile.DataSource = null;
            }

            foreach (XmlNode xNode in xns)
            {
                XmlNodeList xnRows = xNode.SelectNodes("row");
                string note = "";
                XmlNode xnNote = xNode.SelectSingleNode("notes");
                if (xnNote != null)
                {
                    note = xnNote.InnerText;
                }

                if (xnRows == null)
                {
                    continue;
                }


                foreach (XmlNode xnRow in xnRows)
                {

                    Orders order = new Orders()
                    {
                        DelNumber = xNode.Attributes["number"].Value,
                        BuyerRef = xnRow.Attributes["buyer_ref"].Value,
                        RowId = xnRow.Attributes["unique_rowid"].Value,
                        CustomerRef = xNode.Attributes["customer_ref"].Value,
                        DelStore = xNode.Attributes["store_id"].Value,
                        DelFrom = xNode.Attributes["delivery_date_from"].Value,
                        DelTo = xNode.Attributes["delivery_date_to"].Value,
                        Quantity = Convert.ToDouble(xnRow.Attributes["quantity"].Value.Replace(".", ",")),
                        Price = Convert.ToDouble(xnRow.Attributes["price"].Value.Replace(".", ",")),
                        IssueDate = Convert.ToDateTime(xNode.Attributes["issue_date"].Value),
                        Description = xnRow.Attributes["description"].Value.Length>150? xnRow.Attributes["description"].Value.Substring(0,140): xnRow.Attributes["description"].Value,
                        Note = note,
                        WillFlow = false,
                        IsNotPassed = false,
                        DidFlow = false,
                        WeekOfYear = GetIso8601WeekNumber(Convert.ToDateTime(xNode.Attributes["issue_date"].Value))
                    };
                    if (!xnRow.Attributes["buyer_ref"].Value.StartsWith("36"))
                    {
                        string aciklama = "Sipno:"+xNode.Attributes["number"].Value.ToString() +"-Urun Kodu:"+xnRow.Attributes["buyer_ref"].Value.ToString()+"-Aciklama: " + xnRow.Attributes["description"].Value.ToString();
                        MessageBox.Show("Özel Ürün İsteği bulundu. Lütfen yeni açılacak kutucuğa açıklamaya göre ilgili stok kodunu giriniz. Akabinde açıklamasını giriniz. Aksi halde aktarım olmayacaktır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        order.BuyerRef = Interaction.InputBox(aciklama, "Lütfen Stok Kodunu Giriniz.");
                        order.TurkNote = Interaction.InputBox(aciklama, "Lütfen Türkçe Kalem Açıklaması Giriniz.");
                    }
                    orders.Add(order);
                }
            }
            dgFile.DataSource = orders;

            tbxRows.Enabled = false;
            tbxQuantity.Enabled = false;
            tbxFisno.Enabled = false;
            dgFile.Refresh();

        }
        public string CreateNumber()
        {
            string fisno = "";
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string yil = DateTime.Now.Year.ToString();
                SqlCommand cmd = new SqlCommand(@"SELECT ISNULL((SELECT TOP 1 'D'+@yil+RIGHT('000000'+CONVERT(VARCHAR(15),RIGHT(GURMENFISNO,5)+1),6) FROM (
                SELECT GURMENFISNO FROM FLOW_ONSIPMAS  WHERE GURMENFISNO LIKE 'D%' AND LEN(GURMENFISNO) > 10
                UNION ALL
                SELECT FATIRSNO FROM FLOW_FISNORES WHERE TIPI = 'DS' AND FATIRSNO LIKE 'D%' AND LEN(FATIRSNO) > 10
                    )Z WHERE SUBSTRING(GURMENFISNO, 2, 4) =@yil
                    ORDER BY GURMENFISNO DESC),'D' +@yil +'000001') FISNO
                ", conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@yil", yil);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fisno = reader["FISNO"].ToString();
                }
                reader.Close();
                cmd.CommandText = @"INSERT INTO FLOW_FISNORES(FATIRSNO,TIPI)
                SELECT @fisno,'DS'
                ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@fisno", fisno);
                cmd.ExecuteNonQuery();

            }
            return fisno;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "XML Dosyası |*.xml";
            file.FilterIndex = 2;
            file.CheckFileExists = false;
            file.Title = "XML Dosyası Seçiniz..";
            try
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = file.FileName;
                    Dosya_Yukle(txtFile.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA OLUŞTU : " + ex.ToString(), "Bilgi");
            }
        }
        private void btnAktar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_controlled)
                {
                    MessageBox.Show("Lütfen önce Kontrol Et butonuna basarak kayıtların doğruluğunu kontrol ediniz.");
                    return;
                }

                if (_error)
                {
                    MessageBox.Show(
                        "Hatalı stok kodu söz konusudur. Lütfen tüm stok kodlarının tanımlı olduğundan emin olunuz. Log txt dosyasını inceleyiniz.",
                        "Hata");

                }
                var firstOne = orders.First();
                List<FLOW_ONSIPDET> sipDetList = new List<FLOW_ONSIPDET>();
                FLOW_ONSIPMAS sipMas = new FLOW_ONSIPMAS()
                {
                    FISNO = tbxOrderNo.Text,
                    AKTIF = "E",
                    TIPI = "YURTDISI",
                    CARI = "A-20-01-0402",
                    EXPORTREFNO = tbxExRefno.Text,
                    EXPORTTYPE = cbxExport.SelectedValue.ToString(),
                    FABRIKACIKIS = dtpFabCikis.Value.Date,
                    GENISK = 0,
                    GURMENFISNO = tbxFisno.Text,
                    INS_ID = tbxFisno.Text,
                    KACINCISIPARIS = "0",
                    KAYITTARIHI = dtpTarih.Value.Date,
                    KAYITYAPAN = "XML App",
                    KAYIT_DURUMU = "Yeni Siparis",
                    MUSTERIADI = "LEROY MERLIN ITALIA S.R.L (EURO)",
                    OZELKOD = "1",
                    PLASIYER = "3504",
                    REVIZEINSID = "0",
                    SEVKTIPI = cbxSevkTip.SelectedText,
                    SIRKET = ProDatabase,
                    TARIH = firstOne.IssueDate,
                    TERMINTARIHI = dtpTerTarih.Value.Date
                };
                using (GURMENPVCEntities db = new GURMENPVCEntities())
                {
                    var filteredOrders = orders.Where(a => a.WillFlow).ToList();
                    foreach (var el in filteredOrders)
                    {
                        FLOW_ONSIPDET sipDet = new FLOW_ONSIPDET()
                        {
                            FISNO = sipMas.FISNO,
                            AKTIF = "E",
                            INS_ID = tbxFisno.Text,
                            ACIKLAMA = el.DelNumber,
                            BFIYAT = Convert.ToDecimal(el.Price),
                            DEPO_KOD = "400",
                            DOVIZTIP = 1,
                            FIYAT = Convert.ToDecimal(el.Price),
                            MAGAZA = el.DelStore,
                            MARKA = "X",
                            MIKTAR = Convert.ToDecimal(el.Quantity),
                            OBR = 1,
                            SATICI = "S-50-01-0006",
                            SATISK = 0,
                            STOK_KOD = el.OurRef,
                            SIPNO = el.RowId,
                            TESLIM_MIKTAR = 0,
                            KALEMNOTU = el.Description,
                            TURKCENOT = el.TurkNote


                        };
                        sipDetList.Add(sipDet);
                    }

                    db.FLOW_ONSIPMAS.Add(sipMas);
                    db.SaveChanges();
                    db.FLOW_ONSIPDET.AddRange(sipDetList);
                    db.SaveChanges();
                    Email(CreateHtml("YENİ KAYIT!"), "YENİ KAYIT!");//aktarılanları mail at emri gönderiyor
                    if (orders.Any(a => a.IsNotPassed))//600ün altında kalan varsa kalanları mail at emri gönderiyor
                    {
                        Email(CreateHtml("600ün altında kalanlar!", true), "600ün altında kalanlar!", true);
                    }
                    MessageBox.Show("Aktarım Başarıyla Tamamlandı.", "Aktarım Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dgFile.DataSource = null;
                    orders.Clear();
                    tbxFisno.Text = "";
                    tbxOrderNo.Text = "";
                    tbxExRefno.Text = "";
                    tbxQuantity.Text = "0";
                    tbxRows.Text = "0";

                }

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    MessageBox.Show(eve.Entry.Entity.GetType().Name+" ------ "+ eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show(ve.PropertyName+" ---- "+ ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                LogYaz(exception.Message);
            }
        }
        public void LogYaz(string _log)//log txt dosyasına logları atar
        {


            using (StreamWriter writetext = new StreamWriter("log.txt", true))
            {

                writetext.WriteLine(DateTime.Now + " " + _log);
                writetext.WriteLine("--------------------------");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            rbKayit.ForeColor = Color.DarkGreen;
            rbIptal.ForeColor = Color.Red;
            btnAktar.BackColor = Color.Green;
            btnIptal.BackColor = Color.Red;
            rbKayit.Checked = true;
            EditForm();

            List<ExportType> exTypes = new List<ExportType>()
            {

                new ExportType(){Val = "1",Text = "FOB"},
                new ExportType(){Val = "2",Text = "CIF"},
                new ExportType(){Val = "3",Text = "CF"},
                new ExportType(){Val = "4",Text = "FOT"},
                new ExportType(){Val = "5",Text = "İhr.kay"},
                new ExportType(){Val = "6",Text = "DAF"},
                new ExportType(){Val = "7",Text = "EXW"},
                new ExportType(){Val = "8",Text = "İhr.Kur.F."},
                new ExportType(){Val = "9",Text = "CIP"},
                new ExportType(){Val = "10",Text = "CPT"},
                new ExportType(){Val = "11",Text = "DAT"},
                new ExportType(){Val = "12",Text = "DAP"},
                new ExportType(){Val = "13",Text = "DDP"},
                new ExportType(){Val = "14",Text = "DES"},
                new ExportType(){Val = "15",Text = "DEQ"},
                new ExportType(){Val = "16",Text = "DDU"},
                new ExportType(){Val = "17",Text = "FCA"},
                new ExportType(){Val = "18",Text = "FAS"},
                new ExportType(){Val = "19",Text = "CFR"}
            };
            cbxExport.DataSource = exTypes;
            cbxExport.DisplayMember = "Text";
            cbxExport.ValueMember = "Val";
            cbxExport.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSevkTip.DropDownStyle = ComboBoxStyle.DropDownList;
            dtpTarih.Value = DateTime.Now.Date;
            dtpTarih.Enabled = false;
            cbxExport.SelectedIndex = 12;
        }
        private void btnKontrol_Click(object sender, EventArgs e)
        {
            btnKontrol.Text = "Lütfen Bekleyiniz!";
            Kontrol();
            _controlled = true;
            dgFile.Refresh();
            btnKontrol.Text = "Kontrol Et";
        }
        public void Kontrol()
        {
            _error = false;

            if (tbxOrderNo.Text == "" || tbxExRefno.Text == "" || cbxSevkTip.Text == "" || !orders.Any())
            {
                MessageBox.Show("Order No, Export Refno ve Sevk Tipi, Kalem bilgileri boş geçilemez.");
                return;
            }

            if (dtpTerTarih.Value <= dtpTarih.Value)
            {
                MessageBox.Show("Termin tarihi bugünden büyük olmalıdır.");
                return;
            }
            if (tbxFisno.Text == "") tbxFisno.Text = CreateNumber();
            var firstOne = orders.First();
            using (GURMENPVCEntities db = new GURMENPVCEntities())
            {
                var order = db.FLOW_ONSIPMAS.Where(a => a.AKTIF == "E" && a.FISNO == tbxFisno.Text)
                    .ToList(); //SQL kontrolü girilen fişno kullanıldı mı?
                if (order.Any())
                {
                    MessageBox.Show(
                        tbxFisno.Text +
                        " numarasıyla girilmiş aktif sipariş bulunmaktadır lütfen Benzersiz(Unique) Order numarası oluşturunuz.",
                        "Hata");
                    return;
                }

              

                string hata = "";

                foreach (var el in orders)
                {
                
                    el.WillFlow = true;
                    var stSabit = db.TBLSTSABIT.FirstOrDefault(a => a.URETICI_KODU == el.BuyerRef || a.STOK_KODU == el.BuyerRef);
                    if (stSabit == null)
                    {
                        MessageBox.Show(el.BuyerRef + " artikel numarasına kayıtlı stok kodu bulunamadı.");
                        LogYaz(el.BuyerRef + " artikel numarasına kayıtlı stok kodu bulunamadı.");
                        _error = true;
                        hata = "Stok kartı olmayan kalemler var. Lütfen açılmasını sağlayınız.";
                        el.WillFlow = false;
                        continue;
                    }
                    el.OurRef = stSabit.STOK_KODU;
                    el.OurName = stSabit.STOK_ADI;
                    el.Price = el.BuyerRef.StartsWith("361") ? Convert.ToDouble(stSabit.DOV_SATIS_FIAT) : el.Price;//fiyatı sqlden alıyor eğer 361 ile başlıyorsa
                    var sipDetRow = db.FLOW_ONSIPDET.FirstOrDefault(a => ((a.AKTIF == "E" && a.ACIKLAMA + stSabit.URETICI_KODU == el.DelNumber + el.BuyerRef) || (a.AKTIF == "E" && a.ACIKLAMA + stSabit.STOK_KODU == el.DelNumber + el.BuyerRef)));
                    if (sipDetRow != null)
                    {
                        el.DidFlow = true;//aktarıldı mı işaretle
                        el.WillFlow = false;//aktarılmayacak olarak işaretle
                        el.IsNotPassed = false; //600ü geçmeyenlerden daha önce aktarılanları düşüyorum.
                    }
                }

                var orderWeeks = orders.GroupBy(a => a.WeekOfYear).OrderBy(a=> a.Key).ToList();//siparişin haftalarını alır
                foreach (var el in orderWeeks)//her hafta için mağazaların ilgili haftada 600ü geçip geçmediiğini kontrol eder.
                {
                    var sumByWeekStore = orders.Where(a => a.WeekOfYear == el.Key).GroupBy(a => a.DelStore).
                        Select(a => new { a.Key, Total = a.Sum(s => s.Price * s.Quantity) }).ToList();

                    foreach (var el2 in orders)
                    {
                        if (!el2.DidFlow)//daha önce aktarılmadıysa 600 kontrolüne girer
                        {
                            el2.WillFlow = sumByWeekStore.Any(a => a.Key == el2.DelStore && a.Total >= 600);
                            el2.IsNotPassed = !el2.WillFlow;//geçmedi mi
                        }
                    }

                    var sumByNew = orders.Where(a => a.WeekOfYear <= el.Key && !a.DidFlow).GroupBy(a => a.DelStore).
                        Select(a => new { a.Key, Total = a.Sum(s => s.Price * s.Quantity) }).ToList();//ilgili hafta ve öncesinde aktarılmayan siparişleri grupla.
                    foreach (var el2 in orders)
                    {
                        if (!el2.DidFlow)
                        {
                            el2.WillFlow = sumByNew.Any(a => a.Key == el2.DelStore && a.Total >= 600);
                            el2.IsNotPassed = !el2.WillFlow;//geçmedi mi
                        }
                       
                    }
                    var flowedStores = orders.Where(a => a.DidFlow && a.WeekOfYear==el.Key).ToList();//ilgili haftada daha önce aktarılanı olan mağazalar
                    foreach (var el2 in orders)
                    {
                        if (!el2.DidFlow && flowedStores.Any(a=> a.DelStore==el2.DelStore))//daha önce aktarılanı varsa bu mağazanın bu haftada bu siparişi de aktar
                        {
                            el2.WillFlow=true;
                            el2.IsNotPassed= !el2.WillFlow;
                        }
                    }
                }




                //tutarı 600ü geçen mağazalar son olarak kontrol edilmeli
                //var sumByStore = orders.Where(a=> a.WillFlow).GroupBy(a => a.DelStore).
                //    Select(a => new { a.Key, Total = a.Sum(s => s.Price * s.Quantity) }).ToList();

                //foreach (var el in orders)
                //{
                //    if (el.WillFlow)
                //    {
                //        el.WillFlow = sumByStore.Any(a => a.Key == el.DelStore && a.Total >= 600) ? true : false;
                //        el.IsNotPassed = !el.WillFlow;//tutarı 600ü geçmeyen mağazaları alıp mail atmak için
                //    }
                //}



                if (!orders.Any(a => a.WillFlow))
                {
                    _error = true;
                    hata = "Yüklediğiniz XML dosyadaki tüm kayıtlar daha önce aktarılmış. Aktarılacak yeni kayıt yok. ";
                }
                if (_error)
                {
                    MessageBox.Show(hata + "Aktarım yapılmayacak !! Log txt dosyasını inceleyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tbxQuantity.Text = orders.Where(a => a.WillFlow).Sum(a => a.Quantity).ToString();
                    tbxRows.Text = orders.Count(a => a.WillFlow).ToString();
                    MessageBox.Show("Kontrol Başarılı! " + orders.Count(a => a.WillFlow) + " adet aktarılacak satır bulundu. Aktarım adımına geçilebilir.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void rbKayit_CheckedChanged(object sender, EventArgs e)
        {
            EditForm();
        }
        private void rbIptal_CheckedChanged(object sender, EventArgs e)
        {
            EditForm();
        }
        public void EditForm()
        {
            if (rbKayit.Checked)
            {
                btnIptal.Hide();
                groupBox1.Show();
                btnAktar.Show();
                btnKontrol.Show();
            }
            else
            {
                btnAktar.Hide();
                btnIptal.Show();
                groupBox1.Hide();
                btnKontrol.Hide();
            }
        }
        private void btnIptal_Click(object sender, EventArgs e)
        {
            try
            {
                int say = 0;
                using (GURMENPVCEntities db = new GURMENPVCEntities())
                {
                    if (!orders.Any())
                    {
                        MessageBox.Show("Kayıt bulunamadı. Doğru tip dosya yüklediğinizden emin olunuz.");
                        return;
                    }
                    foreach (var el in orders)
                    {
                        el.WillFlow = false;
                        var willCancel = db.FLOW_ONSIPDET.FirstOrDefault(a => a.SIPNO == el.RowId && a.AKTIF=="E" && a.ACIKLAMA==el.DelNumber);
                        if (willCancel != null)
                        {
                            el.WillFlow = true;
                        }
                    }
                    var cancelList = orders.Where(a => a.WillFlow).ToList();
                   
                    if (cancelList.Any())
                    {
                        foreach (var el in cancelList)
                        {
                            var sipList = db.FLOW_ONSIPDET.FirstOrDefault(a => a.SIPNO == el.RowId && a.AKTIF == "E" && a.ACIKLAMA == el.DelNumber);
                            if (sipList != null)
                            {
                                say++;
                                sipList.AKTIF = "H";
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("İptal etmek istediğiniz kayıtların hiçbiri daha önce sisteme atılmamış.");
                        return;
                    }

                    orders = cancelList.Where(a => a.WillFlow).ToList();
                }
                
                Email(CreateHtml("İptal Edilenler!!!"), "İptal Edilenler!!!");
                string message = orders.Count + " Adet Kayıttan " + say.ToString() + " Adet Kayıt Başarıyla İptal Edildi.";
                MessageBox.Show(message);
                LogYaz(message);
                dgFile.DataSource = null;
                orders.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                LogYaz(exception.Message);
            }
        }
        public string CreateHtml(string tipi, bool rest = false)
        {
            List<Orders> filteredOrders;
            if (!rest)//aktarılanları gönder emri geldiyse aktarılanları tabloya atar
            {
                filteredOrders = orders.Where(a => a.WillFlow).ToList();
            }
            else//600 altında kalanları gönder emri geldiyse 
            {
                filteredOrders = orders.Where(a => a.IsNotPassed).ToList();
            }

            var firstOne = filteredOrders.First();
            try
            {
                string messageBody = "Merhaba,<br/><br/> LEROY MERLİN sipariş " + tipi + ".<br/> ";
                if (tipi.Contains("KAYIT"))//İŞLEM TİPİ KAYITSA ilgili bilgileri atar.
                {
                    messageBody += "<font>Gürmen Fişno:" + tbxFisno.Text + " </font><br><br>";
                    messageBody += "<font>Order No:" + tbxOrderNo.Text + " </font><br><br>";
                    messageBody += "<font>Sevk Tip:" + cbxSevkTip.Text + " </font><br><br>";
                    messageBody += "<font>Sipariş Tarihi:" + dtpTarih.Value + " </font><br><br>";
                    messageBody += "<font>Termin Tarihi:" + dtpTerTarih.Value + " </font><br><br>";

                }
                messageBody += "<font>Kayıt Yapan:XML Uygulaması ile atılmıştır. </font><br><br>";
                messageBody += "<font>" + tipi + " Kalem Bilgileri aşağıdaki gibidir; </font><br><br>";
                if (!filteredOrders.Any()) return messageBody + "there are no records!";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Mağaza Order No" + htmlTdEnd;
                messageBody += htmlTdStart + "Mağaza No" + htmlTdEnd;
                messageBody += htmlTdStart + "Stok Kodu" + htmlTdEnd;
                messageBody += htmlTdStart + "Müşteri Kodu" + htmlTdEnd;
                messageBody += htmlTdStart + "Stok Adı" + htmlTdEnd;
                messageBody += htmlTdStart + "Miktar" + htmlTdEnd;
                messageBody += htmlTdStart + "Ölçü Br." + htmlTdEnd;
                messageBody += htmlTdStart + "Açıklama" + htmlTdEnd;
                messageBody += htmlTdStart + "Hafta" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;

                foreach (var el in filteredOrders)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + el.DelNumber + htmlTdEnd; //adding student name  
                    messageBody = messageBody + htmlTdStart + el.DelStore + htmlTdEnd; //adding student name  
                    messageBody = messageBody + htmlTdStart + el.OurRef + htmlTdEnd; //adding student name  
                    messageBody = messageBody + htmlTdStart + el.BuyerRef + htmlTdEnd; //adding DOB  
                    messageBody = messageBody + htmlTdStart + el.OurName + htmlTdEnd; //adding Email  
                    messageBody = messageBody + htmlTdStart + el.Quantity + htmlTdEnd; //adding Mobile  
                    messageBody = messageBody + htmlTdStart + "AD" + htmlTdEnd; //adding Mobile  
                    messageBody = messageBody + htmlTdStart + el.Description + htmlTdEnd; //adding Mobile  
                    messageBody = messageBody + htmlTdStart + el.WeekOfYear.ToString() + htmlTdEnd; //adding Mobile  
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void Email(string htmlString, string subject = "", bool rest = false)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("postmaster@prolinepvc.com", "Sipariş Aktarım Uygulaması");
                message.To.Add(new MailAddress("s.ozbek@prolinepvc.com"));
                message.To.Add(new MailAddress("u.ozer@prolinepvc.com"));
                message.CC.Add(new MailAddress("u.oguz@prolinepvc.com"));
                if (rest)//kalanları gönder emri geldiyse sadece dış ticaret maillerine atsın
                {
                    message.To.Add(new MailAddress("export@prolinepvc.com"));
                    message.CC.Add(new MailAddress("k.uzumcu@prolinepvc.com"));
                    message.CC.Add(new MailAddress("o.elmali@prolinepvc.com"));
                    message.CC.Add(new MailAddress("a.cigirgil@prolinepvc.com"));
                }
                else//atılanları gönder emri geldiyse planlama üretim vs maillerine atsın
                {
                    message.To.Add(new MailAddress("k.uygun@prolinepvc.com"));
                    message.To.Add(new MailAddress("h.adiguzel@prolinepvc.com"));
                    message.To.Add(new MailAddress("export@prolinepvc.com"));
                    message.To.Add(new MailAddress("a.dogru@prolinepvc.com"));
                    message.CC.Add(new MailAddress("k.uzumcu@prolinepvc.com"));
                    message.CC.Add(new MailAddress("o.elmali@prolinepvc.com"));
                    message.CC.Add(new MailAddress("a.cigirgil@prolinepvc.com"));
                    message.To.Add(new MailAddress("planlama@prolinepvc.com"));
                    message.To.Add(new MailAddress("planlama2@prolinepvc.com"));
                }

                string subject1 = subject.Contains("KAYIT") ? tbxFisno.Text + " No'lu Leroy Siparişi " + subject : "Leroy Merlin Mağaza Siparişlerinden " + subject;

                message.Subject = subject1;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                string xlsAdi = subject + dtpTarih.Value.Date.ToString() + " " + tbxFisno.Text + " No'lu Leroy" + ".xlsx";
                message.Attachments.Add(new Attachment(CreateExcel(rest), xlsAdi, "text/xlsx"));
                smtp.Port = 587;
                smtp.Host = "smtp.yandex.com.tr"; //for yandex host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("postmaster@prolinepvc.com", "fW345IONasdf234");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public MemoryStream CreateExcel(bool rest = false)
        {
            List<Orders> filteredOrders;
            IExportEngine engine = new ExcelExportEngine();
            if (!rest)
            {
                filteredOrders = orders.Where(a => a.WillFlow).ToList();
            }
            else
            {
                filteredOrders = orders.Where(a => a.IsNotPassed).ToList();
            }

            engine.AddData(filteredOrders);

            MemoryStream memory = engine.Export();
            return memory;
        }
        public static int GetIso8601WeekNumber( DateTime date)
        {
            var thursday = date.AddDays(4 - ((int)date.DayOfWeek + 5) % 7);
            return 1 + (thursday.DayOfYear - 1) / 7;
        }
     
    }
}
