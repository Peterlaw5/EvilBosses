using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] bossPrefabs; 
    public Boss boss;
    public int indexBoss;
    public bool isBossLive;
    public Player player;
    public Text[] levels;

    public GameObject pausePanel;
    public GameObject defeatPanel;

    public static float multWave = 1f;
    public float decreaseWave = 0.2f;
    public float minWaveMult = 0.5f;

    public static float multSpeed = 1f;
    public float increaseSpeed = 0.2f;
    public float maxSpeedMult = 0.5f;

    public static float multHealth = 1f;
    public float increaseHealth = 0.2f;
    public float maxHealtMult = 0.5f;

    int counterBoss;
    public TextMeshProUGUI counterBossTxt;
    public TextMeshProUGUI finalScore;

    public GameObject knokedImage;

    public GameObject ultimateButton;

    public GameObject nuvola;
    public GameObject[] nuvolePositions;

    public AudioSource fightsound;
    public AudioSource bossko;
    public AudioSource bosshaha;

    bool onReady;
    public GameObject ready;
    float cooldown = 0f;

    bool onFight;
    public GameObject fight;
    float cooldown2 = 0f;

    public float MultWave { get => multWave; set => multWave = value; }
    public float MultSpeed { get => multSpeed; set => multSpeed = value; }
    public float MultHealth { get => multHealth; set => multHealth = value; }


    // Start is called before the first frame update
    void Awake()
    {
        multSpeed = 1;
        multHealth = 1;
        multWave = 1;
        //Time.timeScale = 1;
        isBossLive = true;
        onReady = true;
        onFight = false;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (onReady)
        {
            cooldown += Time.unscaledDeltaTime;

            if (cooldown > 5f)
            {
                ready.SetActive(false);
                fight.SetActive(true);
                onReady = false;
                onFight = true;
            }
        }

        if (onFight)
        {
            fightsound.Play();
            Fight();
        }

        if (isBossLive && boss != null && boss.currentHealth <= 0)
        {
            bossko.Play();
            isBossLive = false;
            BossDefeated();
        }

        for (int i = 0; i < levels.Length ; ++i)
        {
            levels[i].gameObject.SetActive(false);
        }

        switch (player.currentphase)
        {
            case Player.phases.ZERO:
                Time.timeScale = 0;
                finalScore.text = ("Boss Eliminated: " + counterBoss);
                defeatPanel.SetActive(true);
                break;
            case Player.phases.ONE:
                levels[0].gameObject.SetActive(true);
                ultimateButton.SetActive(false);
                break;
            case Player.phases.TWO:
                levels[1].gameObject.SetActive(true);
                ultimateButton.SetActive(false);
                break;
            case Player.phases.THREE:
                levels[2].gameObject.SetActive(true);
                ultimateButton.SetActive(false);
                break;
            case Player.phases.ULTI:
                ultimateButton.SetActive(true);
                break;
                
        }

    }

    private void FixedUpdate()
    {
        
    }

    void BossDefeated()
    {
        StopAllCoroutines();
        StartCoroutine(BossDefeatedCo());
    }

    void Ready()
    {
        //Time.timeScale = 0f;
        cooldown += Time.unscaledDeltaTime * (1.0f / 0.2f);

        if (cooldown > 5f)
        {
            ready.SetActive(false);
            fight.SetActive(true);
            
            onReady = false;
            onFight = true;
        }
    }

    void Fight()
    {
        cooldown2 += Time.unscaledDeltaTime * (1.0f / 0.2f);

        if (cooldown2 > 2f)
        {
            onFight = false;
            fight.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    

    IEnumerator BossDefeatedCo()
    {
        if(indexBoss < bossPrefabs.Length - 1)
        {
            indexBoss++;
        }
        
        counterBoss++;
        counterBossTxt.text = counterBoss.ToString();

        multHealth += increaseHealth;
        multHealth = Mathf.Clamp(multHealth, 1, maxHealtMult);

        multSpeed += increaseSpeed;
        multSpeed = Mathf.Clamp(multSpeed, 1, maxSpeedMult);

        multWave -= decreaseWave;
        multWave = Mathf.Clamp(multWave, 1, minWaveMult);

        Debug.Log(multWave);
        Debug.Log(multSpeed);
        Debug.Log(multHealth);
        boss.Dead();
        yield return new WaitForSeconds(1.34f);

        Vector3 pos = boss.transform.position;
        Destroy(boss.gameObject);
        knokedImage.SetActive(true);

        yield return new WaitForSeconds(2f);

        List<GameObject> nuvole = new List<GameObject>();
        foreach(GameObject go in nuvolePositions)
        {
            nuvole.Add(Instantiate(nuvola, go.transform.position, Quaternion.identity));
        }

        knokedImage.SetActive(false);
        boss = Instantiate(bossPrefabs[indexBoss], pos, Quaternion.identity).GetComponent<Boss>();
        bosshaha.Play();
        boss.gameManager = this;
        boss.maxHealth *= multHealth;
        boss.currentHealth = boss.maxHealth;
        isBossLive = true;

        yield return new WaitForSeconds(0.35f);

        foreach(GameObject go in nuvole)
        {
            Destroy(go);
        }
    }

    public void Ultimate()
    {
        player.DoUltimate();
        boss.currentHealth = 0;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);        
    }
    
}