using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBrain : MonoBehaviour
{
    public List<PersonalityTrait> personalityTraits;

    public AI_PersonalityManager personalityManager;
    private NPCMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = this.GetComponent<NPCMovement>();

        personalityManager.CreatePersonality();
        personalityTraits = personalityManager.personality;

        //This will use the personality as needed
        for (int t = 0; t < personalityTraits.Count; t++)
        {
            string trait = personalityTraits[t].traitName;
            if (trait == "Fit" || trait == "Average" | trait == "OutOfShape")
            {
                movement.SetMoveSpeed(trait);
                movement.StartWalking();
            }
        }
    }

}
