using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.BalanceQueries
{
    public class BalancesQuery : IQuery<IEnumerable<BalanceDTO>>
    { }

    public class BalancesQueryHandler : IQueryHandler<BalancesQuery, IEnumerable<BalanceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BalancesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<BalanceDTO> Execute(BalancesQuery context)
        {
            var entities = _unitOfWork.BalanceRepository.GetAll().ToList();
            return Mapper.Map<IEnumerable<Balance>, IEnumerable<BalanceDTO>>(entities);
        }
    }
}
