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

    private ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();

        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log($"{this.name} I'm collide with {other.name}");
        
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
        vfx.transform.localScale = new Vector3(
            gameObject.transform.localScale.x / 3, 
            gameObject.transform.localScale.y / 3,
            gameObject.transform.localScale.z / 3);
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentTransform;
        vfx.transform.localScale = new Vector3(
            gameObject.transform.localScale.x / 3, 
            gameObject.transform.localScale.y / 3,
            gameObject.transform.localScale.z / 3);

        scoreBoard.IncreaseScore(scorePerHit);
        Destroy(this.gameObject);
    }
}
