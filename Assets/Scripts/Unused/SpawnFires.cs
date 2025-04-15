using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFires : MonoBehaviour
{
    public ObjectFireController objectFireController;

    public GameObject[] firesToSpawn;
    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
    }
    public void GetRandomFireToSpawn()
    {
        if (firesToSpawn == null || firesToSpawn.Length == 0)
        {
            Debug.LogWarning("firesToSpawn est� vazio ou n�o configurado!");
            return;
        }

        foreach (var fire in firesToSpawn)
        {
            if (fire != null)
                fire.SetActive(false);
        }

        int randomIndex = Random.Range(0, firesToSpawn.Length);

        if (firesToSpawn[randomIndex] != null && canSpawn)
        {
            canSpawn = false;
            firesToSpawn[randomIndex].SetActive(true);
            Invoke("CanSpawnTrue", 2f);
        }
        else
        {
            Debug.LogWarning($"O GameObject na posi��o {randomIndex} � nulo");
        }
    }

    public void CanSpawnTrue() 
    {
        canSpawn = true;
    }
}
