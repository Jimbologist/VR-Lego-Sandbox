﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawn : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab;
    public GameObject player;
    public ColorButton ColorSwatch;

    public void SpawnObject()
    {
        // Old spawn code, changed below to always spawn where player is facing.
        //Instantiate(myPrefab, new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z + 1), transform.rotation);
        // Spawn in front of player by using the forward direction of the camera's transform and offset a bit.
        GameObject newObj = ObjectPooler.Instance.SpawnFromPool(myPrefab.name, player.transform.position + (player.transform.forward * 3), Quaternion.identity);
        //ColorSwatch.GetComponent<MeshRenderer>().material.GetColor
        LegoGroup newGroup = newObj.GetComponent<LegoGroup>();
        newGroup.ChangeGroupColor(ColorSwatch.CurrentColor);
        newGroup.AudioFX.PlaySpawnSound();
    }

    public void CloneGroup(LegoGroup clonedGroup)
    {
        GameObject newObj = ObjectPooler.Instance.SpawnFromPool(clonedGroup.gameObject.name, player.transform.position + (player.transform.forward * 3), Quaternion.identity);

        LegoGroup newGroup = newObj.GetComponent<LegoGroup>();
        newGroup.testSize = clonedGroup.GroupSize;
        newGroup.ChangeGroupColor(clonedGroup.BaseLegoData.LegoRenderer.material.color);
        newGroup.RefreshLegoGroup(clonedGroup.GroupSize.x, clonedGroup.GroupSize.y);
        newGroup.SetKinematic(false);
        newGroup.ToggleLegoHighlights(false);
        newGroup.AudioFX.PlaySpawnSound();
    }

}
