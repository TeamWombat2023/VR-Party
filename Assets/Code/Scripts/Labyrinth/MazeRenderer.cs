using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MazeRenderer : MonoBehaviour {

    [SerializeField] private PhotonView myPV;
    [SerializeField] [Range(1, 50)] private int width = 10;
    [SerializeField] [Range(1, 50)] private int height = 10;
    [SerializeField] private float size = 1f;
    [SerializeField] private Transform wallPrefab = null;
    //
    // [SerializeField]
    // private Transform floorPrefab = null;

    [SerializeField] private Transform coinPrefab = null;

    private bool generate_maze;
    private bool generate_maze_first_run = true;
    private Vector3 final_wall_pos;
    private float startTime;
    private float genTime;

    private WallState[,] maze;
    private string maze_json;
    private WallList wallList;

    // Start is called before the first frame update
    private void Start() {
        //start a game timer 
        startTime = PhotonNetwork.ServerTimestamp;
        genTime = startTime;

        Debug.Log("Is master client: " + PhotonNetwork.IsMasterClient);

        //generate and draw the maze
        if(PhotonNetwork.IsMasterClient) {
            maze = MazeGenerator.Generate(width, height);
            //maze_json = JsonUtility.ToJson(maze);
            //Debug.Log("Createde json: " + maze_json);
            Draw(maze, true);
            Debug.Log(wallList.list);
            maze_json = JsonUtility.ToJson(wallList);
            myPV.RPC("Sync_walls", RpcTarget.Others, maze_json, true);
            Debug.Log("First maze Generated and sent to clients");
        }
        generate_maze = false;
    }

    [PunRPC]
    private void Sync_walls(string maze_json, bool initial) {
        Debug.Log("Syncing walls");
        wallList = JsonUtility.FromJson<WallList>(maze_json);
        Debug.Log(wallList.list.Count);
        for (int i = 0; i < wallList.list.Count; i++) {
            var wall = Instantiate(wallPrefab, transform) as Transform;
            wall.position = wallList.list[i];
            if (wallList.sidewall[i] == true) {
                Debug.Log("Sidewall");
                wall.eulerAngles = new Vector3(0, 90, 0);
            }
            wall.localScale = wallList.localScale[i];
            Debug.Log("Created wall at position:" + wall.position);
        }
    }

    [PunRPC]
    private void sync_up(){
        var obj = GameObject.FindGameObjectsWithTag("Wall");
        Debug.Log("Animating going up. Found: " + obj.Length + " walls");
        for (var i = 0; i < obj.Length; i++)
            obj[i].transform.position = obj[i].transform.position + new Vector3(0, 0.025f, 0);
    }

    [PunRPC]
    private void sync_down(){
        var obj = GameObject.FindGameObjectsWithTag("Wall");
        for (var i = 0; i < obj.Length; i++) {
            //animate while moving
            obj[i].transform.position = Vector3.Lerp(obj[i].transform.position,
            obj[i].transform.position + new Vector3(0, -10, 0), Time.deltaTime / 4);
            //destroy the wall
            Destroy(obj[i], 1);
        }
    }


    private void Draw(WallState[,] maze, bool initial) {
        //var floor = Instantiate(floorPrefab, transform);
        //floor.localScale = new Vector3(width * width * size, 1, height * height * size);
        //Debug.Log(maze_json);
        //maze = JsonUtility.FromJson<WallState[,]>(maze_json);

        wallList= new WallList();
        wallList.list = new List<Vector3>();
        wallList.localScale = new List<Vector3>();
        wallList.sidewall = new List<bool>();

        for (var i = 0; i < width; ++i)
        for (var j = 0; j < height; ++j) {
            var cell = maze[i, j];
            var position = new Vector3((-width / 2 + i) * size, 0, (-height / 2 + j) * size);

            //draw the coins
            if (initial) {
                var coin = Instantiate(coinPrefab, transform) as Transform;
                coin.position = position + new Vector3(0, 0.5f, 0);
            }

            if (cell.HasFlag(WallState.UP)) {
                Transform topWall = Instantiate(wallPrefab, transform) as Transform;
                topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                if (initial)
                    topWall.position = position + new Vector3(0, 0, size / 2);
                else
                    topWall.position = position + new Vector3(0, -10, size / 2);
                wallList.list.Add(topWall.position);
                wallList.localScale.Add(topWall.localScale);
                wallList.sidewall.Add(false);
            }

            if (cell.HasFlag(WallState.LEFT)) {
                Transform leftWall = Instantiate(wallPrefab, transform) as Transform;
                leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                leftWall.eulerAngles = new Vector3(0, 90, 0);
                if (initial)
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                else
                    leftWall.position = position + new Vector3(-size / 2, -10, 0);
                wallList.list.Add(leftWall.position);
                wallList.localScale.Add(leftWall.localScale);
                wallList.sidewall.Add(true);
            }

            if (i == width - 1)
                if (cell.HasFlag(WallState.RIGHT)) {
                    Transform rightWall = Instantiate(wallPrefab, transform) as Transform;
                    rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    rightWall.eulerAngles = new Vector3(0, 90, 0);
                    if (initial)
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                    else
                        rightWall.position = position + new Vector3(size / 2, -10, 0);
                    wallList.list.Add(rightWall.position);
                    wallList.localScale.Add(rightWall.localScale);
                    wallList.sidewall.Add(true);
                }

            if (j == 0)
                if (cell.HasFlag(WallState.DOWN)) {
                    Transform bottomWall = Instantiate(wallPrefab, transform) as Transform;
                    bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    if (initial)
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                    else
                        bottomWall.position = position + new Vector3(0, -10, -size / 2);
                    wallList.list.Add(bottomWall.position);
                    wallList.localScale.Add(bottomWall.localScale);
                    wallList.sidewall.Add(false);
                }
        }
    }

    // Update is called once per frame
    private void Update() {
        //remove the current maze
        if (PhotonNetwork.ServerTimestamp - genTime > 10000 && generate_maze == false ) {
            Debug.Log("Removing Maze");
            //RemoveMaze();

            var obj = GameObject.FindGameObjectsWithTag("Wall");
            Debug.Log("Found " + obj.Length + " walls");

            if (obj.Length == 0){
                generate_maze = true;
                Debug.Log("generate_maze set to true");
            }

            if (PhotonNetwork.IsMasterClient){
                myPV.RPC("sync_down", RpcTarget.Others);
                for (var i = 0; i < obj.Length; i++) {
                    //animate while moving
                    obj[i].transform.position = Vector3.Lerp(obj[i].transform.position,
                    obj[i].transform.position + new Vector3(0, -10, 0), Time.deltaTime / 4);
                    //destroy the wall
                    Destroy(obj[i], 1);
                }
            }

        }

        else if (generate_maze == true && PhotonNetwork.IsMasterClient) {

            if (generate_maze_first_run == true) {
                
                Debug.Log("Generating new maze for master");
                maze = MazeGenerator.Generate(width, height);
                Draw(maze, false);
                string maze_json = JsonUtility.ToJson(wallList);
                myPV.RPC("Sync_walls", RpcTarget.Others, maze_json, false);
                Debug.Log("New maze Generated and sent to clients");
                
                var obj = GameObject.FindGameObjectsWithTag("Wall");
                final_wall_pos = obj[0].transform.position + new Vector3(0, 10, 0);
                generate_maze_first_run = false;
            }

            else if (generate_maze == true){
                //animate going up
                var obj = GameObject.FindGameObjectsWithTag("Wall");
                Debug.Log("Animating going up. Found: " + obj.Length + " walls");
                for (var i = 0; i < obj.Length; i++)
                    obj[i].transform.position = obj[i].transform.position + new Vector3(0, 0.025f, 0);
                myPV.RPC("sync_up", RpcTarget.Others);

                if (final_wall_pos.y <= obj[0].transform.position.y) {
                    generate_maze = false;
                    generate_maze_first_run = true;
                    genTime = PhotonNetwork.ServerTimestamp;
                }
            }
        }
        
        //if(generate_maze == true){
        //    //animate going up
        //    var obj = GameObject.FindGameObjectsWithTag("Wall");
        //    Debug.Log("Animating going up. Found: " + obj.Length + " walls");
        //    for (var i = 0; i < obj.Length; i++)
        //        obj[i].transform.position = obj[i].transform.position + new Vector3(0, 0.025f, 0);
//
        //    if (final_wall_pos.y <= obj[0].transform.position.y) {
        //        generate_maze = false;
        //        generate_maze_first_run = true;
        //        genTime = PhotonNetwork.ServerTimestamp;
        //    }
        //    
        //}
    }
}

class WallList {
    public List<Vector3> list;
    public List<Vector3> localScale;
    public List<bool> sidewall;
}