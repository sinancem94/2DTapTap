using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public static Color normalColor = new Color32(159, 77, 77, 255);
    public static Color reverseColor = new Color32(75, 170, 120, 255);

    public static Sprite normalBlock = Resources.Load<Sprite>("Sprites/NormalBlock");//, typeof(Texture)) as Texture;
    public static Sprite reverseBlock = Resources.Load<Sprite>("Sprites/ReverseBlock");

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
            blockSprite.sprite = reverseBlock;
            blockSprite.color = reverseColor;
        }
        else
        {
            type = blockType.normal;
            blockSprite.sprite = normalBlock;
            blockSprite.color = normalColor;
        }
    }
}


