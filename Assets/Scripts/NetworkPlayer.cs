using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

[XmlRoot("player")]
public class NetworkPlayer : NetworkBehaviour {
	[SyncVar(hook="SetPlayerObjectName")]
	public string playerName;
	//public Deck deck;
	public SyncListString syncListStringDeck = new SyncListString ();

	public NetworkPlayer() { }

    [ClientRpc]
    public void RpcGoToMainMenu()
    {
        NetworkController.GetNetworkController().networkPlayer = this;
        SceneManager.LoadScene("Main Menu");
    }

    public void SetPlayerObjectName(string name)
    {
        this.gameObject.name = name;
        Debug.Log("Name set to " + name);
    }

    public void Start()
	{
		DontDestroyOnLoad (this);
	}

	public override void OnStartLocalPlayer()
	{
		DontDestroyOnLoad (this);
		Debug.Log ("Starting local player");
	}

	[Command]
	public void CmdAddCardToDeck(Card card)
	{
		Debug.Log ("CmdAddCardToDeck");
		syncListStringDeck.Add(card.name);
	}

	[Command]
	public void CmdRemoveCardFromDeck(Card card)
	{
		Debug.Log ("CmdRemoveCardToDeck");
		syncListStringDeck.Remove (card.name);
	}

	public bool CanAddCardToDeck(Card card)
	{
		return true;
	}

	public void SaveToXML()
	{
		PlayerXML pxml = new PlayerXML (this);
		pxml.Save (Path.Combine (Application.persistentDataPath, "users/" + playerName + "/player.xml"));
		Debug.Log("Saving player to " + Path.Combine (Application.persistentDataPath, "users/" + this.name + "/player.xml"));
	}

	public void LoadFromXML(string playerName)
	{
		PlayerXML pxml = PlayerXML.Load (Path.Combine (Application.persistentDataPath, "users/" + playerName + "/player.xml"));
		this.name = pxml.name;
		this.syncListStringDeck = pxml.XMLDeck.ToDeck ().ToSyncListString();
		Debug.Log("Loading player from " + Path.Combine (Application.persistentDataPath, "users/" + this.name + "/player.xml"));
    }
}
