using ModelDrivenGUISystem.Text;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class FloatViewModel : BaseViewModel {
        protected FloatText text;

        public FloatViewModel(FieldInfo f) : base(f) { }

        public override BaseViewModel BuildViewModel<Parent>(Parent parent) {
            this.text = new FloatText(GetValueFromModel<Parent, float>(parent, field));
            return this;
        }
        
        public override void ConvertbackToModel<T>(T parent) {
            SetValueToModel(parent, text.Value);
        }
        public override void ConvertFromModel<Parent>(Parent parent) {
            text.Value = GetValueFromModel<Parent, float>(parent, field);
        }
    }
}