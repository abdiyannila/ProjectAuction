using Auction2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction2.Controllers
{
    public class BidController : Controller
    {
        private OperationDataContext context = null;

        public BidController()
        {
            context = new OperationDataContext();
        }
        // GET: Bid
        public ActionResult Index()
        {
            //masukkan ke list di CS
            List<BidUserModel> bidList = new List<BidUserModel>();

            var queres = from a_bid in context.BidUsers
                         join a_merk in context.Mereks
                         on a_bid.MerkId equals a_merk.Id
                         select new BidUserModel
                         {
                             Id = a_bid.Id,
                             IdKTP = a_bid.IdKTP,
                             Nama = a_bid.Nama,
                             Alamat = a_bid.Alamat,
                             Pekerjaan = a_bid.Pekerjaan,
                             TanggalBid = a_bid.TanggalBid,
                             BatasBid = a_bid.BatasBid,
                             Harga = a_bid.Harga,
                             MerkName = a_merk.NamaMobil
                         };
            bidList = queres.ToList();
            return View(bidList);
        }
    }
}