using Application.DTOs;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Activity, Activity>().ReverseMap();
        CreateMap<CreateActivityDto, Activity>().ReverseMap();
    }
}