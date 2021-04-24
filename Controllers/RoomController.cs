using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;

namespace WebAppHotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private HotelDBEntities objHotelDBEntities;
        public RoomController()
        {
            objHotelDBEntities = new HotelDBEntities();

        }
        // GET: Room
        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDBEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();
            objRoomViewModel.ListOfRoomType = (from obj in objHotelDBEntities.RoomTypes
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.RoomTypeName,
                                                        Value = obj.RoomTypeId.ToString()
                                                    }).ToList();
            return View(objRoomViewModel);
        }
    }
}