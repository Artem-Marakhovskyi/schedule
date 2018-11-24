using System.ComponentModel.DataAnnotations;

namespace Schedule.Data.Validation
{
    public class OnlyTextAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            foreach (var c in value?.ToString() ?? string.Empty)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
