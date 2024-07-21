using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCIDentity.Core.Extensions
{
    public static class ObjectExtensions
    {

        public static bool IsNotNullOrEmpty(this object obj)
        {
            if (obj is not null)
            {
                var type = obj.GetType();

                if (type.IsClass && type.FullName != "System.String")
                {
                    var props = type.GetRuntimeProperties();

                    return props.Any(p => p.GetValue(obj).IsNotNullOrEmpty());
                }
            }

            return obj is not null;
        }



    }
}
