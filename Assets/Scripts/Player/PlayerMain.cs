using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMain : MonoBehaviour
{
    private InputService inputService;

    [Header("Player Models")]
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject cameraHolder, cameraVerti;
    [SerializeField] Rigidbody rb;

    [Header("Config Vals")]
    [SerializeField] float mouseSens = 1f;

    private float vertiRot = 0, horiRot = 0;
    public float moveSpeed = 16f, angleOffset;

    //Gravity Values
    public Vector3 gravityDir;
    public float gravityStrength, maxGraviLerps;
    private Quaternion gravityRotOffset, RotBeforeFlipping;
    private float graviLerps = 61;

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
        updMovement();
        applyGravity();

        if (inputService.jumpVal) jump();
    }

    void Update()
    {
        updCamera();
    }
    

    //Movement Stuff
    void updMovement()
    {
        Vector2 moveDirection = inputService.moveVal;

        Vector2 moveForce = moveDirection * new Vector2(moveSpeed, moveSpeed);
    
        Vector3 finalMoveForce = new Vector3(moveForce.x, 0, moveForce.y);
        finalMoveForce = cameraHolder.transform.TransformDirection(finalMoveForce);

        rb.AddForce(finalMoveForce, ForceMode.VelocityChange);

        //Counter Movement
        Vector3 counterForce = playerModel.transform.TransformDirection(rb.linearVelocity);
        counterForce = new Vector3(counterForce.x, 0, counterForce.z);
        counterForce = playerModel.transform.InverseTransformDirection(counterForce);

        rb.AddForce(counterForce * -0.5f, ForceMode.VelocityChange);
    }

    void jump()
    {
        inputService.jumpVal = false;
        Debug.Log("JUMP!");
        rb.AddForce(-gravityDir * 15, ForceMode.VelocityChange);
    }

    //Gravity
    void applyGravity()
    {
        rb.AddForce(gravityDir * gravityStrength, ForceMode.Acceleration);
        gravityRotOffset = Quaternion.FromToRotation(gravityDir, Vector3.down);

        if (graviLerps <= maxGraviLerps)
        {
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Inverse(gravityRotOffset), graviLerps / maxGraviLerps);
            graviLerps += 1;
        }

        else playerModel.transform.rotation = Quaternion.Inverse(gravityRotOffset);
    }

    public void changeGravity(Vector3 newGrav)
    {
        gravityDir = newGrav;
        graviLerps = 0;
        RotBeforeFlipping = playerModel.transform.rotation;
    }

    //Camera Stuff

    void updCamera()
    {
        Vector2 delta = inputService.cameraVal;
        Vector3 playerModelEuler = playerModel.transform.localRotation.eulerAngles;

        vertiRot -= delta.y * 0.1f * mouseSens;
        horiRot += delta.x * 0.1f * mouseSens;

        vertiRot = math.clamp(vertiRot, -85f, 85f);
        horiRot = horiRot % 360f;

        //Set Rotations
        cameraVerti.transform.localRotation = Quaternion.Euler(vertiRot,0,0);
        cameraHolder.transform.localRotation = Quaternion.Euler(0, horiRot,0);
    }

}
