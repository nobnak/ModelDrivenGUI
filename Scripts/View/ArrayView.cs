using nobnak.Gist.IMGUI.Scope;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

	public class ArrayView : BaseView {
        public virtual IReadOnlyReactiveProperty<string> Count { get; set; }
        public virtual IReadOnlyReactiveProperty<BaseView[]> Views { get; set; }

        public virtual ReactiveCommand CommandAdd { get; set; }
        public virtual ReactiveCommand CommandRemove { get; set; }

        protected bool visible = true;

        public override void Initialize() {
            if (initialized)
                return;
            base.Initialize();

            foreach (var v in Views.Value)
                v.Initialize();
        }
        public override void Draw() {
            using (new GUILayout.VerticalScope())
            using (new FoldoutScope(ref visible, Title, Tooltip))
            using (new IndentScope(20f)) {
                if (visible) {
                    GUILayout.Label(string.Format("Size : {0}",  Count.Value));
                    using (new GUILayout.HorizontalScope()) {
                        if (GUILayout.Button("Add"))
                            CommandAdd.Execute();
                        if (GUILayout.Button("Remove"))
                            CommandRemove.Execute();
                    }
                    if (Views.Value != null)
                        foreach (var v in Views.Value)
                            v.Draw();
                }
            }
        }
    }
}
