using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

	public GameObject go;
	public HingeJoint2D hj;
	public FixedJoint2D fj;
	public bool grabbed;

	public ServoController right;
	public ServoController1 right1;
	public LeftServoController left;
	public LeftServoController1 left1;

	float v = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightShift))
		{
			ToggleGrab();
		}
	}

	public void ToggleGrab() {
        Debug.Log("Toggle grab");
		if (grabbed) {
			UnGrab ();
		} else {
			Grab ();
		}
	}

	public void Grab() {

		Collider2D[] colliders = new Collider2D[10];
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.useTriggers = true;
		int num_collisions = go.GetComponent<CircleCollider2D>().OverlapCollider(contactFilter, colliders);
//        int num_collisions = go.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders);

        //Debug.Log("Number of collisions " + num_collisions);
        if (num_collisions > 0)
		{
			for (int j = 0; j < num_collisions; j++)
			{
				if (colliders[j].gameObject == go)
					continue;
//				if (colliders[j].gameObject == go.transform.parent.gameObject)
//					continue;
				
//				hj = go.AddComponent<HingeJoint2D>() as HingeJoint2D;
//				hj.connectedBody = colliders[j].GetComponent<Rigidbody2D>();
				//hj.connectedAnchor = go.transform.position - player;
//				fj = go.AddComponent<FixedJoint2D>() as FixedJoint2D;
//				fj.connectedBody = colliders[j].GetComponent<Rigidbody2D>();
				grabbed = true;
				break;
			}
		}
	}

	public void UnGrab() {
		Destroy (hj);
		grabbed = false;
		right.lockJoint();
		right.drag_state = 0;
		right1.lockJoint();
		right1.drag_state = 0;
		left.lockJoint();
		left.drag_state = 0;
		left1.lockJoint();
		left1.drag_state = 0;
	}


}
