using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace FarmerDemo
{
    public class GameManagerScript : MonoBehaviourSingletonBase<GameManagerScript>
    {
        public GameObject Player;
        public GameObject UIController;
        private void Start()
        {
            WorldBuilderScript.Instance.BuildInitialWorld();
        }
    }
}
