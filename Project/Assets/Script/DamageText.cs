using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public Text textMesh;

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
        textMesh.color -= new Color(0, 0, 0, 1) * Time.deltaTime;
    }

    private void OnEnable()
    {
        textMesh.color += new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);
        StartCoroutine(Animation());
    }    

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
}
