using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    public Menu _Menu;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Photon on ==> " + PhotonNetwork.CloudRegion + " server");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindMatch()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a Room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not find a room. Creating a room");
        MakeRoom();
    }

    private void MakeRoom()
    {
        int randRoomNumber = UnityEngine.Random.Range(1, 5000);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2,
        };
        PhotonNetwork.CreateRoom("RoomName_" + randRoomNumber, roomOptions);
        Debug.Log("Room created, waiting for Another player");
        _Menu.searchingPanel.SetActive(true);
    }

    public void StopSearching()
    {
        _Menu.searchingPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Stopped, Back to Menu");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + " /2 Starting Game");
            _Menu.PlayCourse();
        }
    }
}
