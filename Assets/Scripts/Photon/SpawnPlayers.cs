using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject guiPrefab; //*

    public float minX, maxX;
    public float minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 2f, Random.Range(minZ, maxZ));
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        GameObject getPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity); //*
        GameObject getGUI = Instantiate(guiPrefab, Vector3.zero, Quaternion.identity);
        if (getPlayer.GetComponent<PhotonView>().IsMine)
        {
            getPlayer.GetComponent<PlayerMovement>()._ControlConnect = getGUI.transform;
            getGUI.transform.GetChild(1).GetComponent<Camera>().enabled = true;
            getGUI.transform.GetChild(1).GetComponent<AudioListener>().enabled = true;
            getGUI.transform.GetChild(2).GetComponent<CameraMovement>()._cmFL =
                getGUI.transform.GetChild(2).GetComponent<Cinemachine.CinemachineFreeLook>();
            getGUI.transform.GetChild(2).GetComponent<CameraMovement>().enabled = true;
            getPlayer.GetComponent<PlayerMovement>().enabled = true;
            

            //getPlayer.GetComponent<PlayerMovement>()._player = getPlayer;
        }
        //if (temp.GetComponent<PhotonView>().IsMine)
        //temp.GetComponent<PlayerController>().SetJoysticks(Instantiate(cameraPrefab, randomPosition, Quaternion.identity)); //*
    }


}
