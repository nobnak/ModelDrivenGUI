using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.ConstructorExt {

    public static class ConstructorExtension {

        public static object CreateInstanceHierarchy(this System.Type t) {
            var instance = (t.IsArray 
                ? System.Array.CreateInstance(t.GetElementType(), 0)
                : System.Activator.CreateInstance(t));

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
