using UnityEngine;
using System.Collections;



namespace MalbersAnimations
{
    [CreateAssetMenu(menuName = "MalbersAnimations/ActionsEmotions")]
    public class Actions : ScriptableObject
    {
        [SerializeField]
        public ActionsEmotions[] actions;
    }
}