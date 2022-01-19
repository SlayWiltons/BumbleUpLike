using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneEnv
{
    public class StairsDestroy : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Destroy(transform.parent.gameObject);
        }
    }
}
