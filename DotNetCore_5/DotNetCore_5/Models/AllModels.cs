using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_5.Models
{
    public class AllModels
    {
        public class Models
        {
            public class Customer
            {
                [Key]
                [Required]
                public int CustomerId { get; set; }
                [Required, MaxLength(50)]
                public string FirstName { get; set; }
                [Required, MaxLength(50)]
                public string LastName { get; set; }
                [Required]
                
                public DateTime DateOfBirth { get; set; }
                [Required, MaxLength(150)]
                public string Address { get; set; }
                [Required, MaxLength(20)]
                public string City { get; set; }
                [Required, MaxLength(30)]
                public string PostCode { get; set; }
                [Required, MaxLength(50)]
                public string Email { get; set; }
                [Required, MaxLength(12)]
                public string PhoneNumber { get; set; }
                
                public string ImageFullPath { get; set; }
                public string ImageName { get; set; }
                public virtual ICollection<Booking> Bookings { get; set; }
            }

            public class Vehicle
            {
                [Key]
                [Required]
                public int VehicleID { get; set; }
                [Required, MaxLength(15)]
                public string RegistrationNumber { get; set; }
                [Required, MaxLength(30)]
                public string Model { get; set; }
                [Required, MaxLength(30)]
                public string Manufacturer { get; set; }
                [Required, Column(TypeName = "decimal(16, 2)")]
                public decimal DailyHireRate { get; set; }
                public virtual ICollection<VechileImage> VechileImages { get; set; }
                public virtual ICollection<Booking> Bookings { get; set; }
                public virtual ICollection<Vehicle_Availability> Vehicle_Availabilities { get; set; }

            }
            public class VechileImage
            {
                [Key]
                public int ImageId { get; set; }

                [Required]
                public string ImageName { get; set; }

                [Required]
                public int VehicleID { get; set; }

                [ForeignKey("VehicleID")]
                public virtual Vehicle Vehicle { get; set; }
            }

            public class Vehicle_Availability
            {
                [Key]
                public int ID { get; set; }
                [Required]
                public int VehicleID { get; set; }
                [Required]
                public int Availability_CodeID { get; set; }
                [Required]
                public DateTime Availability_Date { get; set; }
                [ForeignKey("VehicleID")]
                public virtual Vehicle Vehicle { get; set; }
                [ForeignKey("Availability_CodeID")]
                public virtual Availability_Code Availability_Code { get; set; }
            }
            public class Availability_Code
            {
                [Key]
                public int Availability_CodeID { get; set; }
                [Required, MaxLength(10)]
                public string AvailabilityCode { get; set; }
                [Required, MaxLength(100)]
                public string Description { get; set; }
                public virtual ICollection<Vehicle_Availability> Vehicle_Availability { get; set; }
            }
            public class Payment
            {
                [Key]
                public int PaymentId { get; set; }
                [Required]
                public DateTime PaymentDate { get; set; }
                [Required, Column(TypeName = "decimal(16, 2)")]
                public decimal PaymentAmount { get; set; }
                [Required]
                public int PaymentTypeCode { get; set; }
                public virtual Payment_Type Payment_Type { get; set; }
               
            }
            public class Payment_Type
            {
                [Key]
                public int PaymentTypeCode { get; set; }
                [Required, MaxLength(30)]
                public string PaymentDiscription { get; set; }
                public virtual ICollection<Payment> Payments { get; set; }
            }
            public class Booking
            {
                [Key]
                public int BookingId { get; set; }
                [Required]
                public int CustomerId { get; set; }
                [Required]
                public DateTime PickUpDate { get; set; }
                [Required]
                public DateTime DropOutDate { get; set; }
                [Required]
                public string DropPoint { get; set; }
                [Required]
                public string HirePoint { get; set; }
                
                [Required]
                public int VehicleID { get; set; }
              
                [ForeignKey("VehicleID")]
                public virtual Vehicle Vehicle { get; set; }
               
                public virtual ICollection<Booking_Status_Code> Booking_Status_Codes { get; set; }
                [ForeignKey("CustomerId")]
                public virtual Customer Customer { get; set; }
                
            }
            public class Booking_Status_Code
            {
                [Key]
                public int BookingStatusId { get; set; }
                [Required, MaxLength(100)]
                public string Status { get; set; }
                [Required]
                [ForeignKey("BookingId")]
                public int BookingId { get; set; }
                public virtual Booking Bookings { get; set; }
            }
        }
    }
}
