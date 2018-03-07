using ModelDrivenGUISystem.Extensions.VectorExt;
using ModelDrivenGUISystem.ValueWrapper;
using System.Linq;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.ViewModel {

    public class Vector4ViewModel : BaseViewModel {
        public ReactiveProperty<Vector4> Input { get; protected set; }
        public ReactiveCollection<string> Output { get; protected set; }

        public Vector4ViewModel(IValue<Vector4> model) {
            Input = new ReactiveProperty<Vector4>(model.Value);
            Output = new ReactiveCollection<string>(Input.Value.Split().Select(v => v.ToString()));
            Input.Subscribe(vec => {
            model.Value =  vec;
                for (var i = 0; i < 4; i++)
                    Output[i] = vec[i].ToString();
            });
            Output.ObserveReplace().Subscribe(e => {
                float next;
                if (float.TryParse(e.NewValue, out next)) {
                    var vec = Input.Value;
                    vec[e.Index] = next;
                    Input.Value = vec;
                }
            });
        }
    }
}
