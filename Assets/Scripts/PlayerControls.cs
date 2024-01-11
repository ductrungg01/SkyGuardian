using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [FormerlySerializedAs("speedControl")] [SerializeField] private float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally")] [SerializeField] private float xRange = 10f;
    [Tooltip("How far player moves vertically")] [SerializeField] private float yRange = 7f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] private GameObject[] lasers;
    private List<ParticleSystem.EmissionModule> emissions = new List<ParticleSystem.EmissionModule>();
    
    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    
    [Header("Player input based tuning")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    private float xThrow, yThrow;

    private void Start()
    {
        foreach (GameObject laser in lasers)
        {
            emissions.Add(laser.GetComponent<ParticleSystem>().emission);
        }
        
        SetLasersActive(false);
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        for (int i = 0; i < emissions.Count; i++)
        {
            ParticleSystem.EmissionModule emissionModule = emissions[i];
            emissionModule.enabled = isActive;
        }
    }
}
