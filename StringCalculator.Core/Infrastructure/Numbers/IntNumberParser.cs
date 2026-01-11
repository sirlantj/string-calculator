using System.Globalization;
using StringCalculator.Core.Contracts;

namespace StringCalculator.Core.Infrastructure.Numbers
{
    public class IntNumberParser : INumberParser
    {
        public int? Parse(string part)
        {
            if (string.IsNullOrWhiteSpace(part))
                return null;
            var normalized = part.Replace(" ", "");

            if (int.TryParse(
                normalized,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var number))
            {
                return number;
            }

            return null;
        }
    }
}
