using Domain.Dtos.Reservation;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfases;

public interface IReservationService
{
    Task<PagedResponse<List<GetReservationDto>>> GetAll(ReservationFilter filter);
    Task<Response<GetReservationDto>> Create(CreateReservationDto reservationDto);
    Task<Response<GetReservationDto>> Update(int id, UpdateReservationDto reservationDto);
    Task<Response<GetReservationDto>> Get(int id);
    Task<Response<string>> Delete(int id);
}