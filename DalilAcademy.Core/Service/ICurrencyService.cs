using System;
using System.Collections.Generic;
using System.Text;
using static DalilAcademy.Core.DTOs.Currency;

namespace DalilAcademy.Core.Service
{
    public interface ICurrencyService
    {
        Task <ConvertPriceResponseDTO> ConvertAsync(ConvertPriceRequestDTO request);


    }
}
