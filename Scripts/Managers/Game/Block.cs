using UnityEngine;
using UnityEngine.Serialization;

public abstract class Block : MonoBehaviour
{
    [FormerlySerializedAs("_blockType")]
    public Define.BlockType Type = Define.BlockType.None;

    public abstract void Init();
}
