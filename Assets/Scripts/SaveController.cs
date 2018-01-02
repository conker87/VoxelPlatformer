using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using System.Text;
using System.Xml;

public class SaveController : MonoBehaviour {

	// TODO: Make it so that it actually saves to a file.
		// Maybe obfuscate the save file to prevent editing? Who cares? Question mark?

	static List<string> _starsInWorld = new List<string> ();
	public static List<string> StarsInWorld {

		get { return _starsInWorld; }
		set { _starsInWorld = value; }

	}

	static List<string> _coinsInWorld = new List<string>();
	public static List<string> CoinsInWorld {

		get { return _coinsInWorld; } 
		set { _coinsInWorld = value; }

	}

	static List<string> _abilitiesOfPlayer = new List<string>();
	public static List<string> AbilitiesOfPlayer {

		get { return _abilitiesOfPlayer; } 
		set { _abilitiesOfPlayer = value; }

	}

	static XmlWriter xmlWriter;

	void Start() {
		
	}

	public static void LoadGame() {

		XmlReader xmlReader = XmlReader.Create("saveGameTest.xml");

		GameController.current.Abilities.Clear ();
		GameController.current.Coins.Clear ();
		GameController.current.Stars.Clear ();

		while(xmlReader.Read()) {

			if (xmlReader.NodeType == XmlNodeType.Element) {

				// <SaveData SaveFileName="SaveFileName" SaveFileDate="SaveFileDate" SaveFileTime="SaveFileTime" SaveFileTimeOnGame="SaveFileTimeOnGame" stationID="SAVE_STATION_A">
				if (xmlReader.Name == "SaveData") {

					// Player.instance.transform.position = SaveStationLocations.FirstOrDefault (a => a.InteractableID == xmlReader.GetAttribute ("stationID")).transform.position;

				}

				// 	<abilities JUMP="True" JUMP_DOUBLE="False" JUMP_TRIPLE="False" WALL_SLIDE="False" WALL_JUMP="False" WALL_HIGH_JUMP="False"
				//		DASH="False" DASH_MEGA="False" CHEAT_JUMP="False" CHEAT_DASH="False" WeaponProjectileModifier="1" />
				if (xmlReader.Name == "ability") {

					while (xmlReader.MoveToNextAttribute ()) {

						GameController.current.Abilities.Add (xmlReader.Value);

					}

				}

				if (xmlReader.Name == "coin") {

					while (xmlReader.MoveToNextAttribute ()) {

						GameController.current.Coins.Add (xmlReader.GetAttribute("CollectableID"));

					}

				}

				if (xmlReader.Name == "star") {

					while (xmlReader.MoveToNextAttribute ()) {

						GameController.current.Stars.Add (xmlReader.Value);

					}

				}

			}


			//			if((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "saveLocation")) { 
//
//				Debug.Log ("SaveLocation");
//
//				if(xmlReader.HasAttributes)
//					Debug.Log(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));  
//				
//			}
		}

		// TODO: We should make sure that when we load the level, it also does this, because having all levels loaded is fucking dumb af.
		foreach (Coin coin in GameObject.FindObjectsOfType<Coin>()) {

			if (GameController.current.Coins.Contains(coin.CollectableID)) {

				coin.CollectableCollected = true;

			}

		}

		foreach (Star star in GameObject.FindObjectsOfType<Star>()) {

			if (GameController.current.Stars.Contains(star.CollectableID)) {

				star.CollectableCollected = true;

			}

		}

	}

	public static void SaveGame() {

		Debug.Log ("Saving...");

		xmlWriter = XmlWriter.Create("saveGameTest.xml");
		xmlWriter.WriteStartDocument();
		xmlWriter.WriteWhitespace("\n");

		xmlWriter.WriteStartElement("SaveData");
		xmlWriter.WriteAttributeString ("SaveFileName", "SaveFileName");
		xmlWriter.WriteAttributeString ("SaveFileDate", "SaveFileDate");
		xmlWriter.WriteAttributeString ("SaveFileTime", "SaveFileTime");
		xmlWriter.WriteAttributeString ("SaveFileTimeOnGame", "SaveFileTimeOnGame");

		// SaveLocation (saveStationID);
		SaveCoinsCollected ();
		SaveStarsCollected ();
		SaveAbilitiesCollected ();

		xmlWriter.WriteEndElement();
		xmlWriter.WriteEndDocument();
		xmlWriter.Close();

	}

	static void SaveStarsCollected() {

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Stars collected.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("stars");

		foreach (string star in GameController.current.Stars) {

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("star");
			xmlWriter.WriteAttributeString ("CollectableID", star);

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveAbilitiesCollected() {

		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Abilities collected.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("abilities");

		foreach (string ability in GameController.current.Abilities) {

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("ability");
			xmlWriter.WriteAttributeString ("AbilityID", ability);

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

	static void SaveCoinsCollected() {
		
		xmlWriter.WriteWhitespace("\n");
		xmlWriter.WriteComment ("This is the list of Coins collected.");
		xmlWriter.WriteWhitespace("\n\t");

		xmlWriter.WriteStartElement("coins");

		foreach (string coin in GameController.current.Coins) {

			xmlWriter.WriteWhitespace("\n\t\t");

			xmlWriter.WriteStartElement("coin");
			xmlWriter.WriteAttributeString ("CollectableID", coin);

			xmlWriter.WriteEndElement();

		}

		xmlWriter.WriteWhitespace("\n\t");
		xmlWriter.WriteEndElement();
		xmlWriter.WriteWhitespace("\n");

	}

}
