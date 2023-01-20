using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Catcher : MonoBehaviour
{
    public GameObject   Parent; 
    public int          PlayerScore;
    public bool         CanCatch = true;
    SFXManager sFXManager;
    public AudioClip catchFood;
    public AudioClip pinyaCatch;
    // Start is called before the first frame update

    private Vector3 objSize;
    private Vector3 scaleSize;
    
    void Start()
    {
        sFXManager = GetComponent<SFXManager>();
        Parent = this.gameObject;
        CanCatch = true;

        objSize = this.transform.localScale;
        scaleSize = objSize * 1.3f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanCatch) { return; }
        FallingFood collidedFood = collision.gameObject.GetComponent<FallingFood>();
        if (collidedFood)
        {
            if (collidedFood.Name != "Pinya")
            {
                sFXManager.PlaySFX(catchFood);
                StartCoroutine(scaleTween());
                //transform.DOScale(scaleSize, 0.5f);
            }
            else
            {
                sFXManager.PlaySFX(pinyaCatch);
            }
            Debug.Log("Collided with" + collidedFood.name);
            collidedFood.OnCollided(Parent);
            Destroy(collidedFood.gameObject);   
        }
    }

    IEnumerator scaleTween()
    {
        transform.DOScale(scaleSize, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(objSize, 0.5f);
    }
    
}
