using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stage Containers")]
    public GameObject stageForest;
    public GameObject stageDump;

    [Header("UI")]
    public TMP_Dropdown stageDropdown;

        // Stage list
    private string[] stageNames = { "Forest", "Dump"};

    private void Start()
    {
        // Prevent duplicate listeners
        stageDropdown.onValueChanged.RemoveAllListeners();

        stageDropdown.ClearOptions();

        stageDropdown.AddOptions(new System.Collections.Generic.List<string>(stageNames));

        stageDropdown.onValueChanged.AddListener(ChangeStage);

        // Initialize stage state
        ChangeStage(stageDropdown.value);
    }

    public void ChangeStage(int index)
    {
        stageForest.SetActive(index == 0);
        stageDump.SetActive(index == 1);
    }
}