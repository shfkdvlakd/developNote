using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 빠른 입력
        string[] tokens = Console.ReadLine().Split(" ");
        int n = int.Parse(tokens[0]);
        int m = int.Parse(tokens[1]);
        int v = int.Parse(tokens[2]);
        
        //필요한 것. 
        // 1. 정점 리스트
        // 2. 정점에서 방문할 수 있는 정점. sort 
        // 3. 해당 점 방문했는지 체크
        
        // 정점 리스트 1~n개 생성.
        List<int>[] g = new List<int>[n+1];
        for(int i=1;i<=n;i++){
                g[i] = new List<int>();
        }
        // 각 정점에서 도달할 수 있는 점들 ( 정렬 x )
        for(int i=0;i<m;i++){
            var e = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int a = e[0],b = e[1];
            g[a].Add(b);
            g[b].Add(a);
        }
        // 정렬
        for(int i = 1; i<=n;i++){
            g[i].Sort();
        }
        ////////////////////////////////////////
        
        // 여기에 알고리즘 작성
        // 1. DFS 
        bool[] DFSV = new bool[n+1];
        
        string DFSResult = DFS(v,g,DFSV,"").Trim();
        Console.WriteLine(DFSResult);
        
        // 2. BFS
        bool[] BFSV = new bool[n+1];
        string BFSResult = BFS(v,g,BFSV,"").Trim();
        Console.WriteLine(BFSResult);
    }
    
    // DFS 
    // v = 처음 방문하는 점 및 이후 이동될 점
    // g = 각 정점에서 이동가능한 점들이 정렬되어 있는 배열리스트
    // DFSV = 각 정점을 방문했는지 체크할 불린 배열
    // DFSR = 방문한 정점을 기록할 문자열
    private static string DFS(int v,List<int>[] g,bool[] DFSV,string DFSR){
        
        // 모든 점 다 순회했으면 종료
        if(DFSV[v]){
            return DFSR;
        }
        // 방문했다고 표시
        DFSV[v] = true;
        
        // 방문점 찍기
        DFSR += v.ToString() + " ";
        
        // 다음 점 으로 보내기
        foreach(int next in g[v]){
            if(!DFSV[next]){
                DFSR =  DFS(next,g,DFSV,DFSR);
            }
        }
        return DFSR;
    }
    
    // BFS 
    // Queue 사용
    // v = 처음 방문하는 점 및 이후 이동될 점
    // g = 각 정점에서 이동가능한 점들이 정렬되어 있는 배열리스트
    // BFSV = 각 정점을 방문했는지 체크할 불린 배열
    // BFSR = 방문한 정점을 기록할 문자열
    
    private static string BFS(int v,List<int>[] g,bool[] BFSV,string BFSR){
        // 
        Queue<int> q = new Queue<int>();
        // 큐 집어넣고, 방문했다 표시하고, 방문점 기록
        q.Enqueue(v);
        BFSV[v] = true;
        BFSR += v.ToString()+" ";
        
        // 큐 1개 이상이면 진입
        while(q.Count > 0){
            // 큐 해제와 동시에 현재 방문점 넘기고
            int current = q.Dequeue();
            
            // 현재 방문점에 연결되어 있는 정점들 반복
            foreach(int next in g[current]){
                // 아직 방문 안했으면 
                if(!BFSV[next])
                {
                    // 큐에 넣고,방문표시하고,방문점 기록하고
                    q.Enqueue(next);
                    BFSV[next] = true;
                    BFSR += next.ToString() + " ";
                }
            }
        }
        return BFSR;
    }
    
    
    
    
}
