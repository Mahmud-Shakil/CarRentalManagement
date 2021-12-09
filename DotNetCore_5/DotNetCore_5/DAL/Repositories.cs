using DotNetCore_5.Models;
using DotNetCore_5.ViewModels;
using DotNetCore_5.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_5.Data;
using static DotNetCore_5.Models.AllModels.Models;

namespace DotNetCore_5.DAL
{
    public class Repositories
    {
        public class CustomerRepository : ICustomer
        {
            private readonly ApplicationDbContext _context;
            public CustomerRepository(ApplicationDbContext context)
            {
                this._context = context;
            }
            public void Delete(int id)
            {
                Customer customer = _context.Customers.Find(id);
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }

            public IEnumerable<CustomerViewModel> GetAll()
            {
                IEnumerable<CustomerViewModel> listOfCustomer = _context.Customers.Select(e => new CustomerViewModel
                {
                    CustomerId = e.CustomerId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,
                    Email = e.Email,
                    City = e.City,
                    PostCode = e.PostCode,
                    PhoneNumber = e.PhoneNumber,
                    ImageName = e.ImageName,
                    Address = e.Address,
                   FullName=e.FirstName+" "+e.LastName,
                   ImageFullPath=e.ImageFullPath

                }).ToList();
                return listOfCustomer;
            }

            public CustomerViewModel GetById(int id)
            {
                Customer e = _context.Customers.AsNoTracking().SingleOrDefault(e => e.CustomerId == id);
                CustomerViewModel customer = new CustomerViewModel
                {
                    CustomerId = e.CustomerId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DateOfBirth = e.DateOfBirth,
                    Email = e.Email,
                    City = e.City,
                    PostCode = e.PostCode,
                    PhoneNumber = e.PhoneNumber,
                    ImageName = e.ImageName,
                    Address = e.Address,
                    ImageFullPath=e.ImageFullPath

                };
                return customer;
            }

            public void Save(CustomerViewModel obj)
            {
                Customer customer = new Customer
                {
                    CustomerId = obj.CustomerId,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    DateOfBirth = obj.DateOfBirth,
                    Email = obj.Email,
                    City = obj.City,
                    PostCode = obj.PostCode,
                    PhoneNumber = obj.PhoneNumber,
                    Address = obj.Address,
                    ImageName = obj.ImageName,
                    ImageFullPath=obj.ImageFullPath
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }

            public void Update(CustomerViewModel Upobj)
            {
                Customer customer = new Customer();
                customer.CustomerId = Upobj.CustomerId;
                customer.FirstName = Upobj.FirstName;
                customer.LastName = Upobj.LastName;
                customer.DateOfBirth = Upobj.DateOfBirth;
                customer.Email = Upobj.Email;
                customer.City = Upobj.City;
                customer.PostCode = Upobj.PostCode;
                customer.PhoneNumber = Upobj.PhoneNumber;
                customer.ImageName = Upobj.ImageName;
                customer.Address = Upobj.Address;
                customer.ImageFullPath = Upobj.ImageFullPath;
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }
         
        }
        public class BookingRepositories : IBooking
        {
            private readonly ApplicationDbContext _Context;
            public BookingRepositories(ApplicationDbContext context)
            {
                this._Context = context;
            }
            public void Delete(int id)
            {
                Booking booking = _Context.Bookings.Find(id);
                _Context.Bookings.Remove(booking);
                _Context.SaveChanges();
            }

            public IEnumerable<BookingViewModel> GetAll()
            {
                IEnumerable<BookingViewModel> listofBookings = _Context.Bookings.Select(e => new BookingViewModel
                {

                    BookingId = e.BookingId,
                    PickUpDate = e.PickUpDate,
                    DropOutDate = e.DropOutDate,
                    HirePoint = e.HirePoint,
                    DropPoint = e.DropPoint,
                    CustomerId = e.CustomerId,
                    VehicleID = e.VehicleID,
                    ImageName = e.Customer.ImageName,
                    Model = e.Vehicle.Model,
                    DailyHireRate = e.Vehicle.DailyHireRate,
                    City = e.Customer.City,
                    PostCode = e.Customer.PostCode,
                    PhoneNumber = e.Customer.PhoneNumber,
                    FirstName = e.Customer.FirstName,
                    LastName = e.Customer.LastName,
                    TotalDaysOfHiring = GetDate(e.PickUpDate, e.DropOutDate)
                    

                }).ToList();
                return listofBookings;
            }

