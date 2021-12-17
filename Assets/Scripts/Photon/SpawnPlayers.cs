using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject guiPrefab;

    public float minX, maxX;
    public float minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 2f, Random.Range(minZ, maxZ));

        GameObject getPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        GameObject getGUI = Instantiate(guiPrefab, Vector3.zero, Quaternion.identity);
        if (getPlayer.GetComponent<PhotonView>().IsMine)
        {
            // Set up
            getPlayer.GetComponent<PlayerMovement>()._ControlConnect = getGUI.transform;
            getGUI.transform.GetChild(2).GetComponent<CameraMovement>()._cmFL =
                getGUI.transform.GetChild(2).GetComponent<Cinemachine.CinemachineFreeLook>();

            // Enabling components
            getGUI.transform.GetChild(1).GetComponent<Camera>().enabled = true;
            getGUI.transform.GetChild(1).GetComponent<AudioListener>().enabled = true;
            getGUI.transform.GetChild(2).GetComponent<CameraMovement>().enabled = true;
            getPlayer.GetComponent<PlayerMovement>().enabled = true;
        }
    }
}
