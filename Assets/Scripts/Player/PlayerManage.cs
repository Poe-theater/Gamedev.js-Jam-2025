using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerDropSystem dropSystem;

    private void Start()
    {
        dropSystem.yCoord = gridSystem.quadBlocker.transform.position.y + 10;
        dropSystem.SetBlock(gridSystem.SpawnObject(0));

        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop;
    }

    private void OnDisable()
    {
        dropSystem.OnBlockDrop -= DropSystem_OnBlockDrop;
    }

    private void DropSystem_OnBlockDrop(object sender, System.EventArgs e)
    {
        gridSystem.ChangeBlockerStatus();
    }

    private void Update()
    {
        dropSystem.Loop();
    }

}
