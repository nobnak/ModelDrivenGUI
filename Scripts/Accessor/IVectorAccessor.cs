using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Accessor {

    public interface IVectorAccessor<VectorType, ElementType> {
        int Count { get; }

        IEnumerable<ElementType> Split(VectorType vector);
        VectorType Join(IEnumerable<ElementType> elements);

        ElementType GetElement(ref VectorType vector, int index);
        void SetElement(ref VectorType vector, int index, ElementType element);

        bool TryParse(string s, out ElementType result);
    }
}
