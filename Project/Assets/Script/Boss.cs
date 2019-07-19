using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Phase
{
    public Wave[] waves;
}

public class Boss : MonoBehaviour
{
    public Weakness[] damagePoints;
    public float delayWeakness;

    public float maxHealth;
    public float currentHealth;

    public Phase[] phases;
    int indexPhase;

    public Image healthbar;

    public GameManager gameManager;

    public Animator anim;

    public AudioSource bossHit;

    public Canvas bossCanvas;
    
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine(StartWave());
        StartCoroutine(ActiveWeakness());
        bossCanvas.worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = Mathf.Clamp(currentHealth * 1 / maxHealth, 0, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10;
        }
    }

    IEnumerator ActiveWeakness()
    {
        yield return new WaitForSeconds(delayWeakness);

        int indexWeakness = Random.Range(0, damagePoints.Length);

        if (!damagePoints[indexWeakness].gameObject.activeSelf)
        {
            damagePoints[indexWeakness].gameObject.SetActive(true);
        }
        
        StartCoroutine(ActiveWeakness());
    }

    IEnumerator StartWave()
    {
        Wave[] waves = phases[indexPhase].waves;
        int indexWave = Random.Range(0, waves.Length);
        Wave wave = waves[indexWave];

        foreach(Spawn spawn in wave.spawns)
        {
            StartCoroutine(SpawnBullet(spawn.bullet,spawn.position,spawn.delaysSpawn * gameManager.MultWave));
        }

        yield return new WaitForSeconds(waves[indexWave].durationWave * gameManager.MultWave);

        if(currentHealth <= maxHealth / 3)
        {
            indexPhase = 1;
        }
        else if(currentHealth <= maxHealth / 3 * 2)
        {
            indexPhase = 2;
        }

        StartCoroutine(StartWave());
    }

    IEnumerator SpawnBullet(GameObject bullet, Vector3 spawnPoint, float delay)
    {
        yield return new WaitForSeconds(delay);

        Bullet b = Instantiate(bullet, spawnPoint, Quaternion.identity).GetComponent<Bullet>();
        b.gameManager = gameManager;
    }


    public void Dead()
    {
        StopAllCoroutines();
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach(Bullet b in bullets)
        {
            Destroy(b.gameObject);
        }
        anim.SetBool("Dead", true);
    }
    //IEnumerator SpawnBullet()
    //{        
    //    int bullet = Mathf.RoundToInt(Random.Range(0,bullets.Length));
    //    Bullet b = bullets[bullet].GetComponent<Bullet>();

    //    int spawnPoint;
    //    if (b.isBig)
    //    {
    //        spawnPoint = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length-1));
    //    }
    //    else
    //    {
    //        spawnPoint = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));
    //    }        

    //    Instantiate(bullets[bullet], spawnPoints[spawnPoint].transform.position, Quaternion.identity).GetComponent<Bullet>();

    //    yield return new WaitForSeconds(delayBullet);
    //    StartCoroutine(SpawnBullet());
    //}
}
