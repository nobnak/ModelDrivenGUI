using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {
    
    public class FieldValue<ValueType> : IValue<ValueType> {
        public virtual object Parent { get; protected set; }
        public virtual FieldInfo Field { get; protected set; }

        public FieldValue(object parent, FieldInfo field) {
            this.Parent = parent;
            this.Field = field;
        }

        public virtual ValueType Value {
            get {
                return (ValueType)Field.GetValue(Parent);
            }
            set {
                Field.SetValue(Parent, value);
            }
        }
    }
}
