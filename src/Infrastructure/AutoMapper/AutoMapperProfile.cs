using Application.DTO;
using AutoMapper;
using Domain.Entities;

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
            CreateMap<Member, MemberDto>().ReverseMap();
            CreateMap<MemberRole, MemberRoleDto>().ReverseMap();
            CreateMap<MemberEditDto, MemberDto>();
            CreateMap<PlayerStatType, PlayerStatTypeDto>().ReverseMap();
            CreateMap<PlayerStatsLog, PlayerStatsDto>().ReverseMap();
            CreateMap<CreateMatchDto, MatchDto>();
            CreateMap<CreateMatchDto, Match>();
            CreateMap<CreatePlayerStatsDto, PlayerStatsDto>();
            CreateMap<CreatePlayerStatsDto, PlayerStatsLog>();
        }

    }
}
