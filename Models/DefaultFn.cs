using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwagAndHelp.Models
{
    class DefaultClassFn
    {
        internal DateTime DateTimeNull(DateTime? _val)
        {
            return DateTimeNull_pa(_val, DateTime.MinValue);
        }
        internal DateTime DateTimeNull(DateTime? _val, DateTime _def)
        {
            return DateTimeNull_pa(_val, _def);
        }
        internal decimal DecimalNull(decimal? _val)
        {
            return DecimalNull_pa(_val, 0);
        }
        internal decimal DecimalNull(decimal? _val, decimal _def)
        {
            return DecimalNull_pa(_val, _def);
        }
        internal string StringNull(string _val)
        {
            return StringNull_pa(_val, "");
        }
        internal string StringNull(string _val, string _def)
        {
            return StringNull_pa(_val, _def);
        }
        internal DateTime StringToDateTime(string _val)
        {
            return StringToDateTime_pa(_val, DateTime.MinValue);
        }
        internal DateTime StringToDateTime(string _val, DateTime _def)
        {
            return StringToDateTime_pa(_val, _def);
        }
        private string StringNull_pa(string _val, string _def)
        {
            if (string.IsNullOrEmpty(_val))
                return _def;
            else
                return _val;
        }
        private DateTime DateTimeNull_pa(DateTime? _val, DateTime _def)
        {
            if (_val.HasValue)
                return _val.Value;
            else
                return _def;
        }
        private decimal DecimalNull_pa(decimal? _val, decimal _def)
        {
            if (_val.HasValue)
                return _val.Value;
            else
                return _def;
        }
        private DateTime StringToDateTime_pa(string _val, DateTime _def)
        {
            DateTime Result = DateTime.MinValue;
            if (string.IsNullOrEmpty(_val))
                return _def;
            if (DateTime.TryParse(_val, out Result))
                return Result;
            else
                return _def;
        }
    }
}