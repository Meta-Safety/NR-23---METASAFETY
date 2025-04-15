using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExtinguishers : MonoBehaviour
{
    public GameObject extinguisherA;
    public GameObject extinguisherB;

    public void ChangeExtinguisher() 
    {
        extinguisherA.SetActive(false);
        extinguisherB.SetActive(true);
    }
}
