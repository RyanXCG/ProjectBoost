using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
  // Start is called before the first frame update
  Vector3 startingPosition;
  [SerializeField] Vector3 movementVector;
  [SerializeField][Range(0, 1)] float movementFactor;
  [SerializeField] float period = 2f;

  void Start()
  {
    startingPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (period <= Mathf.Epsilon) { return; }
    const float tau = Mathf.PI * 2;
    float cycles = Time.time / period;
    float rawSinWave = Mathf.Sin(tau * cycles);
    Vector3 offset = movementVector * (rawSinWave + 1) / 2;
    transform.position = startingPosition + offset;
  }
}
