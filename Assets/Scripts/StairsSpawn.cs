using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneEnv
{
    public class StairsSpawn : MonoBehaviour
    {
        [SerializeField] GameObject stairs;
        int numNotStairsObjectInStairway = 0;
        Vector3 newStairwayPosition;

        void Start()
        {
            try
            {
                stairs.GetComponent<GameObject>();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
                return;
            }

            newStairwayPosition = transform.parent.GetChild(0).transform.position;
            newStairwayPosition.x -= transform.parent.childCount - 14f;
            newStairwayPosition.y += transform.parent.childCount - 16.7f;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Instantiate(stairs, newStairwayPosition, transform.rotation);
        }
    }
}
