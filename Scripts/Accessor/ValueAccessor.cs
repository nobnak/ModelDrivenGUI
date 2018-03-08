using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Accessor {

    public class IntAccessor : IValueAccessor<int> {

        public bool TryParse(string s, out int result) {
            return int.TryParse(s, out result);
        }
    }

    public class FloatAccessor : IValueAccessor<float> {

        public bool TryParse(string s, out float result) {
            return float.TryParse(s, out result);
        }
    }
}
