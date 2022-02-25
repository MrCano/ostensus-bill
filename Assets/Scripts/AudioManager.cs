using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OstensusBill
{
    public class AudioManager : MonoBehaviour
    {
        //public static AudioManager instance;

        private static AudioManager instance = null;
        public static AudioManager Instance
        {
            get { return instance; }
        }

        public AudioSource[] sfx;
        public AudioSource[] bgm;
        
        // Start is called before the first frame update
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);

            //instance = this;
            //DontDestroyOnLoad(this.gameObject);

            //PlayVoiceBGMusic();

        }

        // Update is called once per frame
        void LateUpdate()
        {
            
        }

        public void PlaySFX(int soundToPlay)
        {
            if(gameObject != null)
            {
                if (soundToPlay < sfx.Length)
                {
                    sfx[soundToPlay].Play();
                }
            }
            

        }

        private AudioClip clip;
        private AudioSource audioSource;
        public void PlayBGM(int musicToPlay)
        {
            if (gameObject != null)
            {
                if (!bgm[musicToPlay].isPlaying)
                {
                    StopMusic();

                    if (musicToPlay < bgm.Length)
                    {
                        bgm[musicToPlay].Play();
                    }
                }
            }
        }

        public void StopMusic()
        {
            for (int i = 0; i < bgm.Length; i++)
            {
                bgm[i].Stop();
            }
            for (int i = 0; i < sfx.Length; i++)
            {
                sfx[i].Stop();
            }
        }



        //////////////// Audio Sounds /////////////////////

        // SFX

        public void PlayUISFX()
        {
            PlaySFX(0);
        }
        public void PlayJumpSFX()
        {
            PlaySFX(1);
        }
        public void PlayCoinSFX()
        {
            PlaySFX(2);
        }
        public void PlayWinSFX()
        {
            PlaySFX(3);
        }
        public void PlayDeathSFX()
        {
            PlaySFX(4);
        }
        public void PlayDoorOpenSFX()
        {
            PlaySFX(5);
        }
        public void PlayGruntSFX()
        {
            PlaySFX(6);
        }
        public void PlayBounceSFX()
        {
            PlaySFX(7);
        }




        // Music

        public void PlayMenuMusic()
        {
            PlayBGM(0);
        }
        public void PlayGameMusic()
        {
            PlayBGM(1);
        }

    }
}

