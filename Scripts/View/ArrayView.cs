using ModelDrivenGUISystem.Scope;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class ArrayView : BaseView {
        public virtual ReactiveCollection<BaseView> Input { get; set; }

        protected bool visible = true;

        public override void Draw() {
            using (new GUILayout.VerticalScope())
            using (new FoldoutScope(ref visible, Title))
            using (new IndentScope(20f)) {
                if (visible) {
                    using (new GUILayout.HorizontalScope()) {
                        GUILayout.Label("Size", GUILayout.ExpandWidth(false));
                        GUILayout.TextField(string.Format("{0}", Input.Count));
                    }
                    foreach (var v in Input)
                        v.Draw();
                }
            }
        }
    }
}
