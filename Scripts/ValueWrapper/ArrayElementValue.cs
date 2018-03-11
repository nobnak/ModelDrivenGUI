using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public class ArrayElementValue<ValueType> : IValue<ValueType> {
        public System.Array Array { get; set; }
        public int Index { get; set; }

        public ArrayElementValue(System.Array array, int index) {
            this.Array = array;
            this.Index = index;
        }

        public ValueType Value {
            get {
                return (ValueType)Array.GetValue(Index);
            }
            set {
                Array.SetValue(value, Index);
            }
        }

    }
}
