using ModelDrivenGUISystem.ValueWrapper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public interface IModelFactory {

        IValue<T> CreateValue<T>();
        object CreateValue(System.Type typeOfValue);
    }
}
