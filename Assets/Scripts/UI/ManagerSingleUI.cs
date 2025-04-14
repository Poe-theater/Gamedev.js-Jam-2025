using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blockNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    public void setBlockSO(BlockObjectSO blockSO)
    {
        blockNameText.text = blockSO.name;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        iconTemplate.GetComponent<Image>().sprite = blockSO.sprite;
    }
}
