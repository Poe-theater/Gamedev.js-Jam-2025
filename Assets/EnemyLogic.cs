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
        int index = Random.Range(0, spawnPoint.Count);
        var t = blockListSO.blockListSO[Random.Range(0, blockListSO.blockListSO.Count)].prefab;
        GameObject test = Instantiate(t, spawnPoint[index].position, Quaternion.identity);
        test.transform.parent = parent;
        test.GetComponent<Rigidbody>().isKinematic = false;
        var coll = test.GetComponent<BoxCollider>();
        coll.enabled = true;
        Block block = test.GetComponent<Block>();

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
