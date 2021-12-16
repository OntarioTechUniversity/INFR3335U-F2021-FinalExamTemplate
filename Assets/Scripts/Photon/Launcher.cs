using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime; //*
using UnityEngine.SceneManagement; //*


public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject lobbyPanel; //*
    public GameObject roomPanel; //*

    public TMPro.TMP_InputField inputID;

   
    public TMPro.TMP_Text error;

    public TMPro.TMP_Text roomName; //*
    

    public GameObject playerListing; //*
    public Transform playerListContent; //*

    public Button startButton; //*
    public TMPro.TMP_Text _switchText;
    //public Button settingsButton;

    //public GameObject readyButton;
    //public Transform buttonOrganizer;

    public GameObject switchButton;

    public void Start()
    {
        lobbyPanel.SetActive(true); //*
        roomPanel.SetActive(false); //*
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(inputID.text))
            return;
        PhotonNetwork.CreateRoom(inputID.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputID.text);
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false); //*
        roomPanel.SetActive(true); //*

        switchButton.SetActive(true);
        _switchText.text = "Back to Lobby";

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList; //*

        

        for (int i = 0; i < players.Length; i++) //*
        {
            Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(players[i]);

            if (i == 0)
            {
                startButton.interactable = true;
                //settingsButton.interactable = true;
            }
            else
            {
                startButton.interactable = false;
                //settingsButton.interactable = false;
            }
        }

        //Instantiate(readyButton, buttonOrganizer).GetComponent<ReadyButton>().SetCurrentPlayer(playerListContent.gameObject);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error.text = message;
        Debug.Log("Error creating room! " + message);
    }

    public void OnClickLeaveRoom() //*
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //*
    {
        SceneManager.LoadScene(1);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //*
    {
        Instantiate(playerListing, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(newPlayer);
    }

    public void OnClickStartGame() //*
    {
        PhotonNetwork.LoadLevel(3);

    }

    public void switchOnClick()
    {
        roomPanel.SetActive(!roomPanel.activeSelf);
        lobbyPanel.SetActive(!lobbyPanel.activeSelf);
        if (roomPanel.activeSelf)
            _switchText.text = "Back to Lobby";
        else
            _switchText.text = "Back to Room";
    }

}
