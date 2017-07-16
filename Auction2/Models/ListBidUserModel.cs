using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction2.Models
{
    public class ListBidUserModel
    {
        [DisplayName("No")]
        public int Id { get; set; }
        public int? IdKTP { get; set; }   
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Pekerjaan { get; set; }
        public DateTime TanggalBid { get; set; }
        public DateTime BatasBid { get; set; }
        public decimal Harga { get; set; }

        [DisplayName("Merk Mobil")]
        public int MerkId { get; set; }
        public string MerkName { get; set; }

        //nambahin dropdown merk mobil
        public IEnumerable<SelectListItem> Merks { get; set; }

        public ListBidUserModel()
        {
            Merks = new List<SelectListItem>();
        }
    }
}