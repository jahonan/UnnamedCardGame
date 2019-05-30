using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Text notesText;
    public Text charismaText;
    public Image type;

	// Use this for initialization
	/*void Start () {
        nameText.text = card.name;
        descriptionText.text = card.description;
        notesText.text = card.notes.ToString();
        charismaText.text = card.charisma.ToString();
        if (card.type == 1)
        { type.color = Color.white; }
        else
        {
            if (card.type == 2)
            {
                type.color = Color.blue;
            }
            else { type.color = Color.green; }
            
        }
        
	}*/

    public void refresh () {
        nameText.text = card.name;
        descriptionText.text = card.description;
        notesText.text = card.notes.ToString();
        charismaText.text = card.charisma.ToString();
        if (card.type == 1)
        { type.color = Color.white; }
        else
        {
            if (card.type == 2)
            {
                type.color = Color.blue;
            }
            else { type.color = Color.green; }

        }

    }

}
