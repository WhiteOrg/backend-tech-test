# Stech Backend Technical Challenge

Hi, welcome to the tech challenge. Challenge should take 10 mins. Please don't use any compiler or IDE when answering questions.

## The Challenge
1. What do you expect for the output and what is the cause of the difference between s1 and s2?
```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Program 
{
  static ArrayList _arr1 = new ArrayList();
  static List<int> _list1 = new List<int>();

  public static void Main() 
  {
    for (int i = 0; i < 10000; i++) 
    {
      _arr1.Add(0);
      _list1.Add(0);
    }
    const int m = 10000;
    Stopwatch s1 = new Stopwatch();
    s1.Start();
    for (int i = 0; i < m; i++) {
      A();
    }
    s1.Stop();
    
    Stopwatch s2 = new Stopwatch();
    s2.Start();
    for (int i = 0; i < m; i++) {
      B();
    }
    s2.Stop();
    
    Console.WriteLine("{0},{1}",
      s1.ElapsedMilliseconds,
      s2.ElapsedMilliseconds);
  }

  static void A() 
  {
    foreach(var i in _arr1) 
    {
      int v = (int) i;
    }
  }

  static void B() 
  {
    foreach(var i in _list1) 
    {
      int v = i;
    }
  }
}
```

2. How to write and use extension methods 

3. What are the differences between IEnumerable and IQueryable interfaces?

4. What is the output
```csharp
using System;
public class Program 
{
  static Program() 
  {
    Console.WriteLine("Static constructor");
  }

  public Program() 
  {
    Console.WriteLine("Constructor");
  }

  public static void Main(string[] args) 
  {
    Console.WriteLine("Main");
  }
}
```

5. Rewrite this code block without `using` statement
```csharp
using System;
using System.Data.SqlClient;
public class Program
{
	public static void Main(string[] args)
	{
		using(SqlConnection con = new SqlConnection("conStr"))
		{
        //code
        //code
		}
	}
}

```

6. What do you expect for the output and what is the cause of the difference between s1 and s2?
```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Program 
{
  static int[] _arr1 = new int[1000000];
  static List<int> _list1 = new List<int>();

  public static void Main() 
  {
    Stopwatch s1 = new Stopwatch();
    s1.Start();
    for (int i = 0; i < 1000000; i++) 
    {
      _arr1[i] = 0;
    }
    s1.Stop();
    using (Stream s = new MemoryStream()) 
    {
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(s, _arr1);
      Console.WriteLine("_arr1:" + s.Length);
    }
	  
    
    Stopwatch s2 = new Stopwatch();
    s2.Start();
    for (int i = 0; i < 1000000; i++) 
    {
      _list1.Add(0);
    }
    s2.Stop();
	  
    using (Stream s = new MemoryStream()) 
    {
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(s, _list1);
      Console.WriteLine("_list1:" + s.Length);
    }
	  
    
    Console.WriteLine("{0},{1}",
      s1.ElapsedMilliseconds,
      s2.ElapsedMilliseconds);
  }
}
```

7.  What is the output?
```csharp
using System;

public class DrawingObject 
{
  public virtual void Draw() 
  {
    Console.WriteLine("I am a drawing object.");
  }
}
public class Triangle: DrawingObject 
{
  public override void Draw() 
  {
    Console.WriteLine("I am a Triangle.");
  }
}
public class Circle: DrawingObject 
{
  public override void Draw() 
  {
    Console.WriteLine("I am a Circle.");
  }
}
public class Rectangle: DrawingObject 
{
  public new void Draw() 
  {
    Console.WriteLine("I am a Rectangle.");
  }
}
public class DrawDemo 
{
  public static void Main() 
  {
    DrawingObject[] drawObjArray = new DrawingObject[4];

    drawObjArray[0] = new Triangle();
    drawObjArray[1] = new Circle();
    drawObjArray[2] = new Rectangle();
    drawObjArray[3] = new DrawingObject();

    foreach(DrawingObject drawObj in drawObjArray) 
      drawObj.Draw();
  }
}
```

8. If you find any potential bugs, fix the code
```csharp
[HttpGet("{id}")]
public async Task<ActionResult<object>> GetTodoItem(long id)
{
    using(var client = new HttpClient())
    {
        var result = await client.GetAsync($"https://stech.com/items/{id}");
        return await result.Content.ReadAsStringAsync();
    }
}
```

9. Explain difference between 2 methods
```csharp
using System;
using System.Data.SqlClient;
public class Program
{
	public async Task<IEnumerable<object>> GetObjectListAsync()
	{
		using(SqlConnection con = new SqlConnection("conStr"))
		{
        await con.OpenAsync();

        //code
        //code

        return await new SqlCommand("QUERY").ExecuteScalarAsync();
		}
	}



  
	public async Task<IEnumerable<object>> GetObjectList()
	{
		using(SqlConnection con = new SqlConnection("conStr"))
		{
        var task = con.OpenAsync();
        task.Result;

        //code
        //code

        object commandTask = new SqlCommand("QUERY").ExecuteScalarAsync();
        return commandTask.Result;
		}
	}
}
```

10. What is the output?
```csharp
using System;
public class Program
{
  public static void Main() 
  {
     int i = 0;
     Console.WriteLine(i++);
     Console.WriteLine(++i);
  }
}
```