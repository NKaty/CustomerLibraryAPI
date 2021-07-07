using System.ComponentModel.DataAnnotations;

namespace CustomerLibraryAPI.BusinessEntities.ValidationAttributes
{
    public class AddressTypesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(AddressTypes))
                {
                    return true;
                }

                ErrorMessage = "Address type must be Shipping or Billing.";
                return false;
            }

            return true;
        }
    }
}