using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private BlockListSO blockListSO;
    [SerializeField] private List<GameObject> piece;

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
        var t = blockListSO.blockListSO[UnityEngine.Random.Range(0, blockListSO.blockListSO.Count)].prefab;
        GameObject scarpe = Instantiate(t);
        
        print("spawn nemico");
    }

    private void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            SpawnEnemy();
            targetTime = 2.0f;
        }
    }
}
