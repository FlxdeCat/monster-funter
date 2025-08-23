using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCLooking : NPCState
{

    private int messageCount;
    private bool talkToLyra;
    private bool missionDarian = false;
    private bool missionCesiya = false;
    private Vector3 lookRotate;
    private Quaternion lookRotation;
    private CharacterMovement playerCM;

    public override void enterState(NPCController controller)
    {
        playerCM = this.player.GetComponent<CharacterMovement>();
        this.nmAgent.destination = this.npc.transform.position;
        this.interactHUD.SetActive(true);
        messageCount = 0;
        talkToLyra = false;
    }
    public override void whileState(NPCController controller)
    {
        lookRotate = new Vector3(this.player.transform.position.x - this.npc.transform.position.x, 0f, this.player.transform.position.z - this.npc.transform.position.z);
        lookRotation = Quaternion.LookRotation(lookRotate);
        this.npc.transform.rotation = Quaternion.RotateTowards(this.npc.transform.rotation, lookRotation, 600f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (this.npc.name != "Lyra")
            {
                if (messageCount == this.npcMessages.Length && this.npc.name == "Darian" && !missionDarian && this.playerCM.mission.missionIndex == 3 && this.playerCM.mission.missionActive)
                {
                    missionDarian = true;
                    this.playerCM.mission.tracker.addProgress();
                }
                if (messageCount == this.npcMessages.Length && this.npc.name == "Cesiya" && !missionCesiya && this.playerCM.mission.missionIndex == 3 && this.playerCM.mission.missionActive)
                {
                    missionCesiya = true;
                    this.playerCM.mission.tracker.addProgress();
                }
                this.messageTxt.text = "";
                if (messageCount < this.npcMessages.Length)
                {
                    controller.showText(this.npcMessages[messageCount]);
                }
                else
                {
                    controller.showText("");
                }
                messageCount++;
            }
            else
            {
                if (playerCM.mission.missionIndex == 0)
                {
                    if (this.playerCM.mission.tracker.currProgress == 0) this.playerCM.mission.tracker.addProgress();
                    if (messageCount == 5 && !this.playerCM.mission.missionActive) MissionGiver.giveNextQuest();
                    this.messageTxt.text = "";
                    if (messageCount < 5)
                    {
                        controller.showText(this.npcMessages[messageCount]);
                    }
                    else
                    {
                        controller.showText("");
                    }
                    messageCount++;
                }
                else if(playerCM.mission.missionIndex == 1)
                {
                    if (playerCM.mission.missionActive)
                    {
                        if (PlayerPrefs.GetString("chosenCharacter") == "Wizard") controller.showText(this.npcMessages[5]);
                        else controller.showText(this.npcMessages[6]);
                    }
                    else
                    {
                        if (!talkToLyra)
                        {
                            talkToLyra = true;
                            messageCount = 7;
                        }
                        if (messageCount == 9 && !this.playerCM.mission.missionActive)
                        {
                            talkToLyra = false;
                            MissionGiver.giveNextQuest();
                        }
                        this.messageTxt.text = "";
                        if (messageCount < 9)
                        {
                            controller.showText(this.npcMessages[messageCount]);
                        }
                        else
                        {
                            controller.showText("");
                        }
                        messageCount++;
                    }
                }
                else if (playerCM.mission.missionIndex == 2)
                {
                    if (playerCM.mission.missionActive) controller.showText(this.npcMessages[9]);
                    else
                    {
                        if (!talkToLyra)
                        {
                            talkToLyra = true;
                            messageCount = 10;
                        }
                        if (messageCount == 12 && !this.playerCM.mission.missionActive)
                        {
                            talkToLyra = false;
                            MissionGiver.giveNextQuest();
                        }
                        this.messageTxt.text = "";
                        if (messageCount < 12)
                        {
                            controller.showText(this.npcMessages[messageCount]);
                        }
                        else
                        {
                            controller.showText("");
                        }
                        messageCount++;
                    }
                }
                else if (playerCM.mission.missionIndex == 3)
                {
                    if (playerCM.mission.missionActive) controller.showText(this.npcMessages[12]);
                    else
                    {
                        if (!talkToLyra)
                        {
                            talkToLyra = true;
                            messageCount = 13;
                        }
                        if (messageCount == 15 && !this.playerCM.mission.missionActive)
                        {
                            talkToLyra = false;
                            MissionGiver.giveNextQuest();
                        }
                        this.messageTxt.text = "";
                        if (messageCount < 15)
                        {
                            controller.showText(this.npcMessages[messageCount]);
                        }
                        else
                        {
                            controller.showText("");
                        }
                        messageCount++;
                    }
                }
                else if (playerCM.mission.missionIndex == 4)
                {
                    if (playerCM.mission.missionActive) controller.showText(this.npcMessages[15]);
                }
            }
        }
        if (!NPCController.npcNearPlayer(player, npc))
        {
            controller.swap(controller.stay);
        }
    }
    public override void endState(NPCController controller)
    {
        this.interactHUD.SetActive(false);
        controller.showText("");
    }

}
