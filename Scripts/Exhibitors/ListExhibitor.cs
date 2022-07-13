using ModelDrivenGUISystem.Factory;
using ModelDrivenGUISystem.ValueWrapper;
using ModelDrivenGUISystem.View;
using nobnak.Gist;
using nobnak.Gist.Exhibitor;
using nobnak.Gist.Extensions.ComponentExt;
using nobnak.Gist.Layer2;
using nobnak.Gist.ObjectExt;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Exhibitors {

    public abstract class ListExhibitor<ArtWorkType, DataTransformType> 
        : AbstractExhibitor where ArtWorkType : Component {

        public Layer layer;
        public Transform parent;
        public ArtWorkType nodefab;

        protected List<ArtWorkType> nodes = new List<ArtWorkType>();
        protected Validator validator = new Validator();

        protected BaseView view;

        #region Unity
        protected virtual void OnEnable() {
            validator.Reset();
            validator.Validation += () => {
                ReflectChangeOf(MVVMComponent.Model);
            };
            validator.Validate();
        }
        protected virtual void Update() {
            validator.Validate();
        }
        protected virtual void OnValidate() {
            validator.Invalidate();
        }
        protected virtual void OnDisable() {
            validator.Invalidate();
            Clear();
        }
        #endregion

        #region interfaces

        #region List
        protected virtual void Add(ArtWorkType n) {
            n.gameObject.hideFlags = HideFlags.DontSave;
            n.transform.SetParent(parent, false);
            n.hideFlags = HideFlags.DontSave;
            nodes.Add(n);
            n.CallbackSelf<IExhibitorListener>(l => l.ExhibitorOnParent(parent));
        }
        protected virtual void AddRange(IEnumerable<ArtWorkType> niter) {
            foreach (var n in niter)
                Add(n);
        }
        protected virtual void Remove(ArtWorkType n) {
            nodes.Remove(n);
            n.CallbackSelf<IExhibitorListener>(l => l.ExhibitorOnUnparent(parent));
        }
        protected virtual void Removerange(IEnumerable<ArtWorkType> niter) {
            foreach (var n in niter)
                Remove(n);
        }
        protected virtual void Clear() {
            var removelist = new List<ArtWorkType>(nodes);
#if UNITY_EDITOR
			foreach (var n in parent.GetComponentsInChildren<ArtWorkType>())
				if (n != parent && !removelist.Contains(n)) removelist.Add(n);
#endif
			foreach (var n in removelist) {
                if (n == null)
                    continue;
                Remove(n);
				n.DestroyGo();
            }
            nodes.Clear();
        }
        #endregion

        #region IExhibitor
        public override string SerializeToJson() {
            validator.Validate();
            return JsonUtility.ToJson(CurrentData);
        }
        public override void DeserializeFromJson(string json) {
            CurrentData = JsonUtility.FromJson<DataTransformType>(json);
            validator.Invalidate();
        }
		public override object RawData() { return CurrentData; }
        public override void Draw() {
            validator.Validate();
            GetView().Draw();
        }

        public override void ResetViewModelFromModel() {
            ResetNodesFromData();
        }
        public override void ApplyViewModelToModel() {
            ResetNodesFromData();
        }
        public override void ResetView() {
            if (view != null) {
                view.Dispose();
                view = null;
            }
        }
        #endregion

        public abstract DataTransformType CurrentData { get; set; }
        public abstract void ResetNodesFromData();

        public virtual Validator Validator { get { return validator; } }
        #endregion

        #region member
        protected virtual BaseView GetView() {
            validator.Validate();
            if (view == null) {
                var f = new SimpleViewFactory();
                view = ClassConfigurator.GenerateClassView(new BaseValue<object>(CurrentData), f);
            }
            return view;
        }
        #endregion
    }
}
