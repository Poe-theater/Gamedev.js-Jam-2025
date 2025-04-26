using UnityEngine;
using CodeMonkey.CameraSystem;
using System;

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

    public event EventHandler<GameState> OnGameStateChange;

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

        OnGameStateChange?.Invoke(this, gameState);
        switch (gameState)
        {
            case GameState.MainMenu:
                soundManager.PlayWindAmbience();
                cameraSystem.SwitchCamera(gameState);
                break;

            case GameState.Playing:
                EnemyLogic.Instance.isStarted = true;
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
    }

    private void UnsubscribeAll()
    {
        dropSystem.OnBlockDrop -= DropSystem_OnBlockDrop;
        dropSystem.OnBlockGrab -= DropSystem_OnBlockGrab;
        gridSystem.OnSpawnObject -= GridSystem_OnSpawnObject;
        gridSystem.OnGridLvUp -= GridSystem_OnGridLvUp;
    }

    public void DestroyEnemyPiece()
    {
        EnemyLogic.Instance.OnAttack();
    }

    public void DestroyPlayerPiece()
    {
        gridSystem.DestroyPiece();
    }


    private void OnPlaying()
    {
        //cameraSystem.ResetToDefault();
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
