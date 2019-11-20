using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
	public bool isCursorHidden = true;
	public float minPitch = -20f, maxPitch = 80f;
	public Vector2 speed = new Vector2(120f, 120f);
	
	private Vector2 euler;
    // Start is called before the first frame update
    void Start()
    {
        if(isCursorHidden)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		
		euler = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        euler.y += Input.GetAxis("Mouse X") * speed.x * Time.deltaTime;
        euler.x -= Input.GetAxis("Mouse Y") * speed.y * Time.deltaTime;
		
		euler.x = Mathf.Clamp( euler.x, minPitch, maxPitch);
		
		transform.parent.localEulerAngles = new Vector3(0, euler.y, 0);
		transform.localEulerAngles = new Vector3(euler.x, 0, 0);
		
    }
}
