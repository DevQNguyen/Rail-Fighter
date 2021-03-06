using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movementAction;
    [SerializeField] InputAction firingAction;

    [SerializeField] GameObject[] lasers;
    
    [Header("General Movement Settings")]
    [Tooltip("How fast player ship moves up and down.")]
    [SerializeField] float controlSpeed = 20f;
    [Tooltip("Screen movement limit on x-axis.")]
    [SerializeField] float xRange = 20f;
    [Tooltip("Screen movement limit on y-axis.")]
    [SerializeField] float yRange = 10f;

    [Header("Screen Position Tuning")]
    [Tooltip("Pitch factor applied to transform's local y location.")]
    [SerializeField] float positionPitchFactor = -3f;
    [Tooltip("Yaw factor applied to transform's local x location.")]
    [SerializeField] float positionYawFactor = 2f;
    [Tooltip("Roll factor applied to transform's local x location.")]
    [SerializeField] float positionRollFactor = -2f;
    
    [Header("Player Input Tuning")]
    [Tooltip("Pitch factor applied y-throw input.")]
    [SerializeField] float pitchControlFactor = -10f;
    [Tooltip("Roll factor applied to x-throw input.")]
    [SerializeField] float rollControlFactor = -15f;

    SoundEffectsPlayer SFXPlayer;

    float xThrow, yThrow;

    private void Awake()
    {
        SFXPlayer = FindObjectOfType<SoundEffectsPlayer>();
        if (SFXPlayer == null)
        {
            Debug.Log("SoundEffectsPlayer object not found.");
        }
    }

    void OnEnable()
    {
        // Enable movement Input System
        movementAction.Enable();
        // Enable firing Input System
        firingAction.Enable();
        
    }

    void OnDisable()
    {
        // Disable movement Input System
        movementAction.Disable();
        // Disable firing Input System
        firingAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        // New Input System
        xThrow = movementAction.ReadValue<Vector2>().x;
        //Debug.Log($"Horizontal: {xThrow}");

        yThrow = movementAction.ReadValue<Vector2>().y;
        //Debug.Log($"Vertical: {yThrow}");

        // Old Input System
        //float horizontalThrow = Input.GetAxis("Horizontal");
        //float verticalThrow = Input.GetAxis("Vertical");

        // Assign reference to controller inputs 
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        // Apply offset to current transform position
        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        // Apply clamp to the raw positions
        float xClamped = Mathf.Clamp(rawXPos, -xRange, xRange);
        float yClamped = Mathf.Clamp(rawYPos, -yRange, yRange);
        //Debug.Log($"xClamped: {xClamped} - yClamped: {yClamped}");

        // Apply new position to transform
        transform.localPosition = new Vector3(xClamped, yClamped, transform.localPosition.z);
        //Debug.Log($"LocalPosition: {transform.localPosition}");
    }

    void ProcessRotation()
    {
        float pitchDueToLocation = transform.localPosition.y * positionPitchFactor;
        float rollDueToLocation = transform.localPosition.x * positionRollFactor;

        float pitchDueToControl = yThrow * pitchControlFactor;
        float rollDueToControl = xThrow * rollControlFactor;

        float pitch = pitchDueToLocation + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = rollDueToLocation + rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }


    void ProcessFiring()
    {
        // If firing input true
        if (firingAction.triggered)
        {
            SFXPlayer.PlayLaserClip();
            // Grab all laser items
            foreach (GameObject laser in lasers)
            {
                // Reference particle system
                laser.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}


