using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    public PlayerController player;
    public TouchManagement touchManagement;
    public PlatformManager platformManager;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        player.InjectTouchManagement(touchManagement);
        touchManagement.InjectPlayerController(player);
        platformManager.InjectPlayerController(player);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
