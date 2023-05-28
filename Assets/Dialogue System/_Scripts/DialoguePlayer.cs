using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class DialoguePlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float textSpeed = 0.02f;    


    [Header("References")]
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private GameObject arrowIcon;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI speechText;


    [Header("Audio")]
    [Range(0, 1)]
    [SerializeField] private float volume = 0.02f;
    [SerializeField] private bool makePredictable;
    [SerializeField] private AudioClip defaultAudioClip;

    private AudioSource audioSource;
    


    //Flags 
    private bool isPlayingDialogue  = false;
    private bool canContinueToNextLine = false;

    //Dialogue refs
    private DialogueSO currentDialogue;
    private int conversationIdx = -1;
    private Coroutine displayLineCoroutine;



    //this is super ugly and I should propbably just be using events instead of a callback structure
    private DialogueTrigger triggerThatStartedDialogue;


    public static DialoguePlayer Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        audioSource = this.gameObject.AddComponent<AudioSource>();
        
    }



    // Update is called once per frame
    void Update()
    {
        if (!isPlayingDialogue) return;

        //TODO replace this with new input system
        if(canContinueToNextLine && Input.GetKeyDown(KeyCode.Space)) 
        {
            StepConversation();
        }


    }

    public bool GetIsPlayingDialogue()
    {
        return isPlayingDialogue;
    }

    //Starts the dialogue playback loop with a reference to the DialogueTrigger instance that called it.
    public void StartDialogue(DialogueSO dialogue, DialogueTrigger caller)
    {
        if(dialogue.conversation.Length < 1)
        {
            Debug.LogWarning("conversation has a length of 0. Please make sure to populate dialogue");
            return;
        }
        if(isPlayingDialogue)
        {
            Debug.LogWarning("Tried to start dialogue while dialogue is already running");
            return;
        }
        else
        {
            triggerThatStartedDialogue= caller;
            isPlayingDialogue= true;
            SuspendGame();
            currentDialogue = dialogue;
            dialogueBox.SetActive(true);
            StepConversation();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        canContinueToNextLine = false;
        arrowIcon.SetActive(false);
        speechText.text = "";

        foreach(char letter in line.ToCharArray())
        {            
            PlayDialogueSounds(speechText.text.Length, letter);
            speechText.text += letter;
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(textSpeed));
        }

        canContinueToNextLine = true;
        arrowIcon.SetActive(true);
    }

    private void PlayDialogueSounds(int currentDisplayedCharacterCount, char currentCharacter)
    {
        DialogueActorSO currentActor = currentDialogue.conversation[conversationIdx].actor;

        AudioClip[] dialogueTypingSoundClips = currentActor.dialogueTypingSoundClips;
        if(dialogueTypingSoundClips.Length < 1)
        {
            Debug.LogWarning("make sure to set at least 1 audio clip for: " + currentActor.actorName);
            dialogueTypingSoundClips = new AudioClip[] {defaultAudioClip};
        }
        int characterFrequency = currentActor.characterFrequency;
        float minPitch = currentActor.minPitch;
        float maxPitch = currentActor.maxPitch;
        bool stopAudioSource = currentActor.stopAudioSource;


        audioSource.volume = volume;
        if (currentDisplayedCharacterCount % characterFrequency == 0)
        {
            AudioClip soundClip = null;
            if (stopAudioSource)
            {
                audioSource.Stop();
            }

            if(makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();

                int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
                soundClip = dialogueTypingSoundClips[predictableIndex];

                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt = minPitchInt;
                if(pitchRangeInt != 0)
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float predictablePitch = predictablePitchInt/ 100f;
                    audioSource.pitch = predictablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            else
            {
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                soundClip = dialogueTypingSoundClips[randomIndex];
                audioSource.pitch = Random.Range(minPitch, maxPitch);
            }


            
            
            audioSource.PlayOneShot(soundClip);
        }
    }

    //increments the conversation index and populates the UI, or if at the end of the conversation, it will finish it.
    private void StepConversation()
    {
        if(conversationIdx < currentDialogue.conversation.Length -1)
        {
            conversationIdx++;
            nameText.text = currentDialogue.conversation[conversationIdx].actor.actorName;
            nameText.color = currentDialogue.conversation[conversationIdx].actor.color;

            //speechText.text = currentDialogue.conversation[conversationIdx].text;
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentDialogue.conversation[conversationIdx].text));

        }
        else
        {
            FinishConversation();
        }
    }

    //run callback on object that started the dialogue then reset the dialogue player and unsuspend the game
    private void FinishConversation()
    {
        triggerThatStartedDialogue.OnFinishedDialogue(currentDialogue);        
        isPlayingDialogue = false;
        currentDialogue.hasFinished = true;
        conversationIdx = -1;
        dialogueBox.SetActive(false);
        UnsuspendGame();
        currentDialogue = null;

    }

    public void SuspendGame()
    {
        Time.timeScale = 0;
    }

    public void UnsuspendGame()
    {
        Time.timeScale = 1;
    }
}


/// <summary>
///  Since the time scale is zero when we suspend the game, a normal coroutine will never finish yielding
///  for seconds. To get around this we'll use a custom coroutine utility to wait for real word time
///  independent of the game time.///  
/// </summary>
public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
