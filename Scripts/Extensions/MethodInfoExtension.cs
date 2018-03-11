using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.MethodInfoExt {

    public static class MethodInfoExtension {

        public static MethodInfo GetMethodInfo(LambdaExpression expression) {
            var body = expression.Body as MethodCallExpression;
            if (body == null)
                throw new System.ArgumentException("Not method call expression");
            return body.Method;
        }
        
        public static MethodInfo GetMethodInfo<T0, T1, TResult>(
                Expression<System.Func<T0,T1,TResult>> expression) {
            return GetMethodInfo((LambdaExpression)expression);
        }
    }
}