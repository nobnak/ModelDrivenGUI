using ModelDrivenGUISystem.Extensions.GUIExt;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class TextFieldView : BaseView {
        public ReactiveProperty<string> Input { get; set; }

        public override void Draw() {
            using (new GUILayout.HorizontalScope(new GUIContent(Title, Tooltip), GUIStyle.none)) {
				GUILayout.Label(Title, GUILayout.ExpandWidth(false));
                Input.Value = GUILayout.TextField(Input.Value);

			}
        }
    }
}
