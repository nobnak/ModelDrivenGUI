using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using UniRx;
using System.Linq;

namespace ModelDrivenGUISystem.ViewModel {

    public class ArrayViewModel<T> : BaseViewModel {

        public ReactiveProperty<T[]> Input { get; set; }
        public ReactiveCollection<BaseView> Output { get; set; }

        public ArrayViewModel(IValue<T[]> model, IViewFactory viewFactory) {
            Input = new ReactiveProperty<T[]>(model.Value);
            Input.Subscribe(v => model.Value = v);

            Output = new ReactiveCollection<BaseView>(
                Input.Value.Select(t => ClassConfigurator.GenerateClassView(
                    new BaseValue<object>(t), viewFactory)));
        }
    }
}
