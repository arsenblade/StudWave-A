using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlaceInteraction;
using UnityEngine;
using UnityEngine.UI;

public class PlaceInteractionOccurrence : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _intermediatePanel;
    [SerializeField] private PlaceInteractionsRepository _interactions;
    [SerializeField] private ButtonsKeeper _buttonKeeper;

    public void SetPanelButtons(string key)
    {
        var buttons = _buttonKeeper.GetPlaceInteractionButtons();
        var currentPlaceInteractions = _interactions.GetInteractionsByKey(key);

        _panel.SetActive(true);

        var currentButtonIndex = 0;
        
        for (var i = 0; i < currentPlaceInteractions.Count; i++)
        {
            if (currentButtonIndex == buttons.Length)
                break;

            if (currentPlaceInteractions[i].AppearanceCriteria())
            {
                var currentButton = buttons[currentButtonIndex];
                var interaction = currentPlaceInteractions[i];
                currentButton.onClick.AddListener(() =>
                {
                    interaction.Effect();
                    _intermediatePanel.GetComponentInChildren<Text>().text =
                        interaction.TextAfterChoice;
                    _panel.gameObject.SetActive(false);
                    _intermediatePanel.gameObject.SetActive(true);
                });
                buttons[currentButtonIndex].GetComponentInChildren<Text>().text = currentPlaceInteractions[i].Text;
                buttons[currentButtonIndex].gameObject.SetActive(true);
                currentButtonIndex++;
            }
        }
        
    }
}