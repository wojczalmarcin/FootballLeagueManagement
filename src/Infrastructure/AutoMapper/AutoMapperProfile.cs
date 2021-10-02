using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Match, MatchDto>().ReverseMap();
            CreateMap<MatchScore, MatchScoreDto>().ReverseMap();
            CreateMap<Season, SeasonDto>().ReverseMap();
            CreateMap<Stadium, StadiumDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, TeamStatisticsDto>();
            CreateMap<Season, CreateSeasonDto>().ReverseMap();
            CreateMap<CreateSeasonDto, SeasonDto>().ReverseMap();
        }

    }
}
