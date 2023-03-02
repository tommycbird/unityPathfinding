using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 80f;

    // Update is called once per frame
    void Update()
    {
         // Move the camera up, down, left, and right with arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);

        // Zoom in and out with mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float zoomAmount = Camera.main.orthographicSize - (scrollInput * zoomSpeed);
        Camera.main.orthographicSize = Mathf.Clamp(zoomAmount, minZoom, maxZoom);
    }
}

