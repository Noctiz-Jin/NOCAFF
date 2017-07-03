using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOCHandle : MonoBehaviour {

	void Start () {
		
	}
	
	public void GrabHandle(Transform transform, Transform parent)
	{
		gameObject.transform.position = transform.position;
		gameObject.transform.rotation = transform.rotation;
		gameObject.transform.SetParent(parent);
	}
}
