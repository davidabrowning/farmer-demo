using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ItemBuilders
{
    public abstract class ItemBuilderBase<T> : MonoBehaviourSingleton<T> where T : MonoBehaviourSingleton<T>
    {
        public GameObject Prefab;
        public Transform ParentObject;
    }
}
