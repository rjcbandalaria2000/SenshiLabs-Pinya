using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class CutsceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Image pageImage;
    public List<Sprite> spritePages;
    public int pageCount;
   // public List<string> story;
    //public TextMeshProUGUI text;
    private SceneChange sceneChange;
    public string sceneName;

    private void Awake()
    {
        sceneChange = GetComponent<SceneChange>();
    }
    void Start()
    {
        pageImage.sprite = spritePages[0];
    //    text.text = story[0];
    }

    public void OnNextPage()
    {
        if(pageCount < spritePages.Count -1)
        {
            pageCount++;
            pageImage.sprite = spritePages[pageCount];
      //      text.text = story[pageCount];
        }
        else
        {
            //Move Scene
            sceneChange.OnChangeScene(sceneName);
        }
      
    }
}
