using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.Agent;
using Property.DTOs.Reservation;
using Property.Models;
using Property.Services.OtherServices;

namespace Property.Services.ReservationServices
{
    public class ReservationServices : IReservationServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetReservationDTO>>> AddReservation(AddReservationDTO newReservation)
        {
            var serviceResponse = new ServiceResponse<List<GetReservationDTO>>();
            var reservation = _mapper.Map<Reservation>(newReservation);

            bool result; int number;

            // Get Agent
            var product = await _context.ProductsRealEstate
                .Include(x => x.Rent)
                .Include(x => x.Rent.RentRealEstatePerDay)
                .Include(x => x.Rent.RentRealEstatePerMounth)
                .FirstOrDefaultAsync(x => x.Id == newReservation.ProductRealEstateId);
            
            if (product is not null)
            {
                reservation.ProductRealEstate = product;
            }

            TimeSpan duration = newReservation.Departure.ToDateTime(TimeOnly.Parse("10:00 PM")) - newReservation.Arrival.ToDateTime(TimeOnly.Parse("10:00 PM"));

            reservation.Amount = duration.TotalDays * product.Price;
            reservation.ReservationFee = 5;

            reservation.NumberOfPeople = newReservation.NumberOfPeople;

            reservation.Date = DateTime.Now;

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = await _context.Reservations
                .Select(x => _mapper.Map<GetReservationDTO>(x)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReservationDTO>> GetReservationById(int id)
        {
            var reservation = await _context.Reservations
                .Include(x => x.applicationUser)
                .Include(x => x.ProductRealEstate)
                .FirstOrDefaultAsync(x => x.Id == id);

            var serviceResponse = new ServiceResponse<GetReservationDTO>()
            {
                Data = _mapper.Map<GetReservationDTO>(reservation)
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReservationDTO>>> GetAllReservations()
        {
            var agents = await _context.Reservations
                .Include(c => c.applicationUser)
                .Include(c => c.ProductRealEstate)
                .ToListAsync();
            var serviceResponse = new ServiceResponse<List<GetReservationDTO>>()
            {
                Data = agents.Select(p => _mapper.Map<GetReservationDTO>(p)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReservationDTO>> UpdateReservation(UpdateReservationDTO updatedReservation)
        {
            var serviceResponse = new ServiceResponse<GetReservationDTO>();

            try
            {
                var reservation = await _context.Reservations
                    .FirstOrDefaultAsync(x => x.Id == updatedReservation.Id);
                if (reservation is null) { throw new Exception($"Reservation with Id '{updatedReservation.Id}' not found"); }

                reservation.UserName = updatedReservation.UserName;
                reservation.UserEmail = updatedReservation.UserEmail;
                reservation.NumberOfPeople = updatedReservation.NumberOfPeople;
                reservation.Arrival = updatedReservation.Arrival; 
                reservation.Departure = updatedReservation.Departure;
                DateTime dateTimeValue = DateTime.Now; // Example DateTime value
                DateOnly dateOnlyValue = new DateOnly(dateTimeValue.Year, dateTimeValue.Month, dateTimeValue.Day);


                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetReservationDTO>(reservation);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReservationDTO>>> DeleteReservation(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetReservationDTO>>();

            try
            {
                var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
                if (reservation is null) { throw new Exception($"Reservation with Id '{id}' not found"); }

                _context.Reservations.Remove(reservation);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Reservations
                    .Select(c => _mapper.Map<GetReservationDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
