using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class TextFieldView : BaseView {
        public ReactiveProperty<string> Input { get; set; }

        public override void Draw() {
            using (new GUILayout.HorizontalScope()) {
				if (!string.IsNullOrWhiteSpace(Tooltip))
					Debug.Log($"Title={Title}, Tooltip={Tooltip}");
                GUILayout.Label(new GUIContent(Title, Tooltip), GUILayout.ExpandWidth(false));
                Input.Value = GUILayout.TextField(Input.Value);
            }
        }
    }
}
