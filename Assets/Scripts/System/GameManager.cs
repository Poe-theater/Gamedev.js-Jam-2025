using UnityEngine;
using CodeMonkey.CameraSystem;

public enum GameState { MainMenu = 0, Playing = 1, Options = 2, Quit = 3 }

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState gameState = GameState.MainMenu;
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerDropSystem dropSystem;
    [SerializeField] private BlockProduction blockProduction;
    [SerializeField] private SoundManager soundManager;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetGameState(gameState);
    }

    private void OnDisable()
    {
        UnsubscribeAll();
    }

    public void SetGameState(GameState newState)
    {
        UnsubscribeAll();

        gameState = newState;

        switch (gameState)
        {
            case GameState.MainMenu:
                soundManager.PlayWindAmbience();
                cameraSystem.SwitchCamera(gameState);
                break;

            case GameState.Playing:
                cameraSystem.SwitchCamera(gameState);
                blockProduction.ActiveProduction();
                SubscribeGameplayEvents();
                OnPlaying();
                break;
            
            case GameState.Options:
                break;
            
            case GameState.Quit:
                Application.Quit();
                break;
        }
    }

    private void SubscribeGameplayEvents()
    {
        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop;
        dropSystem.OnBlockGrab += DropSystem_OnBlockGrab;
        gridSystem.OnSpawnObject += GridSystem_OnSpawnObject;
        gridSystem.OnGridLvUp += GridSystem_OnGridLvUp;
        LanePoint.OnAnyCollisionEnter += LanePoint_OnAnyCollisionEnter;
    }

    private void UnsubscribeAll()
    {
        dropSystem.OnBlockDrop -= DropSystem_OnBlockDrop;
        dropSystem.OnBlockGrab -= DropSystem_OnBlockGrab;
        gridSystem.OnSpawnObject -= GridSystem_OnSpawnObject;
        gridSystem.OnGridLvUp -= GridSystem_OnGridLvUp;
        LanePoint.OnAnyCollisionEnter -= LanePoint_OnAnyCollisionEnter;
    }

    private void OnPlaying()
    {
        //cameraSystem.ResetToDefault();
    }

    private void LanePoint_OnAnyCollisionEnter((int, bool) tuple, Collision collision)
    {
        gridSystem.RemoveLastBlock();
    }

    private void GridSystem_OnSpawnObject(object sender, System.EventArgs e)
    {
        // handle spawn
    }

    private void DropSystem_OnBlockGrab(object sender, System.EventArgs e)
    {
        // handle grab
    }

    private void DropSystem_OnBlockDrop(object sender, System.EventArgs e)
    {
        // handle drop
    }

    private void GridSystem_OnGridLvUp(object sender, System.EventArgs e)
    {
        cameraSystem.IncreaseHeight();
    }
}
