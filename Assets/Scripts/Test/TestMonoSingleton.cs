using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naive;
using UnityEngine;

namespace NaiveTest
{
    class TestMonoSingleton : MonoSingleton<TestMonoSingleton>
    {
        public override void Awake()
        {
            base.Awake();

            Debug.Log("TestMonoSingleton Awake");
        }


        public override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("TestMonoSingleton Destroy");
        }
    }
}
