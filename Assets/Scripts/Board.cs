using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Tilemap tilemap;

    List<Vector3Int> doors = new List<Vector3Int>();
    Dictionary<string, Vector3Int> rooms = new Dictionary<string, Vector3Int>();
    Dictionary<string, List<Vector3Int>> roomsToDoors = new Dictionary<string, List<Vector3Int>>();
    private string[] roomName = { "Hall", "Lounge", "Dining Room", "Kitchen", "Ballroom", "Conservatory", "Billiard Room", "Library", "Study" };

    public void Awake()
    {
        FillDoors();
        FillRooms();
        FillRoomsToDoors();
        tilemap = GetComponent<Tilemap>();
        playerController.PlayerRolledTheDice += ShowAvailableMoves;
        playerController.PlayerFinishedMove += ClearSelection;

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            visited.Add(position, false);
            depth.Add(position, -1);
        }
    }

    List<Vector3Int> reachableCells;

    public void OnCellSelected(Vector3 worldPoint)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPoint);
        if (reachableCells == null)
        {
            return;
        }
        if (!reachableCells.Contains(cellPosition))
        {
            return;
        }
        Tile tile = tilemap.GetTile<Tile>(cellPosition);
        for (int i = 0; i < roomName.Length; i++)
        {
            if (tile.name.Contains(roomName[i]))
            {
                playerController.GoOnCell(rooms[roomName[i]], roomName[i]);
                return;
            }
        }
        playerController.GoOnCell(cellPosition, "outer");
    }

    public void ChooseSecretPassage()
    {
        string cellType = playerController.CellType;
        playerController.CanMove = true;
        if (cellType == "Lounge")
        {
            playerController.GoThroughPassage();
            playerController.GoOnCell(rooms["Conservatory"], "Conservatory");
        }
        else if (cellType == "Conservatory")
        {
	    playerController.GoThroughPassage();
            playerController.GoOnCell(rooms["Lounge"], "Lounge");
        }
        else if (cellType == "Kitchen")
        {
	    playerController.GoThroughPassage();
            playerController.GoOnCell(rooms["Study"], "Study");
        }
        else if (cellType == "Study")
        {
	    playerController.GoThroughPassage();
            playerController.GoOnCell(rooms["Kitchen"], "Kitchen");
        }
    }

    private void FillDoors()
    {
        doors.Add(new Vector3Int(-7, 8, 0));
        doors.Add(new Vector3Int(-6, 3, 0));
        doors.Add(new Vector3Int(-9, 1, 0));
        doors.Add(new Vector3Int(-11, -1, 0));
        doors.Add(new Vector3Int(-7, -4, 0));
        doors.Add(new Vector3Int(-8, -8, 0));
        doors.Add(new Vector3Int(-3, 7, 0));
        doors.Add(new Vector3Int(-1, 5, 0));
        doors.Add(new Vector3Int(0, 5, 0));
        doors.Add(new Vector3Int(-4, -8, 0));
        doors.Add(new Vector3Int(-3, -6, 0));
        doors.Add(new Vector3Int(2, -6, 0));
        doors.Add(new Vector3Int(3, -8, 0));
        doors.Add(new Vector3Int(8, -7, 0));
        doors.Add(new Vector3Int(6, 2, 0));
        doors.Add(new Vector3Int(5, 6, 0));
    }

    private void FillRooms()
    {
        rooms.Add("Hall", new Vector3Int(0, 7, 0));
        rooms.Add("Lounge", new Vector3Int(9, 9, 0));
        rooms.Add("Dining Room", new Vector3Int(9, 0, 0));
        rooms.Add("Kitchen", new Vector3Int(9, -10, 0));
        rooms.Add("Ballroom", new Vector3Int(0, -9, 0));
        rooms.Add("Conservatory", new Vector3Int(-9, -10, 0));
        rooms.Add("Billiard Room", new Vector3Int(-9, -3, 0));
        rooms.Add("Library", new Vector3Int(-9, 3, 0));
        rooms.Add("Study", new Vector3Int(-9, 9, 0));
    }

    private void FillRoomsToDoors()
    {
        roomsToDoors.Add("Study", new List<Vector3Int> { doors[0] });
        roomsToDoors.Add("Library", new List<Vector3Int> { doors[1], doors[2] });
        roomsToDoors.Add("Billiard Room", new List<Vector3Int> { doors[3], doors[4] });
        roomsToDoors.Add("Conservatory", new List<Vector3Int> { doors[5] });
        roomsToDoors.Add("Hall", new List<Vector3Int> { doors[6], doors[7], doors[8] });
        roomsToDoors.Add("Ballroom", new List<Vector3Int> { doors[9], doors[10], doors[11], doors[12] });
        roomsToDoors.Add("Kitchen", new List<Vector3Int> { doors[13] });
        roomsToDoors.Add("Dining Room", new List<Vector3Int> { doors[14] });
        roomsToDoors.Add("Lounge", new List<Vector3Int> { doors[15] });
    }

    private void ShowAvailableMoves()
    {
        List<Vector3Int> doors;
        roomsToDoors.TryGetValue(playerController.CellType, out doors);
        if (doors == null)
        {
            reachableCells = GetReachableCells(playerController.CurrentPosition, 
                playerController.Dice);
        }
        else
        {
            reachableCells = new List<Vector3Int>();
            for (int i = 0; i < doors.Count; i++)
            {
                reachableCells.AddRange(GetReachableCells(doors[i], 
                    playerController.Dice));
            }
        }
        foreach (var position in reachableCells)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, Color.red);
        }
    }

    private List<Vector3Int> GetReachableCells(Vector3Int position, int dice)
    {
        queue.Clear();
        List<Vector3Int> keys = new List<Vector3Int>(visited.Keys);
        foreach (var key in keys)
        {
            visited[key] = false;
            depth[key] = -1;
        }
        bfs(position);
        List<Vector3Int> res = new List<Vector3Int>();
        foreach (var key in depth.Keys)
        {
            if (depth[key] >= 0 && depth[key] <= dice)
            {
                res.Add(key);
            }
        }
        return res;
    }

    Queue<Vector3Int> queue = new Queue<Vector3Int>();
    Dictionary<Vector3Int, bool> visited = new Dictionary<Vector3Int, bool>();
    Dictionary<Vector3Int, int> depth = new Dictionary<Vector3Int, int>();

    private void bfs(Vector3Int start)
    {
        queue.Enqueue(start);
        visited[start] = true;
        depth[start] = 0;
        while (queue.Count != 0)
        {
            Vector3Int cur = queue.Dequeue();
            Vector3Int[] neighbours = GetNeighbours(cur);
            foreach (Vector3Int neighbour in neighbours)
            {
                Tile tile = tilemap.GetTile<Tile>(neighbour);
                if (!visited[neighbour] && tile != null && 
                    (tile.name.Contains("outer") || IsDoor(tile)))
                {
                    queue.Enqueue(neighbour);
                    visited[neighbour] = true;
                    depth[neighbour] = depth[cur] + 1;
                }
            }
        }
    }

    private Vector3Int[] direction = new Vector3Int[] { new Vector3Int(1,0,0),
    new Vector3Int(0,1,0), new Vector3Int(-1,0,0), new Vector3Int(0,-1,0) };

    private Vector3Int[] GetNeighbours(Vector3Int position)
    {
        List<Vector3Int> res = new List<Vector3Int>();
        for (int i = 0; i < direction.Length; i++)
        {
            Tile tile = tilemap.GetTile<Tile>(position + direction[i]);
            if (tile != null && 
                (tile.name.Contains("outer") || IsDoor(tile)))
            {
                res.Add(position + direction[i]);
            }
        }
        return res.ToArray();
    }

    private bool IsDoor(Tile tile)
    {
        for (int i = 0; i < 9; i++)
        {
            if (tile.name.Contains(roomName[i]))
            {
                return true;
            }
        }
        return false;
    }

    private void ClearSelection()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, Color.white);
        }
    }
}
