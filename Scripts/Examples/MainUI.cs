using nobnak.Gist;
using nobnak.Gist.DocSys;
using nobnak.Gist.Events;
using nobnak.Gist.Exhibitor;
using nobnak.Gist.Extensions.FileExt;
using nobnak.Gist.Extensions.ReflectionExt;
using nobnak.Gist.IMGUI.Scope;
using nobnak.Gist.InputDevice;
using nobnak.Gist.Loader;
using nobnak.Gist.MathAlgorithms.Extensions.MathExt;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using nobnak.Gist.Extensions.ScreenExt;
using ModelDrivenGUISystem.Extensions.GUIExt;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ModelDrivenGUISystem.Examples {

    [ExecuteAlways]
    public class MainUI : MonoBehaviour {
        public const string SERIALIZED_EXT = "txt";

        [SerializeField]
        protected ExhibitorFolder exhitFolder = new ExhibitorFolder();
        [SerializeField]
        protected FolderPath folderPath = new FolderPath();
		[SerializeField]
		protected Tuner tuner = new Tuner();
		[SerializeField]
		protected Events events = new Events();

        [SerializeField]
        protected KeycodeToggle uiVisibility = new KeycodeToggle(KeyCode.C);
		protected bool currUIVisibility;

        protected Validator validator = new Validator();
        protected Rect windowRect = new Rect(10, 10, 300f, 500f);
        protected Vector2 scrollPos;
        protected Vector2 scrollSizeDiff;

        protected int selectedTab = 0;
        protected string[] tabNames = new string[0];
		protected GUIContent[] tabContens = new GUIContent[0];
        protected string[] serializedNames = new string[0];

        protected System.IDisposable unirxLogger;

        #region unity
        private void OnEnable() {
            unirxLogger = ObservableLogger.Listener.Where(v => v.LogType == LogType.Exception).LogToUnityDebug();

            uiVisibility.Reset();
            uiVisibility.Toggle += v => NotifyVisibility();

            validator.Reset();
            validator.Validation += () => {
                var exhibitors = exhitFolder.exhibitors;
                tabNames = exhibitors.Select(v => v.Name).ToArray();
				tabContens = exhibitors.Select(v => new GUIContent(v.Name, v.RawData().GetType().GetTooltip())).ToArray();
				var appName = Application.productName.SanitizeFilename();
                var scene = SceneManager.GetActiveScene();
                var sceneName = scene.name;
                serializedNames = exhibitors
                    .Select(v => $"{appName}-{sceneName}-{v.gameObject.name.SanitizeFilename()}-{v.GetType().Name.SanitizeFilename()}.{SERIALIZED_EXT}")
                    .ToArray();
            };

            NotifyVisibility();

            Load();
        }
        private void OnDisable() {
            if (unirxLogger != null) {
                unirxLogger.Dispose();
                unirxLogger = null;
            }
            NotifyVisibility();
        }
        private void OnValidate() {
            validator.Invalidate();
        }
        private void Update() {
            validator.Validate();
            uiVisibility.Update();
        }
        private void OnGUI() {
            if (!uiVisibility.Visible)
                return;

			if (tuner.enableDpiScale) {
				ScreenExtension.ScaleGUIBasedOnDpi();
			}

			windowRect = GUILayout.Window(GetInstanceID(), windowRect, Window, name);

            var currSize = windowRect.size;
            currSize.x += Mathf.Max(0f, scrollSizeDiff.x);
            windowRect.size = currSize;
        }
		#endregion

		#region member
		protected void Window(int id) {
            var exhibitors = exhitFolder.exhibitors;
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save")) {
                Save();
            }
            if (GUILayout.Button("Load")) {
                Load();
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("", GUI.skin.horizontalSlider);

            selectedTab = GUILayout.Toolbar(selectedTab, tabContens);
            var currExhibit = (0 <= selectedTab && selectedTab < exhibitors.Length)
                ? exhibitors[selectedTab] : null;
            if (currExhibit != null) {
                using (new GUIChangedScope(() => currExhibit.ReflectChangeOf(MVVMComponent.View)))
                    currExhibit.Draw();
            }

            GUILayout.EndVertical();

			GUIExtension.DrawTooltips();

			GUI.DragWindow();
		}

		private void Save() {
            Debug.Log($"Save : {GetType().Name}");
            validator.Validate();
            var exhibitors = exhitFolder.exhibitors;
            for (var i = 0; i < exhibitors.Length; i++) {
                var ex = exhibitors[i];
                var filename = serializedNames[i];
                var json = ex.SerializeToJson();
                folderPath.TrySave(filename, json);
            }
        }
        private void Load() {
            Debug.Log($"Load : {GetType().Name}");
            validator.Validate();
            var exhibitors = exhitFolder.exhibitors;
            for (var i = 0; i < exhibitors.Length; i++) {
                var ex = exhibitors[i];
                var filename = serializedNames[i];
                string json;
                if (folderPath.TryLoad(filename, out json)) {
                    ex.DeserializeFromJson(json);
                }
            }
        }

        private void NotifyVisibility(bool v) {
			if (currUIVisibility != v) {
				currUIVisibility = v;
				events.Visibility?.Invoke(v);
			}
        }
        private void NotifyVisibility() {
            NotifyVisibility(isActiveAndEnabled && uiVisibility.Visible);
        }
		#endregion

		#region interfaces
		public IEnumerable<string> GenerateDoc(AbstractExhibitor e) {
			var root = e.RawData().GetType();
			foreach (var fi in root.GetInstancePublicFields()) {
				foreach (var line in fi.GenerateDoc(0)) {
					yield return line;
				}
			}
		}
		public IEnumerable<string> GenerateDoc(IEnumerable<AbstractExhibitor> list, int i = 0) {
			foreach (var e in list) {
				if (e is ExhibitorGroup) {
					foreach (var line in GenerateDoc(((ExhibitorGroup)e).Exhibitors, i + 1))
						yield return line;
					continue;
				}

				yield return $"{new string('#', i + 1)} {e.Name} : {e.RawData().GetType().GetTooltip()}";
				foreach (var line in GenerateDoc(e)) {
					yield return line;
				}
			}
		}
		public IEnumerable<string> GenerateDoc() {
			foreach (var line in GenerateDoc(exhitFolder.exhibitors)) {
				yield return line;
			}
		}
		#endregion

		#region classes
		[System.Serializable]
        public class ExhibitorFolder {
            public AbstractExhibitor[] exhibitors = new AbstractExhibitor[0];
        }
		[System.Serializable]
		public class Tuner {
			public bool enableDpiScale = true;
		}

		[System.Serializable]
		public class Events {
			public UnityEngine.Events.UnityEvent<bool> Visibility = new UnityEngine.Events.UnityEvent<bool>();
		}
        #endregion
    }
}
