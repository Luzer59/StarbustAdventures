using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Dialogues
{
    public string[] text;
    public int[] usedPic1;
    public int[] usedPic2;
}

[System.Serializable]
public class ManualDialogues
{
    public string[] text;
    public int[] usedPic1;
    public int[] usedPic2;
}

public class LevelController : MonoBehaviour
{
    public int levelNumber = 0;
    public float levelTime = 0;
    public float totalLevelTime = 0;
    public GameObject paaseMenu;
    public float[] dialogueStartTimes;
    public Dialogues[] dialogues;
    public ManualDialogues[] manualDialogues;
    public Sprite[] pics;
    public GameObject textBox;
    public Image leftPic;
    public Image rightPic;
    public Text text;
    public float levelTimeScale = 1f;

    private SpriteRenderer[] picRenderers;

    private int currentDialogueSet = 0;
    private bool dialogueActive = false;
    private bool isPaused = false;

    private AudioSource ac;
    public AudioClip paaseSwitchClip;

    void Awake()
    {
        ac = GetComponent<AudioSource>();
    }

    public void ManualPaaseRemoval()
    {
        paaseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        ac.PlayOneShot(paaseSwitchClip);
    }

    void Update()
    {
        levelTime += Time.deltaTime * levelTimeScale;
        totalLevelTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                paaseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
                ac.PlayOneShot(paaseSwitchClip);
            }
            else
            {
                paaseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
                ac.PlayOneShot(paaseSwitchClip);
            }
        }

        if (dialogueStartTimes.Length - 1 >= currentDialogueSet)
        {
            if (levelTime >= dialogueStartTimes[currentDialogueSet] && !dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(DialogueTimer());
            }
        }
    }

    public void ManualActivation(int index)
    {
        StartCoroutine(ManualDialogueTimer(index));
    }

    IEnumerator DialogueTimer()
    {
        levelTimeScale = 0f;
        textBox.SetActive(true);
        print(dialogues[currentDialogueSet].text.Length);
        for (int i = 0; i < dialogues[currentDialogueSet].text.Length; i++)
        {
            text.text = dialogues[currentDialogueSet].text[i];
            leftPic.sprite = pics[dialogues[currentDialogueSet].usedPic1[i]];
            rightPic.sprite = pics[dialogues[currentDialogueSet].usedPic2[i]];
            while (true)
            {
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }
        textBox.SetActive(false);
        dialogueActive = false;
        currentDialogueSet++;
        levelTimeScale = 1f;
    }
    IEnumerator ManualDialogueTimer(int index)
    {
        levelTimeScale = 0f;
        textBox.SetActive(true);
        print(manualDialogues[index].text.Length);
        for (int i = 0; i < manualDialogues[index].text.Length; i++)
        {
            text.text = manualDialogues[index].text[i];
            leftPic.sprite = pics[manualDialogues[index].usedPic1[i]];
            rightPic.sprite = pics[manualDialogues[index].usedPic2[i]];
            while (true)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    break;
                }
                yield return null;
            }
            yield return null;
        }
        textBox.SetActive(false);
        dialogueActive = false;
        levelTimeScale = 1f;
        GetComponent<PlayerDeath>().EndGameStart(false);
    }
}
