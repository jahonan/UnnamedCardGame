using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Card", menuName ="Card")]
public class Card : ScriptableObject {

    public new string name;
    public string description;

    //type of card tells us what sort of action will it do when played
    public int type;
    public int notes;
    public int charisma;

    //I need id because it's easier for me to code deck that way.
    //I'm bad at this, I know.
    public int id;
}
