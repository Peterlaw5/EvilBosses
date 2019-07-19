using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;
    public float damage;
    public float speed;

    public BulletType bulletType;
    public enum BulletType
    {
        Damage,
        Charge
    }

    protected virtual void Start()
    {
        Invoke("SetVelocity", 1f);
        Destroy(gameObject, 10f);
    }

    void SetVelocity()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * GameManager.multSpeed, 0);
    }

}
