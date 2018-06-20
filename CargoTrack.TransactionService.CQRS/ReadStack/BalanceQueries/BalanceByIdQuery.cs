using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.BalanceQueries
{
    public class BalanceByIdQuery : IQuery<BalanceDTO>
    {
        public BalanceByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class BalanceByIdQueryHandler : IQueryHandler<BalanceByIdQuery, BalanceDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BalanceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BalanceDTO Execute(BalanceByIdQuery context)
        {
            var entity = _unitOfWork.BalanceRepository.GetById(context.Id);
            return Mapper.Map<Balance, BalanceDTO>(entity);
        }
    }
}
