using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Assertions;

public class DisplayInteractMessage : MonoBehaviour
{

    public GameObject       Parent;
    public TextMeshProUGUI  Text;
    public Sprite rmb;
    public Image interactLogo;
    public Vector3 shake;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {

        if (Parent == null)
        {
            Parent = this.gameObject.transform.parent.GetComponent<UnitInfo>().Parent;
        }
        Assert.IsNotNull(Parent, "Parent is not set or is null");

        
        Events.OnEnterInteraction.AddListener(ChangeMessage);
        Events.OnFinishInteract.AddListener(RemoveMessage);
        Events.OnSceneChange.AddListener(OnSceneChange);
        this.gameObject.SetActive(false);
    }

    public void ChangeMessage()
    {
        //Debug.Log("Message appear ");
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
            StartCoroutine(RMBAnimation());
        }
       


    }

    public void RemoveMessage(GameObject player = null)
    {
        interactLogo.color = Color.white;

        this.gameObject.SetActive(false);
       // StopCoroutine(RMBAnimation());
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnEnterInteraction.RemoveListener(ChangeMessage);
        Events.OnFinishInteract.RemoveListener(RemoveMessage);
        Debug.Log("Removed Interact Message listener");
    }

    public IEnumerator RMBAnimation()
    {
        while (gameObject.activeInHierarchy)
        {
           // interactLogo.DOColor(Color.gray, 0.5f);
           // interactLogo.transform.DORotate(shake, 0.5f, RotateMode.Fast).WaitForCompletion();
           
            yield return new WaitForSeconds(1f);
            ///interactLogo.DOColor(Color.white, 0.5f);
            //interactLogo.transform.DORotate(new Vector3(0,0,0), 0.5f, RotateMode.Fast).WaitForCompletion();
        }
    }
}
