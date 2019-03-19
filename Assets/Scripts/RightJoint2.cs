using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightJoint2 : MonoBehaviour {

	Rigidbody2D rb;
	public static bool flag;
	float nextTime = 0;
	float v = 0f;
	Vector2 PrevPoint;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		flag =false;
	}

	void Update () {
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (!flag)
			{
				//             rb.AddTorque(-Input.GetAxis("Vertical") * Time.deltaTime * 100f);
				v = 0;
				if (Input.GetKey(KeyCode.LeftArrow))
					v = -1;
				if (Input.GetKey(KeyCode.RightArrow))
					v = 1;
				
				if (touch.phase == TouchPhase.Moved)
				{
					Vector2 pos = touch.position;

					if(pos.x>900 && pos.y <=500)
					{
						PrevPoint = pos - touch.deltaPosition;

			//            if (transform.rotation.z <= -0.99f) flag = true;

			//            transform.Rotate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * 50f);
			//			transform.Rotate(Vector3.forward * v * Time.deltaTime * 50f);
						transform.Rotate(Vector3.forward * (pos.x-PrevPoint.x) * Time.deltaTime*5f);
						nextTime = Time.time + 0.5f;
						PrevPoint = Input.GetTouch(0).position;
					}
				}
			}
			else
			{
				if (Time.time > nextTime)
				{
					flag = false;
				}
				else
				{
					transform.Rotate(Vector3.forward * Time.deltaTime * 10f);
				}
			}
		}
	}
}
