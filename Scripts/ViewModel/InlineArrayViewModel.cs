using ModelDrivenGUISystem.Text;
using System.Reflection;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public abstract class InlineArrayViewModel<ElementType> : BaseViewModel {
        protected BaseNumberText<ElementType>[] textList;

        public InlineArrayViewModel(FieldInfo f, int count) : base(f) {}
        
        public abstract BaseNumberText<ElementType> GenerateTextElementAt(int index, ElementType v);
        public abstract void SetValueToField<Parent, Value>(Parent parent, Value v, int index);
        public abstract Value GetValueFromField<Parent, Value>(Parent parent, int index);


        public virtual int CountValue() { return (textList == null ? 0: textList.Length); }
        public virtual string GetStrFromText(int index) {
            return textList[index].StrValue;
        }
        public virtual void SetStrToText(int index, string v) {
            textList[index].StrValue = v;
        }
        public virtual ElementType GetValueFromText<Parent>(int index) {
            return textList[index].Value;
        }
        public virtual void SetValueToText<Parent>(int index, ElementType v) {
            textList[index].Value = v;
        }

    }
}