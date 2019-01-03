using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public static Color normalColor = new Color32(159, 77, 77, 255);
    public static Color reverseColor = new Color32(75, 170, 120, 255);

    public enum blockType
    {
        normal,
        reverse
    };

    //Changes block type whether its a normal block or reverse block
    public static void ChangeBlockType(ref blockType type,SpriteRenderer blockSprite)
    {
        if (type == blockType.normal)
        {
            type = blockType.reverse;
            blockSprite.color = reverseColor;
        }
        else
        {
            type = blockType.normal;
            blockSprite.color = normalColor;
        }
    }
}


