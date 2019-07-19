using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineCurveBullet : Bullet
{
    public float moveSpeed;
    public float offsetY;

    Vector3 nextPosition;
    Rigidbody2D rb;

    public Direction direction;
    public enum Direction
    {
        Up,
        Down
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if(direction == Direction.Down)
        {
            nextPosition = transform.position - new Vector3(0, offsetY, 0);
        }
        else
        {
            nextPosition = transform.position + new Vector3(0, offsetY, 0);
        }
        
    }

    protected override void Start()
    {
        if(direction == Direction.Up)
        {
            rb.velocity = new Vector2(-speed /* gameManager.MultSpeed*/, moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(-speed /* gameManager.MultSpeed*/, -moveSpeed);
        }
        
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {        
        if(direction == Direction.Up)
        {
            if(transform.position.y > nextPosition.y)
            {
                nextPosition -= new Vector3(0, offsetY, 0);
                rb.velocity = new Vector2(-speed, -moveSpeed);
                direction = Direction.Down;
                return;
            }
        }
        else
        {
            if (transform.position.y < nextPosition.y)
            {
                nextPosition += new Vector3(0, offsetY, 0);
                rb.velocity = new Vector2(-speed, moveSpeed);
                direction = Direction.Up;
                return;
            }
        }
    }
}
