using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OstensusBill
{
    public class Exit : MonoBehaviour
    {
        public SceneReference nextScene;

        public Sprite[] sprites;

        private float coinsAlive = 0f;

        private void Start()
        {
            
        }

        private void Update()
        {
            if(coinsAlive > 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && coinsAlive == 0)
            {
                AudioManager.Instance.PlayWinSFX();
                Time.timeScale = 0;
                GameMenu.instance.victoryScreen.SetActive(true);
            }
        }

        public void FollowingScene(string scene)
        {
            
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
        }

        public void RegisterCoin()
        {
            coinsAlive++;
        }
        public void CoinPickedUp()
        {
            coinsAlive--;
            if(coinsAlive == 0)
            {
                AudioManager.Instance.PlayDoorOpenSFX();
            }
        }
    }

}
