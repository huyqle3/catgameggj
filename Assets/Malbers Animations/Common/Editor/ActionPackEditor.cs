using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace MalbersAnimations
{
    [CustomEditor(typeof(Actions))]
    public class ActionsPackEditor : Editor
    {
        private ReorderableList list;
        private Actions MInput;
        private bool swap;


        private void OnEnable()
        {
            MInput = ((Actions)target);

            list = new ReorderableList(serializedObject, serializedObject.FindProperty("actions"), true, true, true, true);
            list.drawElementCallback = drawElementCallback;
            list.drawHeaderCallback = HeaderCallbackDelegate;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.HelpBox("ID Value is the value for the transitions ActionID on the Animator in order to Execute the desirable animation clip", MessageType.Info);
            EditorGUI.BeginChangeCheck();
            list.DoLayoutList();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }


        /// <summary>
        /// Reordable List Header
        /// </summary>
        void HeaderCallbackDelegate(Rect rect)
        {
            Rect R_1 = new Rect(rect.x+12, rect.y, (rect.width) / 2, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_1, "Name");
            Rect R_2 = new Rect(rect.x + (rect.width - 12) / 2, rect.y, (rect.width - 20) / 2, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_2, "ID");
        }


        void drawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            ActionsEmotions element = MInput.actions[index];
            
            rect.y += 2;

            Rect R_1 = new Rect(rect.x , rect.y,(rect.width-20)/2-23, EditorGUIUtility.singleLineHeight);
            GUIStyle a = new GUIStyle();

            //This make the name a editable label
            a.fontStyle = FontStyle.Normal;
            element.name = EditorGUI.TextField(R_1, element.name,a);

            Rect R_2 = new Rect(rect.x + (rect.width - 20) / 2, rect.y, (rect.width - 20) / 2, EditorGUIUtility.singleLineHeight);
            element.ID =EditorGUI.IntField(R_2, element.ID);
        }
    }
}