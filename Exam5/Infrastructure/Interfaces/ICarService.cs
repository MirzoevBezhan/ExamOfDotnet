using Domain.DTOs;
using Domain.DTOs.Booking;
using Domain.DTOs.Car;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Responces;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    public Task<Response<List<GetCarDto>>> GetAll();
    public Task<Response<GetCarDto>> Get(int id);
    public Task<Response<GetCarDto>> Update(int id, UpdateCarDto updateCarDto);
    public Task<Response<string>> Delete(int id);
    public Task<Response<GetCarDto>> Create(CreateCarDto createCarDto);
    public Task<Response<List<AvailableCarDto>>> GetAvailableCarDtos();
    public Task<Response<List<PopularCarDto>>> GetPopularCars();

}
