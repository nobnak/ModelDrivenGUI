using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public class BaseValue<ValueType> : IValue<ValueType> {
        public ValueType Value { get; set; }

        public BaseValue(ValueType value) {
            this.Value = value;
        }
    }
}
