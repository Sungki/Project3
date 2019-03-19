using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMove : MonoBehaviour {

    float delta;
    public float rotationSpeed;

    public GameObject wrist;
    public GameObject elbow;



	// Use this for initialization
	void Start () {
        rotationSpeed = 100;
	}
	
	// Update is called once per frame
	void Update () {
        Rotation();
	}

    void Rotation()
    {
        delta = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        //float z = wrist.transform.rotation.z;
        wrist.transform.Rotate(new Vector3(0, 0, -delta));
        elbow.transform.Rotate(new Vector3(0, 0, delta));
    }
   
}
