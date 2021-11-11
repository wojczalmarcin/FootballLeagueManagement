using Application.DTO;
using Application.Interfaces.Services;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlayerStatType
{
    /// <summary>
    /// Player stat type service
    /// </summary>
    public class PlayerStatTypeService : Service, IPlayerStatTypeService
    {
        private readonly IPlayerStatTypeValidator _playerStatTypeValidator;

        private readonly IPlayerStatTypeRepository _playerStatTypeRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="playerStatTypeValidator">The player stat type validator</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="playerStatsRepository">The player stat type repository</param>
        public PlayerStatTypeService(IPlayerStatTypeValidator playerStatTypeValidator, 
            IMapper mapper,
            IPlayerStatTypeRepository playerStatTypeRepository) : base(mapper)
        {
            _playerStatTypeValidator = playerStatTypeValidator;
            _playerStatTypeRepository = playerStatTypeRepository;
        }

        /// <summary>
        /// Gets all player stats type
        /// </summary>
        /// <returns>Response data with collection of <see cref="PlayerStatTypeDto"/></returns>
        public async Task<ResponseData<IEnumerable<PlayerStatTypeDto>>> GetAllPlayerStatTypesAsync()
            => await GetAllAsync<PlayerStatTypeDto, Domain.Entities.PlayerStatType>(_playerStatTypeRepository.GetAllPlayerStatTypesAsync,
                 _playerStatTypeValidator.ValidatePlayerStatTypeExistence);
    }
}
