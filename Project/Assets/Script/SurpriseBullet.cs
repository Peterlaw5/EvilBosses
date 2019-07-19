using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseBullet : Bullet
{
    public GameObject bad;
    public GameObject good;
    public SpriteRenderer spriteR;
    public Sprite[] sprites;
    public float delayColors;
    public float time;
    bool isChanging;

    protected override void Start()
    {
        base.Start();
        isChanging = true;
        StartCoroutine(ChangeColor());
        Invoke("SetBulletType",time);
    }

    IEnumerator ChangeColor()
    {
        if(spriteR.sprite == sprites[0])
        {
            spriteR.sprite = sprites[1];
        }
        else
        {
            spriteR.sprite = sprites[0];
        }

        yield return new WaitForSeconds(delayColors);

        if (isChanging)
        {
            StartCoroutine(ChangeColor());
        }
        
    }

    void SetBulletType()
    {
        isChanging = false;

        int type = Mathf.RoundToInt(Random.value);

        if(type == 0)
        {
            Instantiate(bad, transform);
        }
        else
        {
            Instantiate(good, transform);
        }

        spriteR.enabled = false;
        bulletType = (BulletType)type;
    }

}
