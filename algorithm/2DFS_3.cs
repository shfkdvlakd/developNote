using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    
    public static void Main(){
        
		// 지도 사이즈
        int size = int.Parse(Console.ReadLine());
        
		// 지도 초기화
        int[,] map = new int[size,size];
        
		// 지도 크기만큼 반복
        for(int i = 0;i<size;i++){
            
			// 지도 좌표 입력 받음 문자열로 받으니까 문자인 '숫자' 를 숫자로 변환 및 배열 처리
            var input = Console.ReadLine().ToCharArray().Select(c => c-'0').ToArray();
			
            for(int j=0; j<size;j++){
                map[i,j] = input[j];
            }
        }
        
        // 단지수 기록할 배열
        List<int> apt = new List<int>();
		// 방문기록할 배열
        bool[,] visited = new bool[size,size];
		
		// 지도 크기만큼 반복
        for(int y = 0;y<size;y++)
        {
            for(int x=0;x<size;x++)
            {
				// 방문안했고, 아파트 단지 있을 때 
                if(!visited[y,x] && map[y,x] == 1){
                    int area = DFS(y,x,map,visited);
                    apt.Add(area);
                }
                
            }
        }
        
        // 단지 그룹 개수
        Console.WriteLine(apt.Count);
		// 단지 오름차순 정렬
		apt.Sort();
		// 단지 그룹 개수만큼 반복
        foreach(int a in apt){
			// 단지 출력
            Console.WriteLine(a);
        }
    }
    
    public static int DFS(int y, int x, int[,] map,bool[,] visited){
        
        // 방문 추적
        visited[y,x] = true;
        int count = 1;
        // 4방향 좌표 
        int[] dy = {-1,1,0,0};
        int[] dx = {0,0,-1,1};
        
        // 4방향으로 갈거니까 
        for(int d=0;d<4;d++){
            int ny = y + dy[d];
            int nx = x + dx[d];
            
            // 지도 범위 벗어나면 무시
            if(ny<0 || ny>=map.GetLength(0)|| nx<0|| nx>=map.GetLength(1) )
            continue;
            
            if(!visited[ny,nx] &&map[ny,nx] == 1){
               count += DFS(ny,nx,map,visited);
            }
        }
        return count;
    }
    
    
    
}
