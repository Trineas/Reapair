using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CinemachineBrain cmBrain;
    public CinemachineFreeLook freeLook;

    public GameObject camTarget1, camTarget2, camTarget3;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        freeLook.LookAt = camTarget1.transform;
    }

    void Update()
    {
        if (ModelSwitcher.modelSwitch == 1)
        {
            freeLook.LookAt = camTarget1.transform;
        }

        if (ModelSwitcher.modelSwitch == 2)
        {
            freeLook.LookAt = camTarget2.transform;
        }

        if (ModelSwitcher.modelSwitch == 3)
        {
            freeLook.LookAt = camTarget3.transform;
        }
    }
}
