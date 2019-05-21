using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor 
{
	public class Level_Object : MonoBehaviour
	{
        public string obj_Id;
        public int gridPosX;
        public int gridPosY;
        public GameObject modeVisualization;
        public Vector3 worldPositionOffset;
        public Vector3 worldRotation;

        public bool isStackableObj = false;
        public bool isWallObject = false;

        public float rotateDegrees = 90;

         
        /// <summary>
        /// update node when game object is placed on it.
        /// </summary>
        /// <param name="grid"></param>
        public void UpdateNode(Node[,] grid)
        {
            Node node = grid[gridPosX, gridPosY];

            Vector3 worldPosition = node.vis.transform.position;
            worldPosition += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosition;
        }


        /// <summary>
        /// change rotation of the gameObject
        /// </summary>
        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotateDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }


        // this saves all the things we want to save...
		public SaveableLevelObject GetSaveableObject()
		{
			SaveableLevelObject savedObj = new SaveableLevelObject();
			savedObj.obj_Id = obj_Id;
			savedObj.posX = gridPosX;
			savedObj.posY = gridPosY;

			worldRotation = transform.localEulerAngles;

			savedObj.rotX = worldRotation.x;
            savedObj.rotZ = worldRotation.z;
            savedObj.rotY = worldRotation.y;
			savedObj.isWallObject = isWallObject;
			savedObj.isStackable = isStackableObj;

			return savedObj;
		}


		[System.Serializable]
		public class SaveableLevelObject
		{
			public string obj_Id;
			public int posX;
			public int posY;

			public float rotX;
			public float rotY;
            public float rotZ;

			public bool isWallObject = false;
			public bool isStackable = false;
		}
	}
}
