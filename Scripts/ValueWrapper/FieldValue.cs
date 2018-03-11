using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {
    
    public class FieldValue<ValueType> : IFieldValue<ValueType> {
        public virtual IValue<object> Parent { get; protected set; }
        public virtual FieldInfo Field { get; protected set; }

        public FieldValue(IValue<object> parent, FieldInfo field) {
            this.Parent = parent;
            this.Field = field;
        }
        public FieldValue(object parent, FieldInfo field) 
            : this(new BaseValue<object>(parent), field) { }

        public virtual ValueType Value {
            get {
                return (ValueType)Field.GetValue(Parent.Value);
            }
            set {
                Field.SetValue(Parent.Value, value);
            }
        }
    }
}
