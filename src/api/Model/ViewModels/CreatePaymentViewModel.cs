using api.Model.EntityModel;
using api.Model.Validations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace api.Model.ViewModels
{
    public class CreatePaymentViewModel
    {
        [Display(Name = "cardnumber"), JsonRequired, JsonValidCardNumber]
        public string CardNumber { get; set; }

        [Display(Name = "installments"), JsonRequired]
        [Range(1, 12, ErrorMessage = "{0} must be between 1 and 12.")]
        public int? Installments { get; set; }

        [Display(Name = "amount"), JsonRequired]
        [Range(0, 999999.99, ErrorMessage = "{0} must be between 0 and 999999.99.")]
        public decimal? Amount { get; set; }

        public CardPayment ToCardPayment() => new CardPayment
        {
            CardNumber = CardNumber,
            Installments = Installments.Value,
            Amount = Amount.Value
        };
    }
}
