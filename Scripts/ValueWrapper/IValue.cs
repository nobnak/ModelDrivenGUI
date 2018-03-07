using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ValueWrapper {

    public interface IValue<ValueType> {
        ValueType Value { get; set; }
    }
}
