using System;
using System.Collections.Generic;
using System.Linq;

public class Program{
    public static void Main(){
        // 입력
        string[] input = Console.ReadLine().Split(" ");
        //할당
        int M = int.Parse(input[0]),N = int.Parse(input[1]), K = int.Parse(input[2]);
        
        // 영역, 1차원 DFS 의 간선
        bool[,] board = new bool[M,N];
        
        // 영역 입력 받기 
        for(int i=0; i<K;i++){
            string[] rect = Console.ReadLine().Split(" ");
            int x1 = int.Parse(rect[0]);
            int y1 = int.Parse(rect[1]);
            int x2 = int.Parse(rect[2]);
            int y2 = int.Parse(rect[3]);
            
            // 영역 할당하기
            for(int y = y1;y<y2;y++){
                for(int x=x1;x<x2;x++){
                    board[y,x] = true;
                }
            }
        }
        List<int> areaList = new List<int>();
        bool[,] visited = new bool[M,N]; 
        
        // 도화지 전체 0,0~ M,N 까지 완전 탐색.
        for(int y=0; y<M; y++){
            for(int x=0;x<N; x++){
                // 칠해져 있지 않고, 방문 하지 않았으면 탐색
                if(!board[y,x] && !visited[y,x]){
                    int area = DFS(y,x,board,visited);
					areaList.Add(area);
                }
            }
        }
		areaList.Sort();
		
        // 총 영역 개수 
        Console.WriteLine(areaList.Count);
        // 각 영역의 넓이
		Console.WriteLine(string.Join(" ",areaList));
    }
    public static int DFS(int y,int x, bool[,] board, bool[,] visited){
        
        // 방문했다고 표시
        visited[y,x] = true;
        // 방문했으니까 1
        int area = 1;
        
        // 상하좌우로 연결된 곳의 넓이 구하기
        
        int[] dy = {-1,1,0,0};
        int[] dx = {0,0,-1,1};
        
        // 4인 이유 = 4방향 상하좌우라서
        for(int d = 0;d<4; d++){
            
            //다음에 향할 y 축
            int ny = y + dy[d];
            int nx = x + dx[d];
            
            // 진행하려는 방향이 도화지의 크기를 벗어나려 하면 무시.
            if(ny< 0 || ny >= board.GetLength(0) || nx < 0 || nx >= board.GetLength(1))
                continue;
            // 방문하지 않았고, 칠해져 있지 않으면 ㄱㄱ
            if(!visited[ny,nx] && !board[ny,nx]){
                area += DFS(ny,nx,board,visited);
            }
        }
        
        return area;
        
        
    }
}
