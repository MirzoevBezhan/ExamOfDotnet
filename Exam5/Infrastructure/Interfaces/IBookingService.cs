using Domain.DTOs.Booking;
using Domain.DTOs.User;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface IBookingService
{   
    public Task<Response<List<GetBookingDto>>> GetAll();
    public Task<Response<GetBookingDto>> Get(int id);
    public Task<Response<GetBookingDto>> Update(int id,UpdateBookingDto updateBookingDto);
    public Task<Response<string>> Delete(int id);
    public Task<Response<GetBookingDto>> Create(CreateBookingDto createBookingDto);
}
