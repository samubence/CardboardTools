using UnityEngine;
using System.Collections;

public interface CardboardEventListener 
{
	void OnTrigger(CardboardHead head);
	void OnOver();
	void OnOut();
}
