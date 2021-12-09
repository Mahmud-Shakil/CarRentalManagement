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
    public class VehicelController : Controller
    {
        private readonly IWebHostEnvironment _iWebHostEnvironment;
        IVehicles _iVehicleRepository;
        IVechileImage _iVehicelImage;
        public VehicelController(IWebHostEnvironment iWebHostEnvironment, IVehicles iVehicleRepository, IVechileImage iVehicelImage)
        {
            _iWebHostEnvironment = iWebHostEnvironment;
            _iVehicleRepository = iVehicleRepository;
            _iVehicelImage = iVehicelImage;
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
            IEnumerable<VehicleViewModel> list = _iVehicleRepository.GetAll();
            int slNo = 1;
            foreach (var item in list)
            {
                item.SerialNo = slNo++;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(e => e.Model.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(e => e.Manufacturer).ToList();
                    break;
                default:
                    list = list.OrderBy(e => e.Manufacturer).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]

        public IActionResult Create(VehicleViewModel objModel)
        {

            string uniqueImageName = null;
            bool result = false;
            if (ModelState.IsValid)
            {
                VehicleViewModel vehicleViewModel = _iVehicleRepository.Save(objModel);
                VechileImageViewModel imagemodel = new VechileImageViewModel();
                if (objModel.Photos != null && objModel.Photos.Count > 0)
                {
                    foreach (IFormFile photo in objModel.Photos)
                    {
                        string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/vehicel_images");
                        uniqueImageName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueImageName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        photo.CopyTo(fileStream);
                        fileStream.Close();
                        imagemodel.VehicleID = vehicleViewModel.VehicleID;
                        imagemodel.ImageName = uniqueImageName;
                        _iVehicelImage.Save(imagemodel);

                    }
                }
                result = true;
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {

            VehicleViewModel vehicle = _iVehicleRepository.GetById(id);
            ViewBag.Images = _iVehicelImage.GetImagesId(id);
            return View(vehicle);
        }
        [HttpPost]
        public IActionResult Edit(VehicleViewModel objModel)
        {
            string uniqueImageName = null;
            bool result = false;
            if (ModelState.IsValid)
            {
                if (objModel.VehicleID > 0)
                {
                    VehicleViewModel returnObj = _iVehicleRepository.Update(objModel);
                    VechileImageViewModel imageObj = new VechileImageViewModel();
                    if (objModel.Edit_Photos != null && objModel.Edit_Photos.Count > 0)
                    {
                        foreach (IFormFile photo in objModel.Edit_Photos)
                        {
                            string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/vehicel_images");
                            uniqueImageName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(uploadFolder, uniqueImageName);
                            FileStream fileStream = new FileStream(filePath, FileMode.Create);
                            photo.CopyTo(fileStream);
                            fileStream.Close();
                            imageObj.VehicleID = returnObj.VehicleID;
                            imageObj.ImageName = uniqueImageName;
                            _iVehicelImage.Save(imageObj);
                        }
                    }
                    result = true;
                }
              

            }

            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var Images = _iVehicelImage.GetImagesId(id);
                if (Images != null)
                {
                    foreach (var image in Images)
                    {
                        if (image.ImageName != null)
                        {
                            string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/vehicel_images");
                            DeleteExistingImage(Path.Combine(uploadFolder, image.ImageName));
                        }
                        _iVehicelImage.Delete(image.ImageId);
                    }
                }
                _iVehicleRepository.Delete(id);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Vehicel is not deleted";
                return RedirectToAction("Message");
            }
        }
        public IActionResult DeleteImage(int id)
        {
            var image = _iVehicelImage.GetById(id);
            if (image.ImageName != null)
            {
                string uploadFolder = Path.Combine(_iWebHostEnvironment.WebRootPath, "images/vehicel_images");
                DeleteExistingImage(Path.Combine(uploadFolder, image.ImageName));
            }
            _iVehicelImage.Delete(image.ImageId);
            return RedirectToAction("Edit", new { id = image.VehicleID });
        }
        private void DeleteExistingImage(string imagePath)
        {
            FileInfo fileObj = new FileInfo(imagePath);
            if (fileObj.Exists)
            {
                fileObj.Delete();
            }
        }
        public PartialViewResult Details(int id)
        {

            VehicleViewModel vehicle = _iVehicleRepository.GetById(id);
            ViewBag.Images = _iVehicelImage.GetImagesId(id);
            return PartialView("~/Views/Vehicel/_Details.cshtml", vehicle);

        }
        public PartialViewResult ViewImages(int id)
        {
            IEnumerable<VechileImageViewModel> obj = _iVehicelImage.GetImagesId(id);
            ViewBag.ViewImages = "Show";
            return PartialView("~/Views/Vehicel/_ViewImages.cshtml", obj);
        }
    }
}
