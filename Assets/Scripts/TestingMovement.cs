using UnityEngine;
using System.Collections;

public class TestingMovement : MonoBehaviour {

	public Vector3 nextWayPoint;
	public int next = 1;

	public int speedOffset = 20;

	// Use this for initialization
	void Start () {
		this.transform.position = WayPointGenerator.wayPoints [0];
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.RotateAround (WayPointGenerator.center, Vector3.up, speedOffset * Time.deltaTime);
		
	}

	void OnTriggerExit(Collider c){
		next += 1;
		if (next > WayPointGenerator.wayPoints.Length) {
			next = 0;
		}
	}
}
