
using DotNetCore_5.DAL;
using DotNetCore_5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace DotNetCore_5.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        ICustomer _iCustomerRepository;
        public CustomerController(IWebHostEnvironment iWebHostEnvironment, ICustomer icustomerRepository)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iCustomerRepository = icustomerRepository;
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
            IEnumerable<CustomerViewModel> list = _iCustomerRepository.GetAll();
            int slNo = 1;
            foreach (var item in list)
            {
                item.SerialNo = slNo++;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.FirstName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.Address).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.Address).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerMultipleInsert objModel, IFormFile[] ImageName)
        {
            if (ModelState.IsValid)
            {
                string uniqueImageName = null;
                if (ImageName != null)
                {
                    if (objModel.CustomerViewModels.Count == ImageName.Count())
                    {
                        for (int i = 0; i < objModel.CustomerViewModels.Count; i++)
                        {
                            if (ImageName[i] != null)
                            {
                                string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/customer_images");
                                string uploadFolder1 = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/customer_images/");
                                uniqueImageName = Guid.NewGuid().ToString() + "_" + ImageName[i].FileName;
                                string filePath = Path.Combine(uploadFolder, uniqueImageName);
                                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                                ImageName[i].CopyTo(fileStream);
                                fileStream.Close();
                                objModel.CustomerViewModels[i].ImageFullPath = uploadFolder1 + uniqueImageName;
                                objModel.CustomerViewModels[i].ImageName = uniqueImageName;
                                _iCustomerRepository.Save(objModel.CustomerViewModels[i]);
                            }

                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return View();

        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
           
            CustomerViewModel customer = _iCustomerRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Views/Customer/_Edit.cshtml", customer);
        }
        [HttpPost]
        public IActionResult Edit(CustomerViewModel objModel)
        {
            string uniqueImageName = null;
            bool result = false;
            if (ModelState.IsValid)
            {
                if (objModel.CustomerId > 0)
                {
                    if (objModel.Photo != null)
                    {
                        string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/customer_images");
                        string uploadFolder1 = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/customer_images/");
                        DeleteExistingImage(Path.Combine(uploadFolder, objModel.ImageName));
                        uniqueImageName = Guid.NewGuid().ToString() + "_" + objModel.Photo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueImageName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        objModel.Photo.CopyTo(fileStream);
                        fileStream.Close();
                        objModel.ImageFullPath = uploadFolder1 + uniqueImageName;
                        objModel.ImageName = uniqueImageName;
                    }
                    _iCustomerRepository.Update(objModel);
                    result = true;
                }

            }

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                
                CustomerViewModel customer = _iCustomerRepository.GetById(objModel.CustomerId);
                return View(customer);
            }
        }

        private void DeleteExistingImage(string imagePath)
        {
            FileInfo fileObj = new FileInfo(imagePath);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
        [HttpGet]
        public PartialViewResult Delete(int id)
        {
            CustomerViewModel obj = _iCustomerRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Views/Customer/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult PostDelete(CustomerViewModel obj)
        {
            if (obj.ImageName != null)
            {
                string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/customer_images");
                DeleteExistingImage(Path.Combine(uploadFolder, obj.ImageName));
            }
            _iCustomerRepository.Delete(obj.CustomerId);
            return RedirectToAction("Index");
           
        }
        public PartialViewResult Details(int id)
        {

            CustomerViewModel vehicle = _iCustomerRepository.GetById(id);
            
            return PartialView("~/Views/Customer/_Details.cshtml", vehicle);

        }
    }
}
