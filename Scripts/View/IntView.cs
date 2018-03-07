using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class IntView : BaseView {
        public ReactiveProperty<string> Input { get; set; }

        public override void Draw() {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.Label(Title, GUILayout.ExpandWidth(false));
                Input.Value = GUILayout.TextField(Input.Value);
            }
        }
    }
}
