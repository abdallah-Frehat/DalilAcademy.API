using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DTOs
{
    public class Currency
    {
        public record ConvertPriceRequestDTO
            (decimal Amount, string FromCurrency, string ToCurrency);
        public record ConvertPriceResponseDTO
            (decimal OriginalAmount, string FromCurrency, decimal ConvertedAmount, string ToCurrency,
             decimal ExchangeRate);
    }
}
