using ModelDrivenGUISystem.Extensions.GUIExt;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class VectorView : BaseView {
        public virtual ReactiveCollection<string> Input { get; set; }

		protected GUIStyle labelStyle;

		public VectorView() {
			labelStyle ??= new GUIStyle(UnityEngine.GUI.skin.label) {
				alignment = TextAnchor.MiddleLeft,
				wordWrap = false,
			};
		}

		public override void Draw() {
            using (new GUILayout.HorizontalScope(new GUIContent(Title, Tooltip), GUIStyle.none)) {
                GUILayout.Label(Title, labelStyle, GUILayout.ExpandWidth(false));
				for (var i = 0; i < Input.Count; i++) {
					Input[i] = GUILayout.TextField(Input[i]);
				}
            }
        }
    }
}
