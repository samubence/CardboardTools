using UnityEngine;
using System.Collections;

public class CardboardEventLoadLevel : MonoBehaviour, CardboardEventListener 
{
	public string levelToLoad;

	public void OnTrigger (CardboardHead head)
	{
		Application.LoadLevel (levelToLoad);
	}
	public void OnOver ()
	{

	}
	public void OnOut ()
	{

	}
}
