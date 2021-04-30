using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarInputManager im;
    public Rigidbody carRB;
    public GameObject[] meshWheels=new GameObject[4];
    public MeshCollider[] colWheels=new MeshCollider[4];

    private BoxsterS car;
    public TextMeshProUGUI messageTextBox;
    public TextMeshProUGUI timeTextBox;
    public TextMeshProUGUI velocityTextBox;
    public TextMeshProUGUI rpmTextBox;
    public TextMeshProUGUI gearTextBox;
    public TextMeshProUGUI distanceTextBox;
    
    public TextMeshProUGUI txtTemp;
    
    private double x0, y0, z0;
    private double vx0, vy0, vz0;
    private double time, density;
    private void Start()
    {
        x0 = 0.0;
        y0 = 0.0;
        z0 = 0.0;
        vx0 = 0.0;
        vy0 = 0.0;
        vz0 = 0.0;
        time = 0.0;
        density = 1.2;
        car = new BoxsterS(x0, y0, z0, vx0, vy0, vz0, time, density);
    }

    private float tempDiv = 0.326f;
    private float realtime = 0f;
    private void FixedUpdate()
    {
        if (car.OmegaE <= 8000.0)
        {
            txtTemp.text = realtime + "";
        }

        realtime += Time.deltaTime;
        
        
        //  Display the current time
        timeTextBox.text = "" + realtime;
        //timeTextBox.text = "" + (float)car.GetTime() * realtime;
        
        //  Convert the velocity from m/s to km/hr and
        //  only show integer values
        velocityTextBox.text = "" + (int)(car.GetVx() * 3.6) ;     //시간과 관련

        //  Only show integer values for rpm, gear number, 
        //  and distance.
        rpmTextBox.text = "" + (int)car.OmegaE;
        gearTextBox.text = "" + (int)car.GearNumber;
        distanceTextBox.text = "" + (int)car.GetX();
        
        
        car.Mode = "accelerating";  //임시
        if (Input.GetKey(KeyCode.W))
        {
            car.Mode = "accelerating";
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            car.Mode = "braking";
        }
        else
        {
            car.Mode = "accelerating";  //임시
            //car.Mode = "cruising";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            car.ShiftGear(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            car.ShiftGear(-1);
        }
        
        //  Update the car velocity and position at the next
        //  time increment. 
        double timeIncrement = 0.06 * tempDiv;  //시간과 관련
        car.UpdateLocationAndVelocity(timeIncrement);

        //  Compute the new engine rpm value
        double rpm = car.GetVx() * 60.0 * car.GetGearRatio() 
            * car.FinalDriveRatio / (2.0 * Math.PI * car.WheelRadius);      //시간과 관련
        car.OmegaE = rpm;
        
        //  If the rpm exceeds the redline value, put a
        //  warning message on the screen. First, clear the
        //  message textfield of any existing messages. 
        messageTextBox.text = "";
        if (car.OmegaE > car.Redline)
        {
            messageTextBox.text = "Warning: Exceeding redline rpm";
        }
        if (car.OmegaE > 8000.0)
        {
            messageTextBox.text = "You have blown the engine!";
            //gameTimer.Stop();
        }
        
        
        
        
    }
    
    
}
