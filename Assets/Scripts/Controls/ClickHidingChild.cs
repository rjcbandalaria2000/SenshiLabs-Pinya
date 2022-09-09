using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHidingChild : MonoBehaviour
{
    [SerializeField] private GameObject child;

    public HideSeekManager groceryMiniGame;

    private void Start()
    {
        child = this.gameObject;    
        groceryMiniGame = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Child Found");
        groceryMiniGame.score += 1;
        child.SetActive(false); // or DeleteDestroy?

        groceryMiniGame.checkChildren();

    }
}
