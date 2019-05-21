using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour
{
	public int posX;
	public int posY;
	public int textureid;

	public void UpdateNodeObject(Node curNode, NodeObjectSaveable saveable)
	{
		posX = saveable.posX;
		posY = saveable.posY;
		textureid = saveable.textureId;

		ChangeMaterial(curNode);
	}

	void ChangeMaterial(Node curNode)
	{
		Material getMaterial = LevelEditor.ResourcesManager.GetInstance().GetMaterial(textureid);
		curNode.tileRenderer.material = getMaterial;
	}
	public NodeObjectSaveable GetSaveable()
	{
		NodeObjectSaveable saveable = new NodeObjectSaveable();
		saveable.posX = this.posX;
		saveable.posY = this.posY;
		saveable.textureId = this.textureid;

		return saveable;
	}
}

[System.Serializable]
public class NodeObjectSaveable
{
	public int posX;
	public int posY;
	public int textureId;
}
