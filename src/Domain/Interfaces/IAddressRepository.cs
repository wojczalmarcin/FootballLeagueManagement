using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAddressRepository
    {
        /// <summary>
        /// Gets address by it's Id
        /// </summary>
        /// <param name="addressId">address Id</param>
        /// <returns>Address</returns>
        Task<Address> GetAddressByIdAsync(int addressId);
    }
}
