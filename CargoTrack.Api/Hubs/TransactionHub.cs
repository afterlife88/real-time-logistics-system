using System;
using AutoMapper;
using CargoTrack.Api.Configuration;
using CargoTrack.Api.Models.Service;
using CargoTrack.Api.Models.Service.Balance;
using CargoTrack.Api.Models.Service.Transaction;
using CargoTrack.Common.Utilities;
using CargoTrack.TransactionService.Contracts;
using CargoTrack.TransactionService.Contracts.Models.Service.Balance;
using CargoTrack.TransactionService.Contracts.Models.Service.Commands;
using log4net;
using Microsoft.AspNet.SignalR;

namespace CargoTrack.Api.Hubs
{
    /// <summary>
    /// SignalR hub for balance and transaction related methods
    /// </summary>
    public class TransactionHub : Hub
    {
        // Variables
        private readonly ITransactionService _transactionService;
        private readonly ILog _log = LogManager.GetLogger(typeof(OrganizationHub));

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="transactionService"></param>
        public TransactionHub(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            AutomapperConfiguration.Load();
        }

        /// <summary>
        /// Get balances for all cargo types for a given organization
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetBalanceByOrganizationIdResponse GetBalancesByOrganizationId(GetBalanceByOrganizationIdRequest request)
        {
            try
            {
                Guard.IsNotNull(request);

                var response =
                    _transactionService
                        .GetOrganizationBalancesByOrganizationId(Mapper
                            .Map<GetOrganizationBalancesByIdRequest>(request));

                if (response.ServiceStatus != TransactionService.Contracts.Models.Service.ServiceStatus.Success)
                    return new GetBalanceByOrganizationIdResponse(null, ServiceStatus.ServiceError,
                        response.ErrorMessage);

                return Mapper.Map<GetBalanceByOrganizationIdResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new GetBalanceByOrganizationIdResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
        }

        /// <summary>
        /// Get balances for all cargo types for an organization by its Kardex
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetBalancesByKardexResponse GetBalancesByKardex(GetBalancesByKardexRequest request)
        {
            try
            {
                Guard.IsNotNull(request);

                var response =
                    _transactionService.GetOrganizationBalancesByKardex(
                        Mapper.Map<GetOrganizationBalancesByKardexRequest>(request));

                if (response.ServiceStatus != TransactionService.Contracts.Models.Service.ServiceStatus.Success)
                    return new GetBalancesByKardexResponse(null, ServiceStatus.ServiceError,
                        response.ErrorMessage);

                return Mapper.Map<GetBalancesByKardexResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new GetBalancesByKardexResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
        }

        /// <summary>
        /// Get ledger transactions for an organization and cargo type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetLedgerTransactionsResponse GetLedgerTransactions(GetLedgerTransactionsRequest request)
        {
            try
            {
                Guard.IsNotNull(request);

                var response =
                    _transactionService
                        .GetLedgerTransactions(Mapper
                            .Map<TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionsRequest>(
                                request));

                if (response.ServiceStatus != TransactionService.Contracts.Models.Service.ServiceStatus.Success)
                    return new GetLedgerTransactionsResponse(null, ServiceStatus.ServiceError, response.ErrorMessage);

                return Mapper.Map<GetLedgerTransactionsResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new GetLedgerTransactionsResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
        }

        /// <summary>
        /// Get details for a ledger transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetLedgerTransactionDetailsResponse GetLedgerTransactionDetails(
            GetLedgerTransactionDetailsRequest request)
        {
            try
            {
                Guard.IsNotNull(request);
                var response =
                    _transactionService
                        .GetLedgerTransactionDetails(Mapper
                            .Map
                            <TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionDetailsRequest>
                            (request));

                if (response.ServiceStatus != TransactionService.Contracts.Models.Service.ServiceStatus.Success)
                    return new GetLedgerTransactionDetailsResponse(null, ServiceStatus.ServiceError,
                        response.ErrorMessage);

                return Mapper.Map<GetLedgerTransactionDetailsResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new GetLedgerTransactionDetailsResponse(null, ServiceStatus.ServiceError, ex.Message);
            }
        }

        /// <summary>
        /// Add a new transaction
        /// 
        /// This will generate ledger transactions and update the balance for the organization / cargo type according to the transactiontype
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddTransactionResponse AddTransaction(AddTransactionRequest request)
        {
            try
            {
                Guard.IsNotNull(request);
                var response = _transactionService.AddTransaction(Mapper.Map<AddTransactionCommandRequest>(request));

                if (response.ServiceStatus != TransactionService.Contracts.Models.Service.ServiceStatus.Success)
                    return new AddTransactionResponse(ServiceStatus.ServiceError, response.ErrorMessage);

                return Mapper.Map<AddTransactionResponse>(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return new AddTransactionResponse(ServiceStatus.ServiceError, ex.Message);
            }
        }
    }
}