using System;
using UnityEngine;

public class BlockProduction : MonoBehaviour
{
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private float productionTimerMax;
    [SerializeField] private PlayerManager playerManager;
    private BlockObjectSO nextRandomBlock;

    private float productionTimer;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler<OnProgressBlockChangedEventArgs> OnProgressBlockChanged;
    public class OnProgressBlockChangedEventArgs : EventArgs
    {
        public BlockObjectSO nextRandomBlockIcon;
    }

    private void Awake()
    { 

        productionTimer = productionTimerMax;
        SelectNewRandomBlock();
    }

    private void Update()
    {

        productionTimer -= Time.deltaTime;

        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = 1 - (productionTimer / productionTimerMax)
        });

        if (productionTimer < 0f)
        {

            productionTimer = productionTimerMax;
            playerManager.AddToInventory(nextRandomBlock);
            SelectNewRandomBlock();
        }
    }

    private void SelectNewRandomBlock()
    {
        nextRandomBlock = blockListSO.blockListSO[UnityEngine.Random.Range(0, blockListSO.blockListSO.Count)];

        OnProgressBlockChanged?.Invoke(this, new OnProgressBlockChangedEventArgs
        {
            nextRandomBlockIcon = nextRandomBlock
        });
    }

    public BlockObjectSO GetCurrentBlock()
    {
        return nextRandomBlock;
    }
}

