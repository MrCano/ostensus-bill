using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OstensusBill
{
    public class LoadingScene : MonoBehaviour
    {
        public float waitToLoad;
        public string firstScene;


        // Start is called before the first frame update
        void Start()
        {
            //AudioManager.instance.StopMusic();
        }

        // Update is called once per frame
        void Update()
        {
            if (waitToLoad > 0)
            {
                waitToLoad -= Time.deltaTime;
                if (waitToLoad <= 0)
                {
                    SceneManager.LoadScene(firstScene);
                }
            }
        }
    }
}