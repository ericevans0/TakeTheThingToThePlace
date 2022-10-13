using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClockDisplay : MonoBehaviour
{
    public int hour = 0;
    [SerializeField] string Label;
    [SerializeField] GameObject TimeDisplay;
    private TMP_Text hourDisplay;

    private void Start()
    {
        hourDisplay = TimeDisplay.GetComponent<TMP_Text>();
    }
    private void Update()
    {
        Debug.Log($"{Label} hour = {hour}");
        hourDisplay.text = Label + ": " + hour.ToString();
    }
}
