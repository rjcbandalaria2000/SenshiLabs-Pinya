using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class CutsceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator pageImage;
    public List<AnimationClip> spritePages;
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
        pageImage.Play(spritePages[0].name);
        //    text.text = story[0];

    }

    public void OnNextPage()
    {
      //  if(pageCount < spritePages.Count -1)
      //  {
      //      pageCount++;
      //      pageImage.sprite = spritePages[pageCount];
      ////      text.text = story[pageCount];
      //  }
      //  else
      //  {
      //      //Move Scene
      //      sceneChange.OnChangeScene(sceneName);
      //  }
      
        if(pageCount < spritePages.Count - 1)
        {
            pageCount++;
            pageImage.Play(spritePages[pageCount].name);
        }
        else
        {
            sceneChange.OnChangeScene(sceneName);
        }
    }
}
