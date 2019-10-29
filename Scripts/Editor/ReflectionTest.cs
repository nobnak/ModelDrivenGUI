using ModelDrivenGUISystem.Extensions.FieldInfoExt;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ModelDrivenGUISystem.Editor {

    public class ReflectionTest {
        [Test]
        public void GenericInterfaceTest() {
            var genericInterface = typeof(IList<>);
            var list = new List<float>();
            var interfaces = list.GetType()
                    .GetInterfaces()
                    .ToArray();

            Assert.AreEqual(
                genericInterface,
                typeof(IList<float>).GetGenericTypeDefinition());

            Assert.True(interfaces.Any(v =>
                v.IsGenericType && (v.GetGenericTypeDefinition() == genericInterface)
                ));
        }
        [Test]
        public void ReflectionTestSimplePasses() {
            var data = new Data();
            Assert.AreEqual(
                DataSectionEnum.ValueType_Vector,
                data.vector3float.Section());
            Assert.AreEqual(
                DataSectionEnum.Primitive_Int,
                data.serializeFieldInt.Section());
            Assert.AreEqual(
                DataSectionEnum.Class_Array,
                data.floatArray.Section());
            Assert.AreEqual(
                DataSectionEnum.Class_ListGeneric,
                data.floatList.Section());

            Debug.Log(data.stringField.Dump());
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
            public string stringField;
        }
    }
}
