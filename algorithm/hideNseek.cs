using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    static void Main(){
        
        string input = Console.ReadLine().Split(" ");
        
        // 수빈(n),동생(k)의 위치
        int n = int.Parse(input[0]);
        int k = int.Parse(input[1]);
        
        // 1. 수빈,동생은 1~10만의 정수.
        // 2. 수빈이는 1초에 걸으면 n+1,n-1 순간이동하면 2n 움직임
        // 3. 동생을 찾는 가장 빠른 시간.
        
        // 핵심. n 상태에서 이동가능한 경우의 수는 최대 3가지, +1,-1,*2
        // 이동할 때 
        
        int[] BFSV = new int[100001];
        // 큐 생성 후 수빈이 위치 입력
        Queue<int> q = new Queue<int>();
        q.Enqueue(n);
        
        // 이동 체크 + 이동 횟수 
        // n번째 칸에 데이터가 있다 == 왔다는 증거
        // n번째 칸에 숫자 == 이동한 횟수
        
        BFSV[n] = 1;
        // 큐 1개 이상이면 루프
        while(q.Count > 0){
            // 수빈이 위치 지정
            int position = q.Dequeue();
            
            // 동생 위치랑 같으면 루프 종료
            
            if(position == k){
                Console.WriteLine(BFSV[position]-1);
            }
            
            
            
            // 현재 위치 기준으로 -1,+1,*2 의 값을 루프 하면서 방문 안했으면 방문
            foreach(int next in new int[]{ position-1,position+1,position*2 }){
                // 방문 안했을 때, 단 수빈이의 위치는 0~10만 사이
                if(BFSV[next] == 0 && next>=0 && next <= 100000){
                    BFSV[next] = BFSV[position] + 1;
                    q.Enqueue(next);
                }
            }
        }
    }
}
