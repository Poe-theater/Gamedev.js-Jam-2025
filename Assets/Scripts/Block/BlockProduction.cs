using System;
using UnityEngine;

public class BlockProduction : MonoBehaviour
{
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private float productionTimerMax;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private float reductionPercentPerBlock;

    private BlockObjectSO nextRandomBlock;
    private float productionTimer;
    private float baseProductionTimerMax;

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
        baseProductionTimerMax = productionTimerMax;
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
            UpdateProductionTimerMax();
            productionTimer = productionTimerMax;
            playerManager.AddToInventory(nextRandomBlock);
            SelectNewRandomBlock();
        }
    }

    private void SelectNewRandomBlock()
    {
        //UnityEngine.Random.Range(0, blockListSO.blockListSO.Count)
        nextRandomBlock = blockListSO.blockListSO[UnityEngine.Random.Range(0, blockListSO.blockListSO.Count)];
        //nextRandomBlock = blockListSO.blockListSO[0];

        OnProgressBlockChanged?.Invoke(this, new OnProgressBlockChangedEventArgs
        {
            nextRandomBlockIcon = nextRandomBlock
        });
    }

    public BlockObjectSO GetCurrentBlock()
    {
        return nextRandomBlock;
    }

    private void UpdateProductionTimerMax()
    {
        if (gridSystem.BlockCount <= 0)
        {
            productionTimerMax = baseProductionTimerMax;
            return;
        }

        float reductionFactorPerBlock = 1f - (reductionPercentPerBlock / 100f);
        float totalReductionFactor = Mathf.Pow(reductionFactorPerBlock, gridSystem.BlockCount);

        productionTimerMax = baseProductionTimerMax * totalReductionFactor;

        productionTimerMax = Mathf.Max(productionTimerMax, 1f);
    }
}

