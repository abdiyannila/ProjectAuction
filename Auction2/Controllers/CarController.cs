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

        // GET: Car
        public ActionResult Index()
        {
            //masukkan ke list di CS
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

        public ActionResult Details(int merkid)
        {
            //masukkan ke list di CS
            List<BidUserModel> bidList = new List<BidUserModel>();
            var queres = (from a_bid in context.BidUsers
                          join a_merk in context.Mereks
                          on a_bid.MerkId equals merkid
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
            return View(bidList);
        }

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
    }
}

//.Distinct().OrderBy(x=>x.Id).Distinct().OrderBy(x=>x.MerkId)