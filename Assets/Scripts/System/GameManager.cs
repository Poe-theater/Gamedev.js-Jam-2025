using CodeMonkey.CameraSystem;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridSystem gridSystem;
    [SerializeField] private CameraSystem cameraSystem;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerDropSystem dropSystem;
    [SerializeField] private BlockProduction blockProduction;

    private void Start()
    {
        dropSystem.OnBlockDrop += DropSystem_OnBlockDrop;
        dropSystem.OnBlockGrab += DropSystem_OnBlockGrab;   
        gridSystem.OnSpawnObject += GridSystem_OnSpawnObject;
        gridSystem.OnGridLvUp += GridSystem_OnGridLvUp;

        LanePoint.OnAnyCollisionEnter += LanePoint_OnAnyCollisionEnter;
    }

    private void OnDisable()
    {
        dropSystem.OnBlockDrop -= DropSystem_OnBlockDrop;
        dropSystem.OnBlockGrab -= DropSystem_OnBlockGrab;
        gridSystem.OnSpawnObject -= GridSystem_OnSpawnObject;
        gridSystem.OnGridLvUp -= GridSystem_OnGridLvUp;

        LanePoint.OnAnyCollisionEnter -= LanePoint_OnAnyCollisionEnter;
    }

    private void LanePoint_OnAnyCollisionEnter((int, bool) tuple, Collision collision)
    {
        gridSystem.RemoveLastBlock();
    }

    private void GridSystem_OnSpawnObject(object sender, System.EventArgs e)
    {

    }

    private void DropSystem_OnBlockGrab(object sender, System.EventArgs e)
    {

    }

    private void DropSystem_OnBlockDrop(object sender, System.EventArgs e)
    {

    }

    private void GridSystem_OnGridLvUp(object sender, System.EventArgs e)
    {
        cameraSystem.IncreaseHeight();
    }
}
