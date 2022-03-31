using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelLoadDelay = 0.5f;
  [SerializeField] float SuccessLevelLoadDelay = 0.2f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;

  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;

  AudioSource asrc;

  bool isTransitioning = false;
  bool collisionDisabled = false;

  void Start()
  {
    asrc = GetComponent<AudioSource>();
  }

  void Update()
  {
    RespondToDebugKyes();
  }

  void RespondToDebugKyes()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionDisabled = !collisionDisabled;
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionDisabled) { return; }
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("Friendly");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      case "Fuel":
        Debug.Log("Fuel");
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  private void StartSuccessSequence()
  {
    isTransitioning = true;
    asrc.Stop();
    asrc.PlayOneShot(success);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", SuccessLevelLoadDelay);
  }

  void StartCrashSequence()
  {
    isTransitioning = true;
    // todo add SFX upon crash
    // todo add particle effect upon crash
    asrc.Stop();
    asrc.PlayOneShot(crash);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }
  void LoadNextLevel()
  {
    int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIdx = currentSceneIdx + 1;
    if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIdx = 0;
    }
    SceneManager.LoadScene(nextSceneIdx);
  }
  void ReloadLevel()
  {
    int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIdx);
  }


}
