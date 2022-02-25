using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D bulletRb;
    public GameObject hitEffect;
    public float dmg;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        //Destroy(gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            //AudioManager.instance.PlaySuccessfulHitSound();

        }
        else
        {
            //AudioManager.instance.PlayHitSound();
        }
        //if (collision.gameObject.tag == "Player")
        //{
        //    AudioManager.instance.PlayHurtSound();
        //    collision.gameObject.GetComponent<PlayerController>().health -= dmg;
        //}


    }
}
