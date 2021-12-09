using DotNetCore_5.DAL;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static DotNetCore_5.DAL.Repositories;
using static DotNetCore_5.Models.AllModels.Models;

namespace DotNetCore_5.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IConfiguration iConfiguration;
        private readonly IWebHostEnvironment iWebHostEnvironment;
        private readonly ICustomer iCustomer;
        public ReportController(IConfiguration _iConfiguration, IWebHostEnvironment _iWebHostEnvironment, ICustomer _iCustomer)
        {
            iConfiguration = _iConfiguration;
            iWebHostEnvironment = _iWebHostEnvironment;
            iCustomer = _iCustomer;
        }
        public IActionResult Report()
        {
            var webReport = GetReport();
            ViewBag.WebReport = webReport;
            return View();
        }
        public WebReport GetReport()
        {
           var webReport=new WebReport();
            var msqlDataConnection = new MsSqlDataConnection();
            msqlDataConnection.ConnectionString = iConfiguration.GetConnectionString("DefaultConnection");
            webReport.Report.Dictionary.Connections.Add(msqlDataConnection);
            webReport.Report.Load(Path.Combine(iWebHostEnvironment.ContentRootPath, "Reports", "Customer.frx"));
            var customer = iCustomer.GetAll();
            webReport.Report.RegisterData(customer, "Customers");
            return webReport;
        }
        public IActionResult Pdf()
        {
            var webReport = GetReport();
            webReport.Report.Prepare();

            using (MemoryStream ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Master-Detail") + ".pdf");
            }
        }
    }
}
