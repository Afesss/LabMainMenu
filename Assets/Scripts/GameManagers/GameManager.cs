using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
[System.Serializable] public class GameStateChange : UnityEvent<GameManager.GameState, GameManager.GameState> { }
public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSE
    }

    [SerializeField] private GameObject[] systemPrefabs;


    private GameState currentGameState = GameState.PREGAME;
    private List<GameObject> instancedSystemPrefabs;
    private List<AsyncOperation> loadOperations;

    private string currentLevelName = string.Empty;
    public GameStateChange gameStateChange;
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();
        InstatiateSystemPrefabs();
    }
    public void UpdateGameState(GameState state)
    {
        GameState previous = currentGameState;
        currentGameState = state;
        switch (currentGameState)
        {
            case GameState.PREGAME:
                Debug.Log("PREGAME");
                break;
            case GameState.RUNNING:
                Debug.Log("RUNNING");
                break;
            case GameState.PAUSE:
                Debug.Log("PAUSE");
                break;
            default:
                break;
        }
        gameStateChange.Invoke(currentGameState, previous);

    }
    #region Load and Unload scene
    public void LoadScene(string sceneName)
    {
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if(asyncOperation == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + sceneName);
            return;
        }

        loadOperations.Add(asyncOperation);
        asyncOperation.completed += LoadScene_Completed;
        currentLevelName = sceneName;
    }
    private void LoadScene_Completed(AsyncOperation asyncOperation)
    {
        if (loadOperations.Contains(asyncOperation))
        {
            loadOperations.Remove(asyncOperation);
        }

        Debug.Log("Scene " + currentLevelName + " loaded");
    }
    public void UnloadScene(string sceneName)
    {

        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        if (asyncOperation == null)
        {
            Debug.LogError("[GameManager] Unable to unload level " + sceneName);
            return;
        }

        asyncOperation.completed += UnLoadScene_Completed;
    }
    
    private void UnLoadScene_Completed(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload completed");
    }
    #endregion
    private void InstatiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < systemPrefabs.Length; ++i)
        {
            prefabInstance =  Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        for(int i = 0;i < instancedSystemPrefabs.Count; ++i)
        {
            Destroy(instancedSystemPrefabs[i]);
        }
        instancedSystemPrefabs.Clear();
    }
    public void StartGame()
    {
        if (CurrentGameState != GameState.RUNNING)
        {
            if(CurrentGameState == GameState.PAUSE)
            {
                UnloadScene("Game");
            }
            LoadScene("Game");
            UpdateGameState(GameState.RUNNING);
        }
    }
    public void Pause()
    {
        if(currentGameState != GameState.PAUSE)
        UpdateGameState(GameState.PAUSE);
    }
    public void ContinueGame()
    {
        if (currentGameState == GameState.PAUSE)
            UpdateGameState(GameState.RUNNING);
    }
    
}
