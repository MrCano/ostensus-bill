using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OstensusBill
{
    public class MainMenu : MonoBehaviour
    {
        public string newGameScene;

        // Start is called before the first frame update
        void Start()
        {
            AudioManager.Instance.PlayMenuMusic();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NewGame()
        {
            SceneManager.LoadScene(newGameScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ReturnToMainMenu()
        {
            //AudioManager.Instance.StopMusic();
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

