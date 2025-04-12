using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    
    public static void Main(){
        
        int size = int.Parse(Console.ReadLine());
        
        Stack<int> s = new Stack<int>();
        
        for(int i=0;i<size;i++){
            int input =  int.Parse(Console.ReadLine());
            if(input !=0){
                s.Push(input);
            }
            else{
                s.Pop();
            }
        }
        Console.WriteLine(s.Sum());
        
    }
    
}
