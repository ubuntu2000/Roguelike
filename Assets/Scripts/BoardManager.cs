using UnityEngine;
using UnityEngine.Tilemaps;




public class BoardManager : MonoBehaviour
{
    
    // Khai báo và khởi tạo biến đại diện cụ thể cho lớp PlayerController
    public PlayerController Player;

    // Biến chỉ đại diện cho lưới(Grid)
    private Grid m_Grid;

    // Biến tham chiếu riêng của Grid( lưới)
    private Tilemap m_Tilemap;
    // chiều rộng của bảng lưới 
    public int Width;
    // Chiều cao của bảng lưới 
    public int Height;
    // Mảng các ô tiles mặt đất của lưới 
    public Tile[] GroundTiles;

    // Mảng các ô tiles tường của lưới
    public Tile[] WallTiles;

    // Khai báo ô( đại từ nhân xưng) 
    private Tile m_tile;
  
    // Khai báo 1 lớp dữ liệu ô chứa dữ liệu bool 
    public class CellData
    {
        // biến biểu diễn ô cho phép player đi qua hay không?
        public bool Passable;
    }
    // Khai báo mảng dữ liệu chứa các phần tử chỉ vị trí ô trong bảng (0,0; 1,1; 1,0; ....) 
    private CellData[,] m_BoardData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_Grid = GetComponentInChildren<Grid>();

       m_BoardData = new CellData[Width, Height];

        for (int x = 0; x < Height; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                m_BoardData[x, y] = new CellData();

                // Tất cả ô dong x=0, y=0, x=7, y=7, thì vẽ tường bao, còn lại vẽ ô ground
                if ( x == 0 || y == 0 || x == Width-1 || y == Height-1 )
                {
                    m_tile = WallTiles[Random.Range(0, WallTiles.Length)];
                  // Dat  false neu la tuong ( x= 0, y =0 , x=7, y=7 thì nhân vật player ko đi qua được)
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    m_tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                  // // Dat true nếu không là tường
                    m_BoardData[x, y].Passable = true;
                }
                    
                m_Tilemap.SetTile(new Vector3Int(x, y, 0), m_tile);

            }
        }
        // spawn player vi tri toa do o 1,1// This chỉ thể hiện của lớp( biểu diễn lớp trong thế giới thưc) ý chỉ PlayCharacter là thể hiện của lớp 
        // PlayController.

        Player.Spawn(this, new Vector2Int(1, 1));
    }

    // Chuyển từ Lưới sang vị trí trong không gian oxyz( ba chiều)
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
    // Phuong thuc lấy vị trí ô trỏ tới;
    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width
            || cellIndex.y < 0 || cellIndex.y >= Height)
        {
            return null;
        }

        return m_BoardData[cellIndex.x, cellIndex.y];
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
