using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private List<GameObject> piece;
    [SerializeField] private List<Transform> spawnPoint;
    [SerializeField] private Transform parent;

    public bool isStarted = false;
    private float timeSinceStart = 0f;
    [SerializeField] private float spawnInterval = 8.0f; 
    private float spawnTimer = 0f;
    [SerializeField] private float difficultyIncreaseInterval = 30.0f;
    [SerializeField] private float difficultyTimer = 0f;
    [SerializeField] private float spawnReductionFactor = 0.95f;
    public static EnemyLogic Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void OnAttack()
    {
        int index = Random.Range(0, piece.Count);

        if (piece.Count > 0)
        {
            Destroy(piece[index]);
            piece.RemoveAt(index);
        }
    }


    void SpawnEnemy()
    {
        if (spawnPoint.Count == 0 || blockListSO.blockListSO.Count == 0)
            return;

        int spawnIndex = Random.Range(0, spawnPoint.Count);
        int blockIndex = Random.Range(0, blockListSO.blockListSO.Count);

        var prefab = blockListSO.blockListSO[blockIndex].prefab;
        var instance = Instantiate(prefab, spawnPoint[spawnIndex].position, Quaternion.identity, parent);
        var bl = instance.TryGetComponent(out Block block);

        block.isEnemy = true;
        if (instance.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        if (instance.TryGetComponent(out BoxCollider collider))
            collider.enabled = true;

    }


    private void Update()
    {
        if (!isStarted)
            return;

        timeSinceStart += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            spawnInterval *= spawnReductionFactor; 
            difficultyTimer = 0f;

            if (spawnInterval < 1.0f)
                spawnInterval = 1.0f;
        }
    }
}
