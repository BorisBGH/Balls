using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private float _sensitivity = 25f;
    [SerializeField] private float _maxXPos = 2.5f;
    private float _xPosition;
    private float _oldMouseX;
    private float _delta;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMouseX = Input.mousePosition.x;

        }

        if (Input.GetMouseButton(0))
        {
            _delta = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPosition += _delta * _sensitivity / Screen.width;
            _xPosition = Mathf.Clamp(_xPosition, -_maxXPos, _maxXPos);
            transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);

        }


    }
}
