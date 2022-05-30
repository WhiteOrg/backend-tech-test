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
ZG: Method A will be slower that method B because it uses type agnostic ArrayList and does explicit type casting.

2. How to write and use extension methods 
ZG: Extension methods are implemented in a static class as a static method which accepts as a first parameter the type we want to extend but with "this" keyword before the type. You can use the extension method by adding using of that static class to the place you want to use the method. Extension methods are used as any other public method of an instance from the extended type.

3. What are the differences between IEnumerable and IQueryable interfaces?
ZG: IQueryable extends IEnumberable but has an additional methods which works with expression trees. IQueryable usually works with external data sources like databases whether IEnumerable works with in-memory data collections. 

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
ZG: Static constructor will be executed first and instance constructor is not called anywhere. This leads to the following result:
"Static constructor"
"Main"

5. Rewrite this code block without `using` statement
```csharp
using System;
using System.Data.SqlClient;
public class Program 
{
  public static void Main(string[] args) 
  {
 	var connection = new SqlConnection("conStr");
    try {
    	// use connection here;
	//
    }
    catch {
    }
    finally {
     connection.Dispose();
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
ZG: I expect second timer to be slighly slower and size of the list to be slighly larger than the array because list is a dynamic structure where each element holds the address of the next element in the list. Array has the initial address of the first element in the list only.

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
ZG: Each class overrides Draw method of the parent class except Rectangle which uses new keyword which creates new method with same name Draw and do not override the method from the parent class. There is an array of DrawingObject instances which means that the Rectangle instance will be casted to DrawingObject instance and calling Draw method will return "I am a drawing object." from the DrawingObject class.
I expect result to be :
"I am a Triangle."
"I am a Circle."
"I am a drawing object."
"I am a drawing object."


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
ZG: I am not sure if that is the purpose of the task but I would not use using for HttpClient. I would use a single instance of the httpClient instead of creating and disposing it for every request. Also before .NET 5 result.Content might be null (in .NET 5 Content is always an object but could be empty) which will cause an NullPointerException. HttpClient class has a method GetStringAsync which could be used in this case instead of getting result and then get the content from it.

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
ZG: First method uses async programming. When the code reach the first await line the execution thread will execute it and will be released and execution of that method will be paused at this line until OpenAsync method returns. When that happen another free thread will continue execution of the current method and when it reach second await method it will execute it and the method will be paused again and current thread released. When ExecuteScalarAsync method returns another available thread will continue execution of the current method.

Second method is synchronious and the execution thread will not be relased when the code reach the line var task = con.OpenAsync(); because task is just a reference to a Task object which will be executed when code reach task.Result but the current thread won't be released but it will just wait for the result. Same behaviour will happen when the code reach second async Task ExecuteScalarAsync.

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

ZG: 0, 2
