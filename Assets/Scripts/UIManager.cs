using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;
    public float fadeSpeed = 1f;
    public bool fadeToBlack, fadeFromBlack;

    public Image bone1, bone2, bone3, bone4, bone5, bone6, bone7, bone8, bone9, bone10, bone11, lives0, lives1, lives2, lives3;

    public GameObject pauseScreen;

    public string reloadLevel, mainMenu;


    public GameObject winVideo;
    public Image loseScreen;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }

        if (PlayerController.bones <= 1)
        {
            bone1.enabled = true;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 2)
        {
            bone1.enabled = false;
            bone2.enabled = true;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 3)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = true;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 4)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = true;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 5)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = true;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 6)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = true;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 7)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = true;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 8)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = true;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 9)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = true;
            bone10.enabled = false;
            bone11.enabled = false;
        }

        if (PlayerController.bones == 10)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = true;
            bone11.enabled = false;
        }

        if (PlayerController.bones >= 11)
        {
            bone1.enabled = false;
            bone2.enabled = false;
            bone3.enabled = false;
            bone4.enabled = false;
            bone5.enabled = false;
            bone6.enabled = false;
            bone7.enabled = false;
            bone8.enabled = false;
            bone9.enabled = false;
            bone10.enabled = false;
            bone11.enabled = true;
        }

        if (HealthManager.lives <= 0)
        {
            lives0.enabled = true;
            lives1.enabled = false;
            lives2.enabled = false;
            lives3.enabled = false;
        }

        if (HealthManager.lives == 1)
        {
            lives0.enabled = false;
            lives1.enabled = true;
            lives2.enabled = false;
            lives3.enabled = false;
        }

        if (HealthManager.lives == 2)
        {
            lives0.enabled = false;
            lives1.enabled = false;
            lives2.enabled = true;
            lives3.enabled = false;
        }

        if (HealthManager.lives >= 3)
        {
            lives0.enabled = false;
            lives1.enabled = false;
            lives2.enabled = false;
            lives3.enabled = true;
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void Restart()
    {
        SceneManager.LoadScene(reloadLevel);
        Time.timeScale = 1f;
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void WinScreen()
    {   
        Time.timeScale = 0f;
        winVideo.gameObject.SetActive(true);
        AudioManager.instance.sfx[1].Stop();
        AudioManager.instance.sfx[2].Stop();
        AudioManager.instance.sfx[3].Stop();
        AudioManager.instance.sfx[4].Stop();
    }

    public void GameOverScreen()
    {
        loseScreen.gameObject.SetActive(true);
    }
}
