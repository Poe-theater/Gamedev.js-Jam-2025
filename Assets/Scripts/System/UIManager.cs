using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Transform container;
    [SerializeField] private Transform BlockTemplate;
    [SerializeField] private BlockObjectSO[] block;


    [ContextMenu("Setup Visual")] 
    private void SetupVisual()
    {
        Debug.Log("ciao");
        foreach (BlockObjectSO item in block) {
            Transform blockTransform = Instantiate(BlockTemplate, container);

            blockTransform.GetComponent<ManagerSingleUI>().setBlockSO(item);
        }
    }

    private void UpdateVisual()
    {

    }
}
