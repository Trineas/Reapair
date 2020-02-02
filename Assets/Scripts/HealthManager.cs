using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public static int currentHealth, maxHealth, lives;

    public float invincibleLength = 2f;
    private float invincCounter;

    public GameObject healEffect;
    public GameObject hitEffect;

    public int soundToPlay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = 1;
        lives = 3;
    }

    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (invincCounter <= 0)
        {
            AudioManager.instance.PlaySFX(soundToPlay);
            PlayerController.bones--;
            PlayerController.bones--;
            PlayerController.bones--;

            Instantiate(hitEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

            if (PlayerController.bones <= 0)
            {
                lives--;
                GameManager.instance.Respawn();
            }

            if (PlayerController.bones <= 0 && lives < 1)
            {
                GameManager.instance.GameOver();
            }

            else
            {
                PlayerController.instance.Knockback();
                invincCounter = invincibleLength;
            }
        }
    }

    public void AddHealth(int amountToHeal)
    {
        Instantiate(healEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
    }
}
