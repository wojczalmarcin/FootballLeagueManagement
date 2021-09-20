using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.Match
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        private readonly IMapper _mapper;

        private MatchValidator matchValidator;
        public MatchService(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
            matchValidator = new MatchValidator();
        }

        /// <summary>
        /// Gets match data by match id
        /// </summary>
        /// <param name="matchId">match id</param>
        /// <returns>Response with match data</returns>
        public async Task<ResponseData<MatchDto>> GetMatchDataById(int matchId)
        {
            var responseData = new ResponseData<MatchDto>();

            var match = _mapper.Map<MatchDto>(await _matchRepository.GetMatchByIdAsync(matchId));
            var matchValidation = matchValidator.ValidateMatchExistence(match);

            responseData.ResponseStatus = matchValidation.statusCode;
            responseData.ValidationErrors = matchValidation.validationErrors;

            if(matchValidation.statusCode == HttpStatusCode.OK)
            {
                responseData.Data = match;
            }

            return responseData;
        }
    }
}
