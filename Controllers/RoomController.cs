﻿using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            String message = String.Empty;
            String ImageUniqueName = String.Empty;
            String ActualImageName = String.Empty;
            if (objRoomViewModel.RoomId == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);

                objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
               
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    isActive = true,
                    RoomImage = ActualImageName,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId,
                    
                };
                objHotelDBEntities.Rooms.Add(objRoom);
                message = "Added";
            }
            else
            {
                Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                if ( objRoomViewModel.Image != null)
                {
                     ImageUniqueName = Guid.NewGuid().ToString();
                     ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                     objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                     objRoom.RoomImage = ActualImageName;

                }
                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.isActive = true;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                message = "Updated";
            }
            

           
            objHotelDBEntities.SaveChanges();
            return Json(new { message = "Room Successfully " + message, success = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                (from objRoom in objHotelDBEntities.Rooms
                 join objBooking in objHotelDBEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                 join objRoomType in objHotelDBEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 where objRoom.isActive == true
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     RoomId = objRoom.RoomId
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }
        
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDBEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHotelDBEntities.Rooms.Single(model => model.RoomId == roomId);
            objRoom.isActive = false;
            objHotelDBEntities.SaveChanges();
            return Json(new { message = "Record successfully deleted.", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}