using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.Persistence;
using WebShop.Infra.DependencyInjection;
using WebShop.Infra.Identity;

namespace WebShop.Infra.Services
{
    public class AddressService : IAddress
    {
        private readonly AppDbContext _context;

        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetUserAddressesAsync(string userId)
        {
            return await _context.Addresses
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.IsDefault)
                .ToListAsync();
        }

        public async Task<Address?> GetAddressByIdAsync(int addressId, string userId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);
        }

        public async Task<int> SaveAddressAsync(Address address)
        {
            if (address.IsDefault)
            {
                await _context.Addresses
                    .Where(a => a.UserId == address.UserId && a.Id != address.Id)
                    .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsDefault, false));
            }

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return address.Id;
        }

        public async Task SetDefaultAddressAsync(int addressId, string userId)
        {
            await _context.Addresses
                .Where(a => a.UserId == userId && a.Id != addressId)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsDefault, false));

            var address = await _context.Addresses.FindAsync(addressId);
            if (address != null && address.UserId == userId)
            {
                address.IsDefault = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task SetDefaultAddressAsync(Address address)
        {
            if (address == null) return;

            await SetDefaultAddressAsync(address.Id, address.UserId);
        }
    }

}
