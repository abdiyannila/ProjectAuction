using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction2.Models;

namespace Auction2.Controllers
{
    public class HomeController : Controller
    {
        private OperationDataContext context = null;

        public HomeController()
        {
            context = new OperationDataContext();
        }

        public ActionResult Index()
        {
            List<LoginUserModel> login = new List<LoginUserModel>();
            var queres = from a_login in context.LoginUsers select a_login;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginUser loginuser)
        {

            if (ModelState.IsValid)
            {
                var queres = from a_login in context.LoginUsers select a_login;
                var obj = queres.Where(a => a.Nama.Equals(loginuser.Nama) && a.Pswd.Equals(loginuser.Pswd)).FirstOrDefault();
                if (obj != null)
                {
                    Session["Nama"] = obj.Nama.ToString();
                    Session["Pswd"] = obj.Pswd.ToString();
                    Session["Alamat"] = obj.Alamat.ToString();
                    Session["IdKTP"] = obj.IdKTP.ToString();
                    Session["Pekerjaan"] = obj.Pekerjaan.ToString();
                    return RedirectToAction("Index", "Car");
                }
            }
            return View(loginuser);
        }

        public ActionResult Register()
        {
            LoginUserModel login = new LoginUserModel();
            return View(login);
        }

        [HttpPost]
        public ActionResult Register(LoginUserModel loginuser)
        {
            try
            {
                LoginUser login = new LoginUser()
                {
                    Nama= loginuser.Nama,
                    Pswd=loginuser.Password,
                    Alamat=loginuser.Alamat,
                    Email=loginuser.Email,
                    IdKTP=loginuser.IdKTP,
                    Pekerjaan= loginuser.Pekerjaan
                };
                context.LoginUsers.InsertOnSubmit(login);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(loginuser);
            }
        }


        public ActionResult About()
        {
            LoginUserModel loginuser = new LoginUserModel();

            ViewBag.Message = "Your application description page.";
            List<BidUserModel> bidList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
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
                          }).Distinct().OrderBy(x => x.Nama);
            bidList = queres.ToList();

            return View(bidList);
            //return View();
        }

        [HttpPost]
        public ActionResult About(string searchString)
        {
            List<BidUserModel> bidList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
                          join a_merk in context.Mereks
                          on a_bid.MerkId equals a_merk.Id
                          where a_bid.Nama.Contains(searchString)
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
                          }).Distinct().OrderBy(x => x.Nama);
            bidList = queres.ToList();

            return View(bidList);
            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ForBidUser(string nama)
        {
            List<BidUserModel> bidList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
                          join a_merk in context.Mereks
                          on a_bid.MerkId equals a_merk.Id
                          where a_bid.Nama.Contains(nama)
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
                          }).Distinct().OrderBy(x => x.Nama);
            bidList = queres.ToList();

            return View(bidList);
        }
    }
}

//List<LoginUserModel> ListLoginUser = new List<LoginUserModel>();
//var queres = from a_login in context.LoginUsers select new LoginUserModel
//{

//};
//ListLoginUser= queres.ToList();