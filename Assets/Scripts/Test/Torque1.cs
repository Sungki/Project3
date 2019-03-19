using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torque1 : MonoBehaviour {

    Rigidbody2D rb;
    public static bool flag;
    float nextTime = 0;
    float v = 0f;

	public float K_p = -1f;
	public float K_d = 1f;
	public float K_i = -1f;
	private Queue<float> errorQueue;
	float last_error = 0;
	private float target_angle = 0;
	private float moveHorizontal;
	private float maxX = 2.5f;
	public float max_angle = 150f;
	float initialRotation = 0;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        flag =false;

		errorQueue = new Queue<float>();
    }

	private float getNormalizedLocalRotation()
	{
		float angle = rb.transform.localEulerAngles.z;
		if (angle > 180)
		{
			angle = angle - 360;
		}
		return angle;
	}

	private void Update()
	{
	}

	void FixedUpdate () 
	{
        if (!flag)
        {
			moveHorizontal = moveHorizontal + Input.GetAxis("Vertical")*0.1f;
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
			//float error = (target_rb.rotation - target_angle);
			float derivative = 0;

			derivative = (last_error - error) / Time.fixedDeltaTime;

			float integral = 0;
			foreach (float prev_e in errorQueue)
			{
				integral += prev_e;
			}

			torque = K_p * error;
			torque += K_d * derivative;
			torque += K_i * integral;



            v = 0;
            if (Input.GetKey(KeyCode.LeftArrow))
                v = -1;
            if (Input.GetKey(KeyCode.RightArrow))
                v = 1;


            if (transform.rotation.z <= -0.99f) flag = true;

			rb.AddTorque(torque);

			last_error = error;

//			rb.AddTorque(-Input.GetAxis("Vertical") * Time.deltaTime * 100f);

//            transform.Rotate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * 50f);
//            transform.Rotate(Vector3.forward * v * Time.deltaTime * 50f);

            nextTime = Time.time + 0.5f;
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
