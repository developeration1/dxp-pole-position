using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Anticube : MonoBehaviour
{
    [SerializeField]
    float speed = 0, maxSpeed = 0, minSpeed = 0;

    CharacterController charCont;

    void Start()
    {
        try
        {
            charCont = GetComponent<CharacterController>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("There is no Character Controller");
        }

        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        charCont.Move((Vector3.back * GlobalVariables.gameSpeed + new Vector3(0, 0, speed)) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalVariables.gameStatus = GameStatus.GameOver;
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
