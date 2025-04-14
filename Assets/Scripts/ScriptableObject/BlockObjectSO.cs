using UnityEngine;

[CreateAssetMenu(fileName = "BlockObjectSO", menuName = "Scriptable Objects/BlockObjectSO")]
public class BlockObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    [SerializeField] private string objectName;
}
