using System.Linq.Expressions;
using System.Reflection;
using System;
using Xamarin.Forms;

namespace NptExplorer.Mobile.ViewModels.Base
{
    public class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            var lambdaExpression = (LambdaExpression)expression;
            operand = lambdaExpression.Body is UnaryExpression body ? (MemberExpression)body.Operand : (MemberExpression)lambdaExpression.Body;
			return operand.Member;
        }
    }
}