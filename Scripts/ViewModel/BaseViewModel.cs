using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public abstract class BaseViewModel {
        protected FieldInfo field;

        public BaseViewModel(FieldInfo f) {
            this.field = f;
        }

        public abstract BaseViewModel BuildViewModel<Parent>(Parent parent);
        public abstract void ConvertFromModel<Parent>(Parent parent);
        public abstract void ConvertbackToModel<Parent>(Parent parent);

        #region Static
        public static Value GetValueFromModel<Parent, Value>(Parent parent, FieldInfo f) {
            return (Value)f.GetValue(parent);
        }
        public static void SetValueToModel<Parent, Value>(Parent parent, FieldInfo f, Value v) {
            f.SetValue(parent, v);
        }
        #endregion

        public virtual void SetValueToModel<Parent, Value>(Parent parent, Value v) {
            SetValueToModel(parent, field, v);
        }
        public virtual Value GetValueFromModel<Parent, Value>(Parent parent) {
            return GetValueFromModel<Parent, Value>(parent, field);
        }
    }
}
