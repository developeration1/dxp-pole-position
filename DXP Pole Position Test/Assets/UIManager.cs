using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _pointsText = null;

    [SerializeField]
    GameObject _gameOverScreen = null;

    [SerializeField]
    GameObject _pauseScreen = null;

    private void OnEnable()
    {
        if(_pointsText == null || _gameOverScreen == null || _pauseScreen == null)
        {
            Debug.LogError("There is or are references lacking.");
            enabled = false;
        }
    }

    public void UpdatePoints(float points)
    {
        _pointsText.text = points + " Km";
    }

    public void SetScreenByActiveStatus()
    {
        switch (GlobalVariables.gameStatus)
        {
            case GameStatus.Paused:
                _gameOverScreen.SetActive(false);
                _pauseScreen.SetActive(true);
                break;
            case GameStatus.Playing:
                _gameOverScreen.SetActive(false);
                _pauseScreen.SetActive(false);
                break;
            case GameStatus.GameOver:
                _gameOverScreen.SetActive(true);
                _pauseScreen.SetActive(false);
                break;
            case GameStatus.Menu:
                _gameOverScreen.SetActive(true);
                _pauseScreen.SetActive(false);
                break;
            default:
                Debug.LogError("No status selected");
                _gameOverScreen.SetActive(false);
                _pauseScreen.SetActive(false);
                break;
        }
    }
}
