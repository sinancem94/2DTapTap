using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
    private Vector3 offset;

	void Start () 
    {
        offset = transform.position - Platform.instance.runner.transform.position;
	}
	

	void LateUpdate () 
    {
        transform.position = Platform.instance.runner.transform.position + offset;
	}
}
