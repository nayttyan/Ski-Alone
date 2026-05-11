using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private bool racing=false;
    public delegate void TimerEvent();
    private TimeSpan bestTime;
    [SerializeField] private int penaltyTimeVal = 3;
    [SerializeField] private TMP_Text raceTimeText, bestTimeText;
    [SerializeField] private string bestTimeKey = "LVLBestTime";

    private void OnEnable()
    {
        StarpGate.StartRace += OnRaceStart;
        FinishGate.FinishRace += OnRaceFinish;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey(bestTimeKey))
        {
            int bestTimeTicks = PlayerPrefs.GetInt(bestTimeKey);
            bestTime = new TimeSpan(bestTimeTicks);
            bestTimeText.text = "BEST TIME: " + bestTime.ToString("ss\\:ff");
        }
        else
        {
            bestTime = new TimeSpan(long.MaxValue);
            bestTimeText.text = "BEST TIME: --:--";
        }
    }

    void AddRacePenalty()
    {
        penaltyTime += new TimeSpan(0, 0, penaltyTimeVal);
    }
    void OnRaceStart()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Race started");
    }

    void OnRaceFinish()
    {
        racing = false;
        if(raceTime < bestTime)
        {
            bestTime = raceTime;
            bestTimeText.text = "BEST TIME: " + bestTime.ToString("ss\\:ff");
            bestTimeText.color = Color.gold;
            PlayerPrefs.SetInt(bestTimeKey, (int)bestTime.Ticks);
            PlayerPrefs.Save();
        }
       // Debug.Log("Race FINISHED");
    }

   private void Update()
    {
        if (racing) 
            raceTime = DateTime.Now - raceStart + penaltyTime;
        //Debug.Log("Race time" + raceTime);
        raceTimeText.text = "Time: " + raceTime.ToString("ss\\:ff");
    }
}
