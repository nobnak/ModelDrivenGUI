using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelDrivenGUISystem.Editor {

    public class ArrayTest {

        [Test]
        public void TestArrayType() {
            var array0 = new int[32];
            var array1 = (System.Array)array0;

            var array0elementType = array0.GetType().GetElementType();
            var array1elementType = array1.GetType().GetElementType();

            var element0 = System.Activator.CreateInstance(array0elementType);
            var element1 = System.Activator.CreateInstance(array1elementType);

            Debug.LogFormat("int[] is array ? {0}", array0.GetType().IsArray);
            Debug.LogFormat("Array is array ? {0}", array1.GetType().IsArray);

            Debug.LogFormat("Element type of int[] ? {0}", array0elementType.Name);
            Debug.LogFormat("Element type of Array ? {0}", array1elementType.Name);

            Debug.LogFormat("Create element of int[0] : {0}", element0.GetType().Name);
            Debug.LogFormat("Create element of Array : {0}", element1.GetType().Name);
        }
    }
}

