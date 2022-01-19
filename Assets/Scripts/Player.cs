using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneEnv;

namespace Characters
{
    public class Player : Character
    {
        [SerializeField] float swipeForce = 15f;

        int numStairs = 0;
        bool jumped = false;
        bool moving = false;
        States currentState = States.Idle;


        public int NumOvercomedStairs
        {
            get { return numStairs; }
        }

        public States State
        {
            get { return currentState; }
            set
            {
                if (currentState == States.Idle)
                    currentState = value;
            }
        }


        protected override void Start()
        {
            base.Start();
            Main.self.Player = this;
        }

        protected override void Thrust(Vector3 direction, float jumpForce)
        {
            moving = true;
            base.Thrust(direction, jumpForce);
        }

        void FixedUpdate()
        {
            DoAction(currentState);
        }

        void DoAction(States state)
        {
            if (moving)
                return;

            if (state == States.Jump)
                StartCoroutine(Jump());

            if (state == States.Left)
                SwipeLeft();

            if (state == States.Right)
                SwipeRight();
        }


        void SwipeLeft()
        {
            StartCoroutine(Swipe(Vector3.back));
        }

        void SwipeRight()
        {
            StartCoroutine(Swipe(Vector3.forward));
        }


        IEnumerator Swipe(Vector3 destination)
        {
            Thrust(Vector3.up, jumpForce / 2f); 
            yield return new WaitForSeconds(0.00f);
            Thrust(destination, swipeForce);
            StartCoroutine(SpeedUpJump());
        }

        IEnumerator Jump()
        {
            jumped = true;
            rb.velocity = Vector3.zero;
            Thrust(Vector3.up, jumpForce);
            yield return new WaitForSeconds(0.1f); 
            Thrust(Vector3.left, jumpForce);
            StartCoroutine(SpeedUpJump());
        }

        IEnumerator SpeedUpJump()
        {
            yield return new WaitForSeconds(0.1f);
            Thrust(Vector3.down, jumpForce / 2.0f);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Stairs") && jumped)
            {
                numStairs++;
                jumped = false;
            }

            if (collision.gameObject.CompareTag("Stairs"))
            {
                moving = false;
                ResetState();
            }
            if (collision.gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FallTrigger"))
                gameObject.SetActive(false);
        }

        protected override void ResetState()
        {
            base.ResetState();
            currentState = States.Idle;
        }
    }
}