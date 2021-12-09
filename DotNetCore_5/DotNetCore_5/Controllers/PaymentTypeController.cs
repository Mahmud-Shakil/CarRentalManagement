using DotNetCore_5.DAL;
using DotNetCore_5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_5.Controllers
{
    [Authorize]
    public class PaymentTypeController : Controller
    {
        IPaymentType _IPaymentRepository;
        public PaymentTypeController(IPaymentType paymentRepository)
        {
            _IPaymentRepository = paymentRepository;
        }
        public IActionResult Index()
        {
            return View(_IPaymentRepository.GetAll());
        }
        [HttpPost]
        public IActionResult Create(PaymentTypeViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _IPaymentRepository.Insert(obj);
            }
            return PartialView("_PaymentTypeList", _IPaymentRepository.GetAll());
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            PaymentTypeViewModel obj = _IPaymentRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Views/PaymentType/_Edit.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult PostEdit(PaymentTypeViewModel obj)
        {
            var result = false;
            if (obj.PaymentTypeCode > 0)
            {
                if (ModelState.IsValid)
                {
                    _IPaymentRepository.Update(obj);
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
        public PartialViewResult Delete(int id)
        {
            PaymentTypeViewModel obj = _IPaymentRepository.GetById(id);
            ViewBag.Details = "Show";
            return PartialView("~/Views/PaymentType/_Delete.cshtml", obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult PostDelete(PaymentTypeViewModel obj)
        {
            _IPaymentRepository.Delete(obj.PaymentTypeCode);
            return RedirectToAction("Index");
        }
    }
}
