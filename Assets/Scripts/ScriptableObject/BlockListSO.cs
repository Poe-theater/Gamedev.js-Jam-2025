using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "BlockList", menuName = "Scriptable Objects/BlockList")]
public class BlockListSO : ScriptableObject
{
    public List<BlockObjectSO> blockListSO;
}
