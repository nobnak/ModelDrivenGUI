using ModelDrivenGUISystem.ValueWrapper;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class IntViewModel : BaseViewModel {
        public ReactiveProperty<int> Input { get; protected set; }
        public ReactiveProperty<string> Output { get; protected set; }

        public IntViewModel(IValue<int> model) {
            Input = new ReactiveProperty<int>(model.Value);
            Input.Subscribe(v => model.Value = v);

            Output = Input.Select(v => v.ToString()).ToReactiveProperty();
            Output.Subscribe(v => {
                int nextValue;
                if (int.TryParse(v, out nextValue))
                    Input.Value = nextValue;
            });
        }
    }
}