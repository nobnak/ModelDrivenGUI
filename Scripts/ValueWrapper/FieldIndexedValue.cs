using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public abstract class FieldIndexedValue<ValueType, ValueElementType>
            : FieldValue<ValueType>, IIndexedValue<ValueType, ValueElementType> {

        public FieldIndexedValue(object parent, FieldInfo field) : base(parent, field) {
        }

        public abstract ValueElementType this[int index] { get; set; }

        public abstract int CountElement { get; }
    }
}
