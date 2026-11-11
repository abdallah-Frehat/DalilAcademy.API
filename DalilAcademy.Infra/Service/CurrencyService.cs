using DalilAcademy.Core.Service;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using static DalilAcademy.Core.DTOs.Currency;

namespace DalilAcademy.Infra.Service
{
    public class CurrencyService(HttpClient http) : ICurrencyService
    {
        private record PairResponse([property: JsonPropertyName
            ("conversion_rate")]decimal ConversionRate);
        public async Task<ConvertPriceResponseDTO> ConvertAsync(ConvertPriceRequestDTO request)
        {
         var data = await http.GetFromJsonAsync<PairResponse>
                ($"pair/{request.FromCurrency}/{request.ToCurrency}")
                ?? throw new HttpRequestException("Failed to get exchange rate");
            var converted =Math.Round(request.Amount * data.ConversionRate, 2);
            return new ConvertPriceResponseDTO(request.Amount, request.FromCurrency,
                converted, request.ToCurrency, data.ConversionRate);
        }

    }
}
