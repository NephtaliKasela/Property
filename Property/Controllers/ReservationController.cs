using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Property.DTOs.Actions;
using Property.DTOs.Country;
using Property.DTOs.Reservation;
using Property.Models;
using Property.Services.AgentServices;
using Property.Services.ProductService.ProductServicesRealEstate;
using Property.Services.ReservationServices;

namespace Property.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductServicesRealEstate _productServicesRealEstate;
        private readonly IReservationServices _reservationServices;

        public ReservationController(UserManager<ApplicationUser> userManager, IProductServicesRealEstate productServicesRealEstate, IReservationServices reservationServices)
        {
            _userManager = userManager;
            _productServicesRealEstate = productServicesRealEstate;
            _reservationServices = reservationServices;
        }

		[HttpGet]
		public async Task<IActionResult> Index(int id)
        {
            var product = await _productServicesRealEstate.GetProductById(id);
            if(product.Data != null)
            {
                return View(product.Data);
            }
            return RedirectToAction("Properties", "MarketPlace");
        }

        [Authorize(Policy = "AdminRole")]
        [HttpGet]
        public async Task<IActionResult> GetReservation()
        {
            var reservations = await _reservationServices.GetAllReservations();
            return View(reservations.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateReservation(int id)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var reservation = await _reservationServices.GetReservationById(id);

                if (reservation != null && reservation.Data.applicationUser.Id == user.Id)
                {
                    return View(reservation.Data);
                }
                return RedirectToAction("Properties", "MarketPlace");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddReservation(AddReservationDTO newReservation)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                newReservation.applicationUser = user;
                await _reservationServices.AddReservation(newReservation);
            }
           

            return RedirectToAction("Properties", "MarketPlace");
        }

        [HttpPost]
        public async Task<IActionResult> SaveUpdateReservation(UpdateReservationDTO updatedReservation)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var reservation = await _reservationServices.GetReservationById(updatedReservation.Id);

                if (reservation != null && reservation.Data.applicationUser.Id == user.Id)
                {
                    var result = await _reservationServices.UpdateReservation(updatedReservation);
                    if (result.Success) { return RedirectToAction("Properties", "MarketPlace"); }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var reservation = await _reservationServices.GetReservationById(id);

                if (reservation != null && reservation.Data.applicationUser.Id == user.Id)
                {
                    var result = await _reservationServices.DeleteReservation(id);
                    if (result.Success) { return RedirectToAction("Properties", "MarketPlace"); }
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
