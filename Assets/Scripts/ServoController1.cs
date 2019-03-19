using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ServoController1 : MonoBehaviour {

    public GameObject target_go;
    private Rigidbody2D target_rb;
    private HingeJoint2D target_joint;
	private FixedJoint2D fixed_joint;

    public GameObject elbow;
    public GameObject wrist;

    public Vector2 touchPosWorld2D;
    private float moveHorizontal;
    private float target_angle = 0;
    public int drag_state = 0;
    public GameObject go;
    private Vector2 touchStartPos;
	private float maxX = 2.5f;
	private Queue<float> errorQueue;
	float initialRotation = 0;
	float last_error = 0;
	public float K_p = -1f;
	public float K_d = 1f;
    public float K_i_q_size = 20;
    public float K_i = -1f;
	public float max_angle = 150f;
//    GraphicRaycaster rc;
	float v = 0f;
    Vector2 PrevPoint;


    public Grabber grab;
    public Grabber1 grab1;


    private void Start()
    {
//        rc = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<GraphicRaycaster>();
        target_rb = target_go.GetComponent<Rigidbody2D>();
		lockJoint();
		errorQueue = new Queue<float>();
    }

    private float getNormalizedLocalRotation()
    {
        target_rb = target_go.GetComponent<Rigidbody2D>();
        float angle = target_rb.transform.localEulerAngles.z;
        if (angle > 180)
        {
            angle = angle - 360;
        }
        return angle;
    }

	public void lockJoint() {

		fixed_joint = target_go.AddComponent<FixedJoint2D>() as FixedJoint2D;
		fixed_joint.connectedBody = target_go.transform.parent.GetComponent<Rigidbody2D> ();
	}

	private void unlockJoint() {
		Destroy (fixed_joint);
	}

    private void Update()
    {
		v = 0;
		if (Input.GetKey(KeyCode.LeftArrow))
			v = -1;
		if (Input.GetKey(KeyCode.RightArrow))
			v = 1;

        if (!grab.grabbed && !grab1.grabbed)
        {
            if (v != 0 && drag_state == 0)
            {
                initialRotation = getNormalizedLocalRotation();
                target_angle = initialRotation;
                last_error = 0;
                unlockJoint();
                drag_state = 1;
                touchStartPos = touchPosWorld2D;
                errorQueue.Clear();
            }
            else if (v != 0 && drag_state == 1)
            {
                // Dragging
                drag_state = 2;
            }
            else if (v == 0 && drag_state > 0)
            {
                // Drag ended
                drag_state = 0;
                lockJoint();
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            v = 0;

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                if (pos.x <= 900 && pos.y > 500)
                {
                    PrevPoint = pos - touch.deltaPosition;

                    if ((pos.x - PrevPoint.x) >= 0) v = 1;
                    else v = -1;

                    if (!grab.grabbed && !grab1.grabbed)
                    {
                        if (v != 0 && drag_state == 0)
                        {
                            initialRotation = getNormalizedLocalRotation();
                            target_angle = initialRotation;
                            last_error = 0;
                            unlockJoint();
                            drag_state = 1;
                            touchStartPos = touchPosWorld2D;
                            errorQueue.Clear();
                        }
                        else if (v != 0 && drag_state == 1)
                        {
                            // Dragging
                            drag_state = 2;
                        }
                        else if (v == 0 && drag_state > 0)
                        {
                            // Drag ended
                            drag_state = 0;
                            lockJoint();
                        }
                    }

                    PrevPoint = Input.GetTouch(0).position;

                }

            }
            
            /*            Vector3 touchPosWorld;
                        touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

                        TouchPhase phase = Input.GetTouch(0).phase;
                        if (phase == TouchPhase.Began && drag_state == 0)
                        {
                            Debug.Log("Began");


                            PointerEventData ped = new PointerEventData(null);
                            ped.position = Input.GetTouch(0).position;
                            List<RaycastResult> results = new List<RaycastResult>();
            //                rc.Raycast(ped, results);
                            foreach (RaycastResult rr in results)
                            {
                                if (rr.gameObject == go)
                                {
                                    initialRotation = getNormalizedLocalRotation();
                                    target_angle = initialRotation;
                                    last_error = 0;
                                    unlockJoint();
                                    drag_state = 1;
                                    touchStartPos = touchPosWorld2D;
                                    errorQueue.Clear();
                                }
                            }

                        }
                        else if (phase == TouchPhase.Moved && drag_state == 1)
                        {
                            // Dragging
                            drag_state = 2;
                        }
                        else if ((phase == TouchPhase.Ended || phase == TouchPhase.Ended) && drag_state > 0)
                        {
                            // Drag ended
                            drag_state = 0;
                            lockJoint();
                        }*/
        }



    }

    private void FixedUpdate()
    {
        if (grab.grabbed|| grab1.grabbed)
        {
            wrist.transform.Rotate(-Vector3.forward * v * Time.deltaTime * 50f);
            elbow.transform.Rotate(Vector3.forward * v * Time.deltaTime * 50f);
        }


        if (drag_state == 2) {
			moveHorizontal = v*0.9f;
//			moveHorizontal = touchPosWorld2D.x - touchStartPos.x;
//			moveHorizontal = Input.GetAxisRaw("Vertical")*0.05f;
			if (moveHorizontal > 0)
				moveHorizontal = Mathf.Min (moveHorizontal, maxX);
			else if (moveHorizontal < 0)
				moveHorizontal = Mathf.Max(moveHorizontal, -maxX);
			target_angle = max_angle * moveHorizontal / maxX;

			target_angle += initialRotation;

			if (target_angle > max_angle) {
				target_angle = max_angle;
			} else if (target_angle < -max_angle) {
				target_angle = -max_angle;
			}
		
            float torque;

            float error = (getNormalizedLocalRotation() - target_angle);
            float derivative = 0;
			
			derivative = (last_error - error) / Time.fixedDeltaTime;

            float integral = 0;
            foreach (float prev_e in errorQueue)
            {
                integral += prev_e;
            }

			torque = K_p * error;
//			torque += K_d * derivative;
//            torque += K_i * integral;
	
			target_rb.AddTorque (torque);

			last_error = error;

            errorQueue.Enqueue(error);
            if (errorQueue.Count > K_i_q_size)
            {
                errorQueue.Dequeue();
            }

        }
        
    }
    
}
