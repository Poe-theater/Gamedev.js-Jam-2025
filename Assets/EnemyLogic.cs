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
    public float targetTime = 2.0f;

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

        if (instance.TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        if (instance.TryGetComponent(out BoxCollider collider))
            collider.enabled = true;

        instance.TryGetComponent(out Block block);
    }


    private void Update()
    {
        if (isStarted)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                SpawnEnemy();
                targetTime = 2.0f;
            }
        }
    }
}
