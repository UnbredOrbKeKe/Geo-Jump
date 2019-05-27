using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : Player {
	public Transform target;
    
	//public float smoothSpeed = 0.125f;
	public float offset;

	private void LateUpdate()
	{
		transform.position = new Vector3(target.position.x + offset, transform.position.y, transform.position.z);
	}

}


