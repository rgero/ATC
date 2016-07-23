using UnityEngine;
using System.Collections;

public class TestingMovement : MonoBehaviour {

	private WayPointGenerator generator;

	public int next = 0;
	public int speedOffset = 20;
	public float rotationDamping = 6.0f;
	public bool smoothFlight;
	public bool landingRequested;
	public bool permissionToLand;
	public bool landed = false;

	public GameObject parent;

	public GameObject[] waypoints;

	// Use this for initialization
	void Start () {

		generator = GameObject.Find ("PointGenerator").GetComponent<WayPointGenerator> ();

		this.transform.position = generator.wayPoints [0].transform.position;

		waypoints = generator.getWayPoints ();
		//waypoints = parent.GetComponentsInChildren<GameObject>();
		this.transform.position = waypoints[0].transform.position;

		smoothFlight = true;


	}
	
	// Update is called once per frame
	void Update () {

		GameObject nextWaypoint = waypoints [next];

		if (!landed) {
			if (smoothFlight) {
				var rotation = Quaternion.LookRotation (nextWaypoint.transform.position - this.transform.position);
				transform.rotation = Quaternion.Slerp (this.transform.rotation, rotation, Time.deltaTime * rotationDamping);
			}
			this.transform.Translate (0, 0, Time.deltaTime * speedOffset);
		}

		if (landingRequested) {
			if (permissionToLand) {
				waypoints = generator.routeToLand (next, waypoints);
			} else {
				permissionToLand = generator.requestPermission ();
			}
		}

	}

	void OnTriggerEnter(Collider c){
		if (!c.CompareTag ("Terrain")) {
			next += 1;
			if (next == waypoints.Length) {
				if (!permissionToLand) {
					next = 0;
				} else {
					landed = true;
				}
			}
		} else {
			Debug.Log ("CRASH");

		}
	}
}
