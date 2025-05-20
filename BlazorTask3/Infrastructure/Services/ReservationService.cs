using System.Net;
using AutoMapper;
using Domain.Dtos.Reservation;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfases;

namespace Infrastructure.Services;

public class ReservationService(DataContext context, IMapper mapper) : IReservationService
{
    public async Task<PagedResponse<List<GetReservationDto>>> GetAll(ReservationFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

        var Reservations = context.Reservations.AsQueryable();

        if (filter.TableId != null)
        {
            Reservations = Reservations.Where(r => r.TableId == filter.TableId);
        }
        if (filter.CustomerId != null)
        {
            Reservations = Reservations.Where(r => r.CustomerId == filter.CustomerId);
        }
           
        if (filter.From != null)
        {
            var now = DateTime.Now.Year;
            Reservations = Reservations.Where(r => now - r.ReservationDate.Year <= filter.From);
        }

        if (filter.To != null)
        {
            var now = DateTime.Now.Year;
            Reservations = Reservations.Where(r => now - r.ReservationDate.Year >= filter.To);
        }
        

        var mapped = mapper.Map<List<GetReservationDto>>(Reservations);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageNumber * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();


        return new PagedResponse<List<GetReservationDto>>(data, validFilter.PageNumber, validFilter.PageSize,
            totalRecords);
    }

    public async Task<Response<GetReservationDto>> Create(CreateReservationDto reservationDto)
    {
        var Reservation = mapper.Map<Reservation>(reservationDto);

        var table = await context.Tables.FindAsync(reservationDto.TableId);
        if (table == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Not Found Table");
        }
        
        var customer = await context.Customers.FindAsync(reservationDto.CustomerId);
        if (customer == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Not Found Customer");
        }
        
        await context.Reservations.AddAsync(Reservation);

        var res = await context.SaveChangesAsync();

        if (res == 0)
        {
            return new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Not Added");
        }

        return mapper.Map<Response<GetReservationDto>>(Reservation);
    }

    public async Task<Response<GetReservationDto>> Update(int id, UpdateReservationDto reservationDto)
    {
        var Reservation = await context.Reservations.FindAsync(id);
        if (Reservation == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Not Found");
        }

        Reservation.ReservationDate = reservationDto.ReservationDate;
        Reservation.From = reservationDto.From;
        Reservation.To = reservationDto.To;
        Reservation.TableId = reservationDto.TableId;
        Reservation.CustomerId = reservationDto.CustomerId;
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetReservationDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetReservationDto>>(Reservation);
    }

    public async Task<Response<GetReservationDto>> Get(int id)
    {
        var Reservation = await context.Reservations.FindAsync(id);
        if (Reservation == null)
        {
            return new Response<GetReservationDto>(HttpStatusCode.NotFound, "Not Found");
        }

        return mapper.Map<Response<GetReservationDto>>(Reservation);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Reservation = await context.Reservations.FindAsync(id);

        if (Reservation == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not Found");
        }
        context.Reservations.Remove(Reservation);
        var res = await context.SaveChangesAsync();
        
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Not Deleted")
            : new Response<string>(HttpStatusCode.OK, "Deleted");
    }
}