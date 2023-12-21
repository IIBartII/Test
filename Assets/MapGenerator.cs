using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    public GameObject PlayerTP;
    public bool reset,baza, kwiatki, environment = false;
    public Tilemap tilemap;
    public int mapWidth = 10;
    public int mapHeight = 10;
    public BazaClass BazaC;
    public KwiatkiGenClass KwiatkiC;
    public EnvironmentClass EnvironmentC;
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
        EnvironmentC.EnvironmentMapHeight = mapHeight;
        EnvironmentC.EnvironmentMapWidth = mapWidth;
    }
    private void Update()
    {
        if (reset)
        {
            tilemap.ClearAllTiles();
            EnvironmentC.tilemap.ClearAllTiles();
            BazaC.Bariera.ClearAllTiles();
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
        if (environment)
        {
            UpdateClass();
            EnvironmentC.GenerateEnvironment();
            environment = false;
        }
    }
    [System.Serializable]
    public class EnvironmentClass
    {
        [HideInInspector] public int EnvironmentMapWidth;
        [HideInInspector] public int EnvironmentMapHeight;
        public TileBase[] Environment;
        public Tilemap tilemap;
        public float szansaNaEnvironment = 1;
        public float dodEnviroment = 5;
        public int odlegloscDodEnv = 5;
        public void GenerateEnvironment()
        {
            tilemap.ClearAllTiles();
            for(int i = 0;i < Random.Range(0,szansaNaEnvironment * (EnvironmentMapHeight * EnvironmentMapWidth)) ;i++)
            {
                int x = Random.Range(0, EnvironmentMapWidth);
                int y = Random.Range(0, EnvironmentMapHeight);
                Vector3Int tilePosition = new(x, y, 0);
                tilemap.SetTile(tilePosition, GetRandomTile());
                for(int q = 0; q< Random.Range(0, dodEnviroment);q++)
                {
                    int xend = x;
                    int yend = y;
                    xend += Random.Range(-odlegloscDodEnv, odlegloscDodEnv);
                    yend += Random.Range(-odlegloscDodEnv, odlegloscDodEnv);
                    Vector3Int tilePositionend = new(xend, yend, 0);
                    tilemap.SetTile(tilePositionend, GetRandomTile());
                }
            }
        }
        TileBase GetRandomTile()
        {

            int randomIndex = Random.Range(0, Environment.Length);
            return Environment[randomIndex];
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