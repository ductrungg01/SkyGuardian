using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private int scorePerHit = 15;
    [SerializeField] private int hitPoints = 1;
    private void Start()
    {
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints--;
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentTransform;
        vfx.transform.localScale = gameObject.transform.localScale;
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentTransform;
        vfx.transform.localScale = gameObject.transform.localScale;

        ScoreBoard.Instance.IncreaseScore(scorePerHit);
        Destroy(this.gameObject);
    }
}
