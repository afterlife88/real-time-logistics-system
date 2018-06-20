using AutoMapper;
using CargoTrack.Api.Models.DTO;
using CargoTrack.Api.Models.Service.Balance;
using CargoTrack.Api.Models.Service.Organization;
using CargoTrack.Api.Models.Service.Transaction;
using CargoTrack.TransactionService.Contracts.Models.Service.Commands;

namespace CargoTrack.Api.Configuration
{
    /// <summary>
    /// Automapper configuration
    /// </summary>
    public static class AutomapperConfiguration
    {
        /// <summary>
        /// Configuration of automapper maps
        /// </summary>
        public static void Load()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<OrganizationDTO, OrganizationService.Contracts.Models.DTO.OrganizationDTO>().ReverseMap();
                config.CreateMap<GetOrganizationsByKardexRequest, OrganizationService.Contracts.Models.Service.Organization.GetOrganizationsByKardexRequest>().ReverseMap();
                config.CreateMap<GetOrganizationsByKardexResponse, OrganizationService.Contracts.Models.Service.Organization.GetOrganizationsByKardexResponse>().ReverseMap();
                config.CreateMap<LedgerTransactionDetailedDTO, TransactionService.Contracts.Models.DTO.LedgerTransactionDetailedDTO>().ReverseMap();
                config.CreateMap<GetLedgerTransactionDetailsRequest, TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionDetailsRequest>().ReverseMap();
                config.CreateMap<GetLedgerTransactionDetailsResponse, TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionDetailsResponse>().ReverseMap();
                config.CreateMap<LedgerTransactionDTO, TransactionService.Contracts.Models.DTO.LedgerTransactionDTO>().ReverseMap();
                config.CreateMap<GetLedgerTransactionsRequest, TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionsRequest>().ReverseMap();
                config.CreateMap<GetLedgerTransactionsResponse, TransactionService.Contracts.Models.Service.Transaction.GetLedgerTransactionsResponse>().ReverseMap();
                config.CreateMap<AddTransactionCommandDTO, TransactionService.Contracts.Models.DTO.Commands.AddTransactionCommandDTO>().ReverseMap();
                config.CreateMap<AddTransactionRequest, AddTransactionCommandRequest>()
                    .ForMember(dest => dest.AddTransactionCommand, dto => dto.MapFrom(src => src.AddTransaction))
                    .ReverseMap()
                    .ForMember(dest => dest.AddTransaction, dto => dto.MapFrom(src => src.AddTransactionCommand));
                config.CreateMap<AddTransactionResponse, AddTransactionCommandResponse>().ReverseMap();
                config.CreateMap<BalanceDTO, TransactionService.Contracts.Models.DTO.BalanceDTO>().ReverseMap();
                config.CreateMap<GetBalancesByKardexRequest,  TransactionService.Contracts.Models.Service.Balance.GetOrganizationBalancesByKardexRequest>().ReverseMap();
                config.CreateMap<GetBalancesByKardexResponse, TransactionService.Contracts.Models.Service.Balance.GetOrganizationBalancesByKardexResponse>().ReverseMap();
            });
        }
    }
}