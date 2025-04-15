using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pine : MonoBehaviour

    
{
    TaskManager missionController;
    public Transform finger;
    public Transform endPoint;

    public float speed = 2f;

    public Animator animator;

    void Start()
    {
        missionController = FindFirstObjectByType<TaskManager>();
        animator = GetComponent<Animator>();
    }
    public void StartAnimation()
    {
        animator.SetBool("pineOff", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        /* if (other.CompareTag("Finger") && missionController.pineMission)
        {
            StartAnimation();
        }
        return;*/
    }
}
