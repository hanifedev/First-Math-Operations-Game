using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startBtn, exitBtn; 


    [SerializeField]
    AudioSource audioSource;

    public AudioClip backgroundSound;

    public AudioClip buttonSound;

    private void Awake()
    {        
        audioSource = GetComponent<AudioSource>();  
        audioSource.PlayOneShot(backgroundSound);     
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    void FadeOut()
    {
       startBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
       exitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }

    public void exitGame()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }

    public void startGameLevel()
    {
        audioSource.PlayOneShot(buttonSound);
        SceneManager.LoadScene("GameLevelScene");      
    }
}
