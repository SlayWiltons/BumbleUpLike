using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] protected float coefForce = 500f;
        [SerializeField] protected float jumpForce = 100f;

        protected float deltaForcesTime;
        protected Rigidbody rb;
        protected Vector3 startPosition;


        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position;
        }

        protected virtual void ResetState()
        {
            rb.velocity = Vector3.zero;
            StopAllCoroutines();
        }

        protected virtual void Thrust(Vector3 direction, float jumpForce)
        {
            rb.AddForce(direction * jumpForce * coefForce * Time.fixedDeltaTime);
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
