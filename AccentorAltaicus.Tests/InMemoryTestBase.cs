using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.MsSql;
using Microsoft.EntityFrameworkCore;
using UseCases.Order.Utils;

namespace AccentorAltaicus.Tests
{
    public abstract class InMemoryTestBase
    {
        protected IDbContext DbContext { get; private set; }

        protected IMapper _mapper;

        protected InMemoryTestBase()
        {
            Init();
        }

        protected abstract void Reset();

        private void Init()
        {
            _mapper = new MapperConfiguration(c => c.AddProfile<MapperProfile>()).CreateMapper();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ApplicationDbContext")
                .Options;

            DbContext = new AppDbContext(options);

            Populate();
            
            DbContext.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);

            Reset();
        }

        private void Populate()
        {
            DbContext.EnsureDeleted();

            PopulateApplicationUserData();
        }

        private void PopulateApplicationUserData()
        {
            DbContext.Orders.AddRange(ApplicationTestData.Orders);
            DbContext.Products.AddRange(ApplicationTestData.Products);
            DbContext.OrderItems.AddRange(ApplicationTestData.OrderItems);
        }
    }
}
