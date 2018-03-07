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

    public class Vector4Accessor : FieldIndexedValue<Vector4, float> {
        public Vector4Accessor(object parent, FieldInfo field) : base(parent, field) {}

        public override float this[int index] {
            get { return Value[index]; }
            set {
                var v = Value;
                v[index] = value;
                Value = v;
            }
        }
        public override int CountElement {
            get {
                return 4;
            }
        }
    }
}
