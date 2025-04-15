using UnityEngine;

public class PlayerManage : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private PlayerDropSystem dropSystem;

    private void Start()
    {
        dropSystem.yCoord = gridSystem.quadBlocker.transform.position.y + 15;
        CreateBlock();

        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop;
    }

    [ContextMenu("create block")]
    void CreateBlock()
    {
        dropSystem.SetBlock(gridSystem.SpawnObject(0));
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
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            CreateBlock();
        }
        dropSystem.Loop();
    }

}
