using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace ModelDrivenGUISystem.View {

    public class LabelView : BaseView {
        public enum UsageEnum { Normal = 0, Comment }
        public const string CD_USAGE = "LabelUsage";
        public const string CD_STYLE = "Style";

        public GUIStyle Style { get; set; }
        public UsageEnum Usage { get; set; }

        public override void Initialize() {
            if (initialized)
                return;
            base.Initialize();

            var usageObj = default(object);
            if (CustomData != null
                && CustomData.TryGetValue(CD_USAGE, out usageObj) 
                && usageObj != null 
                && usageObj is UsageEnum) {

                Usage = (UsageEnum)usageObj;
            }

            switch (Usage) {
                case UsageEnum.Comment:
                    Style = new GUIStyle(GUI.skin.label);
                    Style.normal.textColor = Color.gray;
                    Style.fontStyle = FontStyle.Italic;
                    break;
            }
        }
        public override void Draw() {
            if (Style != null)
                GUILayout.Label(Title, Style);
            else
                GUILayout.Label(Title);
        }
    }
}
