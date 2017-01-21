using UnityEngine;
using UnityEditor;

namespace MalbersAnimations
{
    [CustomEditor(typeof(JumpBehavior))]
    public class JumpBehaviorEd : Editor
    {
        private JumpBehavior MJB;

        private void OnEnable()
        {
            MJB = ((JumpBehavior)target);

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.HelpBox("This Script manage all the Jump Behavior, Deactivate the PosY Constraints(RigidBody) when is in Air", MessageType.Info);

          //  DrawDefaultInspector();
         

            GUIStyle b = new GUIStyle();
            b.fontStyle = FontStyle.Bold;


            EditorGUILayout.LabelField(new GUIContent("Jumping Momentum", "Range in the animation when the character is in the Air"),b);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
         //   MJB.startJump = EditorGUILayout.FloatField(MJB.startJump, GUILayout.Width(50));
            EditorGUILayout.LabelField(string.Format("{0:0.###}", MJB.startJump), GUILayout.Width(40));
            EditorGUILayout.MinMaxSlider(ref MJB.startJump, ref MJB.finishJump, -0.1f, 1.1f);
         //   MJB.finishJump = EditorGUILayout.FloatField(MJB.finishJump, GUILayout.Width(50));
            EditorGUILayout.LabelField(string.Format("{0:0.###}", MJB.finishJump), GUILayout.Width(40));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fall Ray", GUILayout.Width(50));
            MJB.FallRay = EditorGUILayout.FloatField(MJB.FallRay);
            EditorGUILayout.LabelField(new GUIContent("      Treshold", "Max value when is finding a lower jumping down point (greater than that will activate Fall transition  )"), GUILayout.Width(80));
            MJB.treshold = EditorGUILayout.FloatField(MJB.treshold);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.LabelField(new GUIContent("Cliff Momentum (IDInt = 110)", "Range in the animation when the character Can Jump over a Cliff"), b);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(string.Format("{0:0.###}", MJB.startEdge), GUILayout.Width(40));
            EditorGUILayout.MinMaxSlider(ref MJB.startEdge, ref MJB.finishEdge, 0, 1);
            EditorGUILayout.LabelField(string.Format("{0:0.###}", MJB.finishEdge), GUILayout.Width(40));
            EditorGUILayout.EndHorizontal();

            MJB.GroundRay = EditorGUILayout.FloatField("Ground Ray",MJB.GroundRay);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();

        }

      
    }
}