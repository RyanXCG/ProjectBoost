using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  Rigidbody rb;


  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessInput();
  }

  void ProcessInput()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      //Debug.Log("pressed thrusting");
      rb.AddRelativeForce(0, 1, 0);
    }
    // left rotate is given priority
    if (Input.GetKey(KeyCode.A))
    {
      Debug.Log("pressed left Rotate");
    }
    else if (Input.GetKey(KeyCode.D))
    {
      Debug.Log("pressed right Rotate");
    }

  }
}
