using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 300;
  [SerializeField] float rotateThrust = 100;
  [SerializeField] AudioClip mainEngineSound;
  Rigidbody rb;
  AudioSource asrc;
  [SerializeField] ParticleSystem mainThrustParticles;
  [SerializeField] ParticleSystem leftThrustParticles;
  [SerializeField] ParticleSystem rightThrustParticles;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    asrc = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    ProcessInput();
  }

  void ProcessInput()
  {
    // **************** Process thrust ******************
    if (Input.GetKey(KeyCode.W))
    {
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
      if (!asrc.isPlaying)
      {
        asrc.PlayOneShot(mainEngineSound);
        mainThrustParticles.Play();
      }
    }
    else
    {
      asrc.Stop();
      mainThrustParticles.Stop();
    }
    // *********** Process rotation ********************
    // left rotate is given priority
    if (Input.GetKey(KeyCode.A))
    {
      //forward is (0,0,1)
      // right thrust is used when rotate left
      if (!rightThrustParticles.isPlaying) rightThrustParticles.Play();
      ApplyRotation(1);
    }
    else if (Input.GetKey(KeyCode.D))
    {

      if (!leftThrustParticles.isPlaying) leftThrustParticles.Play();
      ApplyRotation(-1);
    }
    else
    {
      //Debug.Log("stopped");
      rightThrustParticles.Stop();
      leftThrustParticles.Stop();
    }
  }

  void ApplyRotation(float direction)
  {
    rb.freezeRotation = true;
    //rb.AddTorque(Vector3.left * direction * rotateThrust * Time.deltaTime);
    transform.Rotate(Vector3.forward * direction * rotateThrust * Time.deltaTime);
    rb.freezeRotation = false;
  }
}
