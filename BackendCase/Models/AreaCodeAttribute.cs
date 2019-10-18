using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace BackendCase.Models
{
    public class AreaCodeAttribute : ValidationAttribute
    {
        static readonly HttpClient client = new HttpClient();
        private const string URL = "https://api.bring.com/shippingguide/api/postalCode.json?clientUrl=insertYourClientUrlHere&pnr=";

        protected override ValidationResult IsValid(object areacode, ValidationContext validationContext)
        {
            var validAreaCode = ValidateAreacode((int)areacode);


            if (validAreaCode)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult($"Area code must be a valid Norwegian areacode");
            }
        }

        public bool ValidateAreacode(int areacode)
        {
            var response = client.GetAsync($"{URL} {areacode}").Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(responseString);
            var validAreaCode = jObject.SelectToken("valid");
            return (bool)validAreaCode;
        }
    }
}
