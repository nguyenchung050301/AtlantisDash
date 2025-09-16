using System.Collections;
using System.IO.Compression;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    //Object Pool
    [SerializeField] private GameObject platform_Prefab;
    [SerializeField] private int howManyInPool;
    [SerializeField] private Transform[] platforms;
    private PlayerController _playerController;
    private float rangeThreshold; //condition for next object in pool
    private bool triggerDisablePrevious;
    private bool canActiveNextInPool;
    private int poolIndex;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        canActiveNextInPool = false;
        poolIndex = 0;
        triggerDisablePrevious = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < howManyInPool; i++)
        {
            platforms[i] = Instantiate(platform_Prefab, transform).transform;
            platforms[i].transform.position =
            new Vector3(platforms[0].position.x, platforms[0].position.y, +platforms[i].localScale.z * i);
            platforms[i].gameObject.SetActive(false);
        }
        platforms[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (poolIndex >= platforms.Length) //reset index
        {
            poolIndex = 0;
        }
        rangeThreshold = ((platforms[0].transform.localScale.z / 2)) / 10;
        if (_playerController.transform.position.z - platforms[poolIndex].transform.position.z > rangeThreshold)
        {
            if (poolIndex < platforms.Length)
            {
                triggerDisablePrevious = true;
                StartCoroutine(DisablePrevious(platforms[poolIndex]));
                poolIndex++;
                platforms[poolIndex].gameObject.SetActive(true);      //next index
            }
        }


        //  Debug.Log(_playerController.transform.position.z - platforms[poolIndex + 1].transform.position.z);
    }
    public void InjectPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }

    private IEnumerator DisablePrevious(Transform obj)
    {
        yield return new WaitForSeconds(5);
        obj.gameObject.SetActive(false);
    }
}
