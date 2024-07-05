using DiamondAPI.Data;
using DiamondAPI.DTOs.Diamond;
using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using DiamondAPI.Repositories;
using DiamondAPI.Services;
using Humanizer;

namespace DiamondAPI.Mappers
{
    public static class DiamondMapper
    {
        public static DiamondDTO ToDiamondDTO(this Diamond diamond)
        {
            return new DiamondDTO
            {
                DProductId = diamond.DProductId,
                Price = DiamondCalculatorService.CalculateDiamondPriceToDiamondDTO(diamond.CaratWeight, diamond.Color, diamond.Clarity, diamond.Cut),
                ImageUrl = diamond.ImageUrl,
                Shape = diamond.Shape?.ShapeName,
                CaratWeight = diamond.CaratWeight,
                Color = diamond.Color switch
                {
                    1 => "K",
                    2 => "J",
                    3 => "I",
                    4 => "H",
                    5 => "G",
                    6 => "F",
                    7 => "E",
                    8 => "D",
                    _ => "Unknown"
                },
                Clarity = diamond.Clarity switch
                {
                    1 => "SI2",
                    2 => "SI1",
                    3 => "VS2",
                    4 => "VS1",
                    5 => "VVS2",
                    6 => "VVS1",
                    7 => "IF",
                    8 => "FL",
                    _ => "Unknown"
                },
                Cut = diamond.Cut switch
                {
                    1 => "Fair",
                    2 => "Good",
                    3 => "Very Good",
                    4 => "Excellent",
                    _ => "Unknown"
                },
                Available = diamond.Available
            };
        }

        public static Diamond ToDiamondFromCreateDTO(this CreateDiamondRequestDTO dto)
        {
            return new Diamond
            {
                DProductId = Guid.NewGuid(),
                ImageUrl = dto.ImageUrl,
                CaratWeight = dto.CaratWeight,
                Color = dto.Color switch
                {
                    "K" => 1,
                    "J" => 2,
                    "I" => 3,
                    "H" => 4,
                    "G" => 5,
                    "F" => 6,
                    "E" => 7,
                    "D" => 8,
                    _ => 0
                },
                Clarity = dto.Clarity switch
                {
                    "SI2" => 1,
                    "SI1" => 2,
                    "VS2" => 3,
                    "VS1" => 4,
                    "VVS2" => 5,
                    "VVS1" => 6,
                    "IF" => 7,
                    "FL" => 8,
                    _ => 0
                },
                Cut = dto.Cut switch
                {
                    "Fair" => 1,
                    "Good" => 2,
                    "Very Good" => 3,
                    "Excellent" => 4,
                    _ => 0
                },
                Available = dto.Available
            };
        }

        public static ModelFliterDiamondRequestDTO ToModelFilterFromModelFliterDiamondRequestDTO(this FilterDiamondRequestDTO filterDiamondRequestDTO)
        {
            return new ModelFliterDiamondRequestDTO
            {
                ShapeName = filterDiamondRequestDTO.ShapeName,
                LowerPrice = filterDiamondRequestDTO.LowerPrice,
                UpperPrice = filterDiamondRequestDTO.UpperPrice,
                LowerCaratWeight = filterDiamondRequestDTO.LowerCaratWeight,
                UpperCaratWeight = filterDiamondRequestDTO.UpperCaratWeight,
                LowerColor = filterDiamondRequestDTO.LowerColor switch
                {
                    "K" => 1,
                    "J" => 2,
                    "I" => 3,
                    "H" => 4,
                    "G" => 5,
                    "F" => 6,
                    "E" => 7,
                    "D" => 8,
                    _ => 0
                },
                UpperColor = filterDiamondRequestDTO.UpperColor switch
                {
                    "K" => 1,
                    "J" => 2,
                    "I" => 3,
                    "H" => 4,
                    "G" => 5,
                    "F" => 6,
                    "E" => 7,
                    "D" => 8,
                    _ => 0
                },
                LowerClarity = filterDiamondRequestDTO.LowerClarity switch
                {
                    "SI2" => 1,
                    "SI1" => 2,
                    "VS2" => 3,
                    "VS1" => 4,
                    "VVS2" => 5,
                    "VVS1" => 6,
                    "IF" => 7,
                    "FL" => 8,
                    _ => 0
                },
                UpperClarity = filterDiamondRequestDTO.UpperClarity switch
                {
                    "SI2" => 1,
                    "SI1" => 2,
                    "VS2" => 3,
                    "VS1" => 4,
                    "VVS2" => 5,
                    "VVS1" => 6,
                    "IF" => 7,
                    "FL" => 8,
                    _ => 0
                },
                LowerCut = filterDiamondRequestDTO.LowerCut switch
                {
                    "Fair" => 1,
                    "Good" => 2,
                    "Very Good" => 3,
                    "Excellent" => 4,
                    _ => 0
                },
                UpperCut = filterDiamondRequestDTO.UpperCut switch
                {
                    "Fair" => 1,
                    "Good" => 2,
                    "Very Good" => 3,
                    "Excellent" => 4,
                    _ => 0
                }
            };
        }

        public static ModelUpdateDiamondRequestDTO ToModelUpdateFromUpdateRequestDTO(this UpdateDiamondRequestDTO dto)
        {
            return new ModelUpdateDiamondRequestDTO
            {
                ImageUrl = dto.ImageUrl,
                ShapeName = dto.ShapeName,
                CaratWeight = dto.CaratWeight,
                Color = dto.Color switch
                {
                    "K" => 1,
                    "J" => 2,
                    "I" => 3,
                    "H" => 4,
                    "G" => 5,
                    "F" => 6,
                    "E" => 7,
                    "D" => 8,
                    _ => 0
                },
                Clarity = dto.Clarity switch
                {
                    "SI2" => 1,
                    "SI1" => 2,
                    "VS2" => 3,
                    "VS1" => 4,
                    "VVS2" => 5,
                    "VVS1" => 6,
                    "IF" => 7,
                    "FL" => 8,
                    _ => 0
                },
                Cut = dto.Cut switch
                {
                    "Fair" => 1,
                    "Good" => 2,
                    "Very Good" => 3,
                    "Excellent" => 4,
                    _ => 0
                }
            };
        }
    }
}
