using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunModifiersMenu : MonoBehaviour
{
    Dictionary<string, string> RunModifiers = new Dictionary<string, string>();

    int deathRuleSetting = 1;

    public TextMeshProUGUI deathRuleText;
    public Button deathRuleButton;
    Button focusedButton;

    PlayerActions controls;

    public Animator sceneAnim;

    private void Start()
    {
        controls = new PlayerActions();

        controls.Enable();
    }

    public void StartRun()
    {
        sceneAnim.SetBool("runStarted", true);
        this.GetComponent<Timer>().StartTimer();
    }

    public void ChangeDeathRule()
    {
        deathRuleSetting += 1;

        if (deathRuleSetting > 3)
        {
            deathRuleSetting = 1;
        }

        string ruleText = "";

        switch (deathRuleSetting)
        {
            case 1: //One Life
                ruleText = "One Life";
                break;

            case 2: //3 Lives w/ Checkpoints
                ruleText = "3 Checkpoint Lives";
                break;

            case 3: //God Mode
                ruleText = "Infinite Lives";
                break;
        }

        deathRuleText.text = ruleText;
    }
}
