using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    public Dialogue[] dialogoInizio;
    public Dialogue[] dialogoObbiettivoRaggiunto;
    public Dialogue[] dialogoCassa;
    public Dialogue[] dialogoFineTutorialAria;
    public Dialogue[] dialogoCheckpoint;
    public Dialogue[] dialogoIntroduzioneGaia;
    public Dialogue[] dialogoTutorialTerra;
    public Dialogue[] dialogoTutorialTerraFine;
    public Dialogue[] dialogoGeysers;
    public Dialogue[] dialogoFineDemo;

    [HideInInspector]
    public bool obiettivoRaggiunto;

    public bool cassaAlzata;
    [HideInInspector]
    public bool tutorialAriaFinito = false;
    [HideInInspector]
    public bool checkPoint;
    [HideInInspector]
    public bool introduzioneGaia;
    [HideInInspector]
    public bool tutorialTerra;
    [HideInInspector]
    public bool tutorialTerraFinito;
    [HideInInspector]
    public bool geysesAttivati;
    [HideInInspector]
    public bool fineDemo;

    public bool rockTotem;
    public bool grassTotem;
    public bool sandTotem;
    public GameObject geysers;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        StartCoroutine(LateStart(1 / 2));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        dialogueManager.StartDialogue(dialogoInizio);
    }

    public void DialogoObiettivoRaggiunto()
    {
        if (obiettivoRaggiunto == false)
        {
            obiettivoRaggiunto = true;
            dialogueManager.StartDialogue(dialogoObbiettivoRaggiunto);
        }

    }

    public void DialogoCassaSollevata()
    {
        if (cassaAlzata == false)
        {
            cassaAlzata = true;
            dialogueManager.StartDialogue(dialogoCassa);
        }

    }

    public void DialogoTutorialAriaFinito()
    {
        if (!tutorialAriaFinito)
        {
            tutorialAriaFinito = true;
            dialogueManager.StartDialogue(dialogoFineTutorialAria);
        }
    }

    public void DialogoCheckpoint()
    {
        if (!checkPoint)
        {
            checkPoint = true;
            dialogueManager.StartDialogue(dialogoCheckpoint);
        }
    }

    public void DialogoIntroduzioneGaia()
    {
        if (!introduzioneGaia)
        {
            introduzioneGaia = true;
            dialogueManager.StartDialogue(dialogoIntroduzioneGaia);
        }
    }

    public void DialogoTutorialTerra()
    {
        if (tutorialTerra == false)
        {
            tutorialTerra = true;
            dialogueManager.StartDialogue(dialogoTutorialTerra);
        }
    }

    public void DialogoTutorialTerraFine()
    {
        if (tutorialTerraFinito == false)
        {
            tutorialTerraFinito = true;
            dialogueManager.StartDialogue(dialogoTutorialTerraFine);
        }
    }

    public void DialogoGeysers()
    {
        if (geysesAttivati == false)
        {
            geysesAttivati = true;
            dialogueManager.StartDialogue(dialogoGeysers);
        }
    }

    public void DialogoFineDemo()
    {
        if (fineDemo == false)
        {
            fineDemo = true;
            dialogueManager.StartDialogue(dialogoFineDemo);
        }
    }

    public void CheckTotem()
    {
        if (rockTotem && grassTotem && sandTotem)
        {
            geysers.SetActive(true);
            geysers.GetComponent<AudioSource>().Play();
            DialogoGeysers();
        }
    }
}
