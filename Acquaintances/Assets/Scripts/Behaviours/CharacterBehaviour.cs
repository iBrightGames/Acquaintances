using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class CharacterBehaviour : MonoBehaviour
{

    public Character character;


    [FoldoutGroup("Hierarchy Members")]
    public SpriteRenderer spriteRenderer;

    [FoldoutGroup("Hierarchy Members")]
    public TextMeshProUGUI nameField;

    [FoldoutGroup("Hierarchy Members")]
    public Slider relationBar;

    [FoldoutGroup("Hierarchy Members")]
    public Transform talkPosition;

    [FoldoutGroup("Hierarchy Members")]
    public GameObject maleObject, femaleObject;

    [ContextMenu("Init")]
    public void Init()
    {

        character.relation = 50;

        nameField.text = character.characterName;
        spriteRenderer.sprite = character.view;
        relationBar.value = character.Relation;

        maleObject.SetActive(character.sex == Character.Sex.Male);
        femaleObject.SetActive(character.sex == Character.Sex.Female);

        character.OnRelationChange += UpdateCharacter;
        character.OnRelationEmpty += UpdateCharacter;
        character.OnRelationFull += UpdateCharacter;

    }

    public void UpdateCharacter()
    {
        relationBar.value = character.Relation;
    }

    public void Effect(Card.Effect effect, ref bool effected)
    {

        effected = false;

        if(character.traits.Contains(effect.trait))
        {
            character.AddRelation(effect.effect);
            effected = true;
        }

        if(effect.includeCounter && character.traits.Contains(effect.trait.counterTrait))
        {
            character.AddRelation(-effect.effect);
            effected = true;
        }

        

    }

    public void Talk()
    {
        GetComponent<AudioSource>().clip = character.clips[Random.Range(0, character.clips.Length)];
        GetComponent<AudioSource>().Play();
    }

    public void StopTalking()
    {
        GetComponent<AudioSource>().Stop();
    }

}
