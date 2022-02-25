using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{

    public class EnemyType2 : MonoBehaviour
    {
        public enum MovementType { Normal }
        public MovementType type;

        public bool facingLeft;
        public Rigidbody2D enemyT2RB;
        public Collider2D enemyT2Col;

        public float timeTIllMaxSpeed;
        public float maxSpeed;

        private float accel, dir, runTime;

        private float currentSpeed;

        // Start is called before the first frame update
        void Start()
        {
            enemyT2RB = GetComponent<Rigidbody2D>();
            enemyT2Col = GetComponent<Collider2D>();


        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Movement();
        }

        protected virtual void Movement()
        {
            if(!facingLeft)
            {
                dir = 1;
            }else
            {
                dir = -1;
            }
            accel = maxSpeed / timeTIllMaxSpeed;
            runTime += Time.deltaTime;
            currentSpeed = dir * accel * runTime;
            CheckSpeed();
            enemyT2RB.velocity = new Vector2(currentSpeed, enemyT2RB.velocity.y);
        }

        protected virtual void CheckSpeed()
        {
            if(currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
            if(currentSpeed < -maxSpeed)
            {
                currentSpeed = -maxSpeed;
            }
        }
    }
}
