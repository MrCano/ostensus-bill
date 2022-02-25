using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{
    public class Shoot : MonoBehaviour
    {
        public Transform shootPoint;
        public GameObject bulletPrefab;

        public float bulletForce;
        public float ammoCount;

        public float originalAmmoCount;

        // Start is called before the first frame update
        void Start()
        {
            originalAmmoCount = ammoCount;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //TODO: No Shooting in game?
                //ShootBullet();
            }
        }

        void ShootBullet()
        {
            if (PlayerController.instance.gameObject != null)
            {
                if (ammoCount > 0)
                {
                    ammoCount -= 1f;
                    //AudioManager.instance.PlayShootSound();
                    GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if(!PlayerController.instance.isFacingLeft)
                    {
                        rb.AddForce(shootPoint.right * bulletForce, ForceMode2D.Impulse);
                    } else
                    {
                        rb.AddForce(-shootPoint.right * bulletForce, ForceMode2D.Impulse);
                    }
                    
                    Destroy(bullet, 3.25f);
                } else if(ammoCount < 0)
                {
                    Debug.Log("NEGATIVE BULLETS?");
                } else
                {
                    Debug.Log("You have no bullets left");
                }
            }
        }
    }
}

