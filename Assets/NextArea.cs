using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextArea : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 destination;
    public Vector3 cameraDestination;
  //  public List<GameObject> cameras;
   // [SerializeField] int cameraIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = destination;

            Camera.main.transform.localPosition = cameraDestination;

            collision.GetComponent<PlayerControls>().SetTargetPosition(destination);
            //foreach (GameObject obj in cameras)
            //{
            //    obj.SetActive(false);
            //}
            //cameras[cameraIndex].SetActive(true);
            //Camera.main = cameras[cameraIndex];
        }
    }
}
