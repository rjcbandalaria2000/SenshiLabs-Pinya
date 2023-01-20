using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(objDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator objDespawn()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
