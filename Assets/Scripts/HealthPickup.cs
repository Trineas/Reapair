using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public float respawnTimer = 0f;

    public MeshRenderer meshRenderer;
    public CapsuleCollider capsuleCollider;

    void Update()
    {
        respawnTimer += Time.deltaTime;

        if (respawnTimer >= 120f)
        {
            meshRenderer.enabled = true;
            capsuleCollider.enabled = true;
            respawnTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            meshRenderer.enabled = false;
            capsuleCollider.enabled = false;
            respawnTimer = 0f;

            PlayerController.bones++;
            HealthManager.instance.AddHealth(healAmount);
        }
    }
}
