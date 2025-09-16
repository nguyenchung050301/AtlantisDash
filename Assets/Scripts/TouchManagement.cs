using UnityEngine;
using static UnityEngine.InputSystem.InputAction;


public class TouchManagement : MonoBehaviour
{
    private PlayerController _playerController;
    private Vector2 lastMousePos;
    private int lastPhase;
    private Vector2 moveDelta;
    [SerializeField] private float isRightOrLeft; //1 = right, -1 = left
    private void Awake()
    {


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangePhase();
      
    }
    public void InjectPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    private void ChangePhase()
    {
        Vector2 moveDelta = Vector2.zero;
        //Use for touch
        if (Input.touchCount > 0) //if there is any pressed touch 
        {
            foreach (var touchInfo in Input.touches)
            {
                lastPhase = (int)touchInfo.phase;
                if (lastPhase == (int)TouchPhase.Began) //when begin to press
                {
                    lastMousePos = touchInfo.position;
                }
                else //when moving
                {
                    moveDelta = touchInfo.position - lastMousePos;
                    lastMousePos = touchInfo.position;
                }
                break; //just get 1 touch
            }

        }


        //Use for mouse
        else if (Input.GetMouseButtonDown(0))
        {
            lastPhase = (int)TouchPhase.Began;
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            lastPhase = (int)TouchPhase.Moved;
            moveDelta = (Vector2)Input.mousePosition - lastMousePos;
            lastMousePos = (Vector2)Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastPhase = (int)TouchPhase.Ended;
            moveDelta = (Vector2)Input.mousePosition - lastMousePos;
            lastMousePos = (Vector2)Input.mousePosition;
         //   _playerController.SetCanSwitchLane(true);
        }
        else
        {
            lastPhase = -1;
        }


        //check right or left slide
        if (moveDelta.x != 0)
        {
            if (moveDelta.x > 0)
            {
                isRightOrLeft = 1;
            }
            else
            {
                isRightOrLeft = -1;
            }
        }
        else
        {
            isRightOrLeft = 0;
        }
    }
    public float GetIsRightOrLeft()
    {
        return isRightOrLeft;
    }
}

