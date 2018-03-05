using UnityEngine;

namespace ModelDrivenGUISystem.Text {

    public abstract class BaseNumberText<T> {
        protected string currText;
        protected bool invalid;
        protected T currValue;

        public event System.Action<BaseNumberText<T>> ValueOnChange;

        public BaseNumberText(T initValue, bool initInvalidity = true) {
            this.currValue = initValue;
            ConvertBackToText(initValue);
            this.invalid = initInvalidity;
        }

        public abstract bool TryConvertToValue(ref T currValue);
        protected abstract void ConvertBackToText(T nextValue);

        public virtual void NotifyValueOnChange() {
            if (ValueOnChange != null)
                ValueOnChange(this);
        }
        public virtual void Invalidate() {
            invalid = true;
        }
        public virtual bool CheckValidation() {
            return !invalid || !(invalid = !TryConvertToValue(ref currValue));
        }

        public virtual T Value {
            get {
                CheckValidation();
                return currValue;
            }
            set {
                if (!currValue.Equals(value)) {
                    currValue = value;
                    ConvertBackToText(value);
                    NotifyValueOnChange();
                }
            }
        }
        public string StrValue {
            get {
                return currText;
            }
            set {
                if (value != null && value != currText) {
                    currText = value;
                    Invalidate();
                }
            }
        }
    }

    public class FloatText : BaseNumberText<float> {

        public FloatText(float exisitingValue) : base(exisitingValue) { }

        #region BaseTextNumber
        public override bool TryConvertToValue(ref float currValue) {
            var nextValue = currValue;
            var res = float.TryParse(currText, out nextValue);
            if (res)
                currValue = nextValue;
            return res;
        }
        protected override void ConvertBackToText(float nextValue) {
            currText = nextValue.ToString();
        }
        #endregion

	}
	
	public class IntText : BaseNumberText<int> {

        public IntText(int existing) : base(existing) { }

        #region BaseTextNumber
        public override bool TryConvertToValue(ref int currValue) {
            var nextValue = currValue;
            var res = int.TryParse(currText, out nextValue);
            if (res)
                currValue = nextValue;
            return res;
        }
        protected override void ConvertBackToText(int nextValue) {
            currText = nextValue.ToString();
        }
        #endregion
        
    }
}