            private static int GetDate(DateTime pickUpDate, DateTime dropOutDate)
            {
              int result=(int)(dropOutDate-pickUpDate).TotalDays;
              return result;
            }

            public BookingViewModel GetById(int id)
            {
                Booking e = _Context.Bookings.Find(id);
                Customer c = _Context.Customers.Find(e.BookingId);
                Vehicle v = _Context.Vehicles.Find(e.BookingId);
                BookingViewModel customer = new BookingViewModel();

                customer.BookingId = e.BookingId;
                customer.PickUpDate = e.PickUpDate;
                customer.DropOutDate = e.DropOutDate;
                customer.HirePoint = e.HirePoint;
                customer.DropPoint = e.DropPoint;
                customer.CustomerId = e.CustomerId;
                customer.VehicleID = e.VehicleID;
                //customer.ImageName = c.ImageName;
                //customer.Model = v.Model;
                //customer.DailyHireRate = v.DailyHireRate;
                //customer.City = c.City;
                //customer.PostCode = c.PostCode;
                //customer.PhoneNumber = c.PhoneNumber;
                //customer.FirstName = c.FirstName;
                //customer.LastName = c.LastName;
                //customer.TotalDaysOfHiring = GetDate(e.PickUpDate, e.DropOutDate);
                return customer;
            }

            public void Save(BookingViewModel obj)
            {
                Booking b = new Booking
                {
                   BookingId=obj.BookingId,
                    PickUpDate = obj.PickUpDate,
                    DropOutDate = obj.DropOutDate,
                    HirePoint = obj.HirePoint,
                    DropPoint = obj.DropPoint,

                    CustomerId = obj.CustomerId,
                    VehicleID = obj.VehicleID,
                };
                _Context.Bookings.Add(b);
                _Context.SaveChanges();
            }

            public void Update(BookingViewModel Upobj)
            {
                Booking b = new Booking();

                b.BookingId = Upobj.BookingId;
                b.PickUpDate = Upobj.PickUpDate;
                b.DropOutDate = Upobj.DropOutDate;
                b.HirePoint = Upobj.HirePoint;
                b.DropPoint = Upobj.DropPoint;
                b.CustomerId = Upobj.CustomerId;
                b.VehicleID = Upobj.VehicleID;
                _Context.Entry(b).State = EntityState.Modified;
                _Context.SaveChanges();
            }
        }
        public class VehiclesRepository : IVehicles
        {
            private readonly ApplicationDbContext _Context;
            public VehiclesRepository(ApplicationDbContext Context)
            {
                this._Context = Context;
            }
            public VehicleViewModel Delete(int id)
            {
                VehicleViewModel obj = GetById(id);
                Vehicle vehicle = _Context.Vehicles.Find(id);
                _Context.Vehicles.Remove(vehicle);
                _Context.SaveChanges();
                return obj;
            }
            public IEnumerable<VehicleViewModel> GetAll()
            {
                IEnumerable<VehicleViewModel> listofVehicle = _Context.Vehicles.Select(e => new VehicleViewModel
                {

                    VehicleID = e.VehicleID,
                    RegistrationNumber = e.RegistrationNumber,
                    Model = e.Model,
                    DailyHireRate = e.DailyHireRate,
                    Manufacturer = e.Manufacturer,

                }).ToList();
                return listofVehicle;
            }

            public VehicleViewModel GetById(int id)
            {
                Vehicle e = _Context.Vehicles.AsNoTracking().SingleOrDefault(e => e.VehicleID == id);
                VehicleViewModel vehicle = new VehicleViewModel
                {
                    VehicleID = e.VehicleID,
                    RegistrationNumber = e.RegistrationNumber,
                    Model = e.Model,
                    DailyHireRate = e.DailyHireRate,
                    Manufacturer = e.Manufacturer,
                    
                };
                return vehicle;
            }

            public VehicleViewModel Save(VehicleViewModel obj)
            {
                Vehicle vehicle = new Vehicle()
                {
                    VehicleID = obj.VehicleID,
                    RegistrationNumber = obj.RegistrationNumber,
                    Model = obj.Model,
                    DailyHireRate = obj.DailyHireRate,
                    Manufacturer = obj.Manufacturer,
                };
                _Context.Vehicles.Add(vehicle);
                _Context.SaveChanges();
                VehicleViewModel obj1 = new VehicleViewModel()
                {
                    VehicleID = vehicle.VehicleID
                };
                return obj1;
            }

