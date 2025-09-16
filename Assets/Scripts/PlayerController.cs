using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEngine.InputSystem.InputAction;


public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        EnhancedTouchSupport.Enable();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.TouchPosition.performed += OnTouchPadAxis;
    }
    private void OnDisable()
    {
        playerInput.Player.TouchPosition.performed -= OnTouchPadAxis;
        playerInput.Disable();
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool check = playerInput.Player.TouchPress.ReadValue<bool>();
        Debug.Log(check);
        Vector2 touchpadValue = playerInput.Player.TouchPosition.ReadValue<Vector2>();
        Debug.Log(touchpadValue);
    }
    public void OnTouchPadAxis(CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>());
    }
    public void OnPress(CallbackContext context)
    {
       // Debug.Log(context.ReadValue<bool>());
    }
}
