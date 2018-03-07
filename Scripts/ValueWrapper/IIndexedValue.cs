using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public interface IIndexedValue<ValueType, ValueElementType> : IValue<ValueType> {
        int CountElement { get; }
        ValueElementType this[int index] {get; set; }
    }
}
