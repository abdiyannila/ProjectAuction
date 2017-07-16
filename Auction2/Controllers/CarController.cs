using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auction2.Models;

namespace Auction2.Controllers
{
    public class CarController : Controller
    {
        private OperationDataContext context = null;

        public CarController()
        {
            context = new OperationDataContext();
        }

        //=====================Untuk bagian Car rModel============================
        // GET: Car
        public ActionResult Index()
        {
            //masukkan ke list di CS Model
            List<CarModel> carList = new List<CarModel>();
            var queres = from a_car in context.Mobils
                         join a_merk in context.Mereks
                         on a_car.MerkId equals a_merk.Id
                         select new CarModel
                         {
                             Id=a_car.Id,
                             Tahun=a_car.Tahun,
                             Transmisi=a_car.Transmisi,
                             BBM=a_car.BBM,
                             NoPolisi=a_car.NoPolisi,
                             MasaBerlaku=a_car.MasaBerlaku,
                             Harga=a_car.Harga,
                             MerkName=a_merk.NamaMobil
                         };
            carList = queres.ToList();
            return View(carList);
        }

        public ActionResult Create()
        {
            CarModel car = new CarModel();
            PopulateDropDown(car);
            return View(car);
        }

        [HttpPost]
        public ActionResult Create(CarModel carModel)
        {
            try
            {
                Mobil car = new Mobil()
                {
                    MerkId=carModel.MerkId,
                    Tahun=carModel.Tahun,
                    Transmisi=carModel.Transmisi,
                    BBM=carModel.BBM,
                    NoPolisi=carModel.NoPolisi,
                    MasaBerlaku=carModel.MasaBerlaku,
                    Harga=carModel.Harga,
                };

                context.Mobils.InsertOnSubmit(car);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(carModel);
            }
        }

        public ActionResult Edit(int id)
        {
            CarModel car = context.Mobils.Where(m => m.Id == id).Select(m => new CarModel()
            {
                Id=m.Id,
                Tahun=m.Tahun,
                Transmisi=m.Transmisi,
                BBM=m.BBM,
                NoPolisi=m.NoPolisi,
                MasaBerlaku=m.MasaBerlaku,
                Harga=m.Harga,
                MerkId=m.MerkId
            }).SingleOrDefault();

            PopulateDropDown(car);
            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(CarModel carmodel)
        {
            try
            {
                Mobil car = context.Mobils.Where(m => m.Id == carmodel.Id).Single<Mobil>();
                car.Tahun = carmodel.Tahun;
                car.Transmisi = carmodel.Transmisi;
                car.BBM = carmodel.BBM;
                car.NoPolisi = carmodel.NoPolisi;
                car.MasaBerlaku = carmodel.MasaBerlaku;
                car.Harga = carmodel.Harga;
                car.MerkId = carmodel.MerkId;

                context.SubmitChanges();
                return View();
            }
            catch
            {
                PopulateDropDown(carmodel);
                return View(carmodel);
            }
        }

        public ActionResult Delete(int id)
        {
            CarModel car = context.Mobils.Where(m => m.Id == id).Select(m => new CarModel()
            {
                Id = m.Id,
                Tahun=m.Tahun,
                Transmisi=m.Transmisi,
                BBM=m.BBM,
                NoPolisi=m.NoPolisi,
                MasaBerlaku=m.MasaBerlaku,
                Harga = m.Harga,
                MerkId = m.MerkId
            }).SingleOrDefault();

            PopulateDropDown(car);
            return View(car);
        }

        [HttpPost]
        public ActionResult Delete(CarModel carmodel)
        {
            try
            {
                Mobil car = context.Mobils.Where(m => m.Id == carmodel.Id).Single<Mobil>();
                context.Mobils.DeleteOnSubmit(car);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(carmodel);
            }
        }

        public ActionResult Details(int id)
        {
            //masukkan ke list di CS
            List<BidUserModel> bidList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
                          join a_merk in context.Mereks
                          on a_bid.MerkId equals id
                          //on a_bid.MerkId equals a_merk.Id
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
                              //MerkName = a_merk.NamaMobil
                          }).Distinct().OrderByDescending(x => x.Harga);
            bidList = queres.ToList();

