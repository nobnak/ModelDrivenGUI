using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Accessor {
    

    public class Vector2Accessor : IVectorAccessor<Vector2, float> {
        public int Count { get; protected set; }

        public Vector2Accessor() {
            this.Count = 2;
        }

        public Vector2 Join(IEnumerable<float> elements) {
            var vec = new Vector2();
            var stream = elements.GetEnumerator();
            for (var i = 0; i < Count && stream.MoveNext(); i++)
                vec[i] = stream.Current;
            return vec;
        }
        public float GetElement(ref Vector2 vector, int index) {
            return vector[index];
        }
        public void SetElement(ref Vector2 vector, int index, float element) {
            vector[index] = element;
        }
        public IEnumerable<float> Split(Vector2 vector) {
            for (var i = 0; i < Count; i++)
                yield return vector[i];
        }

        public bool TryParse(string s, out float result) {
            return float.TryParse(s, out result);
        }
    }
    public class Vector3Accessor : IVectorAccessor<Vector3, float> {
        public int Count { get; protected set; }

        public Vector3Accessor() {
            this.Count = 3;
        }

        public Vector3 Join(IEnumerable<float> elements) {
            var vec = new Vector3();
            var stream = elements.GetEnumerator();
            for (var i = 0; i < Count && stream.MoveNext(); i++)
                vec[i] = stream.Current;
            return vec;
        }
        public float GetElement(ref Vector3 vector, int index) {
            return vector[index];
        }
        public void SetElement(ref Vector3 vector, int index, float element) {
            vector[index] = element;
        }
        public IEnumerable<float> Split(Vector3 vector) {
            for (var i = 0; i < Count; i++)
                yield return vector[i];
        }

        public bool TryParse(string s, out float result) {
            return float.TryParse(s, out result);
        }
    }
    public class Vector4Accessor : IVectorAccessor<Vector4, float> {
        public int Count { get; protected set; }

        public Vector4Accessor() {
            this.Count = 4;
        }

        public Vector4 Join(IEnumerable<float> elements) {
            var vec = new Vector4();
            var stream = elements.GetEnumerator();
            for (var i = 0; i < Count && stream.MoveNext(); i++)
                vec[i] = stream.Current;
            return vec;
        }
        public float GetElement(ref Vector4 vector, int index) {
            return vector[index];
        }
        public void SetElement(ref Vector4 vector, int index, float element) {
            vector[index] = element;
        }
        public IEnumerable<float> Split(Vector4 vector) {
            for (var i = 0; i < Count; i++)
                yield return vector[i];
        }

        public bool TryParse(string s, out float result) {
            return float.TryParse(s, out result);
        }
    }
    public class ColorAccessor : IVectorAccessor<Color, float> {
        public int Count { get; protected set; }

        public ColorAccessor() {
            this.Count = 4;
        }

        public Color Join(IEnumerable<float> elements) {
            var vec = new Color();
            var stream = elements.GetEnumerator();
            for (var i = 0; i < Count && stream.MoveNext(); i++)
                vec[i] = stream.Current;
            return vec;
        }
        public float GetElement(ref Color vector, int index) {
            return vector[index];
        }
        public void SetElement(ref Color vector, int index, float element) {
            vector[index] = element;
        }
        public IEnumerable<float> Split(Color vector) {
            for (var i = 0; i < Count; i++)
                yield return vector[i];
        }

        public bool TryParse(string s, out float result) {
            return float.TryParse(s, out result);
        }
    }
}
