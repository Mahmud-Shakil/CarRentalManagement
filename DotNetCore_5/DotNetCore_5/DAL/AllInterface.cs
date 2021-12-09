
using DotNetCore_5.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_5.DAL
{
    public interface ICustomer
    {
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(int id);
        void Save(CustomerViewModel obj);
        void Update(CustomerViewModel Upobj);
        void Delete(int id);
    }
    public interface IBooking
    {
        IEnumerable<BookingViewModel> GetAll();
        BookingViewModel GetById(int id);
        void Save(BookingViewModel obj);
        void Update(BookingViewModel Upobj);
        void Delete(int id);
    }
    public interface IVehicles
    {
        IEnumerable<VehicleViewModel> GetAll();
        VehicleViewModel GetById(int id);
        VehicleViewModel Save(VehicleViewModel obj);
        VehicleViewModel Update(VehicleViewModel Upobj);
        VehicleViewModel Delete(int id);
    }
    public interface IVechileImage
    {
        IEnumerable<VechileImageViewModel> GetAll();
        VechileImageViewModel GetById(int id);
        void Save(VechileImageViewModel obj);
        void Update(VechileImageViewModel Upobj);
        void Delete(int id);
        IEnumerable<VechileImageViewModel> GetImagesId(int id);
    }
    public interface IPaymentType
    {
        IEnumerable<PaymentTypeViewModel> GetAll();
        PaymentTypeViewModel GetById(int id);
        void Insert(PaymentTypeViewModel objModel);
        void Update(PaymentTypeViewModel objModel);
        void Delete(int id);
        void Save();
    }

}
