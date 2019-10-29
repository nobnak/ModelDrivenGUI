using System;
using System.Collections;
using System.Collections.Generic;
using ModelDrivenGUISystem.ValueWrapper;
using UnityEngine;

namespace ModelDrivenGUISystem.Factory {

    public class ListElementModelFactory : IModelFactory {
        public List<object> list { get; set; }
        public int Index { get; set; }

        public ListElementModelFactory(List<object> list, int index) {
            this.list = list;
            this.Index = index;
        }

        public IValue<S> CreateValue<S>() {
            return new ListElementValue<S>(list, Index);
        }

        public object CreateValue(Type typeOfValue) {
            var typeOfModel = typeof(ListElementValue<>).MakeGenericType(typeOfValue);
            return Activator.CreateInstance(typeOfModel, new object[] { list, Index });
        }
    }
}
