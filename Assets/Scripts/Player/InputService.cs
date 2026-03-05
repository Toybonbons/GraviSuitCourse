using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{

    [SerializeField] InputActionAsset inputMap;
    
    public static InputService Instance;
    private InputAction movementInput, cameraInput, jumpInput, dashInput;

    //Input Vals
    public Vector2 moveVal, cameraVal;
    public bool jumpVal, dashVal;

    //Unity Funcs
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        setupInputs();
    }

    void Update()
    {
        moveVal = movementInput.ReadValue<Vector2>();
        cameraVal = cameraInput.ReadValue<Vector2>();
    }

    //Functions

    void setupInputs()
    {
        InputActionMap playerActionMap = inputMap.FindActionMap("Player");

        movementInput = playerActionMap.FindAction("Movement");
        cameraInput = playerActionMap.FindAction("Camera");

        jumpInput = playerActionMap.FindAction("Jump");
        dashInput = playerActionMap.FindAction("Dash");


        jumpInput.performed += context => jumpVal = true;
        dashInput.performed += context => dashVal = true;


        //movementInput.performed += context => moveVal = context.ReadValue<Vector2>();
    }
}
