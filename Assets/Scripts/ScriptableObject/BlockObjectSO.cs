using UnityEngine;

[CreateAssetMenu(fileName = "BlockObjectSO", menuName = "Scriptable Objects/BlockObjectSO")]
public class BlockObjectSO : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public Transform prefab;
    [SerializeField] private string objectName;
}
