using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;

namespace SettingsParser
{
    public class DynamicResult : DynamicObject
    {
        private readonly Dictionary<string, object?> _dynamicProperties = new();

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            if (_dynamicProperties.ContainsKey(binder.Name))
            {
                result = _dynamicProperties[binder.Name];
                return true;
            }

            result = string.Empty;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (_dynamicProperties.ContainsKey(binder.Name))
                _dynamicProperties[binder.Name] = value;
            else
                _dynamicProperties.Add(binder.Name, value);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            try
            {
                string propertyName = (string)args![0]!;
                result = true;
                switch (binder.Name)
                {
                    case "ContainsProperty":
                        result = _dynamicProperties.ContainsKey(propertyName);
                        return true;
                    case "AddProperty":
                        object value = args[1]!;
                        if (_dynamicProperties.ContainsKey(propertyName))
                            _dynamicProperties[propertyName] = value;
                        else
                            _dynamicProperties.Add(propertyName, value);
                        break;
                    default:
                        result = false;
                        break;
                }

                return true;
            }
            catch (RuntimeBinderException)
            {
                throw new UnknownKeyException();
            }
        }
    }
}

