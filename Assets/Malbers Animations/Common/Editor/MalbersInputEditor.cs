using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace MalbersAnimations
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MalbersInput))]
    public class MalbersInputEditor : Editor
    {
        private ReorderableList list;
        private MalbersInput MInput;
        private bool CameraBaseInput;

        private void OnEnable()
        {
            MInput = ((MalbersInput)target);

            list = new ReorderableList(serializedObject, serializedObject.FindProperty("inputs"), false, true, true, true);
            list.drawElementCallback = drawElementCallback;
            list.drawHeaderCallback = HeaderCallbackDelegate;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.HelpBox("This Script will connect the inputs to the Controller ", MessageType.Info);

            EditorGUI.BeginChangeCheck();
            list.DoLayoutList();

            MInput.cameraBaseInput = EditorGUILayout.Toggle(new GUIContent("Camera Input", "The Character will follow the camera forward axis"), MInput.cameraBaseInput );

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
            Rect R_1 = new Rect(rect.x + 20, rect.y, (rect.width - 20) / 4 - 23, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_1, "Name");

            Rect R_2 = new Rect(rect.x + (rect.width - 20) / 4 + 15, rect.y, (rect.width - 20) / 4, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_2, "Type");

            Rect R_3 = new Rect(rect.x + ((rect.width - 20) / 4) * 2 + 18, rect.y, ((rect.width - 30) / 4) + 11, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_3, "Value");

            Rect R_4 = new Rect(rect.x + ((rect.width) / 4) * 3 + 15, rect.y, ((rect.width) / 4) - 15, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(R_4, "Button");
        }

        void drawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = MInput.inputs[index];
            rect.y += 2;
            element.active = EditorGUI.Toggle(new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight), element.active);

            Rect R_1 = new Rect(rect.x + 20, rect.y, (rect.width - 20) / 4 - 23, EditorGUIUtility.singleLineHeight);
            GUIStyle a = new GUIStyle();

            //This make the name a editable label
            a.fontStyle = FontStyle.Normal;
            element.name = EditorGUI.TextField(R_1, element.name, a);

            Rect R_2 = new Rect(rect.x + (rect.width - 20) / 4+15, rect.y, (rect.width - 20) / 4, EditorGUIUtility.singleLineHeight);
            element.type = (InputType)EditorGUI.EnumPopup(R_2, element.type);

            Rect R_3 = new Rect(rect.x + ((rect.width - 20) / 4) * 2 + 18, rect.y, ((rect.width - 30) / 4)+11 , EditorGUIUtility.singleLineHeight);
            if (element.type != InputType.Key)
                element.input = EditorGUI.TextField(R_3, element.input);
            else
                element.key = (KeyCode)EditorGUI.EnumPopup(R_3, element.key);

            Rect R_4 = new Rect(rect.x + ((rect.width) / 4) * 3 +15, rect.y, ((rect.width) / 4)-15 , EditorGUIUtility.singleLineHeight);
            element.GetPressed = (InputButton)EditorGUI.EnumPopup(R_4, element.GetPressed);
        }
    }
}