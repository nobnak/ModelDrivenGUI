using UnityEngine;

namespace ModelDrivenGUISystem.Text {

    public abstract class BaseNumberListText<T, S> : BaseNumberText<T> {
        protected BaseNumberText<S>[] textList;

        public BaseNumberListText(T existing) : base(existing) { }

        public abstract S GetElementFrom(T t, int index);
        public abstract void SetElementTo(ref T t, int index, S v);

        #region BaseTextNumber
        public override bool TryConvertToValue(ref T currValue) {
            var all = true;
            for (var i = 0; i < textList.Length; i++) {
                var v = GetElementFrom(currValue, i);
                if (textList[i].TryConvertToValue(ref v))
                    SetElementTo(ref currValue, i, v);
                else
                    all = false;
            }
            return all;
        }
        protected override void ConvertBackToText(T nextValue) {
            for (var i = 0; i < textList.Length; i++)
                textList[i].Value = GetElementFrom(nextValue, i);
        }
        #endregion

        public virtual int CountElement() {
            return textList == null ? 0 : textList.Length;
        }
        public string this[int index] {
            get {
                return textList[index].StrValue;
            }
            set {
                textList[index].StrValue = value;
            }
        }
        public S GetElementFromText(int index) {
            return textList[index].Value;
        }
        public void SetElementToText(int index, S v) {
            textList[index].Value = v;
        }
    }

    public abstract class BaseFloatListText<T> : BaseNumberListText<T, float> {
        protected FloatText[] textFloatList;

        public BaseFloatListText(T existing, int elementCount) : base(existing) {
            textList = textFloatList = new FloatText[elementCount];
            for (var i = 0; i < textFloatList.Length; i++)
                textFloatList[i] = new FloatText(GetElementFrom(existing, i));
        }
    }

    public class Vector4Text : BaseFloatListText<Vector4> {

        public Vector4Text(Vector4 existing) : base(existing, 4) { }

        #region BaseTextFloatList
        public override float GetElementFrom(Vector4 t, int index) {
            return t[index];
        }
        public override void SetElementTo(ref Vector4 t, int index, float v) {
            t[index] = v;
        }
        #endregion
    }
    public class MatrixText : BaseFloatListText<Matrix4x4> {

        public MatrixText(Matrix4x4 existing) : base(existing, 16) { }

        #region BaseTextFloatList
        public override float GetElementFrom(Matrix4x4 t, int index) {
            return t[index];
        }
        public override void SetElementTo(ref Matrix4x4 t, int index, float v) {
            t[index] = v;
        }
        #endregion
    }
}