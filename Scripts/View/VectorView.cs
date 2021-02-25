using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class VectorView : BaseView {
        public virtual ReactiveCollection<string> Input { get; set; }

        public override void Draw() {
            using (new GUILayout.HorizontalScope()) {
                GUILayout.Label(new GUIContent(Title, Tooltip), GUILayout.ExpandWidth(false));
                for (var i = 0; i < Input.Count; i++)
                    Input[i] = GUILayout.TextField(Input[i]);
            }
        }
    }
}
