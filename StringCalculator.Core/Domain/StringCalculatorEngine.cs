using StringCalculator.Core.Contracts;
using StringCalculator.Core.Domain.Operations;
using StringCalculator.Core.Domain.ValueObjects;
using StringCalculator.Core.Exceptions;

namespace StringCalculator.Core.Domain
{
    public class StringCalculatorEngine : IStringCalculatorEngine
    {
        private readonly IDelimiterParser _delimiterParser;
        private readonly IDefaultDelimiterProvider _defaultProvider;
        private readonly INumberParser _numberParser;
        private readonly INumberValidator _validator;
        private readonly INumberInclusionPolicy _inclusionPolicy;

        public StringCalculatorEngine(
            IDelimiterParser delimiterParser,
            IDefaultDelimiterProvider defaultProvider,
            INumberParser numberParser,
            INumberValidator validator,
            INumberInclusionPolicy inclusionPolicy)
        {
            _delimiterParser = delimiterParser;
            _defaultProvider = defaultProvider;
            _numberParser = numberParser;
            _validator = validator;
            _inclusionPolicy = inclusionPolicy;
        }

        public CalculationResult Add(string input, CalculatorOptions? options = null)
        {
            return Calculate(input, new AddOperation(), options);
        }

        public CalculationResult Calculate(string input, IOperation operation, CalculatorOptions? options = null)
        {
            options ??= new CalculatorOptions();

            if (string.IsNullOrWhiteSpace(input))
                return new CalculationResult(0, "0 = 0");

            var (customDelims, numbersPart) = _delimiterParser.Parse(input);
            var allDelims = _defaultProvider.GetDefaultDelimiters(options)
                                           .Concat(customDelims)
                                           .Distinct()
                                           .ToList();

            var rawParts = _delimiterParser.Split(numbersPart, allDelims).ToList();

            var validNumbers = new List<int>();
            var negatives = new List<int>();
            var formulaTerms = new List<string>();

            foreach (var part in rawParts)
            {
                var trimmedPart = part.Trim();  

                var numOpt = _numberParser.Parse(trimmedPart);

                if (!numOpt.HasValue)
                {
                    validNumbers.Add(0);
                    formulaTerms.Add("0");
                    continue;
                }

                var num = numOpt.Value;

                _validator.Validate(num, negatives);

                bool shouldInclude;
                if (num < 0)
                {
                    shouldInclude = !options.DenyNegatives;
                }
                else
                {
                    shouldInclude = _inclusionPolicy.ShouldInclude(num, options);
                }

                if (shouldInclude)
                {
                    validNumbers.Add(num);
                    formulaTerms.Add(num.ToString());
                }
                else if (num < 0 && !options.DenyNegatives)
                {
                    validNumbers.Add(num);
                    formulaTerms.Add(num.ToString());
                }
            }

            if (options.DenyNegatives && negatives.Any())
            {
                throw new NegativeNumbersNotAllowedException(negatives);
            }

            var result = operation.Execute(validNumbers);

            var formula = string.Join($" {operation.Symbol} ", formulaTerms);
            if (!formulaTerms.Any())
                formula = "0 = 0";
            else
                formula += $" = {result}";

            return new CalculationResult(result, formula);
        }
    }
}
