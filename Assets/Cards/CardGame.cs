using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardGame : MonoBehaviour {

    //We start from making lists for cards
    public List<Card> CardDeck = new List<Card>();
    public List<Card> CardDiscards = new List<Card>();
    //We make lists for players' cards in their hands
    public List<Card> player1Deck = new List<Card>();

    //We need to control what cards are on screen, so We make a list containing GameObjects
    public List<GameObject> cardsShown = new List<GameObject>();
    public bool cardIsChosen;
    public Card chosenCard;
    public int indexOfChosenCard = -1;
    public GameObject chosenCardGameObject;

    //We need to have players in our game (duhhh...)
    //So We make a list of players using Player class
    public List<Player> playerList = new List<Player>();
    public int currentPlayer = 0;

    //This is for UI, Panel is for popup window
    public GameObject panel;
    public GameObject awaitingForNextPlayerPanel;
    public GameObject awaitingForNextPlayerText;
    public GameObject victimGroup;
    public List<GameObject> chooseVictim = new List<GameObject>();
    public int chosenVictim = -1;
    public GameObject playerTag;
    public GameObject playerNotes;
    public GameObject playerCharisma;

    public Card CardToChange;
    private Card CardToDisplay;
    public GameObject ChangedCard;

    public Card cardToMove;

    void Start()
    {
       //for (int j = 0; j < 5; j++)
        //{


            //At the beginning we take 5 cards to show at screen
            for (int i = 0; i < 5; i++)
            {
                
                player1Deck.Add(DrawCard());
                cardsShown[i].GetComponent<CardDisplay>().card = player1Deck[i];
                cardsShown[i].GetComponent<CardDisplay>().refresh();

            }
        //}
    }

    public void StartGame()
    {
        //here we put player hand as the cards on screen
        for (int i =0; i < 5; i++)
        {
            cardsShown[i].GetComponent<CardDisplay>().card = playerList[currentPlayer].playerHand[i];
            cardsShown[i].GetComponent<CardDisplay>().refresh();
            playerTag.GetComponent<TextMeshProUGUI>().text = playerList[currentPlayer].name;
            playerNotes.GetComponent<TextMeshProUGUI>().text = playerList[currentPlayer].notes.ToString();
            playerCharisma.GetComponent<TextMeshProUGUI>().text = playerList[currentPlayer].charisma.ToString();

        }
    }

    public void Update()
    {
        //for now, there's only pause menu activation
        if (Input.GetKey(KeyCode.Escape))
        {
            panel.SetActive(true);
        }
    }

    public Card DrawCard ()
    {
        //We need to get a card from the deck an put it into player's hand
        //Then remove that card from the deck
        if (CardDeck.Count > 0)
        {

            int temporary;
            temporary = Random.Range(0, CardDeck.Count - 1);
            cardToMove = CardDeck[temporary];
            CardDeck.RemoveAt(temporary);
            return cardToMove;
        }
        else return null;
    }

    public void ChangeCard(int cardNumber)
    {
        player1Deck[cardNumber] = DrawCard();
        cardsShown[cardNumber].GetComponent<CardDisplay>().card= player1Deck[cardNumber];
        cardsShown[cardNumber].GetComponent<CardDisplay>().refresh();
        if (cardIsChosen == false)
        {
            cardsShown[cardNumber].GetComponent<RectTransform>().anchoredPosition += Vector2.up * 10;
            chosenCard = cardsShown[cardNumber].GetComponent<CardDisplay>().card;
            chosenCardGameObject = cardsShown[cardNumber];
            cardIsChosen = true;
        }
        else
        {
            chosenCardGameObject.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 10;
            cardsShown[cardNumber].GetComponent<RectTransform>().anchoredPosition += Vector2.up * 10;
            chosenCard = cardsShown[cardNumber].GetComponent<CardDisplay>().card;
            chosenCardGameObject = cardsShown[cardNumber];
        }
    }

    public void ChooseCard(int cardNumber)
    {
        if (cardIsChosen == false)
        {
            cardsShown[cardNumber].GetComponent<RectTransform>().anchoredPosition += Vector2.up * 10;
            chosenCard = cardsShown[cardNumber].GetComponent<CardDisplay>().card;
            chosenCardGameObject = cardsShown[cardNumber];
            cardIsChosen = true;

            if (chosenCard.type == 2 || chosenCard.type == 3)
            {
                victimGroup.SetActive(true);
                for (int i=0; i < playerList.Count; i++)
                {
                    if (i == currentPlayer)
                    {
                        chooseVictim[i].GetComponentInChildren<Text>().text = "You";
                        chooseVictim[i].GetComponent<Toggle>().interactable = false;
                    }
                    else
                    {
                        chooseVictim[i].GetComponent<Toggle>().interactable = true;
                        chooseVictim[i].GetComponentInChildren<Text>().text = playerList[i].name;
                    }
                }
            }
        }
        else
        {
            chosenCardGameObject.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 10;
            cardsShown[cardNumber].GetComponent<RectTransform>().anchoredPosition += Vector2.up * 10;
            chosenCard = cardsShown[cardNumber].GetComponent<CardDisplay>().card;
            chosenCardGameObject = cardsShown[cardNumber];
        }
        indexOfChosenCard = cardNumber;
    }

    public void SetVictim(int victimNumber)
    {
        chosenVictim = victimNumber;
    }

    public void SetPlayers (int playerNumber)
    {
        for (int i=0; i <= playerNumber; i++)
        {
            List<Card> temporaryList = new List<Card>();
            Player temporaryPlayer=new Player();
            for (int j = 0; j <= 5; j++)
            {
                //temporaryPlayer.playerHand.Add(DrawCard());
                temporaryList.Add(DrawCard());
            }
            string temporaryName = "Player" + (i+1);
            temporaryPlayer.playerHand = temporaryList;
            temporaryPlayer.name = temporaryName;
            temporaryPlayer.number = playerNumber;
            playerList.Add(temporaryPlayer);
            //Debug.Log(playerList[i].playerHand[i].notes);
            //chooseVictim[i].SetActive(true);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("someone just tried to quit Your masterpiece");
    }

    public void Resume()
    {
        panel.SetActive(false);
    }

    public void NextPlayer()
    {
        if (!cardIsChosen) return;
        else
        {
            switch (chosenCard.type)
            {
                case 1:
                    playerList[currentPlayer].notes += chosenCard.notes;
                    playerList[currentPlayer].charisma += chosenCard.charisma;

                    break;
                case 2:
                    playerList[currentPlayer + 1].notes += chosenCard.notes;
                    playerList[currentPlayer].charisma += chosenCard.charisma;
                    break;
                case 3:
                    playerList[currentPlayer].notes += chosenCard.notes;
                    playerList[currentPlayer + 1].charisma += chosenCard.charisma;
                    break;
            }
            playerList[currentPlayer].playerHand[indexOfChosenCard] = DrawCard();
            if (currentPlayer < playerList.Count - 1)
            {
             currentPlayer++;
            }
            else currentPlayer = 0;
            chosenCardGameObject.GetComponent<RectTransform>().anchoredPosition -= Vector2.up * 10;
            chosenCard = null;
            cardIsChosen = false;

            awaitingForNextPlayerPanel.SetActive(true);
            awaitingForNextPlayerText.GetComponent<TextMeshProUGUI>().text = "Next up is Player " + (currentPlayer+1);
            //StartGame();
        }
        chosenVictim = -1;
    }

    //This function is used to go to the next player when awaitingForAnotherPlayerPanel is active
    public void PlayerIsReady()
    {
        awaitingForNextPlayerPanel.SetActive(false);
        StartGame();
    }
}
