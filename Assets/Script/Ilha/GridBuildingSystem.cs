using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem instance;

    public GridLayout GridLayout;
    public Tilemap MainTileMap;
    public Tilemap TempTileMap;

    private static Dictionary<TileTypes, TileBase> Tilebases = new Dictionary<TileTypes, TileBase>();

    public Building Temp;
    public Vector3 prevPos;
    private BoundsInt previArea;

    #region Metodos

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        string tilePath = @"Tiles\";

        Tilebases.Add(TileTypes.VAZIO, null);
        Tilebases.Add(TileTypes.BRANCO, Resources.Load<TileBase>(tilePath + "Branco"));
        Tilebases.Add(TileTypes.VERDE, Resources.Load<TileBase>(tilePath + "Verde"));
        Tilebases.Add(TileTypes.VERMELHO, Resources.Load<TileBase>(tilePath + "Vermelho"));
    }

    // Update is called once per frame
    void Update()
    {
        if(!Temp) {
            return;
        }
        
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if(!Temp.Placed) {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = GridLayout.LocalToCell(touchPos);

            if(prevPos != cellPos) {

                Temp.transform.localPosition = GridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f,0f));
                prevPos = cellPos;
                FollowBuilding();
            }
        }
        if(Input.GetMouseButtonDown(0)) {
            if(Temp.CanBePlaced()) {
                Temp.Place();
            }
        } else if (Input.GetKeyDown(KeyCode.Space)) {
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            print("ESC");
            ClearArea();
            Destroy(Temp.gameObject);
        }
    }

    #endregion

    #region Tilemaps

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
	{
		int counter = 0;

		TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];

		foreach (var v in area.allPositionsWithin)
		{
			Vector3Int pos = new Vector3Int(v.x, v.y, 0);
			array[counter] = tilemap.GetTile(pos);
			counter++;
		}

		return array;
	}

    private static void SetTilesBlock(BoundsInt area, TileTypes type, Tilemap tilemap)
	{
		int size = area.size.x * area.size.y * area.size.z;
		TileBase[] tileArray = new TileBase[size];
		FillTiles(tileArray, type);
		tilemap.SetTilesBlock(area, tileArray);
	}

    private static void FillTiles(TileBase[] arr, TileTypes type)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = Tilebases[type];
		}
	}

    #endregion

    #region construcao

    public void initializeWithBuilding(GameObject building, Sprite spriteImg) {
        if(Temp!=null && !Temp.GetComponent<Building>().Placed) {
            Destroy(Temp.gameObject);
        }
        Temp = Instantiate(building, new Vector3(0f,-0.25f,0f), Quaternion.identity).GetComponent<Building>();
        if(spriteImg!=null){
            Temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteImg;
        }
        Destroy(building);
        FollowBuilding();

    }

    private void ClearArea() {
        TileBase[] toClear = new TileBase[previArea.size.x * previArea.size.y * previArea.size.z ];
        FillTiles(toClear, TileTypes.VAZIO);
        TempTileMap.SetTilesBlock(previArea, toClear);

    }

    private void FollowBuilding() {
        ClearArea();

        Temp.area.position = GridLayout.WorldToCell(Temp.gameObject.transform.position);
        BoundsInt buildingArea = Temp.area;

        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTileMap);
        
        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if(baseArray[i] == Tilebases[TileTypes.BRANCO] ) {
                tileArray[i] = Tilebases[TileTypes.VERDE];
            } else {
                FillTiles(tileArray, TileTypes.VERMELHO);
                break;
            }
        }

        TempTileMap.SetTilesBlock(buildingArea, tileArray);
        previArea = buildingArea;

    }

    public bool CantakeArea(BoundsInt area){
        TileBase[] baseArray = GetTilesBlock(area, MainTileMap);
        foreach (var b in baseArray)
        {
            if(b != Tilebases[TileTypes.BRANCO]){
                print("Cannot Place here");
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area){
        SetTilesBlock(area, TileTypes.VAZIO, TempTileMap);
        SetTilesBlock(area, TileTypes.VERDE, MainTileMap);
    }

    #endregion


}

public enum TileTypes 
{
    VAZIO,
    BRANCO,
    VERDE,
    VERMELHO
}
