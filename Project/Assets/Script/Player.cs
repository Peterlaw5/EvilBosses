using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum phases
    {
        ZERO = 0,
        ONE = 1,
        TWO = 2,
        THREE = 3,
        ULTI = 4
    }


    public phases currentphase;
    private bool ultimate;

    public Image powerBar;
    public GameObject[] teleports;
    public GameObject[] powerUps;
    public GameObject[] playerForms;

    public AudioSource damage;
    public AudioSource charge;
    public AudioSource finishHim;
    public AudioSource onda;

    public float power;
    public float maxPowerPhase1 = 1f;
    public float maxPowerPhase2 = 3f;
    public float maxPowerPhase3 = 5f;

    public Sprite powerSlider;
    public Sprite ultiSlider;
    public GameObject ondaEnergetica;

    GameObject teleport;
    GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
        ultimate = false;
        currentphase = phases.ONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (power < 0)
        {
            power = 0;
            currentphase = phases.ZERO; //GAME OVER

        }

        if (power > 0 && power < maxPowerPhase1)
        {
            currentphase = phases.ONE;
            ultimate = false;
            powerBar.fillAmount = Mathf.Clamp(power, 0, 1);
            powerBar.sprite = powerSlider;
        }

        if (power >= maxPowerPhase1 && power < maxPowerPhase2)
        {
            currentphase = phases.TWO;
            ultimate = false;
            powerBar.fillAmount = Mathf.Clamp(power - maxPowerPhase1, 0, 1);
            powerBar.sprite = powerSlider;
        }

        if (power >= maxPowerPhase2 && power < maxPowerPhase3)
        {
            currentphase = phases.THREE;
            ultimate = false;
            powerBar.fillAmount = Mathf.Clamp(power - maxPowerPhase2, 0, 1);
            powerBar.sprite = powerSlider;
        }

        if (power >= maxPowerPhase3)
        {
            power = maxPowerPhase3;
            currentphase = phases.ULTI;
            ultimate = true;
            powerBar.fillAmount = Mathf.Clamp(power - maxPowerPhase3, 0, 1);
            powerBar.sprite = ultiSlider;
        }

        switch (currentphase)
        { 
            case phases.ONE:
                playerForms[0].gameObject.SetActive(true);
                playerForms[1].gameObject.SetActive(false);
                playerForms[2].gameObject.SetActive(false);
                teleport = teleports[0];
                powerUp = powerUps[0];
                break;
            case phases.TWO:
                playerForms[1].gameObject.SetActive(true);
                playerForms[0].gameObject.SetActive(false);
                playerForms[2].gameObject.SetActive(false);
                teleport = teleports[1];
                powerUp = powerUps[1];
                break;
            case phases.THREE:
                playerForms[2].gameObject.SetActive(true);
                playerForms[0].gameObject.SetActive(false);
                playerForms[1].gameObject.SetActive(false);
                teleport = teleports[2];
                powerUp = powerUps[2];
                break;
        }
    }

        public void DoUltimate()
    {
        onda.Play();
        power = Mathf.Lerp(maxPowerPhase1, maxPowerPhase2, 0.5f);
        GameObject go = Instantiate(ondaEnergetica, transform.position, Quaternion.identity);
        Destroy(go, 0.35f);
    }

    public void Move(GameObject button)
    {
        GameObject t = Instantiate(teleport, transform.position, Quaternion.identity);

        transform.position = button.transform.position;

        Destroy(t,0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.GetComponent<Bullet>().bulletType == Bullet.BulletType.Damage)
        {
            damage.Play();

            power -= collision.gameObject.GetComponent<Bullet>().damage;
        }

        if (collision.gameObject.GetComponent<Bullet>().bulletType == Bullet.BulletType.Charge)
        {
            if (ultimate)
            {
                finishHim.Play();
            }
            else
            {
                charge.Play();
            }
            

            GameObject p = Instantiate(powerUp, transform.position, Quaternion.identity);

            Destroy(p, 0.3f);

            power += collision.gameObject.GetComponent<Bullet>().damage;
        }

        Destroy(collision.gameObject);
    }

    public void Attack()
    {
        foreach(GameObject go in playerForms)
        {
            if (go.activeSelf && !go.GetComponent<Animator>().GetBool("attack"))
            {
                go.GetComponent<Animator>().SetTrigger("attack");
            }            
        }
    }


}

   


