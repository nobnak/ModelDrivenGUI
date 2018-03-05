using ModelDrivenGUISystem.Text;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class IntViewModel : BaseViewModel {
        protected IntText text;

        public IntViewModel(FieldInfo f) : base(f) { }

        public override BaseViewModel BuildViewModel<Parent>(Parent parent) {
            this.text = new IntText(GetValueFromModel<Parent, int>(parent, field));
            return this;
        }

        public override void ConvertbackToModel<T>(T parent) {
            SetValueToModel(parent, text.Value);
        }

        public override void ConvertFromModel<Parent>(Parent parent) {
            text.Value = GetValueFromModel<Parent, int>(parent, field);
        }
    }
}