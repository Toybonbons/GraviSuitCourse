using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMain : MonoBehaviour
{
    private InputService inputService;

    [Header("Player Models")]
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject cameraHolder, cameraVerti, camFlipper;
    [SerializeField] Rigidbody rb;

    [Header("Camera")]
    [SerializeField] Camera playerCam;
    public float targetFov, fovMulti, fovLerp;

    [Header("Config Vals")]
    [SerializeField] float mouseSens = 1f;
    [SerializeField] float baseFov = 90;

    private float vertiRot = 0, horiRot = 0, camHoriRot = 0;
    public float moveSpeed = 16f;

    //Gravity Values
    [Header("Gravity")]
    public Vector3 gravityDir;
    public float gravityStrength, maxGraviLerps;
    private float graviLerps = 61;
    public float graviDelay = 1f, currentGraviDelay = 0f;

    //Jumping
    [Header("Movement Values")]
    public float currentJumps = 2, jumpStrength = 5;
    private float maxJumps = 2;

    //Dashing
    public float currentDashes = 1;
    private float maxDashes = 1, dashStrength = 30;

    //Ground
    public bool grounded = false;
    private float groundDist = 1.1f;

    //Deltatime
    private float deltaTimeVal, fixedDeltaTimeVal;

    //Instancing
    public static PlayerMain instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        inputService = InputService.Instance;

        Cursor.lockState = CursorLockMode.Locked;

        //gravityDir = Vector3.down;
    }

    //Physics Stuff here
    void FixedUpdate()
    {
        fixedDeltaTimeVal = Time.fixedDeltaTime * 100f;

        updMovement();

        applyGravity();
        groundCheck();

        if (inputService.jumpVal) jump();
        if (inputService.dashVal) dash();

        targetFov = baseFov + (rb.linearVelocity.magnitude / fovMulti);
        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFov, fovLerp);
    }

    void Update()
    {
        deltaTimeVal = Time.deltaTime * 100f;

        updCamera();
    }
    

    //Movement Stuff
    void updMovement()
    {
        Vector2 moveDirection = inputService.moveVal;

        Vector2 moveForce = moveDirection * new Vector2(moveSpeed, moveSpeed);
    
        Vector3 finalMoveForce = new Vector3(moveForce.x, 0, moveForce.y);
        finalMoveForce = cameraHolder.transform.TransformDirection(finalMoveForce);

        rb.AddForce(finalMoveForce * fixedDeltaTimeVal, ForceMode.VelocityChange);

        //Counter Movement
        Vector3 counterForce = playerModel.transform.TransformDirection(rb.linearVelocity);
        counterForce = new Vector3(counterForce.x, 0, counterForce.z);
        counterForce = playerModel.transform.InverseTransformDirection(counterForce);

        rb.AddForce(counterForce * -0.5f * fixedDeltaTimeVal, ForceMode.VelocityChange);
    }

    void jump()
    {
        inputService.jumpVal = false;

        if (currentJumps > 0)
        {
            rb.linearVelocity = Vector3.zero;
            Debug.Log("JUMP!");
            StartCoroutine(jumpPhysics());
            currentJumps -= 1;
        }
    }

    IEnumerator jumpPhysics()
    {
        float time = 0.1f;

        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            rb.AddForce(-gravityDir * jumpStrength * fixedDeltaTimeVal * time, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }

    //Dash
    void dash()
    {
        inputService.dashVal = false;

        if (currentDashes <= 0 || grounded) {Debug.Log("Can't Dash"); return;}

        Debug.Log("DASHING!");

        Vector3 dashForce = cameraHolder.transform.forward;
        StartCoroutine(dashPhysics(dashForce));

        currentDashes -= 1;
    }

    IEnumerator dashPhysics(Vector3 dashForce)
    {
        float time = 1;

        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            rb.AddForce(dashForce * dashStrength * time, ForceMode.VelocityChange);

            yield return new WaitForFixedUpdate();
        }
    }

    //Ground
    void groundCheck()
    {
        //The Check (TM)
        grounded = Physics.Raycast(playerModel.transform.position, gravityDir, groundDist);


        //Results of The Check (TM)
        if (grounded)
        {
            currentJumps = maxJumps;
            currentDashes = maxDashes;
        }
    }

    //Gravity
    void applyGravity()
    {
        rb.AddForce(gravityDir * gravityStrength * fixedDeltaTimeVal * (currentGraviDelay / graviDelay), ForceMode.Acceleration);

        if (graviLerps <= maxGraviLerps)
        {
            camFlipper.transform.rotation = Quaternion.Lerp(camFlipper.transform.rotation, Quaternion.LookRotation(camFlipper.transform.forward, -gravityDir), graviLerps / maxGraviLerps);
            graviLerps += 1;
        }

        playerModel.transform.rotation = Quaternion.LookRotation(playerModel.transform.forward, -gravityDir);
        currentGraviDelay = Mathf.Clamp(currentGraviDelay + Time.fixedDeltaTime, 0, graviDelay);
    }

    public void changeGravity(Vector3 newGrav)
    {
        if (gravityDir == newGrav) return;
        

        gravityDir = newGrav;
        graviLerps = 0;
        currentGraviDelay = 0f;

        rb.AddForce(-rb.linearVelocity, ForceMode.VelocityChange);
    }

    //Camera Stuff

    /*
        HERES WHATS WRONG WITH UPSIDE DOWN GRAVITY

        IT WORKS FINE WHEN HORIROT = 0
        HOWEVER ANY OTHER VALUE CAUSES IT TO BREAK >:(
        WE NEED TO SOMEHOW FIX THIS FOR OTHER HORIROT VALUES

        WITH TESTING IVE FOUND ITS OKAY WITH WALL RUNS WITH NON ZERO HORIROTS
    */

    void updCamera()
    {
        Vector2 delta = inputService.cameraVal;

        vertiRot -= delta.y * mouseSens * deltaTimeVal;
        horiRot += delta.x * mouseSens * deltaTimeVal;
        camHoriRot += delta.x * mouseSens * deltaTimeVal;

        vertiRot = math.clamp(vertiRot, -85f, 85f);
        horiRot = horiRot % 360f;
        camHoriRot = camHoriRot % 360;

        //Set Rotations
        cameraVerti.transform.localRotation = Quaternion.Euler(vertiRot,camHoriRot,0);
        cameraHolder.transform.localRotation = Quaternion.Euler(0, horiRot,0);

        camFlipper.transform.position = cameraHolder.transform.position + playerModel.transform.TransformDirection(new Vector3(0, 0.5f, 0));
    }

}
