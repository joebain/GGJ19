using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject GameOverStuff;
    public Text Score;

    private static HUDController instance;

    public static HUDController Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        GameOverStuff.SetActive(false);
    }

    void Update()
    {
        Score.text = Game.Instance.ReturnPlayerObj().Score.ToString().PadLeft(8, '0');
    }

    public void ShowGameOver()
    {
        GameOverStuff.SetActive(true);
    }
}
