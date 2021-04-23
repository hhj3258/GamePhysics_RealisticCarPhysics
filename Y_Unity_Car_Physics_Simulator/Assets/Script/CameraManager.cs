using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    public float distance = 3f;
    public float height = 2f;
    public float dampening = 0.1f;
    public float h2 = 0f;
    public float d2 = 0f;
    public float l = 0f;

    int camMode = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = (camMode + 1) % 2;
        }

        switch (camMode)
        {
            case 1:
                this.transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(l, h2, -d2));
                this.transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 80f;
                break;

            default:
                this.transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)), Time.deltaTime * dampening);
                //this.transform.position = Vector3.Lerp(transform.position, transform.position, Time.deltaTime * dampening);
                this.transform.LookAt(focus.transform);
                Camera.main.fieldOfView = 60f;
                break;
        }
        

        //Debug.Log(camMode);
    }
}
