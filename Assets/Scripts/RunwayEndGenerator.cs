using UnityEngine;
using System.Collections;

public class RunwayEndGenerator : MonoBehaviour {

	private Transform runwayTransform;
	public float runwayLength; 
	public float runwayWidth;
	public GameObject RunwayEndPrefab;

	// Use this for initialization
	void Start () {

		runwayTransform = this.GetComponent<Transform> ();

		runwayLength = runwayTransform.localScale.z;
		runwayWidth = runwayTransform.localScale.x;

		GameObject end1 = Instantiate (RunwayEndPrefab) as GameObject;
		end1.transform.parent = this.transform;
		end1.name = "End 1";
		end1.transform.localPosition = new Vector3 (0, 0, -5);

		GameObject end2 = Instantiate (RunwayEndPrefab) as GameObject;
		float boxColliderZ = end2.GetComponent<BoxCollider> ().size.z;
		end2.transform.parent = this.transform;
		end2.name = "End 2";
		end2.transform.localPosition = new Vector3 (0, 0, 5);

	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
