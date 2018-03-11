using ModelDrivenGUISystem.ValueWrapper;
using System.Reflection;

namespace ModelDrivenGUISystem.Factory {

    public class FieldModelFactory : IModelFactory {
        public IValue<object> Parent { get; protected set; }
        public FieldInfo Field { get; protected set; }

        public FieldModelFactory(IValue<object> parent, FieldInfo field) {
            this.Parent = parent;
            this.Field = field;
        }

        public IValue<T> CreateValue<T>() {
            return new FieldValue<T>(Parent, Field);
        }

        public object CreateValue(System.Type typeOfValue) {
            var modelType = typeof(FieldValue<>).MakeGenericType(Field.FieldType);
            return System.Activator.CreateInstance(modelType, new object[] { Parent, Field });
        }
    }
}
