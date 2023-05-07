using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Define.GameState _currentGameState = Define.GameState.Ready;
    public bool GameStateReady => _currentGameState == Define.GameState.Ready;
    public bool GameStatePlay => _currentGameState == Define.GameState.Play;
    public bool GameStateEnd => _currentGameState == Define.GameState.End;

    private UI_GameScene _uiGameScene = null;

    public UI_GameScene GameSceneUI { get { CheckNull(); return _uiGameScene; } }

    private Cannon _playerCannon = null;
    public Cannon PlayerCannon
    {
        get => _playerCannon;
    }
    private Cannon _enemyCannon = null;
    public Cannon EnemyCannon
    {
        get => _enemyCannon;
    }

    private Stage _currentStage = null;
    public Stage CurrentStage
    {
        get => _currentStage;
    }

    public void Init()
    {
        GameReady();
    }

    private void CheckNull()
    {
        _uiGameScene = FindObjectOfType<UI_GameScene>();
    }

    public void GameReady()
    {
        CheckNull();
        StageInit();

        _playerCannon = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Cannon>();
        _enemyCannon = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<Cannon>();

        _currentGameState = Define.GameState.Ready;
    }

    public void GamePlay()
    {
        _currentGameState = Define.GameState.Play;
    }

    public void GameEnd()
    {
        _currentGameState = Define.GameState.End;
    }

    private void StageInit()
    {
        _currentStage = Managers.Resource.Instantiate("Stage")?.GetComponent<Stage>();
        _currentStage.transform.localPosition = Vector3.zero;
        _currentStage.transform.localRotation = Quaternion.identity;
        _currentStage.Init();
    }

    public void Clear()
    {
        if (_currentStage != null)
        {
            Destroy(_currentStage.gameObject);
            _currentStage = null;
        }

        GameReady();
    }
}
