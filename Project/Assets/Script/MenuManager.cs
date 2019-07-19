using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image tutorialImage;
    public Sprite[] tutorialImages;
    public int indexTutorial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void TutorialLeft()
    {
        if(indexTutorial > 0)
        {
            indexTutorial--;            
        }
        else
        {
            indexTutorial = tutorialImages.Length - 1;
        }

        tutorialImage.sprite = tutorialImages[indexTutorial];
    }

    public void TutorialRight()
    {
        if (indexTutorial < tutorialImages.Length-1)
        {
            indexTutorial++;
        }
        else
        {
            indexTutorial = 0;
        }

        tutorialImage.sprite = tutorialImages[indexTutorial];
    }
}
