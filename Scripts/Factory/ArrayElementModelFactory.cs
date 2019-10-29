using System;
using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.ValueWrapper;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public class ArrayElementModelFactory : IModelFactory {
        public System.Array Array { get; set; }
        public int Index { get; set; }

        public ArrayElementModelFactory(System.Array array, int index) {
            this.Array = array;
            this.Index = index;
        }

        public IValue<T> CreateValue<T>() {
            return new ArrayElementValue<T>(Array, Index);
        }

        public object CreateValue(Type typeOfValue) {
            var typeOfModel = typeof(ArrayElementValue<>).MakeGenericType(typeOfValue);
            return Activator.CreateInstance(typeOfModel, new object[] { Array, Index });
        }
    }
}
