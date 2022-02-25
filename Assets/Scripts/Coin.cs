using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{

    public class Coin : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            var theMenu = GameObject.FindWithTag("Menu").GetComponent<GameMenu>();
            var theExit = GameObject.FindWithTag("Exit").GetComponent<Exit>();
            theExit.RegisterCoin();
            theMenu.RegisterCoin();
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                AudioManager.Instance.PlayCoinSFX();
                PlayerController.instance.currentCoins += 1f;
                var theExit = GameObject.FindWithTag("Exit").GetComponent<Exit>();
                var theMenu = GameObject.FindWithTag("Menu").GetComponent<GameMenu>();
                theMenu.CoinPickedUp();
                theExit.CoinPickedUp();
                Destroy(gameObject);
            }
        }

    }

}
