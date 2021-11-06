using Application.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// The service class
    /// </summary>
    public abstract class Service
    {
        // The mapper
        protected readonly IMapper _mapper;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="mapper"></param>
        public Service(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Gets data tranfer object by it's Id
        /// </summary>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="repositoryFunc">Function from repository</param>
        /// <param name="validatorFunc">Function from validator</param>
        /// <returns>Response data with data transfer objects</returns>
        protected async Task<ResponseData<Dto>> GetByIdAsync<Dto, Entity>(int id, 
            Func<int, Task<Entity>> repositoryFunc,
            Func<Dto, (HttpStatusCode statusCode, List<string> validationErrors)> validatorFunc)
            where Dto : class 
            where Entity : class
        {
            var responseData = new ResponseData<Dto>();

            var season = _mapper.Map<Dto>(await repositoryFunc(id));
            var seasonValidation = validatorFunc(season);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = season;
            return responseData;
        }

        /// <summary>
        /// Gets all data tranfer objects
        /// </summary>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="repositoryFunc">Function from repository</param>
        /// <param name="validatorFunc">Function from validator</param>
        /// <returns>Response data with data transfer objects</returns>
        protected async Task<ResponseData<IEnumerable<Dto>>> GetAllAsync<Dto, Entity>(Func<Task<IEnumerable<Entity>>> repositoryFunc,
            Func<Dto, (HttpStatusCode statusCode, List<string> validationErrors)> validatorFunc)
            where Dto : class
            where Entity : class
        {
            var responseData = new ResponseData<IEnumerable<Dto>>();

            var seasons = _mapper.Map<IEnumerable<Dto>>(await repositoryFunc());
            var seasonValidation = validatorFunc(seasons.ToList()[0]);

            responseData.ResponseStatus = seasonValidation.statusCode;
            responseData.ValidationErrors = seasonValidation.validationErrors;

            if (seasonValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            responseData.Data = seasons;
            return responseData;
        }

        /// <summary>
        /// Edits entity from given data transfer object
        /// </summary>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="editDto">The Dto with edited data</param>
        /// <param name="repositoryFuncGet">Get function from repository</param>
        /// <param name="repositoryFuncEdit">Edit function from repository</param>
        /// <param name="validatorFunc">The validation function</param>
        /// <returns>Response data with edited data</returns>
        protected async Task<ResponseData<Dto>> EditAsync<Dto, Entity>(Dto editDto,
            Func<int, Task<Entity>> repositoryFuncGet,
            Func<Entity, Task<bool>> repositoryFuncEdit,
            Func<Dto, Dto, (HttpStatusCode statusCode, List<string> validationErrors)> validatorFunc)
            where Dto : IDtoWithId
        {
            var responseData = new ResponseData<Dto>();

            var entityToEdit = _mapper.Map<Dto>(await repositoryFuncGet(editDto.Id));

            var editingValidation = validatorFunc(editDto, entityToEdit);

            responseData.ResponseStatus = editingValidation.statusCode;
            responseData.ValidationErrors = editingValidation.validationErrors;

            if (editingValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var editSeason = await repositoryFuncEdit(_mapper.Map<Entity>(editDto));
            if (editSeason)
            {
                responseData.Data = _mapper.Map<Dto>(await repositoryFuncGet(editDto.Id));
            }
            else
            {
                responseData.ValidationErrors.Add("There was a problem with editing this data");
            }

            return responseData;
        }
    }
}
