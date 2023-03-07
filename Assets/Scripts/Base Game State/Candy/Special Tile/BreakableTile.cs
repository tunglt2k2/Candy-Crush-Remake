using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : BackgroundTile
{
    public Sprite syrup1;
    public Sprite syrup2;
    private SpriteRenderer spriteRender;

    public override void Start()
    {
        base.Start();
        spriteRender = GetComponent<SpriteRenderer>();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (hitPoints == 2) spriteRender.sprite = syrup2;
        else spriteRender.sprite = syrup1;
    }
}
