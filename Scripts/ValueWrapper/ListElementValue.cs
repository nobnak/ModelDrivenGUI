using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public class ListElementValue<ValueType> : IValue<ValueType> {
        public List<object> List { get; set; }
        public int Index { get; set; }

        public ListElementValue(List<object> list, int index) {
            this.List = list;
            this.Index = index;
        }

        public ValueType Value {
            get {
                return (ValueType)List[Index];
            }
            set {
                List[Index] = value;
            }
        }

    }
}
