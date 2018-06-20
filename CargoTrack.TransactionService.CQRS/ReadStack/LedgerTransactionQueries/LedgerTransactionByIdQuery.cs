using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.LedgerTransactionQueries
{
    public class LedgerTransactionByIdQuery : IQuery<LedgerTransactionDTO>
    {
        public LedgerTransactionByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class LedgerTransactionByIdQueryHandler : IQueryHandler<LedgerTransactionByIdQuery, LedgerTransactionDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LedgerTransactionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public LedgerTransactionDTO Execute(LedgerTransactionByIdQuery context)
        {
            var entity = _unitOfWork.LedgerTransactionRepository.GetById(context.Id);
            return Mapper.Map<LedgerTransaction, LedgerTransactionDTO>(entity);
        }
    }
}
