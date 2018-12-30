using UnityEngine;

public class BlockType : MonoBehaviour {

    public BlockData.blockType type;
    public SpriteRenderer blockSprite;

    private void Start()
    {
        blockSprite = GetComponent<SpriteRenderer>();
        
        type = BlockData.blockType.normal;
        blockSprite.color = BlockData.normalColor;
    }

    public void ChangeType()
    {
        BlockData.ChangeBlockType(ref type, blockSprite);
    }
}
