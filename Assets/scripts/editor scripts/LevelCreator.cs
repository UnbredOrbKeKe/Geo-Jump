using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
	public class LevelCreator : MonoBehaviour
	{
		LevelManager manager;
		Grid gridBase;
		InterfaceManager ui;

		//place obj variables
		bool hasObj;
		GameObject objToPlace;
		GameObject cloneObj;
		Level_Object objProperties;
		Vector3 mousePosition;
        Vector3 worldPosition;
        bool deleteObj;

        //paint tile variables
        bool hasMaterial;
        bool paintTile;
        public Material matToPlace;
        Node perviousNode;
        Material prevMaterial;
        Quaternion targetRot;
        Quaternion prevRotation;

        //place stack objs variables
        bool placeStackObj;
        GameObject stackObjToPlace;
        GameObject stackCloneObj;
        Level_Object stackObjProperties;
        bool DeleteStackObj;

        //Wall creator variables
        bool createWall;
        public GameObject wallPrefab;
        Node startNode_Wall;
        Node endNodeWall;
        public Material[] wallPlacementMat;
        bool deleteWall;

        void Start()
        {
            gridBase = Grid.GetInstance();
            manager = LevelManager.GetInstance();
            ui = InterfaceManager.GetInstance();

            //PaintAll();
        }

        void Update()
        {
            PlaceObject();
            //paintTile();
            DeleteObjs();
            //PlaceStackedObj();
            //createWall();
            DeleteStackedObjs();
            //DeleteWallsActual();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;
            }
        }

        #region Place Objects
        void PlaceObject()
        {
            if (hasObj)
            {
                UpdateMousePosition();
                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                Debug.Log("test" + mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (cloneObj == null)
                {
                    cloneObj = Instantiate(objToPlace, worldPosition, Quaternion.identity) as GameObject;
                    objProperties = cloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    cloneObj.transform.position = worldPosition;

                    if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                    {
                        if(curNode.placedObj != null)
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                            curNode.placedObj = null;
                        }

                        GameObject actualObjPlaced = Instantiate(objToPlace, worldPosition, cloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosY = curNode.nodePosY;
                        curNode.placedObj = placedObjProperties;
                        manager.inSceneGameObjects.Add(actualObjPlaced);
                    }

                    if (Input.GetMouseButtonUp(1))
                    {
                        objProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if(cloneObj != null)
                {
                    Destroy(cloneObj);
                }
            }
        }

        public void PassGameObjectToPlace(string objId)
        {
            if(cloneObj != null)
            {
                Destroy(cloneObj);
            }

            CloseAll();
            hasObj = true;
            cloneObj = null;
            objToPlace = ResourcesManager.GetInstance().GetObjBase(objId).objPrefab;
        }

        void DeleteObjs()
        {
            if (deleteObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (manager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
                            manager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);
                            Destroy(curNode.placedObj.gameObject);
                        }

                        curNode.placedObj = null;
                    }
                }
            }
        }

        public void DeleteObj()
        {
            CloseAll();
            deleteObj = true;
        }
        #endregion Place Objects

        #region stacked Objects;
        public void PassStackedObjectToPlace(string objId)
        {
            if (stackCloneObj != null)
            {
                Destroy(stackCloneObj);
            }

            CloseAll();
            placeStackObj = true;
            stackCloneObj = null;

            stackObjToPlace = ResourcesManager.GetInstance().GetStackObjBase(objId).objPrefab;
        }

        void PlaceStackObj()
        {
            if (placeStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);

                worldPosition = curNode.vis.transform.position;

                if (stackCloneObj == null)
                {
                    stackCloneObj = Instantiate(stackObjToPlace, worldPosition, Quaternion.identity) as GameObject;
                    stackObjProperties = stackCloneObj.GetComponent<Level_Object>();
                }
                else
                {
                    stackCloneObj.transform.position = worldPosition;

                    if (Input.GetMouseButtonUp(0) && !ui.mouseOverUIElement)
                    {
                        GameObject actualObjPlaced = Instantiate(stackObjToPlace, worldPosition, stackCloneObj.transform.rotation) as GameObject;
                        Level_Object placedObjProperties = actualObjPlaced.GetComponent<Level_Object>();

                        placedObjProperties.gridPosX = curNode.nodePosX;
                        placedObjProperties.gridPosY = curNode.nodePosY;
                        curNode.stackedObjs.Add(placedObjProperties);
                        manager.inSceneStackObjects.Add(actualObjPlaced);

                    }

                    if (Input.GetMouseButtonUp(1))
                    {
                        stackObjProperties.ChangeRotation();
                    }
                }
            }
            else
            {
                if (stackCloneObj != null)
                {
                    Destroy(stackCloneObj);
                }
            }
        }

        public void DeletStackObj()
        {
            CloseAll();
            DeleteStackObj = true;
        }

        void DeleteStackedObjs()
        {
            if (DeleteStackObj)
            {
                UpdateMousePosition();

                Node curNode = gridBase.NodeFromWorldPosition(mousePosition);
                
                if (Input.GetMouseButton(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.stackedObjs.Count > 0)
                    {
                        for (int i = 0; i < curNode.stackedObjs.Count; i++)
                        {
                            if (manager.inSceneStackObjects.Contains(curNode.stackedObjs[i].gameObject))
                            {
                                manager.inSceneStackObjects.Remove(curNode.stackedObjs[i].gameObject);
                                Destroy(curNode.stackedObjs[i].gameObject);
                            }
                        }
                        curNode.stackedObjs.Clear();
                    }
                }
            }
        }
        #endregion stacked Objects;

      

        void CloseAll()
        {
            hasObj = false;
            deleteObj = false;
            placeStackObj = false;
            createWall = false;
            hasMaterial = false;
            DeleteStackObj = false;
            deleteWall = false;
        }
    }
}
