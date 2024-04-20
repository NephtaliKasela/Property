using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property.Data;
using Property.DTOs.Agent;
using Property.DTOs.Reservation;
using Property.Models;

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
            var product = await _context.ProductsRealEstate.FirstOrDefaultAsync(x => x.Id == newReservation.ProductRealEstateId);
            if (product is not null)
            {
                reservation.ProductRealEstate = product;
            }

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
                reservation.NumberOfGuest = updatedReservation.NumberOfGuest;
                reservation.CheckIn = updatedReservation.CheckIn;
                reservation.CheckOut = updatedReservation.CheckOut;

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
