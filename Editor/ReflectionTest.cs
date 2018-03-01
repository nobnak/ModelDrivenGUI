using ModelDrivenGUISystem.Extensions.TypeExt;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem {

    public class ReflectionTest {

        [Test]
        public void ReflectionTestSimplePasses() {
            var data = new Data();
            Assert.AreEqual(
                DataSectionEnum.Class_IList,
                data.floatList.Section());
            Assert.AreEqual(
                DataSectionEnum.Class_IList,
                data.floatArray.Section());
            Assert.AreEqual(
                DataSectionEnum.ValueType_Vector,
                data.vector3float.Section());
            Assert.AreEqual(
                DataSectionEnum.Primitive_Int,
                data.serializeFieldInt.Section());
        }

        public struct SomeStruct {
            public int intData;
        }

        [System.Serializable]
        public class Data {
            public List<float> floatList;
            public float[] floatArray;
            public Vector3 vector3float;
            public SomeStruct someStruct;

            public int serializeFieldInt;
        }
    }
}
