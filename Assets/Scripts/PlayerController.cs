using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TouchManagement _touchManagement;
    [SerializeField] private int laneIndex; //left = 0, mid = 1, right = 2
    [SerializeField] private bool canSwitchLane;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeed_SwitchLane;
    private bool isSwitchBlocked;
    private bool switchRight;
    private bool switchLeft;
    [SerializeField] private Transform[] lanes;
    void Awake()
    {
        isSwitchBlocked = false;
        canSwitchLane = false;
        laneIndex = 1; //mid
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SwitchLane();
    }
    void FixedUpdate()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        //     transform.position = Vector3.MoveTowards(transform.position, pos.position, 5 * Time.fixedDeltaTime);
    }


    public void InjectTouchManagement(TouchManagement touchManagement)
    {
        _touchManagement = touchManagement;
    }
    private void SwitchLane()
    {
        if (_touchManagement != null)
        {
            float checkSwitchLane = _touchManagement.GetIsRightOrLeft();
            if (checkSwitchLane > 0 && !switchRight && !isSwitchBlocked)
            {
                switchRight = true;
                canSwitchLane = true;
                if (laneIndex < 2)
                    laneIndex++;
            }
            if (checkSwitchLane < 0 && !switchLeft && !isSwitchBlocked)
            {
                switchLeft = true;
                canSwitchLane = true;
                if (laneIndex > 0)
                    laneIndex--;
            }
            if (canSwitchLane)
            {
                isSwitchBlocked = true;
                transform.position = Vector3.MoveTowards
                 (transform.position, lanes[laneIndex].position
                  , moveSpeed_SwitchLane * Time.deltaTime);

                if (Vector3.Distance(transform.position, lanes[laneIndex].position) <= 0)
                {
                    isSwitchBlocked = false;
                    canSwitchLane = false;
                    switchLeft = false;
                    switchRight = false;
                }
            }
        }
    }



    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("C");
        }
    }
    public void SetCanSwitchLane(bool value)
    {
        canSwitchLane = value;
    }
}
