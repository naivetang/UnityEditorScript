using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NaiveTest
{
    /// <summary>
    /// 挂载在场景中的测试脚本入口
    /// </summary>

    public class TestMain : MonoBehaviour
    {
        void Start()
        {
            TestNaiveSingleton testNaiveSingleton = TestNaiveSingleton.Instance;

            TestMonoSingleton testMonoSingleton = TestMonoSingleton.Instance;

            TestNaiveSingleton.DestroyInstance();

            TestMonoSingleton.DestroyInstance();
        }
    }
}
