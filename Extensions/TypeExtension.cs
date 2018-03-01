using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.TypeExt {
    public static class TypeExtension {

        public static IEnumerable<FieldInfo> Fields<S>(this S s) {
            return s.GetType().GetFields(BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic
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
                    if (typeof(IList).IsAssignableFrom(t))
                        return DataSectionEnum.Class_IList;
                    return DataSectionEnum.Class_UserDefined;

                case DataDivisionEnum.ValueType:
                    if (t.IsEnum)
                        return DataSectionEnum.ValueType_Enum;
                    else if (t == typeof(Vector2)
                        || t == typeof(Vector3)
                        || t == typeof(Vector4))
                        return DataSectionEnum.ValueType_Vector;
                    else if (t == typeof(Vector2Int)
                        || t == typeof(Vector3Int))
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
    }
}
