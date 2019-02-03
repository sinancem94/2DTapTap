using UnityEngine;
using UnityEngine.UI;

public class BlockType : MonoBehaviour {

    public BlockData.blockType type;
    private SpriteRenderer blockSprite;
    private Text limitText;

    public int limit;

    private void Start()
    {
        blockSprite = GetComponent<SpriteRenderer>();
        blockSprite.sprite = BlockData.normalBlock;
        blockSprite.color = BlockData.normalColor;

        limit = 1;

        type = BlockData.blockType.normal;

    }

    public void ChangeType()
    {
        BlockData.ChangeBlockType(ref type, blockSprite);
    }

    public void ChangeLimit(int num)
    {
        limit += num; 
    }
}
