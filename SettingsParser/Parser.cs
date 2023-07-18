using System.Text.RegularExpressions;

namespace SettingsParser
{
    public class Parser : IParser
    {
        public dynamic Parse(string configuration)
        {
            if (string.IsNullOrEmpty(configuration))
                throw new ArgumentException(null, nameof(configuration));

            string[] lines = configuration.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                throw new ArgumentException(null, nameof(configuration));

            dynamic resultObject = new DynamicResult();
            foreach (var line in lines)
            {
                var keyValue = line.Split(':');
                if (keyValue.Length != 2)
                    throw new InvalidKeyException();

                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim(';').Trim();

                if (string.IsNullOrWhiteSpace(key))
                    throw new EmptyKeyException();

                if (resultObject.ContainsProperty(key))
                    throw new DuplicatedKeyException();

                if (!Regex.IsMatch(key, @"^[a-zA-Z][a-z-A-Z0-9]+$"))
                    throw new InvalidKeyException();

                object resultValue = value;
                if (bool.TryParse(value, out bool boolValue))
                    resultValue = boolValue;
                else if (int.TryParse(value, out int intValue))
                    resultValue = intValue;

                resultObject.AddProperty(key, resultValue);
            }

            return resultObject;
        }
    }
}

