using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class EnumView : BaseView {
        public ReactiveProperty<string> Input { get; set; }

        public int NumberOfColums { get; set; }

        protected System.Type enumType;
        protected string[] names;

        public EnumView() {
            this.NumberOfColums = 10;
        }

        public System.Type EnumType {
            get { return enumType; }
            set {
                enumType = value;
                names = System.Enum.GetNames(value);
            }
        }

        public override void Draw() {
			using (new GUILayout.VerticalScope(new GUIContent(Title, Tooltip), GUIStyle.none)) {
				GUILayout.Label($"{Title}:");
				var index = System.Array.FindIndex(names, v => v == Input.Value);
				index = GUILayout.SelectionGrid(index, names, Mathf.Min(names.Length, NumberOfColums));
				if (0 <= index && index < names.Length) {
					Input.Value = names[index];
				}
			}
        }
    }
}
