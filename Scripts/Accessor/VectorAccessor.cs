using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
	public class Float2Accessor : IVectorAccessor<float2, float> {
		public int Count { get; protected set; }

		public Float2Accessor() {
			this.Count = 2;
		}

		public float2 Join(IEnumerable<float> elements) {
			var vec = new float2();
			var stream = elements.GetEnumerator();
			for (var i = 0; i < Count && stream.MoveNext(); i++)
				vec[i] = stream.Current;
			return vec;
		}
		public float GetElement(ref float2 vector, int index) {
			return vector[index];
		}
		public void SetElement(ref float2 vector, int index, float element) {
			vector[index] = element;
		}
		public IEnumerable<float> Split(float2 vector) {
			for (var i = 0; i < Count; i++)
				yield return vector[i];
		}

		public bool TryParse(string s, out float result) {
			return float.TryParse(s, out result);
		}
	}
	public class Float3Accessor : IVectorAccessor<float3, float> {
		public int Count { get; protected set; }

		public Float3Accessor() {
			this.Count = 3;
		}

		public float3 Join(IEnumerable<float> elements) {
			var vec = new Vector3();
			var stream = elements.GetEnumerator();
			for (var i = 0; i < Count && stream.MoveNext(); i++)
				vec[i] = stream.Current;
			return vec;
		}
		public float GetElement(ref float3 vector, int index) {
			return vector[index];
		}
		public void SetElement(ref float3 vector, int index, float element) {
			vector[index] = element;
		}
		public IEnumerable<float> Split(float3 vector) {
			for (var i = 0; i < Count; i++)
				yield return vector[i];
		}

		public bool TryParse(string s, out float result) {
			return float.TryParse(s, out result);
		}
	}
	public class Float4Accessor : IVectorAccessor<float4, float> {
		public int Count { get; protected set; }

		public Float4Accessor() {
			this.Count = 4;
		}

		public float4 Join(IEnumerable<float> elements) {
			var vec = new Vector4();
			var stream = elements.GetEnumerator();
			for (var i = 0; i < Count && stream.MoveNext(); i++)
				vec[i] = stream.Current;
			return vec;
		}
		public float GetElement(ref float4 vector, int index) {
			return vector[index];
		}
		public void SetElement(ref float4 vector, int index, float element) {
			vector[index] = element;
		}
		public IEnumerable<float> Split(float4 vector) {
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

	public class Vector2IntAccessor : IVectorAccessor<Vector2Int, int> {
		public int Count { get; protected set; }

		public Vector2IntAccessor() {
			this.Count = 2;
		}

		public Vector2Int Join(IEnumerable<int> elements) {
			var vec = new Vector2Int();
			var stream = elements.GetEnumerator();
			for (var i = 0; i < Count && stream.MoveNext(); i++)
				vec[i] = stream.Current;
			return vec;
		}
		public int GetElement(ref Vector2Int vector, int index) {
			return vector[index];
		}
		public void SetElement(ref Vector2Int vector, int index, int element) {
			vector[index] = element;
		}
		public IEnumerable<int> Split(Vector2Int vector) {
			for (var i = 0; i < Count; i++)
				yield return vector[i];
		}

		public bool TryParse(string s, out int result) {
			return int.TryParse(s, out result);
		}
	}
	public class Vector3IntAccessor : IVectorAccessor<Vector3Int, int> {
		public int Count { get; protected set; }

		public Vector3IntAccessor() {
			this.Count = 3;
		}

		public Vector3Int Join(IEnumerable<int> elements) {
			var vec = new Vector3Int();
			var stream = elements.GetEnumerator();
			for (var i = 0; i < Count && stream.MoveNext(); i++)
				vec[i] = stream.Current;
			return vec;
		}
		public int GetElement(ref Vector3Int vector, int index) {
			return vector[index];
		}
		public void SetElement(ref Vector3Int vector, int index, int element) {
			vector[index] = element;
		}
		public IEnumerable<int> Split(Vector3Int vector) {
			for (var i = 0; i < Count; i++)
				yield return vector[i];
		}

		public bool TryParse(string s, out int result) {
			return int.TryParse(s, out result);
		}
	}
}
