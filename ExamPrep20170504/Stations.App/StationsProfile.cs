using System.Linq;
using AutoMapper;
using Stations.DataProcessor.Dto;
using Stations.Models;

namespace Stations.App
{
	public class StationsProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public StationsProfile()
		{
            CreateMap<StationDto, Station>().ReverseMap();

            CreateMap<ClassesDto, SeatingClass>().ReverseMap();

            CreateMap<SeatDto, TrainSeat>().ReverseMap()
                .ForMember(dto => dto.Name, sc => sc.MapFrom(s => s.SeatingClass.Name))
                .ForMember(dto => dto.Abbreviation, sc => sc.MapFrom(s => s.SeatingClass.Abbreviation));

            CreateMap<TrainDto, Train>().ReverseMap()
                .ForMember(dto => dto.Type, tr => tr.MapFrom(t => t.Type.ToString()));

            CreateMap<TripDto, Trip>().ReverseMap();

            CreateMap<CardDto, CustomerCard>().ReverseMap();
		}
	}
}
