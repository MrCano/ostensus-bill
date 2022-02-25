using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemyPrefab;


        public float timeToSpawn;
        private float originalTimeToSpawn;
        private bool isSpawning;

        // Start is called before the first frame update
        void Start()
        {
            isSpawning = false;
            originalTimeToSpawn = timeToSpawn;
        }

        // Update is called once per frame
        void Update()
        {
            if(timeToSpawn > 0)
            {
                timeToSpawn -= Time.deltaTime;
            } else
            {
                isSpawning = true;
                timeToSpawn = originalTimeToSpawn;
            }
            SpawnEnemy();
        }


        void SpawnEnemy()
        {
            if(isSpawning)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                //Destroy(bullet, 5f);
                isSpawning = false;
            }

        }
    }
}
