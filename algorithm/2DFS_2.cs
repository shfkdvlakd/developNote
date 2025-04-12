using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    public static void Main(){
        
		// 체스판 생성
        int[,] board = new int[5,5];
        
		// 체스판 입력
        for(int i=0;i<5;i++){
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for(int j=0;j<5;j++){
                board[i,j] = input[j];
            }
        }
        
        // DFS 시작
		// 이동경로 저장할 해시셋
        HashSet<string> move = new HashSet<string>();
        for(int y = 0;y<5;y++){
            for(int x = 0;x<5;x++){
                // y축, x축, 보드판, 뎁스, 이동경로,이동경로저장 
                DFS(y,x,board,0,board[y,x].ToString(),move);
            }
        }
        Console.WriteLine(move.Count);
        
    }
    
    public static void DFS(int y, int x,int[,] board,int depth,string path,HashSet<string> move){
        
        if(depth == 5){
            move.Add(path.ToString());
            return;
        }
        
        int[] dy = {-1,1,0,0};
        int[] dx = {0,0,-1,1};
        // 4방향중 어디로 움직일지.
        for(int d=0;d<4;d++){
            int ny = y + dy[d];
            int nx = x + dx[d];
            
            // 5x5 체스판 벗어나면 ignore
            if(ny < 0 || ny>=5 || nx < 0|| nx>=5)
                continue;
            DFS(ny,nx,board,depth+1,path+board[ny,nx],move);
        }
    }
    
}
