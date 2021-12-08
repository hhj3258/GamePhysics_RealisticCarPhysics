using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewCarController : MonoBehaviour
{
    public BoxsterS car;

    float x0 = 0.0f;
    float y0 = 0.0f;
    float z0 = 0.0f;
    float vx0 = 0.0f;
    float vy0 = 0.0f;
    float vz0 = 0.0f;
    float t = 0.0f;
    float density = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        car = new BoxsterS(x0, y0, z0, vx0, vy0, vz0, t, density);

        car.S = 0.0;       //  time set to zero
        car.SetQ(0.0, 0);   //  vx0 set to zero
        car.SetQ(0.0, 1);   //  x0 set to zero
        car.OmegaE = 1000.0;
        car.GearNumber = 1;

        car.Mode = "crusing";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);

        if(Input.GetKeyDown(KeyCode.W))
        {
            car.Mode = "accelerating";
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            car.Mode = "cruising";
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            car.Mode = "braking";
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            car.Mode = "cruising";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            car.ShiftGear(1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        car.UpdateLocationAndVelocity(dt);

        double rpm = car.GetVx() * 60.0 * car.GetGearRatio() * car.FinalDriveRatio / (2.0 * Mathf.PI * car.WheelRadius);
        car.OmegaE = rpm;

        transform.position = new Vector3(transform.position.x, transform.position.y, (float)car.GetX());
    }
}
