using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weakness : MonoBehaviour
{
    public Boss boss;
    public float damage;
    public float multLevel2;
    public float multLevel3;
    public float time;
    public GameObject damageText;
    public MeshRenderer bossMeshR;
    public Color colorHit;

    public GameObject circleHit;
    


    private void OnEnable()
    {
        StartCoroutine(Disable());
    }
    

    //IEnumerator HitVFX()
    //{
    //    foreach(Material mat in bossMeshR.materials)
    //    {
    //        mat += colorHit;
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    foreach (Material mat in bossMeshR.materials)
    //    {
    //        mat.color -= colorHit;
    //    }
    //}

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    public void DamageBoss()
    {
        boss.gameManager.player.Attack();
        
        //StartCoroutine(HitVFX());
        GameObject go = Instantiate(circleHit, transform.position, Quaternion.identity);
        go.transform.SetParent(transform.parent, true);
        go.transform.localScale = Vector3.one;
        Destroy(go, 0.5f);

        float dmg = 0;
        Player.phases lvPlayer = boss.gameManager.player.currentphase;

        switch (lvPlayer)
        {
            case (Player.phases.ONE):
                dmg = damage;
                break;
            case (Player.phases.TWO):
                dmg = damage * multLevel2;
                break;
            case (Player.phases.THREE):
                dmg = damage * multLevel3;
                break;
        }

        boss.currentHealth -= dmg;

        boss.bossHit.Play();
                
        damageText.transform.position = transform.position;
        Text dmgTxt = damageText.GetComponent<Text>();
        dmgTxt.text = ("-" + dmg.ToString());
        damageText.SetActive(true);

        gameObject.SetActive(false);
    }
}
