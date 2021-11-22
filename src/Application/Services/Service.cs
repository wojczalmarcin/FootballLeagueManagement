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

            return await EditAsync(editDto, repositoryFuncGet, repositoryFuncEdit,responseData,editingValidation);
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
            Func<Dto, Dto, Task<(HttpStatusCode statusCode, List<string> validationErrors)>> validatorFunc)
            where Dto : IDtoWithId
        {
            var responseData = new ResponseData<Dto>();

            var entityToEdit = _mapper.Map<Dto>(await repositoryFuncGet(editDto.Id));

            var editingValidation = await validatorFunc(editDto, entityToEdit);

            return await EditAsync(editDto, repositoryFuncGet, repositoryFuncEdit, responseData, editingValidation);
        }

        /// <summary>
        /// Edits entity from given data transfer object
        /// </summary>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="editDto">The Dto with edited data</param>
        /// <param name="repositoryFuncGet">Get function from repository</param>
        /// <param name="repositoryFuncEdit">Edit function from repository</param>
        /// <param name="responseData">The response data</param>
        /// <param name="editingValidation">The validation result</param>
        /// <returns></returns>
        protected async Task<ResponseData<Dto>> EditAsync<Dto, Entity>(Dto editDto,
           Func<int, Task<Entity>> repositoryFuncGet,
           Func<Entity, Task<bool>> repositoryFuncEdit,
           ResponseData<Dto> responseData,
           (HttpStatusCode statusCode, List<string> validationErrors) editingValidation)
           where Dto : IDtoWithId
        {
            responseData.ResponseStatus = editingValidation.statusCode;
            responseData.ValidationErrors = editingValidation.validationErrors;

            if (editingValidation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var editEntity = await repositoryFuncEdit(_mapper.Map<Entity>(editDto));
            if (editEntity)
            {
                responseData.Data = _mapper.Map<Dto>(await repositoryFuncGet(editDto.Id));
            }
            else
            {
                responseData.ValidationErrors.Add("There was a problem with editing this data");
            }

            return responseData;
        }

        /// <summary>
        /// Edits entity from given data transfer object
        /// </summary>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="editDto">The Dto with edited data</param>
        /// <param name="repositoryFuncGet">Get function from repository</param>
        /// <param name="repositoryFuncDelete">Edit function from repository</param>
        /// <param name="validatorFunc">The validation function</param>
        /// <returns>Response data with edited data</returns>
        protected async Task<ResponseData<Dto>> DeleteAsync<Dto, Entity>(int entityId,
            Func<int, Task<Entity>> repositoryFuncGet,
            Func<int, Task<bool>> repositoryFuncDelete,
            Func<Dto, (HttpStatusCode statusCode, List<string> validationErrors)> validatorFunc)
            where Dto : IDtoWithId
        {
            var responseData = new ResponseData<Dto>();

            var entity = _mapper.Map<Dto>(await repositoryFuncGet(entityId));

            var validateDeletion = validatorFunc(entity);

            responseData.ResponseStatus = validateDeletion.statusCode;
            responseData.ValidationErrors = validateDeletion.validationErrors;

            if (validateDeletion.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var entityDeletion = await repositoryFuncDelete(entityId);

            if (entityDeletion)
            {
                responseData.Data = entity;
            }
            else
            {
                responseData.ResponseStatus = HttpStatusCode.InternalServerError;
                responseData.ValidationErrors.Add("There was a problem with deleting this data");
            }

            return responseData;
        }

        /// <summary>
        /// Creates given entity
        /// </summary>
        /// <typeparam name="CreateDto">The creation data transfer object</typeparam>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="entityToCreate">The instance of entity to create</param>
        /// <param name="repositoryFunc">The repository create function</param>
        /// <param name="validatorFunc">The repository validation function</param>
        /// <returns>Response data with created entity</returns>
        public async Task<ResponseData<Dto>> CreateAsync<CreateDto, Dto, Entity>(CreateDto entityToCreate,
            Func<Entity, Task<int>> repositoryFunc,
            Func<CreateDto, Task<(HttpStatusCode statusCode, List<string> validationErrors)>> validatorFunc)
            where Dto : IDtoWithId
        {
            var responseData = new ResponseData<Dto>();

            var validateCreation = await validatorFunc(entityToCreate);

            return await CreateAsync(entityToCreate, repositoryFunc, responseData, validateCreation);

        }

        /// <summary>
        /// Creates given entity
        /// </summary>
        /// <typeparam name="CreateDto">The creation data transfer object</typeparam>
        /// <typeparam name="Dto">The data transfer object</typeparam>
        /// <typeparam name="Entity">The entity</typeparam>
        /// <param name="entityToCreate">The instance of entity to create</param>
        /// <param name="repositoryFunc">The repository create function</param>
        /// <param name="validatorFunc">The repository validation function</param>
        /// <returns>Response data with created entity</returns>
        public async Task<ResponseData<Dto>> CreateAsync<CreateDto, Dto, Entity>(CreateDto entityToCreate,
            Func<Entity, Task<int>> repositoryFunc,
            Func<CreateDto, (HttpStatusCode statusCode, List<string> validationErrors)> validatorFunc)
            where Dto : IDtoWithId
        {
            var responseData = new ResponseData<Dto>();

            var validateCreation = validatorFunc(entityToCreate);

            return await CreateAsync(entityToCreate, repositoryFunc, responseData, validateCreation);
        }

        private async Task<ResponseData<Dto>> CreateAsync<CreateDto, Dto, Entity>(CreateDto entityToCreate,
            Func<Entity, Task<int>> repositoryFunc,
            ResponseData<Dto> responseData,
            (HttpStatusCode statusCode, List<string> validationErrors) validateCreation)
            where Dto : IDtoWithId
        {
            responseData.ResponseStatus = validateCreation.statusCode;
            responseData.ValidationErrors = validateCreation.validationErrors;

            if (validateCreation.statusCode != HttpStatusCode.OK)
            {
                return responseData;
            }

            var entityId = await repositoryFunc(_mapper.Map<Entity>(entityToCreate));

            if (entityId > 0)
            {
                var addedEntity = _mapper.Map<Dto>(entityToCreate);
                addedEntity.Id = entityId;
                responseData.Data = addedEntity;
            }
            else
            {
                responseData.ResponseStatus = HttpStatusCode.InternalServerError;
                responseData.ValidationErrors.Add("There was a problem with creating match");
            }

            return responseData;
        }
    }
}
