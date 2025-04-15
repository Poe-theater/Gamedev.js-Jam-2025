using UnityEngine;

public class BlockProduction : MonoBehaviour
{
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private float productionTimerMax;
    [SerializeField] private PlayerManage playerManager;

    private float productionTimer;

    private void Update()
    {
        BlockObjectSO nextRandomBlock = blockListSO.blockListSO[Random.Range(0, blockListSO.blockListSO.Count)];

        productionTimer -= Time.deltaTime;
        if (productionTimer < 0f)
        {
            productionTimer = productionTimerMax;

            playerManager.AddToInventory(nextRandomBlock);
        }
    }

}
