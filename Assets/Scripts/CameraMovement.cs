using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float speed;
    [SerializeField] private float zoomspeed;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Debug.Log("Position cursor " + Input.mousePosition);
        if (Input.mousePosition.x < 40)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }else if (Input.mousePosition.x > 1880)
        {
            transform.Translate(-Vector3.left * speed * Time.deltaTime);
        }

        if (Input.mousePosition.y < 40)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }else if (Input.mousePosition.y > 1040)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _camera.fieldOfView -= zoomspeed * Time.deltaTime;
        }else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _camera.fieldOfView += zoomspeed * Time.deltaTime;
        }
    }
}