            if (bidList.Count == 3)
            {
                //Untuk Pemenang
                var winner = (from a_bid in context.BidUsers
                              join a_merk in context.Mereks
                              on a_bid.MerkId equals id
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
                              }).OrderByDescending(x => x.Harga).FirstOrDefault();
                ViewBag.Winner = winner.Nama;
            }
            return View(bidList);
        }


        //=====================Untuk bagian BidUserModel============================
        public ActionResult AddBid(int id)
        {
            BidUserModel bid = context.BidUsers.Where(m => m.Id == id).Select(m => new BidUserModel()
            {
                Id = m.Id,
                IdKTP = m.IdKTP,
                Nama = m.Nama,
                Alamat = m.Alamat,
                Pekerjaan = m.Pekerjaan,
                TanggalBid = m.TanggalBid,
                BatasBid = m.BatasBid,
                Harga = m.Harga,
                MerkId = m.MerkId,
            }).SingleOrDefault();

            Session["MerkName"] = bid.MerkName.ToString();
            PopulateDropDownBid(bid);
            return View(bid);
        }

        [HttpPost]
        public ActionResult AddBid(BidUserModel bidusermodel)
        {
            try
            {
                BidUser biduser = new BidUser()
                {
                    IdKTP = bidusermodel.IdKTP,
                    Nama = bidusermodel.Nama,
                    Alamat = bidusermodel.Alamat,
                    Pekerjaan = bidusermodel.Pekerjaan,
                    TanggalBid = bidusermodel.TanggalBid,
                    BatasBid = bidusermodel.BatasBid,
                    Harga = bidusermodel.Harga,
                    MerkId = bidusermodel.MerkId,
                };
                context.BidUsers.InsertOnSubmit(biduser);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(bidusermodel);
            }
        }

        public ActionResult DisplayBidUser(BidUserModel bidusermodel)
        {
            //masukkan ke list di CS Model
            List<BidUserModel> listbiduserList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
                          join a_merk in context.Mereks
                          on a_bid.MerkId equals a_merk.Id
                          //on a_bid.MerkId equals a_merk.Id
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
                              //MerkName = a_merk.NamaMobil
                          }).Distinct().OrderByDescending(x => x.Harga);
            listbiduserList = queres.ToList();
            return View(listbiduserList);
        }

        //[HttpPost]
        //public ActionResult AddBid(ListBidUserModel listbidusermodel)
        //{
        //ListBidUser listbiduser = new ListBidUser()
        //{
        //    IdKTP = listbidusermodel.IdKTP,
        //    Nama = listbidusermodel.Nama,
        //    Alamat = listbidusermodel.Alamat,
        //    Pekerjaan = listbidusermodel.Pekerjaan,
        //    TanggalBid = listbidusermodel.TanggalBid,
        //    BatasBid = listbidusermodel.BatasBid,
        //    Harga = listbidusermodel.Harga,
        //    MerkId = listbidusermodel.MerkId,
        //};
        //context.ListBidUsers.InsertOnSubmit(listbiduser);
        //    context.SubmitChanges();
        //    return View();
        //}

        //==============DropDwon=====================================================

        private void PopulateDropDown(CarModel Populatemodel)
        {
            //untuk populate dropdownlist
            //list dropdown
            Populatemodel.Merks = context.Mereks.AsQueryable<Merek>().Select(x => new SelectListItem()
            {
                Text = x.NamaMobil,
                Value = x.Id.ToString()
            });
        }

        private void PopulateDropDownBid(BidUserModel populatemodelbid)
        {
            populatemodelbid.Merks = context.Mereks.AsQueryable<Merek>().Select(x => new SelectListItem()
            {
                Text = x.NamaMobil,
                Value = x.Id.ToString()
            });
        }

    }
}

//.Distinct().OrderBy(x=>x.Id).Distinct().OrderBy(x=>x.MerkId)
//ViewBag.M = ViewBag.data;
//            var value1 = Request["IdKTP"];
//var databidmodel = TempData["dataPostBid"] as BidUserModel;
//ViewData["dataPostBid"] = TempData["dataPostBid"];
//TempData["dataPostBid"] = biduser;
//ViewBag.data = biduser;