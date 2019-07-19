using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimation : MonoBehaviour
{
    Vector3 scale;
    Vector3 upscale;
    float cooldown;

    private void Awake()
    {
        scale = transform.localScale;
        upscale = scale / 20f;        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        transform.localScale += upscale;
        cooldown += 0.05f;
        yield return new WaitForSeconds(0.05f);
        if(cooldown < 1f)
        {
            StartCoroutine(Animation());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
