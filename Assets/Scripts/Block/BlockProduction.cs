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

    public event EventHandler<float> OnProgressChanged;
    public event EventHandler<BlockObjectSO> OnProgressBlockChanged;

    [SerializeField] private bool isStarted = false;
    private void Awake()
    {
        baseProductionTimerMax = productionTimerMax;
        productionTimer = productionTimerMax;
        SelectNewRandomBlock();
    }

    private void Update()
    {
        if (isStarted) 
        {
            productionTimer -= Time.deltaTime;

            OnProgressChanged?.Invoke(this, 1 - (productionTimer / productionTimerMax));

            if (productionTimer < 0f)
            {
                UpdateProductionTimerMax();
                productionTimer = productionTimerMax;
                print("pistole");
                playerManager.AddToInventory(nextRandomBlock);
                SelectNewRandomBlock();
            }
        }
    }

    public void ActiveProduction()
    {
        isStarted = true;
    }

    private void SelectNewRandomBlock()
    {
        //nextRandomBlock = blockListSO.blockListSO[UnityEngine.Random.Range(0, blockListSO.blockListSO.Count)];
        nextRandomBlock = blockListSO.blockListSO[0];
        OnProgressBlockChanged?.Invoke(this, nextRandomBlock);
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

