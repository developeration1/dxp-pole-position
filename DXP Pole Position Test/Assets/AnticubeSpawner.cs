using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnticubeSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _anticube = null;

    [SerializeField]
    float _spawnRate = 0, _spawnZPosition = 0;

    float _spawnTimer = 0;

    private void OnEnable()
    {
        if(_anticube == null)
        {
            Debug.LogError("There is no Anticube");
            enabled = false;
        }
    }

    void Update()
    {
        if(Time.time >= _spawnTimer && GlobalVariables.gameStatus == GameStatus.Playing)
        {
            _spawnTimer = Time.time + _spawnRate;
            GameObject.Instantiate(_anticube, new Vector3(Random.Range(-GlobalVariables.maxPosition, GlobalVariables.maxPosition), 0, _spawnZPosition), Quaternion.identity);
        }
    }
}
