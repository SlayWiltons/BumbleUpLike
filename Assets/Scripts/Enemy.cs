using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Enemy : Character
    {
        //jumpForce = 17f
        [SerializeField] float thrust = 5.08f;
        [SerializeField] float speed = 1.2f;

        protected override void Start()
        {
            base.Start();
            deltaForcesTime = 1 / thrust;
        }

        IEnumerator Jump()
        {
            rb.velocity = Vector3.zero;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(deltaForcesTime);
            Thrust(Vector3.right, thrust);
        }

        void OnCollisionEnter(Collision collision)
        {
            //����� ����������� �� �������� ����� ������
            if (collision.gameObject.CompareTag("Stairs"))
            {
                StopCoroutine(Jump());
                StartCoroutine(Jump());
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FallEnemyTrigger"))
            {
                Destroy(gameObject);
            }
        }
    }
}
