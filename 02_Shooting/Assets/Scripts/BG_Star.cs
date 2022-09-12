using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Star : Background
{
    SpriteRenderer[] spriteRenderers;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderers = new SpriteRenderer[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            spriteRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    protected override void MoveRightEnd(int index)
    {
        base.MoveRightEnd(index);

        int rand = Random.Range(0, 4);
        spriteRenderers[index].flipX = ((rand & 0b_01) != 0);
        spriteRenderers[index].flipY = ((rand & 0b_10) != 0);
    }
}
