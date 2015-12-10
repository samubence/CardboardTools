using UnityEngine;
using System.Collections;

public class CardboardEventDestroy : MonoBehaviour, CardboardEventListener 
{
	void CardboardEventListener.OnTrigger (CardboardHead head)
	{
		Destroy (gameObject);
	}

	void CardboardEventListener.OnOver ()
	{
		
	}

	void CardboardEventListener.OnOut ()
	{
		
	}
}
