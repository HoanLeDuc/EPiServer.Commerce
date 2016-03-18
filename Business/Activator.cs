using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace EPiServer.Commerce.Business
{
    public class Activator<T>
    {
        private delegate T ObjectActivator (params object[] args); 

        public T Activate(params object[] args)
        {
            return Activate(typeof(T), args); 
        }

        public T Activate(Type type, params object[] constructorArguments)
        {
            // TODO: Revisit to implement cache
           var argumentTypes = constructorArguments.Select(x => x != null ? x.GetOriginalType() : typeof(object)).ToArray();
           var objectActivator = GetObjectActivator(type, argumentTypes);

           return objectActivator(constructorArguments); 

            // Passing through the ctor arguments to object activator 
        }

        private static ObjectActivator GetObjectActivator(Type type, Type[] contructorArgumentTypes)
        {
            Type closedType = type.IsGenericTypeDefinition ? type.MakeGenericType(contructorArgumentTypes) : type;

            var contructorInfo = closedType.GetConstructor(contructorArgumentTypes);
            var delegateParameterExpression = Expression.Parameter(typeof(object[]), "args");

            var typeExpressions = CreateTypeExpressions(contructorArgumentTypes, delegateParameterExpression);
            return CreateDelegate(contructorInfo, typeExpressions, delegateParameterExpression); 

        }

        private static IEnumerable<Expression> CreateTypeExpressions(Type[] contructorArgumentTypes, Expression delegateParameterExpression)
        {
            var expressionResults = new Expression[contructorArgumentTypes.Length];
            for (var i = 0; i < contructorArgumentTypes.Length; i++) {
                var paramType = contructorArgumentTypes[i];
                var indexExpression = Expression.Constant(i);
                var paramAccessorExpression = Expression.ArrayIndex(delegateParameterExpression, indexExpression);
                var paramCastExpression = Expression.Convert(paramAccessorExpression, paramType);

                expressionResults[i] = paramCastExpression; 
            }

            return expressionResults; 
        }

        private static ObjectActivator CreateDelegate(ConstructorInfo constructorInfo, IEnumerable<Expression> constructorExpressions, ParameterExpression delegateParameterExpression)
        {
            var constructorExpression = Expression.New(constructorInfo, constructorExpressions); 
            var lambdaExpression = Expression.Lambda(typeof(ObjectActivator), constructorExpression, delegateParameterExpression);

            return (ObjectActivator)lambdaExpression.Compile(); 
        }
    }
}