## Events and Delegates in Unity


### Delegates

passing a function as a parameter.

```csharp
public delegate void Foo(string msh);
```

#### Function Delegates

it has func and action as builtin, u can define custom delegates manually in most cases;

```csharp
public delegate TRsukt Func<in T, out TResult>(T arg);
```

#### Actions

action, is a delegate type. An action type delegate is the same as func delegate, 
except it does not return a value.




