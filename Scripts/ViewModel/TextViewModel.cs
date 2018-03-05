using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class TextViewModel : BaseViewModel {
        protected string text;

        public TextViewModel(FieldInfo f) : base(f) { }

        public override BaseViewModel BuildViewModel<Parent>(Parent parent) {
            this.text = GetValueFromModel<Parent, string>(parent, field);
            return this;
        }

        public override void ConvertbackToModel<T>(T parent) {
            SetValueToModel(parent, text);
        }

        public override void ConvertFromModel<Parent>(Parent parent) {
            text = GetValueFromModel<Parent, string>(parent, field);
        }
    }
}