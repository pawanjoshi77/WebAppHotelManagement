﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppHotelManagement.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name ="Customer Name")]
        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer Address is required")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Customer Phone is required")]
        [Display(Name = "Customer Phone")]
        public string CustomerPhone { get; set; }


        [Display(Name = "Booking From")]        
        [Required(ErrorMessage = "Booking From is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]        
        [Required(ErrorMessage = "Booking To is required")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Room is required")]
        public int AssignRoomId { get; set; }

        [Display(Name = "Number of Members")]
        [Required(ErrorMessage = "Number of Members is required")]
        public int NumberOfMembers { get; set; }

        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }

        public IEnumerable<SelectListItem> ListOfRooms { get; set; }
    }
}