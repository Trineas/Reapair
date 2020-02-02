using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;
    public float fadeSpeed = 1f;
    public bool fadeToBlack, fadeFromBlack;

    public Text boneCounter, livesCounter;

    public GameObject pauseScreen;

    public string reloadLevel, mainMenu;

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

        boneCounter.text = "Bones x" + PlayerController.bones;
        livesCounter.text = "Lives x" + HealthManager.lives;
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
}
