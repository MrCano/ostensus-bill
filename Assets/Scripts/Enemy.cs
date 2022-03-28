using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{

    public class Enemy : MonoBehaviour
    {
        #region Variables

        public Sprite[] sprites;

        public GameObject playerTarget;
        public GameObject deathEffect;

        public float enemySpeed;
        public float enemyDamage;

        public float aggroRange;
        public float aggroMultiplier;
        private float modifiedSpeed;

        private bool aggro;
        private bool playedAggroSound;

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            if(playerTarget == null)
            {
                playerTarget = GameObject.FindGameObjectWithTag("Player");
            }
        }

        //Fixed Update for handling physics
        void FixedUpdate()
        {
            CalculateEnemyMovement();
        }

        #region EnemyMovement

        private void CalculateEnemyMovement()
        {
            if (playerTarget != null)
            {
                if (!aggro)
                {
                    //Create a circle around the Enemy and record any collisions in an array
                    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, aggroRange);
                    foreach (Collider2D i in hits)
                    {
                        //If a collision is with the player, the enemy is aggro'd. Break to stop checking
                        if (i.gameObject == playerTarget)
                        {
                            aggro = true;
                            break;
                        }
                    }
                }

                if (aggro)
                {
                    //Once the enemy is aggro'd, modifiedSpeed is set to 0  
                    modifiedSpeed = enemySpeed;

                    //Here we check whether the player and enemy are looking the same direction (ie. not looking at each other)
                    if (Vector3.Dot(playerTarget.transform.position - transform.position, playerTarget.transform.localScale.x * Vector3.right) > 0)
                    {
                        //Only play grunt the first time an enemy is revealed
                        if (!playedAggroSound)
                        {
                            playedAggroSound = true;
                            AudioManager.Instance.PlayGruntSFX();
                        }

                        //float gets added to modifiedSpeed when the player is not looking. You may want to use multiplication if the enemy's speed is not initially set to 0
                        modifiedSpeed = enemySpeed + aggroMultiplier;

                        //Change the enemy's sprite to the enemy
                        GetComponent<SpriteRenderer>().sprite = sprites[1];
                    }
                    else
                    {
                        //if they are looking at each other, enemy's speed back to 0 and sprite becomese the coin
                        modifiedSpeed = enemySpeed;
                        GetComponent<SpriteRenderer>().sprite = sprites[0];
                    }

                    //Move the enemy towards the player using modifiedSpeed
                    transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, modifiedSpeed * Time.deltaTime);
                }
            }
        }

        #endregion


        #region Collision
        
        //What happens when anything collides with the enemy
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Bullet")
            {
                EnemyDeath();
            }
            if(collision.tag == "Player")
            {
                PlayerController.instance.currentLives -= enemyDamage;
                EnemyDeath();
            }
        }

        //Instantiate the death effect then destroy both the effect and enemy after a period of time
        public void EnemyDeath()
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject, .1f);
        }

        #endregion


        #region UnityEditorGizmos

        //Draw lines on the Unity Editor screen for development
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }

        #endregion

    }
}
