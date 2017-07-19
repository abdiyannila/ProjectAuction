using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction2.Models
{
    public class CarModel
    {
        [DisplayName("No")]
        public int Id { get; set; }
        public string Tahun { get; set; }
        public string Transmisi { get; set; }
        public string BBM { get; set; }

        [DisplayName("Plat Nomor")]
        public string NoPolisi { get; set; }
        public DateTime MasaBerlaku { get; set; }
        public decimal Harga { get; set; }
        public int MerkId { get; set; }

        [DisplayName("Merk Mobil")]
        public string MerkName { get; set; }

        [DisplayName("Merk Mobil")]
        public string MerkImage { get; set; }

        //nambahin dropdown merk mobil
        public IEnumerable<SelectListItem> Merks { get; set; }

        public CarModel()
        {
            Merks = new List<SelectListItem>();
        }
    }
}