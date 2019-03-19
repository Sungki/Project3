using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;

	public GameObject RightBone1;
	public GameObject RightBone2;
	public GameObject RightJoint1;
	public GameObject RightJoint2;

	public GameObject LeftBone1;
	public GameObject LeftBone2;
	public GameObject LeftJoint1;
	public GameObject LeftJoint2;


	bool Rightflag = false;
	bool Leftflag = false;

	Vector2 PrevPoint;

	void Start () {

	}

	void Update () {
//		if (Input.GetKeyDown(KeyCode.RightShift))
//		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);
			
				if (touch.phase == TouchPhase.Moved)
				{
					Vector2 pos = touch.position;

					if(pos.x>=1100 && pos.y <=150)
					{
							if (!Rightflag)
							{
								RightJoint1.transform.parent = null;
								RightJoint2.transform.parent = null;
								RightBone1.transform.parent = null;
								RightBone2.transform.parent = null;

								RightBone1.transform.parent = RightJoint2.transform;
								RightJoint1.transform.parent = RightBone1.transform;
								player.transform.parent = RightJoint1.transform;

								Rightflag = true;
							}
							else
							{
								RightJoint1.transform.parent = null;
								RightJoint2.transform.parent = null;
								RightBone1.transform.parent = null;
								RightBone2.transform.parent = null;
								player.transform.parent = null;

								RightBone2.transform.parent = RightJoint2.transform;
								RightJoint2.transform.parent = RightJoint1.transform;
								RightBone1.transform.parent = RightJoint1.transform;
								RightJoint1.transform.parent = player.transform;

								Rightflag = false;
							}
					}


					if(pos.x<=500 && pos.y <=150)
					{
						if (!Leftflag)
						{
							LeftJoint1.transform.parent = null;
							LeftJoint2.transform.parent = null;
							LeftBone1.transform.parent = null;
							LeftBone2.transform.parent = null;

							LeftBone1.transform.parent = LeftJoint2.transform;
							LeftJoint1.transform.parent = LeftBone1.transform;
							player.transform.parent = LeftJoint1.transform;

							Leftflag = true;
						}
						else
						{
							LeftJoint1.transform.parent = null;
							LeftJoint2.transform.parent = null;
							LeftBone1.transform.parent = null;
							LeftBone2.transform.parent = null;
							player.transform.parent = null;

							LeftBone2.transform.parent = LeftJoint2.transform;
							LeftJoint2.transform.parent = LeftJoint1.transform;
							LeftBone1.transform.parent = LeftJoint1.transform;
							LeftJoint1.transform.parent = player.transform;

							Leftflag = false;
						}
					}

				}
			}
//		}

//		if (Input.GetKeyDown(KeyCode.LeftShift))
//		{
//		}
	}
}
