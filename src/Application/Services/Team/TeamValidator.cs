using Application.DTO;
using Application.DTO.Create;
using Application.Interfaces.Validators;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Team
{
    /// <summary>
    /// The team validator
    /// </summary>
    public class TeamValidator : Validator, ITeamValidator
    {

        private readonly ITeamRepository _teamRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="teamRepository">The team repository</param>
        /// <param name="mapper">The mapper</param>
        public TeamValidator(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Validates team existence
        /// </summary>
        /// <param name="team">team</param>
        /// <returns>Validation result</returns>
        public (HttpStatusCode statusCode, List<string> validationErrors) ValidateTeamExistence(TeamDto team)
            => ValidateEntityExistence(team, "Team with given Id doesn't exist");

        /// <summary>
        /// Validates team editing
        /// </summary>
        /// <param name="team">The team</param>
        /// <param name="teamToEdit">The team to edit</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateTeamEdit(TeamDto team, TeamDto teamToEdit)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = this.ValidateTeamExistence(teamToEdit);
            if (validation.statusCode != HttpStatusCode.OK)
            {
                return validation;
            }
            return await ValidateTeamName(team.Name);
        }

        /// <summary>
        /// Validates season creation
        /// </summary>
        /// <param name="team">The team to create</param>
        /// <returns>Validation result</returns>
        public async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateTeamCreation(CreateTeamDto team)
            => await ValidateTeamName(team.Name);

        private async Task<(HttpStatusCode statusCode, List<string> validationErrors)> ValidateTeamName(string teamName)
        {
            (HttpStatusCode statusCode, List<string> validationErrors) validation = (HttpStatusCode.OK, new List<string>());

            var teams = _mapper.Map<IEnumerable<TeamDto>>(await _teamRepository.GetAllTeamsAsync()).ToList();

            foreach (var t in teams)
            {
                if (string.Equals(t.Name.ToLower(), teamName.ToLower()))
                {
                    validation.validationErrors.Add($"Team {t.Name} already exists");
                    validation.statusCode = HttpStatusCode.BadRequest;
                }
            }
            return validation;
        }
    }
}
