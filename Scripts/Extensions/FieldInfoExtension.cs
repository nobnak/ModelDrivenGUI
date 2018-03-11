using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.FieldInfoExt {

    public static class FieldInfoExtension {

        public static IEnumerable<FieldInfo> Fields<S>(this S s) {
            return s.GetType().GetFields(BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.FlattenHierarchy);
        }

        public static DataDivisionEnum Division(this System.Type t) {
            return t.IsPrimitive ? DataDivisionEnum.Primitive
                : (t.IsValueType ? DataDivisionEnum.ValueType : DataDivisionEnum.Class);
        }
        public static DataDivisionEnum Division<S>(this S obj) {
            return Division(typeof(S));
        }

        public static DataSectionEnum Section(this System.Type t) {
            var div = Division(t);

            switch (div) {
                case DataDivisionEnum.Class:
                    if (t.IsArray)
                        return DataSectionEnum.Class_Array;
                    else if (typeof(IList<>).IsAssignableFrom(t))
                        return DataSectionEnum.Class_IListGeneric;
                    else if (t == typeof(string))
                        return DataSectionEnum.Class_String;
                    return DataSectionEnum.Class_UserDefined;

                case DataDivisionEnum.ValueType:
                    if (t.IsEnum)
                        return DataSectionEnum.ValueType_Enum;
                    else if (t == typeof(Vector2)
                        || t == typeof(Vector3)
                        || t == typeof(Vector4)
                        || t == typeof(Color))
                        return DataSectionEnum.ValueType_Vector;
                    else if (t == typeof(Vector2Int)
                        || t == typeof(Vector3Int)
                        || t == typeof(Color32))
                        return DataSectionEnum.ValueType_VectorInt;
                    return DataSectionEnum.ValueType_Struct;

                default:
                    if (t == typeof(int))
                        return DataSectionEnum.Primitive_Int;
                    else if (t == typeof(float))
                        return DataSectionEnum.Primitive_Float;
                    else if (t == typeof(bool))
                        return DataSectionEnum.Primitive_Bool;
                    return DataSectionEnum.Primitive_Other;
            }
        }
        public static DataSectionEnum Section<S>(this S obj) {
            return Section(typeof(S));
        }
        public static DataSectionEnum Section(this FieldInfo f) {
            return Section(f.FieldType);
        }
        public static string Dump<S>(this S obj) {
            var t = typeof(S);
            return string.Format("array={0} class={1} enum={2} primitive={3} valuetype={4}",
                t.IsArray, t.IsClass, t.IsEnum, t.IsPrimitive, t.IsValueType);
        }
        public static VectorTypeEnum VectorType(this System.Type t) {
            if (t == typeof(Vector2))
                return VectorTypeEnum.Vector2;
            else if (t == typeof(Vector3))
                return VectorTypeEnum.Vector3;
            else if (t == typeof(Vector4))
                return VectorTypeEnum.Vector4;
            else if (t == typeof(Color))
                return VectorTypeEnum.Color;
            else if (t == typeof(Vector2Int))
                return VectorTypeEnum.Vector2Int;
            else if (t == typeof(Vector3Int))
                return VectorTypeEnum.Vector3Int;
            else if (t == typeof(Color32))
                return VectorTypeEnum.Color32;
            return VectorTypeEnum.Unsupported;

        }
        public static VectorTypeEnum VectorType(this FieldInfo f) {
            return VectorType(f.FieldType);
        }

        public static string GetMemberName<TField>(Expression<System.Func<TField>> memberAccess) {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
        public static string GetMemberName<T, TField>(this T t, Expression<System.Func<T, TField>> memberAccess) {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
        public static FieldInfo GetField<T, TField>(this T t, Expression<System.Func<T, TField>> memberAccess) {
            return typeof(T).GetField(t.GetMemberName(memberAccess));
        }
        
        public static IEnumerable<string> DumpGenericMethodInfo(this MethodInfo mi) {
            yield return string.Format("Method : {0}\n", mi);
            yield return string.Format("\tGeneric method definition? {0}\n", mi.IsGenericMethodDefinition);
            yield return string.Format("\tGeneric method? {0}\n", mi.IsGenericMethod);
            if (mi.IsGenericMethod) {
                var typeArguments = mi.GetGenericArguments();
                yield return string.Format("\nList type arguments ({0}):\n", typeArguments.Length);

                foreach (var tparam in typeArguments) {
                    if (tparam.IsGenericParameter)
                        yield return string.Format("\t\tParameter:{0} at {1}, declaring method:{2}\n",
                            tparam, tparam.GenericParameterPosition, tparam.DeclaringMethod);
                    else
                        yield return string.Format("\t\t{0}\n", tparam);
                }
            }
        }
    }
}
