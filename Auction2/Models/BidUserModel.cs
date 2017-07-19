using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction2.Models
{
    public class BidUserModel
    {
        public int Id { get; set; }

        [DisplayName("No KTP")]
        public int? IdKTP { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Pekerjaan { get; set; }

        [DisplayName("Tanggal Bid")]
        public DateTime TanggalBid { get; set; }

        [DisplayName("Batas Bid")]
        public DateTime BatasBid { get; set; }
        public decimal Harga { get; set; }

        [DisplayName("Merk Mobil")]
        public int MerkId { get; set; }
        public string MerkName { get; set; }

        //nambahin dropdown merk mobil
        public IEnumerable<SelectListItem> Merks { get; set; }

        public BidUserModel()
        {
            Merks = new List<SelectListItem>();
        }

        public static List<BidUserModel> DataBid { get; set; }
    }    
}