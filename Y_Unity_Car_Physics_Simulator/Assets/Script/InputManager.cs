using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool is_L;
    public bool brake;

    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Vertical");   // W, S
        //Debug.Log(Input.GetAxis("Vertical"));
        steer = Input.GetAxis("Horizontal");    //A, D

        is_L = Input.GetKeyDown(KeyCode.L);

        brake = Input.GetKey(KeyCode.Space);

        //Debug.Log(Input.GetAxis("XboxReset"));
        if (Input.GetKeyDown(KeyCode.R) || (Input.GetAxis("XboxReset") == 1f))
        {
            SceneManager.LoadScene(0);
        }

        
    }
}
