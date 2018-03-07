using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public class Vector4Value : FieldIndexedValue<Vector4, float> {
        public Vector4Value(object parent, FieldInfo field) : base(parent, field) {}

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
