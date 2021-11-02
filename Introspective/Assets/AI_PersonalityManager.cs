using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityTrait
{
    public string traitName;

    public PersonalityTrait(string newName)
    {
        traitName = newName;
    }
}

public class AI_PersonalityManager : MonoBehaviour
{
    [Header("Personality Traits")]
    public string[] tempermentTraits;
    public string[] socialTraits;
    public string[] physicalTraits;


    public List<PersonalityTrait> personality = new List<PersonalityTrait>();


    public void CreatePersonality()
    {
        string temperTrait = tempermentTraits[Random.Range(0, tempermentTraits.Length - 1)];
        personality.Add(new PersonalityTrait(temperTrait));

        string socialTrait = socialTraits[Random.Range(0, socialTraits.Length - 1)];
        personality.Add(new PersonalityTrait(socialTrait));

        string physicalTrait = physicalTraits[Random.Range(0, physicalTraits.Length - 1)];
        personality.Add(new PersonalityTrait(physicalTrait));
    }

}
