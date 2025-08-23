using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionGiver : MonoBehaviour
{
    public Mission[] allMissions;
    private static Mission[] _allMissions;
    private static TMP_Text _missionTxt;
    private static CharacterMovement player;
    private static Transform _missionAnnouncer;
    private static GameObject _mazePortal;
    [SerializeField] private TMP_Text missionTxt;
    [SerializeField] private GameObject playerComponents;
    [SerializeField] private Transform missionAnnouncer;
    [SerializeField] private GameObject mazePortal;

    void Start()
    {
        player = playerComponents.GetComponent<CharacterMovement>();
        _missionTxt = missionTxt;
        _allMissions = allMissions;
        _missionAnnouncer = missionAnnouncer;
        _mazePortal = mazePortal;

        player.mission = allMissions[0];
    }

    void Update()
    {
        if (player.mission.tracker.isFinished() && player.mission.missionActive)
        {
            _missionTxt.color = Color.green;
            missionCompleted();
        }
        if (player.mission.missionIndex != 4) _missionTxt.text = player.mission.missionDescription + " (" + player.mission.tracker.currProgress + "/" + player.mission.tracker.goalProgress + ")";
        else _missionTxt.text = player.mission.missionDescription;
    }

    public void missionCompleted()
    {
        StartCoroutine(announceMissionComplete());
        player.mission.missionActive = false;
    }

    public static void giveNextQuest()
    {
        _missionTxt.color = Color.white;
        int newMissionIndex = player.mission.missionIndex + 1;
        player.mission = _allMissions[newMissionIndex];
        player.mission.missionActive = true;
        if (newMissionIndex == 4) _mazePortal.SetActive(true);
    }

    IEnumerator announceMissionComplete()
    {
        missionAnnouncer.localPosition = new Vector2(0, Screen.height);
        missionAnnouncer.LeanMoveLocalY(0, 0.5f).setEaseOutExpo();
        yield return new WaitForSeconds(2f);
        missionAnnouncer.LeanMoveLocalY(Screen.height, 0.5f).setEaseOutExpo();
    }

}
