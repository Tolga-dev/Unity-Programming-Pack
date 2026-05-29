## Solid

* Single Responsibility Principle
* Open-Closed Principle
* Liskov Substitution Principle
* Interface Segregation Principle
* Dependency Inversion Principle


* E1 folder is SRP



#### SRP
If you have a class that has more than one responsibilities that are not related with each other,
then it violates first principle.

```csharp
public class Object
{
    public int h;
    public int w;
}

public class CalculateArea
{
    public int CalculateAreaObject(Object j)
    {
        return j.h * j.w;
    }   
    public int Save(){}   
}
 
```
instead do this:

```csharp

public class Object
{
    public int h;
    public int w;
}

public class CalculateArea
{
    public int CalculateAreaObject(Object j)
    {
        return j.h * j.w;
    }    
}
public class SaveAreaObject
{
    public int Save(){}    
}

```

#### OC principle

Entities should be opened for extension but closed for modification



```csharp
public class Object
{
    public int h;
    public int w;
}

public class CalculateArea
{
    public int CalculateRectange(Object j)
    {
        return j.h * j.w;
    }    
    public int CalculateSquare(Object m)
    {
        return m.h * m.w;
    }    
}

```
instead. do this

```csharp

public interface Object
{
    public int CalculateArea();
}

public class Rectange
{
    public int h;
    public int w;
    public int CalculateArea()
    {
        return h * w;
    }    
}

public class Square
{
    public int h;
    public int w;
    public int CalculateArea()
    {
        return h * w;
    }      
}
```

### Liskov Substitution Principle

use inherit for all properties and behavior

```csharp

public interface Object
{
    public int CalculateArea();
}

public class CalculareAreaThis
{
    public int h;
    public int w;
    public int CalculateArea();   
}
public class Rectange : CalculareAreaThis
{
    public int CalculateArea()
    {
        return h * w;
    }      
}

public class Triangle :CalculareAreaThis
{
    public int CalculateArea()
    {
        return h * w/2;
    }           
}
```

### Interface Segregation 
Separate more to interfaces

```csharp

public interface Object
{
    public int h;
    public int w;
    public int CalculateArea();
}

public interface DeadlyWeapon : Object
{
    public int Deadly();
}

public interface UnDeadlyWeapon : Object
{
    public int UnDeadlyWeapon();
}

public class Rectange : UnDeadlyWeapon
{
    public int CalculateArea()
    {
        return h * w;
    }      
}
public class Triangle :DeadlyWeapon
{
    public int CalculateArea()
    {
        return h * w/2;
    }           
}
```

Check E1/TEST1 scene

![alt text](E1/Solid1.png)
