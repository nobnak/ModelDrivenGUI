using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public class ListElementValue<ValueType> : IValue<ValueType> {
        public IList List { get; set; }
        public int Index { get; set; }

        public ListElementValue(IList list, int index) {
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
