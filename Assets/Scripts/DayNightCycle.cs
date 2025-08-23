using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    public float timeSpeed;
    public float hourStart;
    public float hourSunrise;
    public float hourSunset;
    public float sunMaxIntensity;
    public float moonMaxIntensity;
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private Color dayAmbience;
    [SerializeField] private Color nightAmbience;
    [SerializeField] private AnimationCurve lightTransitionCurve;
    [SerializeField] private Transform clockHand;
    [SerializeField] private Image clockBase;
    [SerializeField] private Image clockLight;
    private float sunRotation;
    private float sunVector;
    private DateTime curr;
    private TimeSpan sunrise;
    private TimeSpan sunset;

    void Start()
    {
        curr = DateTime.Now.Date + TimeSpan.FromHours(hourStart);
        sunrise = TimeSpan.FromHours(hourSunrise);
        sunset = TimeSpan.FromHours(hourSunset);
    }

    void Update()
    {
        UpdateTime();
        rotateSun();
        updateLight();
    }

    private void UpdateTime()
    {
        curr = curr.AddSeconds(Time.deltaTime * timeSpeed);
    }

    private void rotateSun()
    {
        if (curr.TimeOfDay > sunrise && curr.TimeOfDay < sunset)
        {
            TimeSpan dayDuration = timeDiff(sunrise, sunset);
            TimeSpan dayPassed = timeDiff(sunrise, curr.TimeOfDay);
            float dayProgress = (float)(dayPassed.TotalMinutes / dayDuration.TotalMinutes);
            sunRotation = Mathf.Lerp(0f, 180f, dayProgress);
            clockHand.eulerAngles = new Vector3(0f, 0f, dayProgress * -360f + 125f);
            clockLight.enabled = true;
            clockBase.fillAmount = 1;
            clockLight.fillAmount = dayProgress;
        }
        else
        {
            TimeSpan nightDuration = timeDiff(sunset, sunrise);
            TimeSpan nightPassed = timeDiff(sunset, curr.TimeOfDay);
            float nightProgress = (float)(nightPassed.TotalMinutes / nightDuration.TotalMinutes);
            sunRotation = Mathf.Lerp(180, 360, nightProgress);
            clockHand.eulerAngles = new Vector3(0f, 0f, nightProgress * -360f + 125f);
            clockLight.enabled = false;
            clockBase.fillAmount = nightProgress;
        }
        sun.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }

    private void updateLight()
    {
        sunVector = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, sunMaxIntensity, lightTransitionCurve.Evaluate(sunVector));
        moon.intensity = Mathf.Lerp(moonMaxIntensity, 0, lightTransitionCurve.Evaluate(sunVector));
        RenderSettings.ambientLight = Color.Lerp(dayAmbience, nightAmbience, lightTransitionCurve.Evaluate(sunVector));
    }

    private TimeSpan timeDiff(TimeSpan a, TimeSpan b)
    {
        TimeSpan diff = b - a;
        if (diff.TotalSeconds < 0) diff += TimeSpan.FromHours(24);
        return diff;
    }
}