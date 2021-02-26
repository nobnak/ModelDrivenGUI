using ModelDrivenGUISystem.Extensions.GUIExt;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class VectorView : BaseView {
        public virtual ReactiveCollection<string> Input { get; set; }

        public override void Draw() {
            using (new GUILayout.HorizontalScope(new GUIContent(Title, Tooltip), GUIStyle.none)) {
                GUILayout.Label(Title, GUILayout.ExpandWidth(false));
				for (var i = 0; i < Input.Count; i++) {
					Input[i] = GUILayout.TextField(Input[i]);
				}
            }
        }
    }
}
