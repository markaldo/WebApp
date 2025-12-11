using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebShop.Core.Entities;
using WebShop.Core.Repositories;
using WebShop.Infra.Persistence;

namespace WebShop.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        //public async Task AddAsync(Order order)
        //{
        //    await _context.Orders.AddAsync(order);
        //}

        //public void  DeleteAsync(int id)
        //{
        //    var order = _context.Orders.Where(o => o.Id == id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //}

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
               .Include(o => o.OrderLines)
               .ThenInclude(ol => ol.Product)
               .FirstOrDefaultAsync(o => o.Id == id);

        }

        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        //public void UpdateAsync(Order order)
        //{
        //    _context.Orders.Update(order);
            
            
        //}
    }
}
