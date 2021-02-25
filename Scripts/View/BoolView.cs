using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class BoolView : BaseView {
        public ReactiveProperty<bool> Input { get; set; }

        public override void Draw() {
            Input.Value = GUILayout.Toggle(Input.Value, new GUIContent(Title, Tooltip), GUILayout.ExpandWidth(false));
        }
    }
}
