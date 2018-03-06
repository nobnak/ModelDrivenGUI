using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Extensions.VectorExt {

    public static class VectorExtension {

        public static IEnumerable<float> Split(this Vector4 v) {
            for (var i = 0; i < 4; i++)
                yield return v[i];
        }
        public static IEnumerable<float> Split(this Vector3 v) {
            for (var i = 0; i < 3; i++)
                yield return v[i];
        }
        public static IEnumerable<float> Split(this Vector2 v) {
            for (var i = 0; i < 2; i++)
                yield return v[i];
        }

        public static Vector4 ToVector4(this IEnumerable<float> buffer) {
            var stream = buffer.GetEnumerator();
            var v = new Vector4();
            for (var i = 0; i < 4 && stream.MoveNext(); i++)
                v[i] = stream.Current;
            return v;
        }
        public static Vector3 ToVector3(this IEnumerable<float> buffer) {
            var stream = buffer.GetEnumerator();
            var v = new Vector3();
            for (var i = 0; i < 3 && stream.MoveNext(); i++)
                v[i] = stream.Current;
            return v;
        }
        public static Vector2 ToVector2(this IEnumerable<float> buffer) {
            var stream = buffer.GetEnumerator();
            var v = new Vector2();
            for (var i = 0; i < 4 && stream.MoveNext(); i++)
                v[i] = stream.Current;
            return v;
        }
    }
}
