using Application.Common.Models.ResponseModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Booking, BookingResponseModel>();
    }
}