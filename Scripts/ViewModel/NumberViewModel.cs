using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.ValueWrapper;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class NumberViewModel<ValueType> : BaseViewModel {
        public ReactiveProperty<ValueType> Input { get; protected set; }
        public ReactiveProperty<string> Output { get; protected set; }

        public IValueAccessor<ValueType> Accessor;

        public NumberViewModel(IValue<ValueType> model, IValueAccessor<ValueType> accessor) {
            this.Accessor = accessor;

            Input = new ReactiveProperty<ValueType>(model.Value);
            Input.Subscribe(v => model.Value = v);

            Output = Input.Select(v => v.ToString()).ToReactiveProperty();
            Output.Subscribe(v => {
                ValueType nextValue;
                if (accessor.TryParse(v, out nextValue))
                    Input.Value = nextValue;
            });
        }
    }
}