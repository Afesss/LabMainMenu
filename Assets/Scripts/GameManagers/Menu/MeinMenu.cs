using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeinMenu : MonoBehaviour
{
    [SerializeField] Animation menuAnim;
    [SerializeField] AnimationClip animOutMenuBlack;
    [SerializeField] AnimationClip animInMenuBlack;
    [SerializeField] AnimationClip animInMenuGray;
    [SerializeField] AnimationClip animOutMenuGray;

    [SerializeField] AudioSource audioMenu;
    private float currentValume;
    private void Start()
    {
        GameManager.Instance.gameStateChange.AddListener(StateHandler);
        gameObject.SetActive(true);
    }
    
    public void MenuOut()
    {
        PlayClipOut();
        currentValume = audioMenu.volume;
        StartCoroutine(SoundsDown());
    }
    public void Paused()
    {
        gameObject.SetActive(true);
        PlayClipIn();
        StartCoroutine(SoundUp());
    }
    private void PlayClipIn()
    {
        menuAnim.Stop();
        if (UIManager.Instance.CurrentBackColor == "Gray")
            menuAnim.clip = animInMenuGray;
        else
            menuAnim.clip = animInMenuBlack;
        menuAnim.Play();
    }
    private void PlayClipOut()
    {
        menuAnim.Stop();
        if (UIManager.Instance.CurrentBackColor == "Gray")
            menuAnim.clip = animOutMenuGray;
        else
            menuAnim.clip = animOutMenuBlack;
        menuAnim.Play();
    }
    public void StateHandler(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (currentState == GameManager.GameState.PAUSE && previousState == GameManager.GameState.RUNNING)
        {
            Paused();
        }
        if(currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.PREGAME ||
            currentState == GameManager.GameState.RUNNING && previousState == GameManager.GameState.PAUSE)
        {
            MenuOut();
        }
    }
    IEnumerator SoundsDown()
    {
        yield return new WaitForSeconds(0.1f);
        audioMenu.volume *= 0.8f;

        if (audioMenu.volume > 0.01)
            StartCoroutine(SoundsDown());
    }
    IEnumerator SoundUp()
    {
        yield return new WaitForSeconds(0.1f);
        audioMenu.volume *= 1.1f;

        if(audioMenu.volume < currentValume)
        {
            StartCoroutine(SoundUp());
        }
    }
}
