using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Accessor {

    public interface IValueAccessor<ValueType> {

        bool TryParse(string s, out ValueType result);
    }
}
