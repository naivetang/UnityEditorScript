using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naive;
using Debug = UnityEngine.Debug;

namespace NaiveTest
{
    public class TestNaiveSingleton : NaiveSingleton<TestNaiveSingleton>
    {
        public override void Init()
        {
            base.Init();

            Debug.Log("TestNaiveSingleton init");
        }

        public override void UnInit()
        {
            base.UnInit();

            Debug.Log("TestNaiveSingleton uninit");
        }

    }
}
