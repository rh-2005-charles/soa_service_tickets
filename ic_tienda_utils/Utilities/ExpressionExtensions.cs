using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ic_tienda_utils.Utilities
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyAccess<T, TProperty>(this Expression<Func<T, TProperty>> propertyLambda)
        {
            if (propertyLambda.Body is not MemberExpression member || member.Member is not PropertyInfo propertyInfo)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }
            return propertyInfo;
        }
    }
}