﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour {

    [Header("Stats")]
    public bool Shrink = false;
    public bool Immune = false;
    public bool Teleport = false;
    public bool SpawnCube = false;
  
    [Header("PowerUps Assets")]
    public GameObject[] PowerUps;

    [SerializeField] private GameObject CubeToSpawn;
    public GameObject AbilityUI;

    [Space]
    [Header("Key Bindings")]
    public KeyCode ShrinkKey;
    public KeyCode TeleportKey;
    public KeyCode AddcubeKey;

    private GameObject otherPlayer;


    private void Start ()
    {
        // if the player running the script is not the other players "clone"
        if ((PhotonNetwork.isMasterClient && gameObject.tag == "Player1") || (!PhotonNetwork.isMasterClient && gameObject.tag == "Player2"))
        {
            AbilityUI = Instantiate(AbilityUI);
            AbilityUI.transform.parent = GameObject.Find("Canvas").transform;
            RectTransform rectTransform = AbilityUI.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0f, 50f, 0f);
            rectTransform.localScale = new Vector3(2.75f, 2.75f, 2.75f);
        }

        for (int i = 0; i <= 3; i++)
        {
            PowerUps[i] = AbilityUI.transform.GetChild(i).gameObject;
            PowerUps[i].SetActive(false);
        }
    }
    

    IEnumerator ResizeTimer() // TODO: fix this pls
    {
        yield return new WaitForSeconds(2.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);
        yield return new WaitForSeconds(0.20f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5F);

        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 2, gameObject.transform.localScale.y * 2, gameObject.transform.localScale.z);

        gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1F);
    }

    private void Update ()
    {
        if (Shrink)
        {
            PowerUps[0].SetActive(true);
        }
        if (Teleport)
        {
            PowerUps[1].SetActive(true);
        }
        if (SpawnCube)
        {
            PowerUps[2].SetActive(true);
        }
        if (Immune)
        {
            PowerUps[3].SetActive(true);
        }
        if (Input.GetKey(ShrinkKey))
        {
            if (Shrink)
            {
                Shrink = false;
                ResizePowerUp();

            }
        }
        else if (Input.GetKey(TeleportKey))//grants a 1 time dagame immunity
        {
            if (Teleport)
            {
                Teleport = false;
                TeleportPowerUp();
            }
        }
        else if(Input.GetKey(AddcubeKey))
        {
            if (SpawnCube)
            {
                SpawnCube = false;
                SpawnCubePowerUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Resize")//resizes player for 5 seconds
        {
            Shrink = true;
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "DamageImmune")//grants a 1 time dagame immunity
        {
            Immune = true;
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Teleport")//grants a 1 time dagame immunity
        {
            Teleport = true;
            col.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "SpawnCube")
        {
            SpawnCube = true;
            col.gameObject.SetActive(false);
        }
    }


    private void ResizePowerUp()
    {
        PowerUps[0].SetActive(false);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z);
        StartCoroutine(ResizeTimer());
    }

    private void TeleportPowerUp()
    {
        PowerUps[1].SetActive(false);
        if (otherPlayer == null)
        {
            if (this.tag == "Player1")
            {
                otherPlayer = GameObject.Find("Player2(Clone)");
            }
            else
            {
                otherPlayer = GameObject.Find("Player1(Clone)");
            }
        }
        gameObject.transform.position = otherPlayer.transform.position;
    }

    private void SpawnCubePowerUp()
    {
        PowerUps[2].SetActive(false);
        PhotonNetwork.InstantiateSceneObject(CubeToSpawn.name, gameObject.transform.position, Quaternion.identity, 0, null); // spawning a cube to the scene
    }
}

