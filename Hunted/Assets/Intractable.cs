using UnityEngine;
using System.Collections;

public class Intractable : MonoBehaviour {

	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayCastHit;
		
		if(Physics.Raycast(ray.origin, ray.direction, out rayCastHit, Mathf.Infinity))
		{
			SimpleDoor door = rayCastHit.transform.GetComponent<SimpleDoor>();
			if(door)
			{
				messageOpen();
				if(Input.GetMouseButton(0))
				{
					door.Open();
				}
			}
		}
	}
	
	void messageOpen()
	{
		GameObject go = Instantiate(new GameObject(), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity) as GameObject; 
		go.AddComponent<GUIText>();
		go.GetComponent<GUIText>().text = "Open"; 
	}
}
