using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PictureService.ApiModels.Converter;
using PictureService.ApiModels.ViewModels;
using PictureService.ApiModels.ViewModels.MappingTable;
using PictureService.ApiModels.ViewModels.Pictures;
using PictureService.Domain.Models;

namespace PictureService.ApiModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Equipment, EquipmentVM>();

        // Pictures
        CreateMap<StorePictureVM, Picture>()
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.ImageData)));
        CreateMap<Picture, PictureVM>();
        CreateMap<IFormFile, byte[]>().ConvertUsing<FormFileToByteArrayConverter>();
        CreateMap<ReplacePictureVM, Picture>()
            .ForMember(dest => dest.ImageData, opt =>
            opt.MapFrom(src => src.ImageData));

        //GenericMappingTable
        //CreateMap<TableMappingModel, MappingTableVM>();
        CreateMap<GenericMappingTable, MappingTableVM>()
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ModifiedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.RowVersions, opt => opt.Ignore());

        CreateMap<AddMappingVM, GenericMappingTable>()
            .ForMember(dest => dest.TableName, opt => opt.MapFrom(src => src.TableName))
            .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.EntityId))
            .ForMember(dest => dest.KeyValuePairs, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.KeyValuePairs)));
    }

    private byte[] ConvertIFormFileToByteArray(IFormFile file)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            file.CopyTo(ms);
            return ms.ToArray();
        }
    } /// Dit moet ook omgekeerd gebeuren voor CreateMap<Picture, PicturesVM>();

    // Method to deserialize KeyValuePairs from JSON string to Dictionary in the view model
    private Dictionary<string, object>? DeserializeKeyValuePairs(string keyValuePairs)
    {
        if (string.IsNullOrEmpty(keyValuePairs))
            return null;

        try
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(keyValuePairs);
        }
        catch (Exception ex)
        {
            // Handle the exception (e.g., log it)
            // You can also throw a custom exception or return null
            Console.WriteLine($"Error deserializing KeyValuePairs: {ex.Message}");
            return null;
        }
    }
}