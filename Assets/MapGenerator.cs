using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public GameObject PlayerTP;
    public bool reset, baza, kwiatki, Bushes, Trees,FlowersBig;
    public Tilemap tilemap;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public BazaClass BazaC;
    public KwiatkiGenClass KwiatkiC;
    public BushClass BushC;
    public TreesClass TreesC;
    public FlowersBigClass FlowersBC;
    private void Start()
    {
        UpdateClass();
    }
    private void UpdateClass()
    {
        BazaC.mapWidthBaseClass = mapWidth;
        BazaC.mapHeighBaseClass = mapHeight;
        BazaC.tilemap = tilemap;
        KwiatkiC.KwiatkimapHeight = mapHeight;
        KwiatkiC.KwiatkimapWidth = mapWidth;
        KwiatkiC.tilemap = tilemap;
        BushC.BushMapHeight = mapHeight;
        BushC.BushMapWidth = mapWidth;
        TreesC.TreesMapHeight = mapHeight;
        TreesC.TreesMapWidth = mapWidth;
        FlowersBC.FlowersBMapHeigh = mapHeight;
        FlowersBC.FlowersBMapWidth = mapWidth;
    }
    private void Update()
    {
        if (reset)
        {
            tilemap.ClearAllTiles();
            BazaC.Bariera.ClearAllTiles();
            BushC.tilemap.ClearAllTiles();
            TreesC.tilemap.ClearAllTiles();
            FlowersBC.tilemap.ClearAllTiles();
            reset = false;
            PlayerTP.transform.position = new Vector2(mapWidth / 2, mapHeight / 2);
        }
        if (baza)
        {
            UpdateClass();
            BazaC.GenerateBase();
            baza = false;
            PlayerTP.transform.position = new Vector2(mapWidth / 2, mapHeight / 2);
        }
        if (kwiatki)
        {
            UpdateClass();
            BazaC.GenerateBase();
            KwiatkiC.Generateflwoers();
            kwiatki = false;
        }
        if (Bushes)
        {
            UpdateClass();
            BushC.GenerateBush();
            Bushes = false;
        }
        if (Trees)
        {
            UpdateClass();
            TreesC.GenerateTrees();
            Trees = false;
        }
        if (FlowersBig)
        {
            UpdateClass();
            FlowersBC.GenerateFlowers();
            FlowersBig = false;
        }
    }
    [System.Serializable]
    public class FlowersBigClass
    {
        [HideInInspector] public int FlowersBMapWidth;
        [HideInInspector] public int FlowersBMapHeigh;
        public TileBase[] FlowersBig;
        public Tilemap tilemap;
        public float FlowersChance = 1;
        public float AddtionalFlowers = 5;
        public int AddFlowersDistacne = 5;
        [HideInInspector] private int CurrentFlower;
        public void GenerateFlowers()
        {
            tilemap.ClearAllTiles();
            for (int i = 0; i < Random.Range(0, FlowersChance * (FlowersBMapHeigh * FlowersBMapWidth)); i++)
            {
                int x = Random.Range(0, FlowersBMapWidth);
                int y = Random.Range(0, FlowersBMapHeigh);
                Vector3Int tilePosition = new(x, y, 0);
                tilemap.SetTile(tilePosition, GetRandomTile());
                for (int q = 0; q < Random.Range(10, AddtionalFlowers); q++)
                {
                    int xend = x;
                    int yend = y;
                    xend += Random.Range(-AddFlowersDistacne, AddFlowersDistacne);
                    yend += Random.Range(-AddFlowersDistacne, AddFlowersDistacne);
                    Vector3Int tilePositionend = new(xend, yend, 0);
                    tilemap.SetTile(tilePositionend, FlowersBig[CurrentFlower]);
                }
            }
        }
        TileBase GetRandomTile()
        {

            int randomIndex = Random.Range(0, FlowersBig.Length);
            CurrentFlower = randomIndex;
            return FlowersBig[randomIndex];
        }
    }

    [System.Serializable]
    public class TreesClass
    {
        [HideInInspector] public int TreesMapWidth;
        [HideInInspector] public int TreesMapHeight;
        public TileBase[] Trees;
        public Tilemap tilemap;
        public float TreeChance = 1;
        public TileBase[] Mushrooms;
        public float MushroomChance;
        public int Distance;
        public void GenerateTrees()
        {
            tilemap.ClearAllTiles();
            for (int i = 0; i < Random.Range(0, TreeChance * (TreesMapHeight * TreesMapWidth)); i++)
            {
                int x = Random.Range(0, TreesMapWidth);
                int y = Random.Range(0, TreesMapHeight);
                Vector3Int tilePosition = new(x, y, 0);
                tilemap.SetTile(tilePosition, GetRandomTree());
                for (int q = 0; q < Random.Range(0, MushroomChance); q++)
                {
                    int xend = x;
                    int yend = y;
                    xend += Random.Range(-Distance, Distance);
                    yend += Random.Range(-Distance, Distance);
                    Vector3Int tilePositionend = new(xend, yend, 0);
                    tilemap.SetTile(tilePositionend, GetRandomMushoorm());
                }
            }
        }
        TileBase GetRandomTree()
        {
            int randomIndex = Random.Range(0, Trees.Length);
            return Trees[randomIndex];
        }
        TileBase GetRandomMushoorm()
        {
            int randomIndex = Random.Range(0, Mushrooms.Length);
            return Mushrooms[randomIndex];
        }
    }
    [System.Serializable]
    public class BushClass
    {
        [HideInInspector] public int BushMapWidth;
        [HideInInspector] public int BushMapHeight;
        public TileBase[] Bushes;
        public Tilemap tilemap;
        public float BushChance = 1;
        public void GenerateBush()
        {
            tilemap.ClearAllTiles();
            for (int i = 0; i < Random.Range(0, BushChance * (BushMapHeight * BushMapWidth)); i++)
            {
                int x = Random.Range(0, BushMapWidth);
                int y = Random.Range(0, BushMapHeight);
                Vector3Int tilePosition = new(x, y, 0);
                tilemap.SetTile(tilePosition, GetRandomTile());
            }
        }
        TileBase GetRandomTile()
        {

            int randomIndex = Random.Range(0, Bushes.Length);
            return Bushes[randomIndex];
        }
    }
    [System.Serializable]
    public class KwiatkiGenClass
    {
        [HideInInspector] public int KwiatkimapWidth;
        [HideInInspector] public int KwiatkimapHeight;
        public TileBase[] Flowers;
        [HideInInspector] public Tilemap tilemap;
        private int szansa;
        public int szansaNaKwiatek = 10;
        public void Generateflwoers()
        {
            for (int x = 0; x < KwiatkimapWidth; x++)
            {
                for (int y = 0; y < KwiatkimapHeight; y++)
                {
                    szansa = Random.Range(0, szansaNaKwiatek);
                    if (szansa == 2)
                    {
                        Vector3Int tilePosition = new(x, y, 0);
                        tilemap.SetTile(tilePosition, GetRandomTile());
                    }
                }
            }
        }
        TileBase GetRandomTile()
        {

            int randomIndex = Random.Range(0, Flowers.Length);
            return Flowers[randomIndex];
        }
    }
    [System.Serializable]
    public class BazaClass
    {
        [HideInInspector] public int mapWidthBaseClass;
        [HideInInspector] public int mapHeighBaseClass;
        public TileBase Baza;
        public Tilemap Bariera;
        public int barrierOffset;
        public TileBase blokBariery;
        [HideInInspector] public Tilemap tilemap;

        public void GenerateBase()
        {
            tilemap.ClearAllTiles();
            for (int x = 0; x < mapWidthBaseClass; x++)
            {
                for (int y = 0; y < mapHeighBaseClass; y++)
                {
                    Vector3Int tilePosition = new(x, y, 0);
                    tilemap.SetTile(tilePosition, Baza);
                }
            }
            GenerateBarrier();
        }
        private void GenerateBarrier()
        {
            for (int x = 0; x < mapWidthBaseClass; x++) //dol
            {
                Vector3Int tilePosition = new(x, barrierOffset, 0);
                Bariera.SetTile(tilePosition, blokBariery);
            }
            for (int x = 0; x < mapWidthBaseClass; x++) //gora
            {
                Vector3Int tilePosition = new(x, mapHeighBaseClass - barrierOffset, 0);
                Bariera.SetTile(tilePosition, blokBariery);
            }
            for (int y = 0; y < mapHeighBaseClass; y++) //lewo
            {
                Vector3Int tilePosition = new(barrierOffset, y, 0);
                Bariera.SetTile(tilePosition, blokBariery);
            }
            for (int y = 0; y < mapHeighBaseClass; y++) //prawo
            {
                Vector3Int tilePosition = new(mapWidthBaseClass - barrierOffset, y, 0);
                Bariera.SetTile(tilePosition, blokBariery);
            }
        }
    }
}