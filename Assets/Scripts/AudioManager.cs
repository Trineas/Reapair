using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int levelMusicToplay;

    public bool levelOneActive;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (levelOneActive)
        {
            StartCoroutine(loopLevelOneMusic());
        }

        else
        {
            PlayMusic(levelMusicToplay);
        }

    }

    public void PlayMusic(int musicToPlay)
    {
        for(int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }

        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    public IEnumerator loopLevelOneMusic()
    {
        music[1].Play();

        yield return new WaitForSeconds(87.5f);

        music[2].Play();
    }
}
