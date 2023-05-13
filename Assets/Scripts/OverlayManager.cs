using UnityEngine;
using TMPro;

public class OverlayManager : MonoBehaviour
{
    [SerializeField] Canvas scoreCanvas;
    RectTransform tapImage;
    RectTransform flappybirdTextImage;
    [SerializeField] Canvas getReadyCanvas;
    [SerializeField] Canvas gameOverCanvas;

    void Awake()
    {
        scoreCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        getReadyCanvas.gameObject.SetActive(true);
        flappybirdTextImage = getReadyCanvas.transform.GetChild(0).GetComponent<RectTransform>();
        tapImage = getReadyCanvas.transform.GetChild(1).GetComponent<RectTransform>();
    }

    void Start()
    {
        StartGetReadyAnimation();
    }

    void OnEnable()
    {
        GameManager.OnGameStart += GameStart;
        GameManager.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
        GameManager.OnGameStart -= GameStart;
    }

    void GameStart()
    {
        scoreCanvas.gameObject.SetActive(true);
        HideGetReadyOverlay();
    }

    void HideGetReadyOverlay()
    {
        void _Disable() => getReadyCanvas.gameObject.SetActive(false);
        for (int i = 0; i < getReadyCanvas.transform.childCount; i++){
            var childRectTranform = getReadyCanvas.transform.GetChild(i).GetComponent<RectTransform>();
            childRectTranform.LeanAlpha(0f, 0.5f).setOnComplete(_Disable);
        }
    }


    void StartGetReadyAnimation()
    {
        tapImage.LeanScale(Vector3.one * 1.2f, 0.4f).setEaseInOutBounce().setLoopPingPong();
        flappybirdTextImage.LeanMoveLocal(new Vector2(2, 90), 1.5f).setEaseInOutQuint().setLoopPingPong();
    }

    void AnimateGameOverCanvas(){
        var gameOverText = gameOverCanvas.transform.GetChild(0).GetComponent<RectTransform>();
        var scoreBoard = gameOverCanvas.transform.GetChild(1).GetComponent<RectTransform>();
        var replayButton = gameOverCanvas.transform.GetChild(2).GetComponent<RectTransform>();
        gameOverText.localPosition = new Vector2(0, -215f);
        gameOverText.LeanMoveY(120f, 0.55f).setEaseInBack();

        scoreBoard.localPosition = new Vector2(0, -235f);

        // Update scores

        // score
        scoreBoard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{GameManager.Instance.Score}";

        // best score
        scoreBoard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{GameManager.Instance.Score}";


        scoreBoard.LeanMoveY(8f, 0.7f).setEaseInBack().setDelay(0.55f);

        replayButton.localPosition = new Vector2(0, -215f);
        replayButton.LeanMoveY(-100f, 0.7f).setEaseInBack().setDelay(1.1f);
    }

    void GameOver()
    {
        // Disable score canvas
        scoreCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        AnimateGameOverCanvas();
    }
}
