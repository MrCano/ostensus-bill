using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace OstensusBill
{
    public class GameMenu : MonoBehaviour
    {
        public static GameMenu instance;

        public GameObject menu;
        public GameObject victoryScreen;
        public Text livesText;
        public Text timeLabel;
        public Text timeText;
        public Text coinsText;
        public Text bulletsText;
        public float coinsAliveUI;

        public float timeCountDown;
        //private float timeCountUp;
        private float originalTimeCountDown;

        private bool menuPressed;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            //timeCountUp = 0f;
            originalTimeCountDown = timeCountDown;
        }

        // Update is called once per frame
        void Update()
        {
            menuPressed = Input.GetButtonDown("Fire2") || Input.GetButtonDown("SubmitEnter");

            timeCountDown -= Time.deltaTime;
            //timeCountUp += Time.deltaTime;

            if (PlayerController.instance != null)
            {
                UpdateUIStats();
            }
            if(timeCountDown <= 0)
            {
                timeCountDown = 0f;
                PlayerController.instance.isDead = true;
            }

            if (menuPressed && !victoryScreen.activeInHierarchy && FindObjectOfType<GameManager>().canOpenMenu)
            {
                if (!menu.activeInHierarchy)
                {
                    Time.timeScale = 0;
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                    Time.timeScale = 1;
                }
                //AudioManager.instance.PlayUISound();
            }

        }

        protected virtual void UpdateUIStats()
        {
            var timeTextColor = timeText.GetComponent<Text>();
            var timeLabelColor = timeLabel.GetComponent<Text>();
            var timePercent = timeCountDown / originalTimeCountDown;
            var str = SceneManager.GetActiveScene().name;
            str = str.Replace("Level", string.Empty);
            str = str.Replace("a", string.Empty);
            livesText.text = "Level = " + str; //PlayerController.instance.currentLives.ToString();
            timeText.text = timeCountDown.ToString("f1"); // Mathf.Ceil(timeCount).ToString();
            if(timePercent <= .33f)// && timePercent > .25f)
            {
                timeTextColor.color = Color.red;
                timeLabelColor.color = Color.red;
            } 
            //else if(timePercent <= .25f)
            //{
            //    timeSprite.color = Color.red;
            //}
            coinsText.text = "Coins Left = " + coinsAliveUI;
            if (coinsAliveUI > 0f)
            {
                coinsText.color = Color.red;
            } else
            {
                coinsText.color = Color.green;
            }
            bulletsText.text = "= " + PlayerController.instance.GetComponent<Shoot>().ammoCount.ToString();
        }

        public void SaveGame()
        {

        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ReturnToMainMenu()
        {
            AudioManager.Instance.StopMusic();
            //Destroy(AudioManager.instance.gameObject);
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
        }

        public void RestartLevel()
        {

            PlayerController.instance.isDead = true;
            menu.SetActive(false);
            victoryScreen.SetActive(false);
            Time.timeScale = 1;
            //var lvl = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene(lvl);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RegisterCoin()
        {
            coinsAliveUI++;
        }
        public void CoinPickedUp()
        {
            coinsAliveUI--;
        }

    }
}
