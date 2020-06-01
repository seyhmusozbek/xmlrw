using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlRW
{
    public class Orders
    {
        [DisplayName("Müşteri Adı")]
        public string CustomerRef { get; set; }
        [DisplayName("Termin Tarihi")]
        [Browsable(false)]
        public string DelFrom { get; set; }
        [DisplayName("Termin Tarihi 2")]
        [Browsable(false)]
        public string DelTo { get; set; }
        [DisplayName("Sipariş Tarihi")]
        public DateTime IssueDate { get; set; }
        [DisplayName("Mağaza No")]
        public string DelStore { get; set; }
        [DisplayName("Sipariş No")]
        public string DelNumber { get; set; }
        [DisplayName("Müşteri Ürün Kodu")]
        public string BuyerRef { get; set; }
        [DisplayName("Netsis Ürün Kodu")]
        public string OurRef { get; set; }
        [DisplayName("Netsis Ürün Adı")]
        public string OurName { get; set; }
        [DisplayName("Satır Numarası-EAN")]
        public string RowId { get; set; }
        [DisplayName("Müşteri Ürün Açıklaması")]
        public string Description { get; set; }
        [DisplayName("Sipariş Notu")]
        public string Note { get; set; }
        [DisplayName("Türkçe Notu")]
        public string TurkNote { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }
        [DisplayName("Miktar")]
        public double Quantity { get; set; }
        [DisplayName("Uygun mu?")]
        public bool WillFlow { get; set; }
        [DisplayName("600ün altında mı?")]
        public bool IsNotPassed { get; set; }
        [DisplayName("Kaçıncı Hafta")]
        public int WeekOfYear { get; set; }
        [Browsable(false)]
        public bool DidFlow { get; set; }
    }
}
