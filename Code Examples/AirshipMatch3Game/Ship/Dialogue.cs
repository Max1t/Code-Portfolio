using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class Dialogue : MonoBehaviour
{
    public string[] situationDialogueLines;
    public string dialogue0;
    public string response1;
    public string response2;
    public string response3;
    public string dialogueEndSplit; //this is the part of the dialogue text that tells about what happens next
    public string currentDialogue;
    public int currentDialogueLineRow = 0;

    private string[] sentences;
    private string[] responses;
    private string[] sentencesSituation1;
    private string[] responsesSituation1;
    private string[] sentencesSituation2;
    private string[] responsesSituation2;
    private string[] sentencesSituation3;
    private string[] responsesSituation3;
    private string currentTextBeingShown;
    private int situation;
    private int amountOfButtons = 2;
    private int randomSituationDialogue = 0;
    private int lastChoice = 3;
    public float typingSpeed;
    private bool ready = false; //ready to start displaying text
    private bool randomDialogue = false; //if we need to choose from 2 dialogues randomly
    private bool noMoreDialogue = false;
    public bool clickedSkipButton = false;

    public TextMeshProUGUI textDisplay;
    public GameObject buttonChoice1;
    public GameObject buttonChoice2;
    public GameObject buttonChoice3;
    public GameObject buttonContinue;
    public GameObject buttonSkipText;
    public GameObject randomEncounterPanel;
    public GameObject shopPanel;
    public GameObject shopButton;
    public GameObject shop2ButtonOpened;
    public GameObject shop2ButtonClosed;
    public GameObject buttonFly;

    //particle system effets to show the player that he got credits/ammo/repairs...
    public GameObject CreditsEffectPrefab;
    public GameObject CreditsLostEffectPrefab;
    public GameObject RepairEffectPrefab;
    public GameObject AmmoEffectPrefab;
    public GameObject GasEffectPrefab;
    public GameObject CrewMemberEffectPrefab;

    private ShopUi shopUI;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTheScene();
        SetSentences();
    }

    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == currentDialogue && ready)
        {
            if (amountOfButtons == 2)
            {
                buttonChoice1.SetActive(true);
                buttonChoice2.SetActive(true);
            }
            else if (amountOfButtons == 1)
            {
                buttonContinue.SetActive(true);
            }
            else
            {
                buttonChoice1.SetActive(true);
                buttonChoice2.SetActive(true);
                buttonChoice3.SetActive(true);
            }
            buttonSkipText.SetActive(false);
        }
    }

    IEnumerator DynamicType(string sentence)
    {
        SetButtons(false);
        //string sentenceWithLines = sentence.Replace("\n", Environment.NewLine);
        string sentenceWithLines = sentence.Replace("\\n", Environment.NewLine);
        currentTextBeingShown = sentenceWithLines;
        currentDialogue = sentenceWithLines;
        //show the dialogue text immediately
        if (AirshipStats.debugging)
        {
            textDisplay.text = sentenceWithLines;
        }
        else
        {
            foreach (char letter in sentenceWithLines.ToCharArray())
            {
                if (clickedSkipButton)
                {
                    textDisplay.text = sentenceWithLines;
                    break;
                }
                else
                {
                    textDisplay.text += letter;
                }
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        clickedSkipButton = false;
    }

    public void DynamicNextSentence(string sentence)
    {
        StopCoroutine("DynamicType");
        textDisplay.text = "";
        StartCoroutine(DynamicType(sentence));
    }

    private void SetSentences()
    {
        amountOfButtons = 1;

        if(AirshipStats.specialBattle)
        {
            AirshipStats.situationEndingText = "First there was the deception, the gravest kind of it. Then what followed was confusion, despair and overly reflecting self-doubt. When your first-mate decided to sell your friendship for power and money, the only thing that is left is the burning desire to seek the redemption.\nThe final battle to settle the score and fulfill your only goal left in this wretched world is here. Prepare for a battle!".Replace("\n", Environment.NewLine); ;
            ShowEndingDialogue();
        }
        else if (AirshipStats.showSituationEndingText)
        {
            ShowEndingDialogue();
        }
        else if (AirshipStats.showMapTargetEndingText)
        {
            ShowMapTargetDialogue();
        }
        else
        {
            ReadEncounterFiles();
        }

        ready = true;
    }

    private void ReadEncounterFiles()
    {
        System.Random rand = new System.Random();
        int amountOfDialogue = 0; //the rows of dialogue that is read from a file
        int amountOfResponses = 1;
        string dialogueFile;

        //read a file manually if true (no need to start from preload scene) and automatically if false (need to start from preload scene)
        if (AirshipStats.debugging && false)
        {
            dialogueFile = AirshipStats.randomEncounters[3];
        }
        else
        {
            int amountOfSituations = AirshipStats.randomEncounters.Count;

            //every random encounter except 1 has already been seen so we need to reload all the files again
            if (amountOfSituations == 1)
            {
                AirshipStats.ReadAllRandomEncounterFiles();
            }

            int situation = rand.Next(0, amountOfSituations); //choose the situation

            dialogueFile = AirshipStats.randomEncounters[situation];
        }

        string[] lines = dialogueFile.Split('\r');
        Debug.Log("Read file: " + dialogueFile);
        situationDialogueLines = lines;
        amountOfDialogue = lines.Length;


        //List<string> lines = new List<string>(tex.text.Split('\n'));
        //foreach(var strin in lines)
        //{
        //    Debug.Log(strin);
        //}

        if (!AirshipStats.debugging)
        {
            //remove the random encounter from the list so that we do not get the same encounter anymore
            AirshipStats.randomEncounters.Remove(dialogueFile);
        }

        //Split the first row of the situation dialogue that happens everytime "0-asdf-2" the ending -2 means 2 responses "0-asdf-1" means 1 response
        string[] situationString0 = lines[0].Split('-');
        dialogue0 = situationString0[1];
        amountOfDialogue -= 1;
        amountOfButtons = 1;

        if (situationString0[2] == "BATTLE" || situationString0[2] == "SHOP" || situationString0[2] == "MONEY" || situationString0[2] == "REPAIR" || situationString0[2] == "AMMO" || situationString0[2] == "MAP")
        {
            amountOfResponses = 0;
            noMoreDialogue = true;
        }
        else if (situationString0[2] == "2")
        {
            amountOfButtons = 2;
            amountOfResponses = 2;
            Debug.Log("amountOfResponses: " + amountOfResponses);
        }
        else if (situationString0[2] == "R2") //choose randomly between the first and the second choice (but only 1 button)
        {
            amountOfButtons = 1;
            SetButtons(true);
            randomSituationDialogue = rand.Next(0, 2);
            currentDialogueLineRow += randomSituationDialogue;
            randomDialogue = true;
        }
        else if (situationString0[2] == "R3") //choose randomly between the first, the second choice and the third choice (but only 1 button)
        {
            amountOfButtons = 1;
            SetButtons(true);
            randomSituationDialogue = rand.Next(0, 3);
            currentDialogueLineRow += randomSituationDialogue;
            randomDialogue = true;
        }
        else if (situationString0[2] == "3")
        {
            amountOfButtons = 3;
            amountOfResponses = 3;
            Debug.Log("amountOfResponses: " + amountOfResponses);
        }

        if (!randomDialogue) //no randomness in the dialogue
        {
            if (lines.Length > 1) //if there's more than 1 line of text in the read file
            {

                //Split the response to the first dialogue that happens everytime "1-asdf"
                string[] responseString1 = lines[1].Split('-');
                response1 = responseString1[1];
                amountOfDialogue -= 1;
                //check if there's more responses than just 1
                if (amountOfResponses > 1) //2nd response
                {
                    string[] responseString2 = lines[2].Split('-');
                    response2 = responseString2[1];
                    amountOfDialogue -= 1;
                }
                if (amountOfResponses == 3) //3rd response
                {
                    string[] responseString3 = lines[3].Split('-');
                    response3 = responseString3[1];
                    amountOfDialogue -= 1;
                }
            }
        }
        else //choose random dialogue
        {
            if (randomSituationDialogue == 0)
            {
                //currentDialogueLineRow = 1; //change here?
                string[] responseString1 = lines[1].Split('-');
                response1 = responseString1[1];
                currentDialogueLineRow = 1;
            }
            else if (randomSituationDialogue == 1)
            {
                string[] responseString1 = lines[2].Split('-');
                response1 = responseString1[1];
                currentDialogueLineRow = 2;
            }
            else
            {
                string[] responseString1 = lines[3].Split('-');
                response1 = responseString1[1];
                currentDialogueLineRow = 3;
            }
        }

        SetButtons(true);

        string dialogue;
        if (amountOfResponses == 0)
        {
            dialogue = dialogue0.Replace("\n", Environment.NewLine);
        }
        else if (amountOfResponses == 1)
        {
            dialogue = dialogue0 + "\n\n" + response1.Replace("\n", Environment.NewLine);
        }
        else if (amountOfResponses == 2)
        {
            dialogue = dialogue0 + "\n\n1. " + response1 + "\n2. " + response2.Replace("\n", Environment.NewLine);
        }
        else
        {
            dialogue = dialogue0 + "\n\n1. " + response1 + "\n2. " + response2 + "\n3. " + response3.Replace("\n", Environment.NewLine);
        }

        StartCoroutine(DynamicType(dialogue));
    }

    private void ClickedChoiceButton(int choice) //choice 3 == continue
    {
        amountOfButtons = 1;

        System.Random rand = new System.Random();
        if (choice == 1)
        {
            lastChoice = 1;
            dialogueEndSplit = situationDialogueLines[currentDialogueLineRow + 1];
            if (currentDialogueLineRow == 0)
                currentDialogueLineRow += 1;
            CheckDialogueEndSplit(dialogueEndSplit, 1); //tells what happens next
        }
        else if (choice == 2)
        {
            lastChoice = 2;
            dialogueEndSplit = situationDialogueLines[currentDialogueLineRow + 2];
            if (currentDialogueLineRow == 0)
                currentDialogueLineRow += 2;
            else
                currentDialogueLineRow++;
            CheckDialogueEndSplit(dialogueEndSplit, 2); //tells what happens next
        }
        else if (choice == 3) //button continue
        {
            if(AirshipStats.specialBattle)
            {
                SceneManager.LoadScene("Match3");
            }
            lastChoice = 3;
            if (noMoreDialogue)
            {
                dialogueEndSplit = situationDialogueLines[currentDialogueLineRow];
                CheckDialogueEndSplit(dialogueEndSplit, 1); //tells what happens next
            }
            else if (AirshipStats.showSituationEndingText)
            {
                AirshipStats.showSituationEndingText = false;
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }

            else if (AirshipStats.showMapTargetEndingText)
            {
                AirshipStats.showMapTargetEndingText = false;
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
            else
            {
                if (situationDialogueLines.Length - 1 == currentDialogueLineRow)
                    dialogueEndSplit = situationDialogueLines[currentDialogueLineRow];
                else
                {
                    if (currentDialogueLineRow == 0)
                        currentDialogueLineRow += 1;
                }
                CheckDialogueEndSplit(dialogueEndSplit, 1); //tells what happens next
            }
        }
        else //button 3
        {
            dialogueEndSplit = situationDialogueLines[currentDialogueLineRow + 3];
            if (currentDialogueLineRow == 0)
                currentDialogueLineRow += 3;
            CheckDialogueEndSplit(dialogueEndSplit, 3); //tells what happens next
        }
    }

    private void CheckDialogueEndSplit(string endSplitString, int choice)
    {
        if (currentDialogueLineRow > 2 && situationDialogueLines[currentDialogueLineRow].Split('-')[2] != "BATTLE")
        {
            string startSplitForNextChoices = situationDialogueLines[currentDialogueLineRow].Split('-')[0] + choice;
            string startSplitStr = "";
            int startDialogueRow = currentDialogueLineRow;

            do //find the next start pos
            {
                if (situationDialogueLines.Length - 1 == currentDialogueLineRow) //no more dialogue
                {
                    currentDialogueLineRow = startDialogueRow;
                    break;
                }
                currentDialogueLineRow++;
                string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                startSplitStr = dialogueStrings[0];
            } while (startSplitStr != (startSplitForNextChoices));
            //now currentDialogueLineRow is in correct pos
        }

        string startSplit = situationDialogueLines[currentDialogueLineRow].Split('-')[0];
        string dialogueStart = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
        endSplitString = situationDialogueLines[currentDialogueLineRow];

        amountOfButtons = 1;
        SetButtons(true);

        string endSplit = situationDialogueLines[currentDialogueLineRow].Split('-')[2];
        Debug.Log("String end split:  " + endSplit);
        System.Random random = new System.Random();
        System.Random rando = new System.Random();

        if (endSplit == "0")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            randomEncounterPanel.SetActive(false);
            buttonFly.SetActive(true);
        }
        else if (endSplit == "1")
        {
            string startSplitString = "";

            //if the choice is 1 then we need to find the line that starts with 11- if the choice is 2 then we need to find the line that starts with 21-
            do
            {
                currentDialogueLineRow++;
                string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                startSplitString = dialogueStrings[0];
            } while (startSplitString != (startSplit + "1"));

            string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
            //found the correct line, now we check if this line ends in choices
            if (situationDialogueLines[currentDialogueLineRow].Split('-')[2] == "2")
            {
                amountOfButtons = 2;
                SetButtons(true);
                //the first choice's startsplit is 1 or 2 but here we find the choices for it which are 11/12 or 21/22
                startSplit = startSplitString;
                do
                {
                    currentDialogueLineRow++;
                    string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                    startSplitString = dialogueStrings[0];
                } while (startSplitString != (startSplit + "1"));
                string[] responseString1 = situationDialogueLines[currentDialogueLineRow].Split('-');
                response1 = responseString1[1];
                //CHANGE
                string[] responseString2 = situationDialogueLines[currentDialogueLineRow + 1].Split('-');
                response2 = responseString2[1];
                //currentDialogueLineRow++;
                //string[] responseString2 = situationDialogueLines[currentDialogueLineRow].Split('-');
                response2 = responseString2[1];
                nextSentence = nextSentence + "\n\n1. " + response1 + "\n2. " + response2.Replace("\n", Environment.NewLine);
            }

            DynamicNextSentence(nextSentence);
        }
        else if (endSplit == "2")
        {
            amountOfButtons = 2;
            SetButtons(true);

            string startSplitString = "";
            dialogueStart = situationDialogueLines[currentDialogueLineRow].Split('-')[1];

            //the first choice is 1 or 2 but here we find the second choice so they are 11 or 21
            do
            {
                currentDialogueLineRow++;
                string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                startSplitString = dialogueStrings[0];
            } while (startSplitString != (startSplit + "1"));

            string[] responseString1 = situationDialogueLines[currentDialogueLineRow].Split('-');
            response1 = responseString1[1];
            string[] responseString2 = situationDialogueLines[currentDialogueLineRow + 1].Split('-');
            response2 = responseString2[1];

            string dialogue = dialogueStart + "\n\n1. " + response1 + "\n2. " + response2.Replace("\n", Environment.NewLine);

            DynamicNextSentence(dialogue);
        }
        else if (endSplit == "R2") //choose randomly between the first and the second choice (but only 1 button)
        {
            randomSituationDialogue = rando.Next(1, 3);

            string newStartSplit = startSplit + randomSituationDialogue;
            string startSplitString = "";

            do
            {
                currentDialogueLineRow++;
                string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                startSplitString = dialogueStrings[0];
            } while (startSplitString != (newStartSplit));

            string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
            DynamicNextSentence(nextSentence);
        }
        else if (endSplit == "R3") //choose randomly between the first, the second choice and the third choice (but only 1 button)
        {
            randomSituationDialogue = rando.Next(1, 4);

            string newStartSplit = startSplit + randomSituationDialogue;
            string startSplitString = "";

            do
            {
                currentDialogueLineRow++;
                string[] dialogueStrings = situationDialogueLines[currentDialogueLineRow].Split('-');
                startSplitString = dialogueStrings[0];
            } while (startSplitString != (newStartSplit));

            string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
            DynamicNextSentence(nextSentence);
        }
        else if (endSplit == "BATTLE")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            string startingRow = endSplitString.Split('-')[0];
            if (startingRow[0] == '\n')
                startingRow = endSplitString.Split('-')[0].Split('\n')[1];
            string startSplitString = "";
            int row = 0;
            do
            {
                row++;
                if (row >= situationDialogueLines.Length - 1) //if the situation answer is not found then break out
                {
                    AirshipStats.showSituationEndingText = false;
                    break;
                }
                string[] dialogueStrings = situationDialogueLines[row].Split('-');
                startSplitString = dialogueStrings[0];
                if (startSplitString[0] == '\n')
                    startSplitString = dialogueStrings[0].Split('\n')[1];
                AirshipStats.showSituationEndingText = true;
                AirshipStats.situationEndingText = situationDialogueLines[row].Split('-')[1];
                //AirshipStats.situationEndingText = situationDialogueLines[row];
                Debug.Log("Changed match3EndingText to: " + AirshipStats.situationEndingText);
            } while (startSplitString != (startingRow + "1"));

            AirshipStats.battleMusicOn = true;
            AudioManager.instance.turnMusicOff();
            SceneManager.LoadScene("Match3");
        }
        else if (endSplit == "SHOP")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            string startingRow = endSplitString.Split('-')[0];
            string startSplitString = "";
            int row = 0;
            do
            {
                row++;
                if (row > situationDialogueLines.Length - 2) //if the situation answer is not found then break out
                {
                    AirshipStats.showSituationEndingText = false;
                    break;
                }
                string[] dialogueStrings = situationDialogueLines[row].Split('-');
                startSplitString = dialogueStrings[0];
                AirshipStats.showSituationEndingText = true;
                AirshipStats.situationEndingText = situationDialogueLines[row].Split('-')[1];
                Debug.Log("Changed shopEndingText to: " + AirshipStats.situationEndingText);
            } while (startSplitString != (startingRow + "1"));

            randomEncounterPanel.SetActive(false);
            shopPanel.SetActive(true);
            shop2ButtonClosed.SetActive(false);
            shop2ButtonOpened.SetActive(true);
            //shopButton.SetActive(true);
            buttonFly.SetActive(true);
        }
        else if (endSplit == "MONEY")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            int rand = 0;
            if (endSplitString.Split('-')[3] == "S")
            {
                rand = random.Next(50, 150);
                Debug.Log("Got a small amount of money " + rand);
                AirshipStats.credits += rand;
                InstantiateBonusEffect(CreditsEffectPrefab);
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
            else if (endSplitString.Split('-')[3] == "M")
            {
                rand = random.Next(150, 250);
                Debug.Log("Got a medium amount of money " + rand);
                AirshipStats.credits += rand;
                InstantiateBonusEffect(CreditsEffectPrefab);
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
            else if (endSplitString.Split('-')[3] == "L")
            {
                rand = random.Next(250, 400);
                Debug.Log("Got a large amount of money " + rand);
                AirshipStats.credits += rand;
                InstantiateBonusEffect(CreditsEffectPrefab);
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
            else //we lose money
            {
                if (AirshipStats.credits >= 50)
                {
                    if (AirshipStats.credits >= 250)
                        rand = random.Next(50, 250);
                    else
                        rand = random.Next(50, AirshipStats.credits);
                }
                Debug.Log("Gave away money: " + rand);
                AirshipStats.credits -= rand;
                InstantiateBonusEffect(CreditsLostEffectPrefab);
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
        }
        else if (endSplit == "AMMO")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            int rand = 0;
            if (endSplitString.Split('-')[3] == "S")
            {
                rand = random.Next(1, 5);
                Debug.Log("Got a small amount of ammo " + rand);
            }
            else if (endSplitString.Split('-')[3] == "M")
            {
                rand = random.Next(3, 8);
                Debug.Log("Got a medium amount of ammo " + rand);
            }
            else if (endSplitString.Split('-')[3] == "L")
            {
                rand = random.Next(6, 12);
                Debug.Log("Got a large amount of ammo " + rand);
            }
            AirshipStats.ammo1 += rand;
            InstantiateBonusEffect(AmmoEffectPrefab);
            randomEncounterPanel.SetActive(false);
            buttonFly.SetActive(true);
        }
        else if (endSplit == "REPAIR")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            int rand = 0;
            if (endSplitString.Split('-')[3] == "S")
            {
                rand = random.Next(10, 20);
                Debug.Log("Was repaired by a small amount " + rand);
            }
            else if (endSplitString.Split('-')[3] == "M")
            {
                rand = random.Next(20, 30);
                Debug.Log("Was repaired by a medium amount " + rand);
            }
            else if (endSplitString.Split('-')[3] == "L")
            {
                rand = random.Next(30, 40);
                Debug.Log("Was repaired by a large amount " + rand);
            }
            RepairShip(rand);
            InstantiateBonusEffect(RepairEffectPrefab);
            randomEncounterPanel.SetActive(false);
            buttonFly.SetActive(true);
        }
        else if (endSplit == "CREW")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            shopUI.BuyCrewMemberClicked();
            InstantiateBonusEffect(CrewMemberEffectPrefab);
            randomEncounterPanel.SetActive(false);
            buttonFly.SetActive(true);
        }
        else if (endSplit == "RANDOM")
        {
            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }
            int rand = 0;
            rand = random.Next(0, 4);
            if (rand == 0)
            {
                rand = random.Next(50, 250);
                InstantiateBonusEffect(CreditsEffectPrefab);
                Debug.Log("Random prize money: " + rand);
                AirshipStats.credits += rand;
            }
            else if (rand == 1)
            {
                rand = random.Next(2, 10);
                InstantiateBonusEffect(AmmoEffectPrefab);
                Debug.Log("Random prize ammo: " + rand);
                AirshipStats.ammo1 += rand;
            }
            else if (rand == 2)
            {
                rand = random.Next(1, 5);
                InstantiateBonusEffect(GasEffectPrefab);
                Debug.Log("Random prize gas: " + rand);
                AirshipStats.gas += rand;
            }
            else
            {
                rand = random.Next(15, 30);
                InstantiateBonusEffect(RepairEffectPrefab);
                Debug.Log("Random prize repairs: " + rand);
                RepairShip(rand);
            }

            string startingRow = endSplitString.Split('-')[0];
            string startSplitString = "";
            int row = 0;
            do
            {
                row++;
                if (row > situationDialogueLines.Length - 2) //if the situation answer is not found then break out
                {
                    AirshipStats.showSituationEndingText = false;
                    break;
                }
                string[] dialogueStrings = situationDialogueLines[row].Split('-');
                startSplitString = dialogueStrings[0];
                AirshipStats.showSituationEndingText = true;
                AirshipStats.situationEndingText = situationDialogueLines[row].Split('-')[1];
                Debug.Log("Changed randomEndingText to: " + AirshipStats.situationEndingText);
            } while (startSplitString != (startingRow + "1"));

            if (AirshipStats.showSituationEndingText)
            {
                ShowEndingDialogue();
            }
            else
            {
                randomEncounterPanel.SetActive(false);
                buttonFly.SetActive(true);
            }
        }
        else if (endSplit == "MAP")
        {

            if (lastChoice == 1 || lastChoice == 2)
            {
                amountOfButtons = 1;
                SetButtons(true);
                string nextSentence = situationDialogueLines[currentDialogueLineRow].Split('-')[1];
                DynamicNextSentence(nextSentence);
                return;
            }

            //Choose a random island to mark as a target. The target cannot be exit or an island we can currently go to.
            int length = AirshipStats.islandsInformation.Count;
            int i = 0;
            do
            {
                int island = random.Next(0, length - 1); //choose a random island
                if (AirshipStats.islandsInformation[island]._canGo == false && AirshipStats.islandsInformation[island]._exit == false && AirshipStats.islandsInformation[island]._searchNext == false)
                {
                    //found a suitable target island
                    AirshipStats.islandsInformation[island]._mapTarget = true;
                    Debug.Log("Chose island " + AirshipStats.islandsInformation[island]._name + " as a target island.");
                    break;
                }
                i++;
                if (i > 100) //if we cannot find a suitable target island just choose 1
                {
                    AirshipStats.islandsInformation[1]._mapTarget = true;
                    Debug.Log("Chose island " + AirshipStats.islandsInformation[island]._name + " as a target island because couldn't find suitable (i > 100).");
                    break;
                }
            } while (true);
            randomEncounterPanel.SetActive(false);
            buttonFly.SetActive(true);
        }
    }

    private void RepairShip(int amount)
    {
        if (AirshipStats.airshipCurrentHealth <= AirshipStats.airshipMaxHealth - amount)
        {
            AirshipStats.airshipCurrentHealth += amount;
        }
        else if (AirshipStats.airshipCurrentHealth < AirshipStats.airshipMaxHealth)
        {
            AirshipStats.airshipCurrentHealth = AirshipStats.airshipMaxHealth;
        }
    }

    private void SetButtons(bool on)
    {
        if (on)
        {
            if (amountOfButtons == 1)
            {
                buttonChoice1.SetActive(false);
                buttonChoice2.SetActive(false);
                buttonChoice3.SetActive(false);
                buttonContinue.SetActive(true);
            }
            else if (amountOfButtons == 2)
            {
                buttonChoice1.SetActive(true);
                buttonChoice2.SetActive(true);
                buttonChoice3.SetActive(false);
                buttonContinue.SetActive(false);
            }
            else
            {
                buttonChoice1.SetActive(true);
                buttonChoice2.SetActive(true);
                buttonChoice3.SetActive(true);
                buttonContinue.SetActive(false);
            }
        }
        else
        {
            buttonChoice1.SetActive(false);
            buttonChoice2.SetActive(false);
            buttonChoice3.SetActive(false);
            buttonContinue.SetActive(false);
            buttonSkipText.SetActive(true);
        }
    }

    public void ShowEndingDialogue()
    {
        randomEncounterPanel.SetActive(true);
        amountOfButtons = 1;

        DynamicNextSentence(AirshipStats.situationEndingText);

        AirshipStats.situationEndingText = "";
    }

    public void ShowMapTargetDialogue()
    {
        randomEncounterPanel.SetActive(true);
        amountOfButtons = 1;

        System.Random ra = new System.Random();
        int days = ra.Next(0, 1001); //choose the amount of days

        if (AirshipStats.howManyNewCrew <= 4) //not full crew
        {
            if (AirshipStats.mapTargetEndingText == "")
                AirshipStats.mapTargetEndingText = "You see a HELP sign written on the sand and fly closer. A man climbs down from a tree house and tells you that he shipwrecked on the island " + days + " days ago. He asks to join your crew which you gladly accept.";
            InstantiateBonusEffect(CrewMemberEffectPrefab);
            AirshipStats.howManyNewCrew++; //add a new crew member
        }
        else
        {
            if (AirshipStats.mapTargetEndingText == "") //full crew
                AirshipStats.mapTargetEndingText = "You see a HELP sign written on the sand and fly closer. A man climbs down from a tree house and tells you that he shipwrecked on the island " + days + " days ago. He gives you all of his credits as a thank you for saving him.";
            AirshipStats.credits += 500;
            InstantiateBonusEffect(CreditsEffectPrefab);
        }

        DynamicNextSentence(AirshipStats.mapTargetEndingText);
        AirshipStats.mapTargetEndingText = "";
    }

    //method instantiates a partical effect that tells the player what bonus (credits, ammo...) he got
    private void InstantiateBonusEffect(GameObject bonus)
    {
        Destroy(Instantiate(bonus, new Vector3(-6.7f, 23.5f, 36.6f), Quaternion.identity), 6);
    }

    private void InitializeTheScene()
    {
        shopUI = GameObject.FindObjectOfType<ShopUi>();
        buttonChoice1.GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(1));
        buttonChoice2.GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(2));
        buttonChoice3.GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(4));
        buttonContinue.GetComponent<Button>().onClick.AddListener(() => ClickedChoiceButton(3));
        buttonSkipText.GetComponent<Button>().onClick.AddListener(() => ClickedSkipButton());
        shop2ButtonOpened.GetComponent<Button>().onClick.AddListener(() => ClickedShopButton());
        buttonChoice1.SetActive(false);
        buttonChoice2.SetActive(false);
        buttonChoice3.SetActive(false);
        buttonContinue.SetActive(false);
        buttonFly.SetActive(false);
        buttonSkipText.SetActive(false);
    }

    private void ClickedShopButton()
    {
        if (AirshipStats.showSituationEndingText)
        {
            ShowEndingDialogue();
        }
    }

    private void ClickedSkipButton()
    {
        Debug.Log("clicked skip button");
        //StopCoroutine("DynamicType");
        //textDisplay.text = currentDialogue;
        clickedSkipButton = true;
    }


}