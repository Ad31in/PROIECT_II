using AutoMapper;
using Business.DTOs;
using DataLogic.Entities;

namespace Business.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserBriefDto>();

            CreateMap<Room, RoomDto>();
            CreateMap<Room, RoomDetailsDto>();

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo.Url));
            CreateMap<MovieDto, Movie>();

            CreateMap<Movie, MovieBriefDto>();
            CreateMap<CreateMovieDto, Movie>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new TimeSpan(src.DurationInHours, src.DurationInMinutes, 0)));

            CreateMap<Seat, SeatDto>();

            CreateMap<Ticket, TicketDto>();

            CreateMap<Showing, ShowingDto>();
            CreateMap<Showing, ShowingBriefDto>();

            CreateMap<Genre, GenreDto>().ReverseMap();
        }
    }
}
