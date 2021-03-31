using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float speed;
    [SerializeField] private float zoomspeed;
    [SerializeField] private float sensibility = 0.1f;

    private Vector2Int screenbounds;
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        if (!(_camera is null)) screenbounds = new Vector2Int(_camera.pixelWidth, _camera.pixelHeight);
        
    }

    void Update()
    {
        if (!UiManager.Instance.inQuitMenu)
        {
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 4f, 100f);

            //Debug.Log("Position cursor " + Input.mousePosition);
            if (Input.mousePosition.x < 40 && transform.position.x > -60)
            {
                transform.Translate(Vector3.left * speed * sensibility);
            }
            else if (Input.mousePosition.x > screenbounds.x - 40 && transform.position.x < 60)
            {
                transform.Translate(-Vector3.left * speed * sensibility);
            }

            if (Input.mousePosition.y < 40 && transform.position.z > -60)
            {
                transform.Translate(-Vector3.up * speed * sensibility);
            }
            else if (Input.mousePosition.y > screenbounds.y - 40 && transform.position.z < 60)
            {
                transform.Translate(Vector3.up * speed * sensibility);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0 && _camera.fieldOfView > 4f)
            {
                _camera.fieldOfView -= zoomspeed * sensibility;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && _camera.fieldOfView < 100f)
            {
                _camera.fieldOfView += zoomspeed * sensibility;
            }
        }
    }
}
