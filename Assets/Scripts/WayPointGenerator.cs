using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointGenerator : MonoBehaviour {

	public int numberOfPoints;
	public float radius;
	public Vector3 center;
	public GameObject[] wayPoints;
	public GameObject prefab;
	public bool landingTriggered;
	public Material TARGET_MATERIAL;

	private GameObject holder;

	GameObject[] calculateWayPoints(int number){
		//This is going to be used to calculate the way points for the plane.

		GameObject[] storage = new GameObject[number];

		float desiredAngle = 360.0f / number;
		float desiredAngleRadians = desiredAngle * Mathf.PI / 180;


		for (int i = 0; i < number; i++) {

			float wayPointX = radius * Mathf.Cos (desiredAngleRadians*i);
			float wayPointZ = radius * Mathf.Sin (desiredAngleRadians*i);


			GameObject temp = Instantiate(prefab) as GameObject;

			if (!GameConstants.DEBUGGING) {
				temp.GetComponent<MeshRenderer> ().enabled = false;
			}


			temp.transform.position = new Vector3 (wayPointX, center.y, wayPointZ);
			temp.name = i.ToString ();
			temp.transform.parent = holder.transform;

			storage [i] = temp;

		}


		return storage;
	}

	List<int> findClosestNodes (GameObject[] objects)
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
				float distance = Vector3.Distance (boxPosition, objects[j].gameObject.transform.position);
				if ( distance < closestDistance) {
					closestDistance = distance;
					closestNode = j;
				}
			}

			closestNodes.Add (closestNode);
		}

		return closestNodes;

	}
		
	public GameObject[] getWayPoints(){
		return wayPoints;
	}

	// Use this for initialization
	void Start () {

		holder = new GameObject ();
		holder.name = "Way Point Holder";

		center = new Vector3 (0, 100, 0);
		wayPoints = calculateWayPoints (numberOfPoints);

	}
		
	
	// Update is called once per frame
	void Update () {

		if (landingTriggered) {
			List<int> closestWaypoints = findClosestNodes (wayPoints);

			foreach (int i in closestWaypoints) {
				wayPoints [i].gameObject.GetComponent<MeshRenderer> ().material = TARGET_MATERIAL;
			}
		}
	
	}
}
