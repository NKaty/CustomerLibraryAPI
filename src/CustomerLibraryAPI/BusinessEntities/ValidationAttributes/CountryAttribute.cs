using System.ComponentModel.DataAnnotations;

namespace CustomerLibraryAPI.BusinessEntities.ValidationAttributes
{
    public class CountryAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string country = value.ToString();
                if (country == "United States" || country == "Canada")
                {
                    return true;
                }

                ErrorMessage = "Country must be United States or Canada.";
                return false;
            }

            return true;
        }
    }
}