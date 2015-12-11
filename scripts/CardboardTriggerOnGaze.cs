using UnityEngine;
using System.Collections;

public class CardboardTriggerOnGaze : MonoBehaviour 
{
	public float lookAtInterval = 1;
	public Material timerMaterial;
	public bool displayTimer = true;

	CardboardHead head;
	GameObject lookAtObject;
	float lookAtStarted;
	GameObject timerObject;
	MeshFilter timerMeshFilter;

	void Start () 
	{
		head = GetComponent<CardboardHead> ();
		lookAtStarted = -1;

		createTimer ();
	}

	void Update () 
	{
		if (head == null) 
		{
			Debug.Log ("CardboardTriggerGaze - Error: head not fount");
			return;
		}

		Ray gaze = head.Gaze;
		RaycastHit hit;
		bool foundEventListener = false;

		if (Physics.Raycast (gaze.origin, gaze.direction, out hit, 100)) 
		{
			//Debug.Log (lookAtObject + "  " + hit.collider.gameObject);
			if (hit.collider.gameObject.GetComponent<CardboardEventListener>() != null) foundEventListener = true;
			if (lookAtObject != hit.collider.gameObject) 
			{				
				if (lookAtObject != null) 
				{
					CardboardEventListener[] pListeners = lookAtObject.GetComponents<CardboardEventListener> ();
					foreach (CardboardEventListener l in pListeners) 
					{
						l.OnOut ();
					}
				}

				lookAtObject = hit.collider.gameObject;
				lookAtStarted = Time.time;

				CardboardEventListener[] listeners = lookAtObject.GetComponents<CardboardEventListener> ();
				foreach (CardboardEventListener l in listeners) 
				{								
					l.OnOver ();
				}					
			}
		}
		else 
		{
			if (lookAtObject != null) 
			{
				CardboardEventListener[] listeners = lookAtObject.GetComponents<CardboardEventListener> ();
				foreach (CardboardEventListener l in listeners) 
				{
					l.OnOut ();
				}
			}
			lookAtObject = null;
		}

		if (lookAtObject && lookAtStarted != -1 && foundEventListener) 
		{
			float d = (Time.time - lookAtStarted) / lookAtInterval;

			if (displayTimer) updateTimer (d);

			if (d >= 1) {
				lookAtStarted = -1;
				CardboardEventListener[] listeners = lookAtObject.GetComponents<CardboardEventListener> ();
				foreach (CardboardEventListener l in listeners) {
					l.OnTrigger (head);
				}	
			}
		}
		else 
		{
			if (displayTimer)
				updateTimer (0);
		}
	}

	void createTimer()
	{
		timerObject = new GameObject ();
		MeshRenderer mr = timerObject.AddComponent<MeshRenderer> ();
		timerMeshFilter = timerObject.AddComponent<MeshFilter> ();

		Material mat = new Material (Shader.Find ("Diffuse"));
		mat.color = Color.black;
		mr.material = mat;

		updateTimer (0);
	}

	void updateTimer(float pct)
	{
		timerObject.SetActive (pct > 0);

		if (pct > 1)
			pct = 1;
		if (pct < 0)
			pct = 0;
		
		if (pct > 0) {
			
			Mesh mesh = new Mesh ();

			int n = 100;
			float r = 0.05f;

			float step = Mathf.PI * 2 / (float)n;
			Vector3[] newVertices = new Vector3[n * 3];
			int[] newTriangles = new int[n * 3];

			for (int i = 0; i < n * pct; i++) {
				int index = i * 3;
				float a = -i * step + Mathf.PI / 2f;
				float aa = -i * step + step + Mathf.PI / 2f;

				newVertices [index + 0] = new Vector3 (0f, 0f, 0f);
				newVertices [index + 1] = new Vector3 (Mathf.Cos (aa) * r, Mathf.Sin (aa) * r, 0f);
				newVertices [index + 2] = new Vector3 (Mathf.Cos (a) * r, Mathf.Sin (a) * r, 0f);

				newTriangles [index + 0] = index + 0;
				newTriangles [index + 1] = index + 1;
				newTriangles [index + 2] = index + 2;
			}

			mesh.vertices = newVertices;
			mesh.triangles = newTriangles;

			timerMeshFilter.mesh = mesh;

			timerObject.transform.position = head.transform.position + (lookAtObject.transform.position - head.transform.position).normalized * 0.6f;
			timerObject.transform.rotation = head.transform.rotation;
				
		}
	}
}
