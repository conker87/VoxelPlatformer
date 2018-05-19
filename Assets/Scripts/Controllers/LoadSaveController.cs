using System;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text;
using System.Xml;

public class LoadSaveController : MonoBehaviour {

    public static string SaveGameLocation = "";

    public static string LoadedSaveName = "";
    public static float LoadedTotalTime = 0f;
    public static Vector3 LoadedPosition = Vector3.zero;
    public static List<Collectable> LoadedCollectables = new List<Collectable>();
    public static List<Interactable> LoadedInteractables = new List<Interactable>();

    public static void LoadMainGame(string saveGameLocation) {

        if (saveGameLocation != "") {

            // This is a loaded game so load the details into the vars.
            LoadSavedGame(saveGameLocation);

        }

        SceneManager.LoadScene("_LoadMainGame");

    }

    public static void SaveMainGame(SaveDetails saveDetails) {

        Debug.Log("Saving...");

        XmlWriter xmlWriter;
        xmlWriter = XmlWriter.Create(saveDetails.SaveGameLocation);

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteWhitespace("\n");

        xmlWriter.WriteStartElement("game");

        xmlWriter.WriteWhitespace("\n\n");

        xmlWriter.WriteComment("Player data.");
        xmlWriter.WriteWhitespace("\n\t");

        xmlWriter.WriteStartElement("player");
        xmlWriter.WriteAttributeString("saveName", "saveDataName");
        xmlWriter.WriteAttributeString("saveTotalTime", saveDetails.TotalTime.ToString());
        xmlWriter.WriteAttributeString("saveCurrentTimestamp", saveDetails.CurrentTimestamp.ToString());

        xmlWriter.WriteAttributeString("playerPositionX", saveDetails.PlayerLocation.x.ToString());
        xmlWriter.WriteAttributeString("playerPositionY", saveDetails.PlayerLocation.y.ToString());
        xmlWriter.WriteAttributeString("playerPositionZ", saveDetails.PlayerLocation.z.ToString());

        xmlWriter.WriteWhitespace("\n\t");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteWhitespace("\n\n");

        xmlWriter.WriteComment("This is the list of Collectables.");
        xmlWriter.WriteWhitespace("\n\t");

        xmlWriter.WriteStartElement("collectables");

        foreach (Collectable Collectable in saveDetails.Collectables) {

            xmlWriter.WriteWhitespace("\n\t\t");

            xmlWriter.WriteStartElement("collectable");
            xmlWriter.WriteAttributeString("CollectableID", Collectable.CollectableID);
            xmlWriter.WriteAttributeString("CollectableType", Collectable.CollectableType.ToString());

            xmlWriter.WriteEndElement();

        }

        xmlWriter.WriteWhitespace("\n\t");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteWhitespace("\n\n");

        xmlWriter.WriteComment("Interactables.");
        xmlWriter.WriteWhitespace("\n\t");

        xmlWriter.WriteStartElement("interactables");

        // foreach (Interactable interactable in Extend_List.NewFindObjectsOfTypeAll<Interactable>()) {
        foreach (Interactable Interactable in saveDetails.Interactables) {

            xmlWriter.WriteWhitespace("\n\t\t");

            xmlWriter.WriteStartElement("interactable");
            xmlWriter.WriteAttributeString("InteractableID", Interactable.InteractableID);
            xmlWriter.WriteAttributeString("IsActivated", Interactable.IsActivated.ToString());
            xmlWriter.WriteAttributeString("IsDisabled", Interactable.IsDisabled.ToString());
            xmlWriter.WriteAttributeString("IsLocked", Interactable.IsLocked.ToString());

            xmlWriter.WriteEndElement();

        }

        xmlWriter.WriteWhitespace("\n\t");
        xmlWriter.WriteEndElement();
        xmlWriter.WriteWhitespace("\n");

        xmlWriter.WriteWhitespace("\n");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();

    }

    static void LoadSavedGame(string saveGameLocation) {

        LoadedSaveName = "";
        LoadedTotalTime = 0f;
        LoadedPosition = Vector3.zero;
        LoadedCollectables.Clear();
        LoadedInteractables.Clear();

        XmlReader xmlReader;
        xmlReader = XmlReader.Create(saveGameLocation);

        while (xmlReader.Read()) {

            if (xmlReader.NodeType == XmlNodeType.Element) {

                // <SaveData SaveFileName="SaveFileName" SaveFileDate="SaveFileDate" SaveFileTime="SaveFileTime" SaveFileTimeOnGame="SaveFileTimeOnGame" SaveFilePositionX/Y/Z="Vector3">
                if (xmlReader.Name == "player") {

                    LoadedSaveName = xmlReader.GetAttribute("saveName");
                    LoadedTotalTime = float.Parse(xmlReader.GetAttribute("saveTotalTime"));

                    LoadedPosition = new Vector3(
                        float.Parse(xmlReader.GetAttribute("playerPositionX")),
                        float.Parse(xmlReader.GetAttribute("playerPositionY")),
                        float.Parse(xmlReader.GetAttribute("playerPositionZ"))
                    );

                }

                if (xmlReader.Name == "collectable") {

                    string CollectableID = xmlReader.GetAttribute("CollectableID");
                    CollectableType CollectableType = (CollectableType)Enum.Parse(typeof(CollectableType), xmlReader.GetAttribute("CollectableType"));
                    bool CollectableCollected = true;

                    Collectable currentLoadingCollectable = new Collectable(
                        CollectableID,
                        CollectableType,
                        CollectableCollected
                    );

                    LoadedCollectables.Add(currentLoadingCollectable);

                }

                if (xmlReader.Name == "interactable") {

                    string InteractableID = xmlReader.GetAttribute("InteractableID");
                    bool IsDisabled = bool.Parse(xmlReader.GetAttribute("IsDisabled"));
                    bool IsLocked = bool.Parse(xmlReader.GetAttribute("IsLocked"));
                    bool IsActivated = bool.Parse(xmlReader.GetAttribute("IsActivated"));

                    Interactable currentLoadingInteractable = new Interactable(
                        InteractableID,
                        IsDisabled,
                        IsLocked,
                        IsActivated
                       );

                    LoadedInteractables.Add(currentLoadingInteractable);

                }
            }
        }

        xmlReader.Close();

    }

}

public class SaveDetails {

    public string SaveGameLocation;
    public Vector3 PlayerLocation;
    public float TotalTime;
    public DateTime CurrentTimestamp;

    public List<Collectable> Collectables = new List<Collectable>();
    public List<Interactable> Interactables = new List<Interactable>();

    public SaveDetails() { }

    public SaveDetails(string _SaveGameLocation, Vector3 _PlayerLocation, float _TotalTime, DateTime _CurrentTimestamp, List<Collectable> _Collectables, List<Interactable> _Interactables) {

        _SaveGameLocation = SaveGameLocation;
        _PlayerLocation = PlayerLocation;
        _TotalTime = TotalTime;
        _CurrentTimestamp = CurrentTimestamp;
        _Collectables = Collectables;
        _Interactables = Interactables;

    }
}