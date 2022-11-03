using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<string> instructionText;
    public List<Sprite> tutorialImages;
    public int pageCount;
    public TextMeshProUGUI textGO;
    public Image imageGO;
    public TextMeshProUGUI currentPage;
    public TextMeshProUGUI maxPage;
    int tempPage;
    public RawImage videoImage;
    VideoManager videoManager;

    private void Awake()
    {
        videoManager = GetComponent<VideoManager>();
    }
    private void OnEnable()
    {
        pageCount = 0;
        textGO.text = instructionText[0];
        tempPage = 1;
        currentPage.text = "1";

        if(tutorialImages.Count > 0)
        {
            imageGO.sprite = tutorialImages[0];
            videoImage.gameObject.SetActive(false);
        }


           

        maxPage.text = instructionText.Count.ToString();
    }
    public void NextPage()
    {

        if (pageCount >= instructionText.Count - 1)
        {
            pageCount = instructionText.Count - 1;

        }
        else
        {
            pageCount++;
            tempPage++;
            textGO.text = instructionText[pageCount];



            if (tutorialImages[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);
                videoImage.gameObject.SetActive(true);
                videoManager.MoveVideo(0);
            }
            else
            {
                if (tutorialImages.Count > 0)
                {
                    videoImage.gameObject.SetActive(false);
                    imageGO.gameObject.SetActive(true);
                    imageGO.sprite = tutorialImages[pageCount];
                }
            }
           
        }
        //    int temp = pageCount + 1;
        currentPage.text = tempPage.ToString();

    }

    public void PreviousPage()
    {

        if (pageCount <= 0)
        {
            pageCount = 0;
            tempPage = 1;
            textGO.text = instructionText[0];

        }
        else
        {
            pageCount--;
            tempPage--;
            textGO.text = instructionText[pageCount];

            if (tutorialImages[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);
                videoImage.gameObject.SetActive(true);
                videoManager.MoveVideo(0);
            }
            else
            {
                if (tutorialImages.Count > 0)
                {
                    videoImage.gameObject.SetActive(false);
                    imageGO.gameObject.SetActive(true);
                    imageGO.sprite = tutorialImages[pageCount];
                }
            }

        }

        currentPage.text = tempPage.ToString();
    }
}
