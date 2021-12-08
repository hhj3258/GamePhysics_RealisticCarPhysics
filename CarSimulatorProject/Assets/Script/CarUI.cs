using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    NewCarController newCarController;

    public Text txtMode;
    public Text txtVelocity;
    public Text txtRPM;
    public Text txtGear;
    public Text txtNowDistance;
    public Text txtTime;

    // Start is called before the first frame update
    void Start()
    {
        newCarController = FindObjectOfType<NewCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        txtMode.text = "" + newCarController.car.Mode;
        txtVelocity.text = "" + (int)(newCarController.car.GetVx() * 3.6);
        txtRPM.text = "" + (int)newCarController.car.OmegaE;
        txtGear.text = "" + (int)newCarController.car.GearNumber;
        txtNowDistance.text = "" + (int)newCarController.car.GetX();
        txtTime.text = "" + (float)newCarController.car.GetTime();
    }
}
