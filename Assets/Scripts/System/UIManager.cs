using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform blockTemplate;
    [SerializeField] private BlockListSO blockListSO;


    [ContextMenu("Setup Visual")] 
    private void SetupVisual()
    {
        Debug.Log("ciao");
        foreach (BlockObjectSO blockSO in blockListSO.blockListSO)
        {
            Transform blockTransform = Instantiate(blockTemplate, container);

            blockTransform.GetComponent<ManagerSingleUI>().setBlockSO(blockSO);
            blockTransform.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        blockTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {

    }
}
