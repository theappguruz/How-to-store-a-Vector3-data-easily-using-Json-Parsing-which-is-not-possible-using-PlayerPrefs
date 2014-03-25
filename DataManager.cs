using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DataManager : MonoBehaviour
{ 
  
    //sets the different kind of data into the player prefs...


    //sets the coins data(position and rotation)...
    public void setCoinsData(GameObject coins)
    {
        int coinCounter = 0;
        for (int i = 0; i < coins.transform.childCount; i++)
        {
            coinCounter++;
            string key = coins.name + "" + coinCounter;
            //Coins Tag,Position,Rotation are passed in the Constructor.
            CoinsData coinData = new CoinsData(key, coins.transform.GetChild(i).position, coins.transform.GetChild(i).rotation);
            //String data stores data 
            string data = JsonFx.Json.JsonWriter.Serialize(coinData);
            // Finally storing my Data i.e parsed from jason parser in Player Prefs
            PlayerPrefs.SetString(coins.tag + "" + coinCounter, data);

        }
    }

    //gets the activated gold coins data(position and rotation)...
    public GameObject getCoinsData()
    {
        GameObject coins = GameObject.FindGameObjectWithTag("Coin");
        for (int i = 0; i < coins.transform.childCount; i++)
        {
            if (coinCounter > 0)
            {
                string data = PlayerPrefs.GetString(coins.tag + "" + coinCounter);
                // Storing Vector3 Data in Jason.
                CoinsData coinData = JsonFx.Json.JsonReader.Deserialize<CoinsData>(data);
                string key = coins.name + "" + coinCounter;
                coinData.coinPositionData = coinData.data [key];
                // Get Coin data.
                coins.transform.GetChild(i).position = new Vector3(coinData.coinPositionData.position.x, coinData.coinPositionData.position.y, coinData.coinPositionData.position.z);
                coins.transform.GetChild(i).rotation = new Quaternion(coinData.coinPositionData.rotation.x, coinData.coinPositionData.rotation.y, coinData.coinPositionData.rotation.z, coinData.coinPositionData.rotation.w);
                coins.transform.GetChild(i).gameObject.SetActive(true);
                coinCounter--;
            } else
            {
                coins.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        return coins;
    }
    class CoinsData
    {
        public PositionData coinPositionData = new PositionData();
        // Storing Coin Data in Dictonary.
        public Dictionary<string, PositionData> data = new Dictionary<string, PositionData>();
        public CoinsData(string key, Vector3 coinPosition, Quaternion coinRotation)
        {
            // Get popstion of coin at run time
            coinPositionData.position = new Cords(coinPosition.x, coinPosition.y, coinPosition.z);
            coinPositionData.rotation = new Rotation(coinRotation.x, coinRotation.y, coinRotation.z, coinRotation.w);
            // Add data to dictionary
            if (key != null)
                data.Add(key, coinPositionData);
        }
        // Default Constructer is needed when u r using Jason Parsing.
        public CoinsData()
        {
        }
    }
    //Get Coins Postion
    class PositionData
    {
        public Cords position;
        public Rotation rotation;
    }
    // Get Coins Cords
    class Cords
    {
        public float x, y, z;
        public Cords(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Cords()
        {
        }
    }
    // Get Coin Rotation
    class Rotation
    {
        public float x, y, z, w;
        public Rotation(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Rotation()
        {
        }
    }
}