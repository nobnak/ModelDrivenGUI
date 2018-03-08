using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {
    
    public interface IFieldValue<ValueType> : IValue<ValueType> {
        object Parent { get; }
        FieldInfo Field { get; }
    }
}
