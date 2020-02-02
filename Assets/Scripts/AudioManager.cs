using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int levelMusicToplay;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayMusic(levelMusicToplay);
    }

    void Update()
    {
        PlaySFX(4);
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
}
