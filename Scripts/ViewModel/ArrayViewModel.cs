using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UniRx;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace ModelDrivenGUISystem.ViewModel {

    public class ArrayViewModel<T> : BaseViewModel where T:new() {

        public ReactiveProperty<T[]> Input { get; set; }
        public ReactiveProperty<string> OutputCount { get; set; }
        public ReactiveProperty<BaseView[]> OutputViews { get; set; }

        public ReactiveCommand CommandAdd { get; set; }

        public ArrayViewModel(IValue<T[]> model, IViewFactory viewFactory) {
            Input = new ReactiveProperty<T[]>(model.Value);
            Input.Subscribe(v => model.Value = v);
            
            OutputCount = Input.Select(v => string.Format("{0}", v.Length)).ToReactiveProperty();
            OutputViews = Input.Select(vs => vs.SelectMany(
                (v, index) => {
                    var typeOfField = typeof(T);
                    var modelFactory = new ArrayEleentModelFactory(model.Value, index);
                    return ClassConfigurator.GenerateFieldView(
                        modelFactory, viewFactory,
                        typeOfField, string.Format("{0}", index));
                }).ToArray()).ToReactiveProperty();

            Debug.LogFormat("OutputCount : {0}", OutputCount);
            Debug.LogFormat("OutputViews : {0}", OutputViews);

            CommandAdd = new ReactiveCommand();
            CommandAdd.Subscribe(v => {
                var currSize = Input.Value.Length;
                var inputArray = Input.Value;
                System.Array.Resize(ref inputArray, currSize + 1);
                inputArray[currSize] = new T();
                Input.Value = inputArray;
            });
        }
    }
}