            public VehicleViewModel Update(VehicleViewModel Upobj)
            {
                Vehicle vehicle = new Vehicle()
                {
                    VehicleID = Upobj.VehicleID,
                    RegistrationNumber = Upobj.RegistrationNumber,
                    Model = Upobj.Model,
                    DailyHireRate = Upobj.DailyHireRate,
                    Manufacturer = Upobj.Manufacturer
                };
                _Context.Entry(vehicle).State = EntityState.Modified;
                _Context.SaveChanges();
                VehicleViewModel obj1 = new VehicleViewModel()
                {
                    VehicleID = vehicle.VehicleID
                };
                return obj1;
            }
        }
        public class VechileImageRepository : IVechileImage
        {
            private readonly ApplicationDbContext _Context;
            public VechileImageRepository(ApplicationDbContext Context)
            {
                this._Context = Context;
            }

            public void Delete(int id)
            {
                VechileImage image = _Context.VechileImages.AsNoTracking().SingleOrDefault(e => e.ImageId == id);
                _Context.VechileImages.Remove(image);
                _Context.SaveChanges();
            }

            public IEnumerable<VechileImageViewModel> GetAll()
            {
                throw new NotImplementedException();
            }

            public VechileImageViewModel GetById(int id)
            {
               VechileImage  e = _Context.VechileImages.AsNoTracking().SingleOrDefault(e => e.ImageId == id);
                VechileImageViewModel image = new VechileImageViewModel
                {
                    ImageId = e.ImageId,
                    ImageName = e.ImageName,
                    VehicleID = e.VehicleID
                };
                return image;
            }

            public void Save(VechileImageViewModel obj)
            {
                VechileImage vehicelImage = new VechileImage()
                {
                    ImageName = obj.ImageName,
                    VehicleID = obj.VehicleID
                };
                _Context.VechileImages.Add(vehicelImage);
                _Context.SaveChanges();
             
            }

            public void Update(VechileImageViewModel Upobj)
            {
                VechileImage vehicelImage = new VechileImage()
                {
                    ImageName = Upobj.ImageName,
                    VehicleID = Upobj.VehicleID
                };
                _Context.Entry(vehicelImage).State = EntityState.Modified;
                _Context.SaveChanges();
            }
            public IEnumerable<VechileImageViewModel> GetImagesId(int id)
            {
                IEnumerable<VechileImageViewModel> listofImage = _Context.VechileImages.Where(e => e.VehicleID == id).Select(i => new VechileImageViewModel
                {
                    ImageId=i.ImageId,
                    ImageName=i.ImageName,
                    VehicleID = i.VehicleID
                    

                }).ToList();
                return listofImage;
            }
        }
        public class PaymentTypeRepository: IPaymentType
        {
            private readonly ApplicationDbContext _context;
            public PaymentTypeRepository(ApplicationDbContext context)
            {
                this._context = context;
            }
            public void Delete(int id)
            {
                Payment_Type category = _context.Payment_Types.Find(id);
                _context.Payment_Types.Remove(category);
                Save();
            }

            public IEnumerable<PaymentTypeViewModel> GetAll()
            {
                IEnumerable<PaymentTypeViewModel> listOfPayType = _context.Payment_Types.Select(e => new PaymentTypeViewModel
                {
                    PaymentTypeCode = e.PaymentTypeCode,
                    PaymentDiscription = e.PaymentDiscription
                }).ToList();
                return listOfPayType;
            }

            public PaymentTypeViewModel GetById(int id)
            {
                Payment_Type c = _context.Payment_Types.SingleOrDefault(e => e.PaymentTypeCode == id);
                PaymentTypeViewModel payType = new PaymentTypeViewModel
                {
                    PaymentTypeCode = c.PaymentTypeCode,
                    PaymentDiscription = c.PaymentDiscription

                };
                return payType;
            }

            public void Insert(PaymentTypeViewModel objModel)
            {
                Payment_Type obj = new Payment_Type()
                {
                    PaymentDiscription = objModel.PaymentDiscription
                };
                _context.Payment_Types.Add(obj);
                Save();
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Update(PaymentTypeViewModel objModel)
            {
                Payment_Type obj = new Payment_Type();
                obj.PaymentTypeCode = objModel.PaymentTypeCode;
                obj.PaymentDiscription = objModel.PaymentDiscription;
                _context.Entry(obj).State = EntityState.Modified;
                Save();
            }
        }
    }
}
