﻿using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using System.Text;
using System.Xml;

public class SaveController : MonoBehaviour {

 //   [SerializeField]
 //   static List<InteractableSave> Interactables = new List<InteractableSave>();
 //   static XmlWriter xmlWriter;

 //   public static void EndGame() {

 //       GameController.current.QuitGame();

 //   }

 //   public static void QuitGame() {

 //       Application.Quit();

 //   }

 //   public static void NewGame() {

 //       GameController.current.StartGame();

 //   }

	//public static void LoadGame() {

	//	XmlReader xmlReader = XmlReader.Create("saveGameTest.xml");

 //       while (xmlReader.Read()) {

	//		if (xmlReader.NodeType == XmlNodeType.Element) {

	//			// <SaveData SaveFileName="SaveFileName" SaveFileDate="SaveFileDate" SaveFileTime="SaveFileTime" SaveFileTimeOnGame="SaveFileTimeOnGame" SaveFilePositionX/Y/Z="Vector3">
	//			if (xmlReader.Name == "player") {

 //                   GameController.current.playerLoadedPosition = new Vector3(
 //                       float.Parse(xmlReader.GetAttribute("playerPositionX")),
 //                       float.Parse(xmlReader.GetAttribute("playerPositionY")),
 //                       float.Parse(xmlReader.GetAttribute("playerPositionZ"))
 //                   );

 //               }

	//			if (xmlReader.Name == "collectable") {

 //                   CollectableType collectableType = (CollectableType) Enum.Parse(typeof(CollectableType), xmlReader.GetAttribute("CollectableType"));

 //                   CollectableListValue newCollectableListValue = new CollectableListValue(
 //                       xmlReader.GetAttribute("CollectableID"),
 //                       collectableType
 //                   );

 //                   GameController.current.Collectables.Add (newCollectableListValue);
                    
	//			}

 //               if (xmlReader.Name == "interactable") {

 //                   InteractableSave temp = new InteractableSave(
 //                       xmlReader.GetAttribute("InteractableID"),
 //                       bool.Parse(xmlReader.GetAttribute("IsDisabled")),
 //                       bool.Parse(xmlReader.GetAttribute("IsLocked")),
 //                       bool.Parse(xmlReader.GetAttribute("IsActivated"))
 //                      );

 //                   Interactables.Add(temp);

 //               }
	//		}
	//	}

 //       xmlReader.Close();

 //       foreach (Collectable collectable in GameController.current.levelsParent.GetComponentsInChildren<Collectable>(true)) {

 //           if (GameController.current.HasAcquiredCollectable(collectable.CollectableID)) {

 //               collectable.CollectableCollected = true;

 //           }
 //       }

 //       foreach (Interactable interactable in GameController.current.levelsParent.GetComponentsInChildren<Interactable>(true)) {

 //           foreach (InteractableSave save in Interactables) {

 //               if (save.InteractableID == interactable.InteractableID) {

 //                   Debug.LogFormat("Found an interactable in the world: {0} and also in the Saved Interactables.", interactable);

 //                   interactable.IsActivated = save.IsActivated;
 //                   interactable.IsDisabled = save.IsDisabled;
 //                   interactable.IsLocked = save.IsLocked;
 //                   break;

 //               }
 //           }
 //       }

 //       Interactables.Clear();

 //       GameController.current.StartGame(SaveState.LoadedGame);

 //   }

	//public static void SaveGame() {

 //       if (GameController.current.Player == null) {

 //           Debug.LogErrorFormat("The Player is null which means that you're probably not in GameState.Ingame, so ignoring Loading.");
 //           return;

 //       }

 //       if (GameController.current.CanCurrentSaveGame == false) {

 //           return;

 //       }

 //       Debug.Log ("Saving...");

	//	xmlWriter = XmlWriter.Create("saveGameTest.xml");
	//	xmlWriter.WriteStartDocument();
	//	xmlWriter.WriteWhitespace("\n");

 //       xmlWriter.WriteStartElement("game");
 //       xmlWriter.WriteWhitespace("\n");

 //       SavePlayerData();
 //       SaveCollectables();
 //       SaveInteractables();

 //       xmlWriter.WriteWhitespace("\n");
 //       xmlWriter.WriteEndElement();

 //       xmlWriter.WriteEndDocument();
	//	xmlWriter.Close();

	//}

 //   static void SavePlayerData() {

 //       xmlWriter.WriteWhitespace("\n");
 //       xmlWriter.WriteComment("Player data.");
 //       xmlWriter.WriteWhitespace("\n\t");

 //       xmlWriter.WriteStartElement("player");
 //       xmlWriter.WriteAttributeString("saveName", "saveDataName");
 //       xmlWriter.WriteAttributeString("saveTotalTime", GameController.current.currentTime.ToString());
 //       xmlWriter.WriteAttributeString("saveCurrentTimestamp", DateTime.Now.ToString());

 //       xmlWriter.WriteAttributeString("playerPositionX", GameController.current.Player.transform.position.x.ToString());
 //       xmlWriter.WriteAttributeString("playerPositionY", GameController.current.Player.transform.position.y.ToString());
 //       xmlWriter.WriteAttributeString("playerPositionZ", GameController.current.Player.transform.position.z.ToString());


 //       xmlWriter.WriteWhitespace("\n\t");
 //       xmlWriter.WriteEndElement();
 //       xmlWriter.WriteWhitespace("\n");

 //   }

 //   static void SaveCollectables() {

 //       xmlWriter.WriteWhitespace("\n");
 //       xmlWriter.WriteComment("This is the list of Collectables.");
 //       xmlWriter.WriteWhitespace("\n\t");

 //       xmlWriter.WriteStartElement("collectables");

 //       foreach (CollectableListValue collectable in GameController.current.Collectables) {

 //           xmlWriter.WriteWhitespace("\n\t\t");

 //           xmlWriter.WriteStartElement("collectable");
 //           xmlWriter.WriteAttributeString("CollectableID", collectable.CollectableID);
 //           xmlWriter.WriteAttributeString("CollectableType", collectable.CollectableType.ToString());

 //           xmlWriter.WriteEndElement();

 //       }

 //       xmlWriter.WriteWhitespace("\n\t");
 //       xmlWriter.WriteEndElement();
 //       xmlWriter.WriteWhitespace("\n");

 //   }

 //   static void SaveInteractables() {

 //       xmlWriter.WriteWhitespace("\n");
 //       xmlWriter.WriteComment("Interactables.");
 //       xmlWriter.WriteWhitespace("\n\t");

 //       xmlWriter.WriteStartElement("interactables");

 //       foreach (Interactable interactable in ExtendList.NewFindObjectsOfTypeAll<Interactable>()) {

 //           xmlWriter.WriteWhitespace("\n\t\t");

 //           xmlWriter.WriteStartElement("interactable");
 //           xmlWriter.WriteAttributeString("InteractableID", interactable.InteractableID);
 //           xmlWriter.WriteAttributeString("IsActivated",interactable.IsActivated.ToString());
 //           xmlWriter.WriteAttributeString("IsDisabled",interactable.IsDisabled.ToString());
 //           xmlWriter.WriteAttributeString("IsLocked",interactable.IsLocked.ToString());

 //           xmlWriter.WriteEndElement();

 //       }

 //       xmlWriter.WriteWhitespace("\n\t");
 //       xmlWriter.WriteEndElement();
 //       xmlWriter.WriteWhitespace("\n");

 //   }
}