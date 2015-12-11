using UnityEngine;
using System.Collections;

public class CardboardEventAnimate : MonoBehaviour, CardboardEventListener {

	public GameObject objectToAnimate;
	public string nameOfAnimation;
	public bool destroyAfterCollision = false;

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnTrigger (CardboardHead head)
	{
		playAnimation ();
	}

	public void OnOver ()
	{

	}

	public void OnOut ()
	{

	}

	void playAnimation ()
	{		
		Animator animator = objectToAnimate.GetComponent<Animator> ();

		if (animator != null) 
		{			
			animator.CrossFade (nameOfAnimation, 0);		
		}

		if (destroyAfterCollision == true) 
		{
			Destroy (gameObject);
		}

	}
}
