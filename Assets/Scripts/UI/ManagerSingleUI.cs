using UnityEngine;
using TMPro;
public class ManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blockNameText;

    public void setBlockSO(BlockObjectSO blockSO)
    {
        blockNameText.text = blockSO.name;

    }
}
