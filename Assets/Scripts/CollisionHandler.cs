using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delayTimeAfterCrash = 1f;
    [SerializeField] private ParticleSystem crashVFX;
    [SerializeField] private AudioSource crashSFX;
    
    private bool _isCrashed = false;

    private void Start()
    {
        this._isCrashed = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        StartCrashSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        if (_isCrashed) return;

        this._isCrashed = true;
        crashVFX.Play();
        crashSFX.Play();
        GameController.Instance.StartGameOver(delayTimeAfterCrash);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().SetLasersActive(false);
        GetComponent<PlayerControls>().enabled = false;
        
        Invoke(nameof(ReloadLevel), delayTimeAfterCrash);
    }

    void ReloadLevel()
    {
        GameController.Instance.ReloadLevel();
    }
}
