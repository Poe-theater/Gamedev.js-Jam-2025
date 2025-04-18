using CodeMonkey.CameraSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerDropSystem dropSystem;

    private void Start()
    {
        gridSystem.OnGridLvUp += GridSystem_OnGridLvUp;
        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop; ;
        dropSystem.OnBlockGrab += DropSystem_OnBlockGrab; ;
    }

    private void DropSystem_OnBlockGrab(object sender, System.EventArgs e)
    {

    }

    private void DropSystem_OnBlockDrop(object sender, System.EventArgs e)
    {

    }

    private void GridSystem_OnGridLvUp(object sender, System.EventArgs e)
    {
        cameraSystem.increaseHeight();
    }
}
