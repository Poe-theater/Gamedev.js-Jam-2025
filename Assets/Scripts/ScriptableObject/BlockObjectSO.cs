using UnityEngine;

[CreateAssetMenu(fileName = "BlockObjectSO", menuName = "Scriptable Objects/BlockObjectSO")]
public class BlockObjectSO : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public GameObject prefab;
    [SerializeField] private string objectName;
}
