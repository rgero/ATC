using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointGenerator : MonoBehaviour {

	public int numberOfPoints;
	public float radius;
	public static Vector3 center;
	public static Vector3[] wayPoints;
	public GameObject[] wayPointGameObjects;
	public GameObject prefab;
	public bool landingTriggered;
	public Material TARGET_MATERIAL;

	Vector3[] calculateWayPoints(int number){
		//This is going to be used to calculate the way points for the plane.

		Vector3[] storage = new Vector3[number];

		float desiredAngle = 360.0f / number;
		float desiredAngleRadians = desiredAngle * Mathf.PI / 180;


		for (int i = 0; i < number; i++) {

			float wayPointX = radius * Mathf.Cos (desiredAngleRadians*i);
			float wayPointZ = radius * Mathf.Sin (desiredAngleRadians*i);

			storage [i] = new Vector3 (wayPointX, center.y, wayPointZ);

		}

		return storage;
	}

	List<int> findClosestNodes (Vector3[] points)
	{
		GameObject runway = GameObject.Find ("Runway");
		BoxCollider[] ends = runway.GetComponentsInChildren<BoxCollider> ();
		int closestNode = int.MinValue;

		List<int> closestNodes = new List<int> ();

		foreach (BoxCollider i in ends)
		{
			Vector3 boxPosition = i.transform.position;
			float closestDistance = float.MaxValue;

			for (int j = 0; j < wayPoints.Length; j++) {
				float distance = Vector3.Distance (boxPosition, wayPoints [j]);
				if ( distance < closestDistance) {
					closestDistance = distance;
					closestNode = j;
				}
			}

			closestNodes.Add (closestNode);
		}

		return closestNodes;

	}



	// Use this for initialization
	void Start () {

		GameObject holder = new GameObject ();
		holder.name = "Way Point Holder";

		center = new Vector3 (0, 100, 0);


		wayPoints = calculateWayPoints (numberOfPoints);

		wayPointGameObjects = new GameObject[wayPoints.Length];

		for (int j = 0; j < wayPoints.Length; j++) {
			GameObject temp = Instantiate (prefab) as GameObject;
			temp.transform.position = wayPoints [j];
			temp.transform.parent = holder.transform;
			wayPointGameObjects [j] = temp;
		}



	}
		
	
	// Update is called once per frame
	void Update () {

		if (landingTriggered) {
			List<int> closestWaypoints = findClosestNodes (wayPoints);

			foreach (int i in closestWaypoints) {
				wayPointGameObjects [i].gameObject.GetComponent<MeshRenderer> ().material = TARGET_MATERIAL;
			}
		}
	
	}
}
