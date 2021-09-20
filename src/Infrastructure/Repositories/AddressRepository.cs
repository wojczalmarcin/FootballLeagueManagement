using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// The address repository
    /// </summary>
    public class AddressRepository : IAddressRepository
    {
        //Data base context
        private readonly FootballLeagueDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext">FootballLeague Data base context</param>
        public AddressRepository(FootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets address by it's Id
        /// </summary>
        /// <param name="addressId">address Id</param>
        /// <returns>Address</returns>
        public async Task<Address> GetAddressByIdAsync(int addressId)
            => await _dbContext.Addresses.AsNoTracking().FirstOrDefaultAsync(a => a.Id == addressId);
    }
}
