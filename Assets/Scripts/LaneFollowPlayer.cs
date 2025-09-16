using UnityEngine;

public class LaneFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerToFollow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerToFollow != null)
            transform.position = new Vector3(transform.position.x, playerToFollow.position.y, playerToFollow.position.z);
    }
}
