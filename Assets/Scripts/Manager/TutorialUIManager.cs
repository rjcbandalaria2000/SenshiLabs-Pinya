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

    public Button nextArrow;
    public Button prevArrow;

    public Button startButton;

    private UIManager uiManager;

    private void Awake()
    {
        videoManager = GetComponent<VideoManager>();
    }

    private void Start()
    {
        if (tutorialImages[pageCount] == null)
        {
            videoImage.gameObject.SetActive(true);

            imageGO.gameObject.SetActive(false);
            videoManager.MoveVideo(0);
            // videoManager.MoveVideo(0);
        }
        else
        {
            if (tutorialImages.Count > 0)
            {
                imageGO.sprite = tutorialImages[0];
                videoImage.gameObject.SetActive(false);
            }
        }

        if(startButton != null)
        {
            startButton.gameObject.SetActive(false);
        }

        uiManager = SingletonManager.Get<UIManager>();
        if (uiManager)
        {
            uiManager.DeactivateGameUI();
        }
    }
    private void OnEnable()
    {
        prevArrow.gameObject.SetActive(false);
        nextArrow.gameObject.SetActive(true);
        pageCount = 0;
        textGO.text = instructionText[0];
        if(tutorialImages[0] != null)
        {
            videoImage.gameObject.SetActive(false);
            imageGO.gameObject.SetActive(true);

            imageGO.sprite = tutorialImages[0];


        }
        else
        {
            videoImage.gameObject.SetActive(true);
            imageGO.gameObject.SetActive(false);
        }
        tempPage = 1;
        currentPage.text = "1"; 

       

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
            prevArrow.gameObject.SetActive(true);
            pageCount++;
            tempPage++;
            textGO.text = instructionText[pageCount];


            if(pageCount >= instructionText.Count - 1)
            {
                nextArrow.gameObject.SetActive(false);

                if(startButton != null && startButton.gameObject.activeSelf == false)
                {
                    startButton.gameObject.SetActive(true);
                }
                else if (startButton != null && startButton.gameObject.activeSelf == true)
                {
                   
                    startButton.gameObject.SetActive(false);
                }
            }


            if (tutorialImages[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);
                videoImage.gameObject.SetActive(true);


                //videoManager.NextVideo();
                //     Debug.Log(pageCount);
               // videoManager.me++;
               // videoManager.MoveVideo(videoManager.counter);
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
            nextArrow.gameObject.SetActive(true);
            pageCount--;
            tempPage--;
            textGO.text = instructionText[pageCount];

            if (tutorialImages.Count > 0)
                imageGO.sprite = tutorialImages[pageCount];

            if (pageCount <= instructionText.Count - 1)
            {

                if (startButton != null)
                {
                    startButton.gameObject.SetActive(false);
                }
               
            }
            else
            {
                startButton.gameObject.SetActive(true);
            }

            if (pageCount <= 0)
            {
                prevArrow.gameObject.SetActive(false);
            }
            if (tutorialImages[pageCount] == null)
            {
                imageGO.gameObject.SetActive(false);
                videoImage.gameObject.SetActive(true);
                // videoManager.MoveVideo(pageCount);
                //   Debug.Log(pageCount - 1);
                //  videoManager.PrevVideo();
            //    videoManager.counter--;
             //   videoManager.MoveVideo(videoManager.counter);
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
