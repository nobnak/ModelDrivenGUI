using ModelDrivenGUISystem.Scope;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class ArrayView : BaseView {
        public virtual ReactiveProperty<string> Count { get; set; }
        public virtual ReactiveProperty<BaseView[]> Views { get; set; }

        public virtual ReactiveCommand CommandAdd { get; set; }

        protected bool visible = true;

        public override void Draw() {
            using (new GUILayout.VerticalScope())
            using (new FoldoutScope(ref visible, Title))
            using (new IndentScope(20f)) {
                if (visible) {
                    using (new GUILayout.HorizontalScope()) {
                        GUILayout.Label("Size", GUILayout.ExpandWidth(false));
                        Count.Value = GUILayout.TextField(string.Format("{0}", Count.Value));
                    }
                    using (new GUILayout.HorizontalScope()) {
                        if (GUILayout.Button("Add"))
                            CommandAdd.Execute();
                    }
                    foreach (var v in Views.Value)
                        v.Draw();
                }
            }
        }
    }
}
