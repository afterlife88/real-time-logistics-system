using CargoTrack.OrderService.Contracts;
using CargoTrack.OrderService.Data;
using CargoTrack.OrderService.Data.Contracts.Base;
using CargoTrack.OrderService.Infrastructure;
using log4net;

namespace CargoTrack.OrderService
{
    [AutomapServiceBehavior]
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly ILog _log = LogManager.GetLogger(typeof(OrderService));
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataDbContext _dbContext;
        
        #endregion

        #region Constructors
        public OrderService(IUnitOfWork unitOfWork, DataDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        #endregion

        public void DoWork()
        {
        }
    }
}
