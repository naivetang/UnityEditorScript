#if  UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Naive.Editor
{
    public enum OnOffEnum
    {
        None = 0,
        On,
        Off,
    }

    [InitializeOnLoad]
    internal class HierarchyQuickVisible
    {

        [MenuItem(EditorMenuDefine.HierarchyQuickVisible + "开启")]
        private static void Open()
        {
            UpdateEnable(OnOffEnum.On);
        }


        [MenuItem(EditorMenuDefine.HierarchyQuickVisible + "开启", true)]
        private static bool OpenCheck()
        {
            return _onOff != OnOffEnum.On;
        }

        [MenuItem(EditorMenuDefine.HierarchyQuickVisible + "关闭")]
        private static void Close()
        {
            UpdateEnable(OnOffEnum.Off);
        }

        [MenuItem(EditorMenuDefine.HierarchyQuickVisible + "关闭", true)]
        private static bool CloseCheck()
        {
            return _onOff == OnOffEnum.On;
        }


        private const string EnableActivity = EditorMenuDefine.HierarchyQuickVisible;

        private static OnOffEnum _onOff = OnOffEnum.None;

        private static readonly List<Type> IconCmp = new List<Type>() { typeof(Image), typeof(Button), typeof(Animation), typeof(Animator), typeof(Text), typeof(Camera) };

        static HierarchyQuickVisible()
        {
            UpdateEnable(OnOffEnum.None);
        }

        static void UpdateEnable(OnOffEnum _enable)
        {
            HierarchyQuickVisible._onOff = _enable;

            if (HierarchyQuickVisible._onOff == OnOffEnum.None)
                HierarchyQuickVisible._onOff = (OnOffEnum)PlayerPrefs.GetInt(EnableActivity,(int)OnOffEnum.On); // 默认开启
            else
                PlayerPrefs.SetInt(EnableActivity, (int)HierarchyQuickVisible._onOff);

            if (HierarchyQuickVisible._onOff == OnOffEnum.On)
            {
                Debug.Log(EditorMenuDefine.HierarchyQuickVisible + " : on");

                EditorApplication.hierarchyWindowItemOnGUI += DrawHierarchyIcon;

                EditorApplication.update += Update;
            }
            else
            {
                Debug.Log(EditorMenuDefine.HierarchyQuickVisible + " : off");

                EditorApplication.hierarchyWindowItemOnGUI -= DrawHierarchyIcon;

                EditorApplication.update -= Update;
            }
        }



        private static void DrawActiveToggle(Rect originRect, GameObject go)
        {
            Rect copyRect = new Rect(originRect);

            copyRect.width = 18;

            GUIStyle guiStyle = new GUIStyle(GUI.skin.label);

            Vector2 size = guiStyle.CalcSize(new GUIContent(go.name));

            copyRect.x += size.x;
            copyRect.x += 20;

            go.SetActive(GUI.Toggle(copyRect, go.activeSelf, string.Empty));
        }

        // 绘制icon方法
        private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
        {
            GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (go == null)
                return;

            Rect hierarchyRowRect = new Rect(
                0,
                selectionRect.y,
                selectionRect.x + selectionRect.width,
                selectionRect.height
            );

            Vector2 mousePos = Event.current.mousePosition;

            if (hierarchyRowRect.Contains(mousePos))
            {
                DrawActiveToggle(selectionRect, go);
            }

            Rect drawRect = GetFirstRect(selectionRect);

            for (int i = 0; i < IconCmp.Count; i++)
            {
                if (Draw(go, drawRect, IconCmp[i]))
                {
                    drawRect.x -= drawRect.width;
                }
            }
        }

        private static bool Draw(GameObject go, Rect drawRect, Type type)
        {
            if (go.GetComponent(type) != null)
            {
                var icon = EditorGUIUtility.ObjectContent(null, type).image;
                GUI.Label(drawRect, icon);
                return true;
            }

            return false;
        }

        private static Rect GetFirstRect(Rect selectionRect)
        {
            Rect rect = new Rect(selectionRect);
            rect.x += rect.width - rect.height;
            rect.width = rect.height;
            return rect;
        }

        private static void Update()
        {
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}

#endif