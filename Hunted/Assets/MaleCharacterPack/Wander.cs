using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
public class Wander : MonoBehaviour
{
	private Animator source;
	private NavMeshAgent nav;
	private ArrayList points;
	private Vector3 dest;

	void Awake ()
	{
		source = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent> ();

		// Way points
		points = new ArrayList () {
			new Vector3(-38,0,24), 	// Libary 
			new Vector3(0,0,0)	 	// Origin
		};

		dest = (Vector3) points [0];
	}
	
	void Update ()
	{
		nav.SetDestination (dest);

		if (nav.nextPosition.Equals (dest)) {
			dest = (Vector3) points[1];
		}
	}
}