using nobnak.Gist;
using nobnak.Gist.Events;
using nobnak.Gist.Exhibitor;
using nobnak.Gist.Extensions.FileExt;
using nobnak.Gist.IMGUI.Scope;
using nobnak.Gist.InputDevice;
using nobnak.Gist.Loader;
using System.Linq;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModelDrivenGUISystem.Examples {

    [ExecuteAlways]
    public class MainUI : MonoBehaviour {
        public const string SERIALIZED_EXT = "txt";

        [SerializeField]
        protected ExhibitorFolder exhitFolder = new ExhibitorFolder();
        [SerializeField]
        protected FolderPath folderPath = new FolderPath();

        [SerializeField]
        protected BoolEvent visibility = new BoolEvent();

        [SerializeField]
        protected KeycodeToggle uiVisibility = new KeycodeToggle(KeyCode.C);

        protected Validator validator = new Validator();
        protected Rect windowRect = new Rect(10, 10, 300f, 500f);
        protected Vector2 scrollPos;
        protected Vector2 scrollSizeDiff;

        protected int selectedTab = 0;
        protected string[] tabNames = new string[0];
        protected string[] serializedNames = new string[0];

        protected System.IDisposable unirxLogger;

        #region unity
        private void OnEnable() {
            unirxLogger = ObservableLogger.Listener.Where(v => v.LogType == LogType.Exception).LogToUnityDebug();

            uiVisibility.Reset();
            uiVisibility.Toggle += v => NotifyVisibility();

            validator.Reset();
            validator.Validation += () => {
                Debug.Log($"{name}-{GetType().Name} : Serialized to {folderPath.Folder}");

                var exhibitors = exhitFolder.exhibitors;
                tabNames = exhibitors.Select(v => v.name).ToArray();
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

            selectedTab = GUILayout.Toolbar(selectedTab, tabNames);
            var currExhibit = (0 <= selectedTab && selectedTab < exhibitors.Length)
                ? exhibitors[selectedTab] : null;
            if (currExhibit != null) {
                using (new GUIChangedScope(()=> currExhibit.ReflectChangeOf(MVVMComponent.View)))
                    currExhibit.Draw();
            }

            GUILayout.EndVertical();
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
            visibility.Invoke(v);
        }
        private void NotifyVisibility() {
            NotifyVisibility(isActiveAndEnabled && uiVisibility.Visible);
        }
        #endregion

        #region classes
        [System.Serializable]
        public class ExhibitorFolder {
            public AbstractExhibitor[] exhibitors = new AbstractExhibitor[0];
        }
        #endregion
    }
}
