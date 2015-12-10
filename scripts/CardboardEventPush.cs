using UnityEngine;
using System.Collections;

public class CardboardEventPush : MonoBehaviour, CardboardEventListener 
{
	public float force;

	public void OnTrigger (CardboardHead head)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb != null) 
		{
			Ray ray = head.Gaze;
			rb.AddForce(ray.direction * force);
		}
	}
	public void OnOver ()
	{
		
	}
	public void OnOut ()
	{
		
	}


}
