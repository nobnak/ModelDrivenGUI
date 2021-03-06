﻿using ModelDrivenGUISystem.Accessor;
using ModelDrivenGUISystem.ValueWrapper;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class BypassViewModel<ValueType> : BaseViewModel {
        public ReactiveProperty<ValueType> Input { get; protected set; }
        public ReactiveProperty<ValueType> Output { get; protected set; }

        public IValueAccessor<ValueType> Accessor;

        public BypassViewModel(IValue<ValueType> model) {
            Input = new ReactiveProperty<ValueType>(model.Value);
            Output = new ReactiveProperty<ValueType>(Input.Value);

            Input.Subscribe(v => {
                model.Value = v;
                Output.Value = v;
            });
            Output.Subscribe(v => Input.Value = v);
        }
    }
}