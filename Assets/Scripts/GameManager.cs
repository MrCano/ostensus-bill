using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace OstensusBill
{
    public class GameManager : MonoBehaviour
    {
        private Vector3 deathCamPosition;
        private PlayerController player;

        public bool canOpenMenu = true;

        // Start is called before the first frame update
        void Start()
        {
            var currentLevel = SceneManager.GetActiveScene();
            player = PlayerController.instance;
            AudioManager.Instance.PlayGameMusic();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                HealthCheck();
                //WinCheck();
            }
                
        }

        private void HealthCheck()
        {
            if(!player.isDead)
            {
                if (player.currentLives > player.maxLives)
                {
                    player.currentLives = player.maxLives;
                }
                if (player.currentLives <= 0)
                {
                    player.currentLives = 0;
                    player.isDead = true;
                }
                
            } else
            {
                if(player.gameObject != null)
                {
                    GameObject effect = Instantiate(player.deathEffect, player.transform.position, Quaternion.identity);
                    Destroy(effect, 1.5f);
                }
                StartCoroutine(PlayerDeath());
            }
            
        }

        protected virtual IEnumerator PlayerDeath()
        {
            if (player.gameObject != null && player.isDead)
            {
                canOpenMenu = false;
                AudioManager.Instance.PlayDeathSFX();
                var cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
                deathCamPosition = cam.position;
                Destroy(player.gameObject, .01f);
                var vCam = GameObject.FindWithTag("VCam").GetComponent<Transform>();
                vCam.position = new Vector3(deathCamPosition.x, deathCamPosition.y, deathCamPosition.z);
                //StopMusic();
                yield return new WaitForSeconds(.01f);
                //PlayGameOverSound();
                yield return new WaitForSeconds(2f);
                player.isDead = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }
}
