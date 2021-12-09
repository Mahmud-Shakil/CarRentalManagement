using DotNetCore_5.DAL;
using DotNetCore_5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using X.PagedList;

namespace DotNetCore_5.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        ICustomer _iCustomerRepository;
        IVehicles _iVehiclesRepository;
        IBooking _IbookingRepository;
        public BookingController(IWebHostEnvironment iWebHostEnvironment, IBooking ibookingRepository,ICustomer iCustomerRepository, IVehicles iVehiclesRepository)
        {
            this._iWebHostEnvironment = iWebHostEnvironment;
            this._iCustomerRepository = iCustomerRepository;
            this._iVehiclesRepository = iVehiclesRepository;
            this._IbookingRepository = ibookingRepository;
        }
        public IActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            ViewBag.CurrentFilter = SearchString;
            IEnumerable<BookingViewModel> booking = _IbookingRepository.GetAll();
            ViewBag.Customer = _iCustomerRepository.GetAll();
            ViewBag.Vehicel = _iVehiclesRepository.GetAll();
            int slNo = 1;
            foreach (var item in booking)
            {
                item.SerialNo = slNo++;
                item.FullName = item.FirstName +" "+ item.LastName;
                item.Total = (int)item.DailyHireRate * item.TotalDaysOfHiring;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                booking = booking.Where(e => e.FullName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    booking = booking.OrderByDescending(e => e.City).ToList();
                    break;
                default:
                    booking = booking.OrderBy(e => e.Address).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
           
            return View(booking.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingViewModel objModel)
        {
            if (ModelState.IsValid)
            {

                _IbookingRepository.Save(objModel);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            IEnumerable<CustomerViewModel> listOfCustomer = _iCustomerRepository.GetAll();
            ViewBag.Customer = listOfCustomer;
            IEnumerable<VehicleViewModel> listOfVehicel = _iVehiclesRepository.GetAll();
            ViewBag.Vehicel = listOfVehicel;
            BookingViewModel booking = _IbookingRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Views/Booking/_Edit.cshtml", booking);
        }
        [HttpPost]
        public IActionResult Edit(BookingViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _IbookingRepository.Update(obj);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
           
            _IbookingRepository.Delete(id);
            return RedirectToAction("Index");

        }
        public PartialViewResult Details(int id)
        {
            IEnumerable<CustomerViewModel> listOfCustomer = _iCustomerRepository.GetAll();
            ViewBag.Customer = listOfCustomer;
            IEnumerable<VehicleViewModel> listOfVehicel = _iVehiclesRepository.GetAll();
            ViewBag.Vehicel = listOfVehicel;
            BookingViewModel vehicle = _IbookingRepository.GetById(id);

            return PartialView("~/Views/Booking/_Details.cshtml", vehicle);

        }
        public JsonResult GetCustomer()
        {
           
            return Json(_iCustomerRepository.GetAll());
        }
        public JsonResult GetAllById(int CustomerId)
        {
            
            return Json(_iCustomerRepository.GetById(CustomerId));
        }
        public JsonResult GetVehicel()
        {

            return Json(_iVehiclesRepository.GetAll());
        }
        public JsonResult GetById(int VehicleID)
        {

            return Json(_iVehiclesRepository.GetById(VehicleID));

        }

    }
}
