using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CargoTrack.TransactionService.Contracts.Models.DTO;
using CargoTrack.TransactionService.CQRS.Common.Queries;
using CargoTrack.TransactionService.CQRS.Write.Contracts.Base;
using CargoTrack.TransactionService.CQRS.Write.Entities;

namespace CargoTrack.TransactionService.CQRS.ReadStack.TransactionQueries
{
    public class TransactionsQuery : IQuery<IEnumerable<TransactionDTO>>
    { }

    public class TransactionsQueryHandler : IQueryHandler<TransactionsQuery, IEnumerable<TransactionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TransactionDTO> Execute(TransactionsQuery context)
        {
            var entities = _unitOfWork.TransactionRepository.GetAllTransactionsWithLedger().ToList();

            return Mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionDTO>>(entities);
        }
    }
}
