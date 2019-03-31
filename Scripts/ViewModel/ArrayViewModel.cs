using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UniRx;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using ModelDrivenGUISystem.Extensions.ConstructorExt;

namespace ModelDrivenGUISystem.ViewModel {

    public class ArrayViewModel<T> : BaseViewModel where T:new() {

        public ReactiveProperty<T[]> Input { get; set; }
        public IReadOnlyReactiveProperty<string> OutputCount { get; set; }
        public IReadOnlyReactiveProperty<BaseView[]> OutputViews { get; set; }

        public ReactiveCommand CommandAdd { get; set; }
        public ReactiveCommand CommandRemove { get; set; }

        public ArrayViewModel(IValue<T[]> model, IViewFactory viewFactory) {
            Input = new ReactiveProperty<T[]>(model.Value);
            Input.Subscribe(v => model.Value = v);
            
            OutputCount = Input.Select(t => string.Format("{0}", t.Length)).ToReactiveProperty();
            OutputViews = Input.Select(ts => {
                DisposeViews();
                return ts.SelectMany((t, index) => {
                    var typeOfField = typeof(T);
                    var modelFactory = new ArrayEleentModelFactory(model.Value, index);
                    return ClassConfigurator.GenerateFieldView(
                        modelFactory, viewFactory,
                        typeOfField, string.Format("{0}", index));
                }).ToArray();
            }).ToReactiveProperty();

            CommandAdd = Input.Select(ts => ts != null).ToReactiveCommand();
            CommandAdd.Subscribe(u => {
                var currSize = Input.Value.Length;
                var inputArray = Input.Value;
                System.Array.Resize(ref inputArray, currSize + 1);
                inputArray[currSize] = ConstructorExtension.CreateInstanceHierarchy<T>();
                Input.Value = inputArray;
            });

            CommandRemove = Input.Select(ts => ts != null && ts.Length > 0).ToReactiveCommand();
            CommandRemove.Subscribe(u => {
                var currSize = Input.Value.Length;
                var inputArray = Input.Value;
                System.Array.Resize(ref inputArray, currSize - 1);
                Input.Value = inputArray;
            });
        }
        public void DisposeViews() {
            if (OutputViews != null && OutputViews.Value != null)
                foreach (var v in OutputViews.Value)
                    v.Dispose();
        }
    }
}
