using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_5.ViewModels
{
    public class CustomerViewModel
    {

        [Key]
        [Required]
        [Display(Name = "Id")]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Fisrt Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile No")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }
        [Display(Name = "Image Name")]
        public int SerialNo { get; set; }
        public IFormFile Photo { get; set; }
        public string FullName { get; set; }
        public string ImageFullPath { get; set; }
        public virtual CustomerMultipleInsert CustomerMultipleInserts { get; set; }
    }
    public class CustomerMultipleInsert
    {
        public virtual List<CustomerViewModel> CustomerViewModels { get; set; }
    }
    public class VehicleViewModel
    {
        [Key]
        [Required]
        [Display(Name = "Vehicle Id")]
        public int VehicleID { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Required]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }
        [Required]
        [Display(Name = "Cost Per Day")]
        public decimal DailyHireRate { get; set; }
        [Display(Name = "Car")]
        public string ImageName { get; set; }
        public int SerialNo { get; set; }
        public List<IFormFile> Edit_Photos { get; set; }
        public List<IFormFile> Photos { get; set; }
       
    }
    public class VechileImageViewModel
    {
        [Key]
        public int ImageId { get; set; }
        [Display(Name = "Car")]
        public string ImageName { get; set; }

        [Required]
        [Display(Name = "Vehicle Id")]
        public int VehicleID { get; set; }

    }
    public class Vehicle_AvailabilityViewModel

    {
        [Key]
       
        public int ID { get; set; }
        [Required]
        public int VehicleID { get; set; }
        [Required]
        public int Availability_CodeID { get; set; }
        [Required]
        public DateTime Availability_Date { get; set; }
        public string Model { get; set; }


    }
    public class BookingViewModel
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        [Display(Name = "Pick Up")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }
        [Required]
        [Display(Name = "Drop Out Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DropOutDate { get; set; }
        [Required]
        [Display(Name = "Drop Point")]
        public string DropPoint { get; set; }
        [Required]
        [Display(Name = "Pick Up Point")]
        public string HirePoint { get; set; }
        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleID { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Model { get; set; }
        public int TotalDaysOfHiring { get; set; }
        public int Total { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Daily HireRate")]
        public decimal DailyHireRate { get; set; }
        [Display(Name = "Iamges")]
        public string ImageName { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public int SerialNo { get; set; }
        public virtual VehicleViewModel VehicleViewModel { get; set; }
    }
    public class Booking_Status_CodeViewModel
    {
        [Key]
        public int BookingStatusId { get; set; }
        [Required]
        [Display(Name = "Status")]
        
        public string Status { get; set; }
        public int BookingId { get; set; }
    }
    public class PaymentTypeViewModel 
    {
        [Key]
        public int PaymentTypeCode { get; set; }
        [Required, MaxLength(30)]
        [Display(Name ="Payment Type ")]
        public string PaymentDiscription { get; set; }
    }
}
