using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlayerSuspension
{
    /// <summary>
    /// The player suspension service
    /// </summary>
    public class PlayerSuspensionService : Service, IPlayerSuspensionService
    {
        private IPlayerSuspensionLogRepository _playerSuspensionLogRepository;

        private IPlayerSuspensionValidator _playerSuspensionValidator;

        public PlayerSuspensionService(
            IMapper mapper,
            IPlayerSuspensionLogRepository playerSuspensionLogRepository,
            IPlayerSuspensionValidator playerSuspensionValidator) : base(mapper)
        {
            _playerSuspensionLogRepository = playerSuspensionLogRepository;
            _playerSuspensionValidator = playerSuspensionValidator;
        }

        /// <summary>
        /// Gets suspension by id
        /// </summary>
        /// <param name="suspensionId">suspension id</param>
        /// <returns>Response data with <see cref="PlayerSuspensionDto"/></returns>
        public async Task<ResponseData<PlayerSuspensionDto>> GetPlayerSusnepsionByIdAsync(int suspensionId)
            => await GetByIdAsync<PlayerSuspensionDto, Domain.Entities.PlayerSuspensionLog>(suspensionId,
                _playerSuspensionLogRepository.GetPlayerSuspensionLogByIdAsync,
                _playerSuspensionValidator.ValidatePlayerSuspensionExistence);

        /// <summary>
        /// Gets suspension by player id
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>Response data with the collection of <see cref="PlayerSuspensionDto"/></returns>
        public async Task<ResponseData<IEnumerable<PlayerSuspensionDto>>> GetPlayerSusnepsionByPlayerIdAsync(int playerId)
        {
            var responseData = new ResponseData<IEnumerable<PlayerSuspensionDto>>();
            var playerSuspension = _mapper.Map<IEnumerable<PlayerSuspensionDto>>(await _playerSuspensionLogRepository.GetPlayerSuspensionLogByPlayerIdAsync(playerId));
            responseData.ResponseStatus = HttpStatusCode.OK;
            responseData.Data = playerSuspension;
            return responseData;
        }
    }
}
