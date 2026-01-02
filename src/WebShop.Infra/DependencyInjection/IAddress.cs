using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infra.Identity;

namespace WebShop.Infra.DependencyInjection
{
    public interface IAddress
    {
        Task<IEnumerable<Address>> GetUserAddressesAsync(string userId);
        Task<Address?> GetAddressByIdAsync(int addressId, string userId);
        Task<int> SaveAddressAsync(Address address);
        Task SetDefaultAddressAsync(int addressId, string userId);
        Task UpdateAddressAsync(Address address);
    }

}
