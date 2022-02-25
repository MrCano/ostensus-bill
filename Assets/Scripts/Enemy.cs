using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{

    public class Enemy : MonoBehaviour
    {
        public Sprite[] sprites;
        public GameObject playerTarget;
        public GameObject deathEffect;
        public float enemySpeed;
        public float aggroRange;
        public float enemyDamage;
        public float aggroMultiplier;

        private float modifiedSpeed;
        private bool aggro, playedAggroSound;

        // Start is called before the first frame update
        void Start()
        {
            if(playerTarget == null)
            {
                playerTarget = GameObject.FindGameObjectWithTag("Player");
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(playerTarget != null)
            {
                if (!aggro)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, aggroRange);
                    foreach (Collider2D i in hits)
                    {
                        if (i.gameObject == playerTarget)
                        {
                            
                            aggro = true;
                            //cuts off loop when successful
                            break;
                        }
                    }
                }

                if (aggro)
                {
                    modifiedSpeed = enemySpeed;
                    if (Vector3.Dot(playerTarget.transform.position - transform.position, playerTarget.transform.localScale.x * Vector3.right) > 0)
                    {
                        if(!playedAggroSound)
                        {
                            playedAggroSound = true;
                            AudioManager.Instance.PlayGruntSFX();
                        }
                        modifiedSpeed = enemySpeed + aggroMultiplier;
                        GetComponent<SpriteRenderer>().sprite = sprites[1];
                    }
                    else
                    {
                        modifiedSpeed = enemySpeed;
                        GetComponent<SpriteRenderer>().sprite = sprites[0];
                    }


                    transform.position = Vector3.MoveTowards(transform.position, playerTarget.transform.position, modifiedSpeed * Time.deltaTime);
                }
            }
            
        }

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

        public void EnemyDeath()
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject, .1f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            Gizmos.DrawWireSphere(transform.position, aggroRange);
        }
    }
}
