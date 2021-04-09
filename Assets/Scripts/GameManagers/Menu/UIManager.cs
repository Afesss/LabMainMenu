using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject options;
    [SerializeField] SpriteRenderer storm;
    private string currentBackColor;
    public string CurrentBackColor
    {
        get { return currentBackColor; }
        set { currentBackColor = value; }
    }
    private void Start()
    {
        StartCoroutine(Storm());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.Pause();
            StartCoroutine(Storm());
        }
            
    }
    public void NewGame()
    {
        GameManager.Instance.StartGame();
        StartCoroutine(Storm());
    }
    public void Continue()
    {
        GameManager.Instance.ContinueGame();
        StartCoroutine(Storm());
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        StartCoroutine(Storm());
    }
    public void Exit()
    {
        Application.Quit();
        StartCoroutine(Storm());
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        StartCoroutine(Storm());
    }
    public void MainMenuTrigger()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            mainMenu.SetActive(false);
        }
    }
    IEnumerator Storm()
    {
        storm.enabled = true;
        yield return new WaitForSeconds(0.1f);
        storm.enabled = false;
    }
}
