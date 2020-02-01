using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject model1, model2, model3;
    public static int modelSwitch;

    void Start()
    {
        modelSwitch = 1;
    }

    void Update()
    {
        if (!PlayerController.armsEnabled && !PlayerController.everythingEnabled)
        {
            modelSwitch = 1;
        }

        if (PlayerController.armsEnabled)
        {
            modelSwitch = 2;
        }

        if (PlayerController.everythingEnabled)
        {
            modelSwitch = 3;
        }

        if (modelSwitch == 1)
        {
            model1.SetActive(true);
            model2.SetActive(false);
            model3.SetActive(false);
            //GameObject.Find("Player").GetComponent<PlayerController>().anim = Resources.Load<Animator>("Skull");
        }

        if (modelSwitch == 2)
        {
            model1.SetActive(false);
            model2.SetActive(true);
            model3.SetActive(false);
            //GameObject.Find("Player").GetComponent<PlayerController>().anim = Resources.Load<Animator>("Torso");
        }

        if (modelSwitch == 3)
        {
            model1.SetActive(false);
            model2.SetActive(false);
            model3.SetActive(true);
            //GameObject.Find("Player").GetComponent<PlayerController>().anim = Resources.Load<Animator>("Skeleton");
        }
    }
}