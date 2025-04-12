using System;
using System.Collections.Generic;
using System.Linq;

class Program{
    public static void Main(){
        
		// - 그룹으로 나누어서 첫째 그룹은 ALL + 그 이외는 전부 -
		// 최대한 () 안에 최대한 숫자 많이 넣어서 피연산자를 크게해 최솟값이 되게해야함.
        string[] input = Console.ReadLine().Split('-');
        int result = Sum(input[0]);
        
        for(int i = 1;i<input.Length;i++)
        {
            result -= Sum(input[i]);
        }
        
        Console.WriteLine(result);
    }
    
    public static int Sum(string expr){
        return expr.Split('+').Select(int.Parse).Sum();
    }
}
