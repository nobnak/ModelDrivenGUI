using ModelDrivenGUISystem.ViewModel;
using System.Collections.Generic;

namespace ModelDrivenGUISystem.Visualizer {

    public class ClassVisualizer {

        protected List<BaseViewModel> viewModels;

        public ClassVisualizer(List<BaseViewModel> fields) {
            this.viewModels = fields;
        }
    }
}
