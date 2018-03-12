using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.ConstructorExt {

    public static class ConstructorExtension {

        public static object CreateInstance(this System.Type t) {
            object instance;
            if (t.IsArray)
                instance = System.Array.CreateInstance(t.GetElementType(), 0);
            else if (t == typeof(string))
                instance = "";
            else
                instance = System.Activator.CreateInstance(t);
            return instance;
        }

        public static object CreateInstanceHierarchy(this System.Type t) {
            var instance = CreateInstance(t);

            foreach (var f in instance.Fields()) {
                var typeOfField = f.FieldType;
                if (typeOfField.IsClass) {
                    f.SetValue(instance, CreateInstanceHierarchy(typeOfField));
                }
            }

            return instance;
        }

        public static T CreateInstanceHierarchy<T>() {
            return (T)CreateInstanceHierarchy(typeof(T));
        }
    }
}
