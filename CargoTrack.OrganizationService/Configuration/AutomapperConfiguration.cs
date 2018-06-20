using AutoMapper;
using CargoTrack.OrganizationService.Contracts.Models.DTO;
using CargoTrack.OrganizationService.Data.Entities;

namespace CargoTrack.OrganizationService.Configuration
{
    /// <summary>
    /// Automapper configuration
    /// </summary>
    public class AutomapperConfiguration
    {
        /// <summary>
        /// Configuration of automapper maps
        /// </summary>
        public static void Load()
        {
            Mapper.Initialize(config =>
            {
                // Model => DTO
                config.CreateMap<Organization, OrganizationDTO>()
                    .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Address, dto => dto.MapFrom(src => src.Address))
                    .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id))
                    .ForMember(dest => dest.City, dto => dto.MapFrom(src => src.City))
                    .ForMember(dest => dest.Kardex, dto => dto.MapFrom(src => src.Kardex))
                    .ForMember(dest => dest.Zipcode, dto => dto.MapFrom(src => src.Zipcode));

                config.CreateMap<Organization, OrganizationDetailedDTO>()
                    .ForMember(dest => dest.OrganizationType, dto => dto.MapFrom(src => src.OrganizationType.Name))
                    .ForMember(dest => dest.Country, dto => dto.MapFrom(src => src.Country))
                    .ForMember(dest => dest.Cvr, dto => dto.MapFrom(src => src.Cvr))
                    .ForMember(dest => dest.Longitude, dto => dto.MapFrom(src => src.Longitude))
                    .ForMember(dest => dest.Lattitude, dto => dto.MapFrom(src => src.Lattitude))
                    .ForMember(dest => dest.Address, dto => dto.MapFrom(src => src.Address))
                    .ForMember(dest => dest.Id, dto => dto.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, dto => dto.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Kardex, dto => dto.MapFrom(src => src.Kardex))
                    .ForMember(dest => dest.Zipcode, dto => dto.MapFrom(src => src.Zipcode));
            });

        }
    }
}
