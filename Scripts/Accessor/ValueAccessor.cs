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

    public class EnumAccessor : IValueAccessor<object> {
        public System.Type EnumType { get; set; }

        public EnumAccessor(System.Type enumType) {
            this.EnumType = enumType;
        }

        public bool TryParse(string s, out object result) {
            try {
                result = System.Enum.Parse(EnumType, s);
                return true;
            } catch (System.Exception e) {
                Debug.LogWarning(e);
            }

            result = default(object);
            return false;
        }
    }
}
