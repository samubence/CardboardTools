using UnityEngine;
using System.Collections;

public class CardboardEventHightlight : MonoBehaviour, CardboardEventListener 
{
	public Material matHightlight;
	Material matOrig;
	Renderer renderer;

	void Start()
	{
		renderer = GetComponent<Renderer> ();
		if (renderer != null)
		{			
			matOrig = renderer.material;
		}
		if (matHightlight == null) 
		{
			matHightlight = new Material (Shader.Find ("Diffuse"));
			matHightlight.color = Color.cyan;
		}
	}
	void CardboardEventListener.OnTrigger (CardboardHead head)
	{
		
	}
	void CardboardEventListener.OnOver ()
	{
		if (renderer != null) 
		{
			renderer.material = matHightlight;
		}
	}
	void CardboardEventListener.OnOut ()
	{
		if (renderer != null) 
		{
			renderer.material = matOrig;
		}
	}
}
