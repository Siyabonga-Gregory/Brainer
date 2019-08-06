using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.tag.Equals ("Dirt")) 
		{
			Debug.Log ("Wall reach ground");
			GetComponent<Rigidbody2D> ().freezeRotation = true;
		}
	}
}
