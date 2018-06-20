using System.ServiceModel;
using CargoTrack.TransactionService.Contracts.Models.Service.Balance;
using CargoTrack.TransactionService.Contracts.Models.Service.Commands;
using CargoTrack.TransactionService.Contracts.Models.Service.Transaction;

namespace CargoTrack.TransactionService.Contracts
{
    [ServiceContract]
    public interface ITransactionService
    {
        [OperationContract]
        GetOrganizationBalancesByIdResponse GetOrganizationBalancesByOrganizationId(GetOrganizationBalancesByIdRequest request);

        [OperationContract]
        GetOrganizationBalancesByKardexResponse GetOrganizationBalancesByKardex(
            GetOrganizationBalancesByKardexRequest request);
        [OperationContract]
        GetLedgerTransactionsResponse GetLedgerTransactions(GetLedgerTransactionsRequest request);

        [OperationContract]
        GetLedgerTransactionDetailsResponse GetLedgerTransactionDetails(GetLedgerTransactionDetailsRequest request);

        [OperationContract]
        AddTransactionCommandResponse AddTransaction(AddTransactionCommandRequest request);
    }
}
