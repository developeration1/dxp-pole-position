using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIManager))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameStatus _gameStatus = GameStatus.Playing;
    GameStatus? _prevStatus = null;

    [SerializeField]
    float _gameSpeed = 0, _points = 0, _pointRate = 0, _pointTimer = 0;

    [SerializeField]
    GameObject _player = null;

    UIManager _uiManager;

    void Start()
    {
        try
        {
            _uiManager = GetComponent<UIManager>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("There is no UI Manager");
        }

        GlobalVariables.gameStatus = _gameStatus;
    }

    private void OnEnable()
    {
        if(_player == null)
        {
            Debug.LogError("There is no Player reference");
            enabled = false;
        }
    }

    void Update()
    {
        if (GlobalVariables.gameStatus != _gameStatus && GlobalVariables.gameStatus == _prevStatus)
        {
            GlobalVariables.gameStatus = _gameStatus;
        }

        if (GlobalVariables.gameStatus != _gameStatus && _gameStatus == _prevStatus)
        {
            _gameStatus = (GameStatus)GlobalVariables.gameStatus;
        }

        _uiManager.SetScreenByActiveStatus();

        switch (_gameStatus)
        {
            case GameStatus.Paused:
                if (_prevStatus != _gameStatus)
                {
                    Time.timeScale = 0;
                }
                break;
            case GameStatus.Playing:
                if(_prevStatus != _gameStatus)
                {
                    Time.timeScale = 1;
                    GlobalVariables.gameSpeed = _gameSpeed;
                }

                if (Time.time >= _pointTimer && Application.isPlaying)
                {
                    _pointTimer = Time.time + _pointRate;
                    _points += .1f;
                    _uiManager.UpdatePoints(_points);
                }
                if (!Application.isPlaying)
                {
                    _points = 0;
                    _uiManager.UpdatePoints(0);
                    _pointTimer = 0;
                }

                break;
            case GameStatus.GameOver:
                if(_prevStatus != _gameStatus)
                {
                    Time.timeScale = 1;
                    GlobalVariables.gameSpeed = 0;
                }
                break;
            case GameStatus.Menu:
                break;
            default:
                Debug.LogError("No status selected");
                Time.timeScale = 0;
                break;
        }

        if (_prevStatus != _gameStatus || _prevStatus == null)
        {
            _prevStatus = _gameStatus;
        }
    }

    public void PauseGame()
    {
        GlobalVariables.gameStatus = GameStatus.Paused;
    }

    public void ResumeGame()
    {
        GlobalVariables.gameStatus = GameStatus.Playing;
    }

    public void ResetGame()
    {
        GameObject[] anticubes = GameObject.FindGameObjectsWithTag("Anticube");
        foreach (var anticube in anticubes)
        {
            Destroy(anticube);
        }
        _player.SetActive(true);
        _player.transform.position = new Vector2(0, _player.transform.position.y);
        _points = 0;
        _uiManager.UpdatePoints(_points);
        GlobalVariables.gameStatus = GameStatus.Playing;
    }
}

public static class GlobalVariables
{
    public static float gameSpeed = 0;
    public static GameStatus? gameStatus = null;
    public const float maxPosition = 8.5f;
}