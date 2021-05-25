using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TouchPlayer : MonoBehaviour
{
    [SerializeField]
    float _turnSpeed = 0;

    float _direction = 0;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("There is no Rigidbody");
        }
    }

    void Update()
    {
        _direction = Input.acceleration.x * _turnSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -GlobalVariables.maxPosition, GlobalVariables.maxPosition), 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_direction, 0); 
    }
}
