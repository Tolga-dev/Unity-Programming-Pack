# Observer Pattern

* it creates a 'one to many' dependency between objects. As one object changes its state the other objects respond automatically.

* The Observer Pattern is a behavioral design pattern where an object, known as the subject, maintains a list of its dependents, called observers, and notifies them of any state changes, usually by calling one of their methods.

# Here's how it works:

## Subject: 
The subject is the main component that holds the state and controls it. It provides methods to register, unregister, and notify observers.

## Observer:
Observers are the dependent components that are interested in the state changes of the subject. They register themselves with the subject to receive notifications.

The Observer Pattern is a design pattern that allows objects to communicate with each other without tight coupling. Here's a breakdown of how to implement it in Unity C#:

### Components:

* Subject (or Publisher): The object that maintains a list of dependent observers and notifies them of changes.
* Observer (or Subscriber): The object interested in receiving notifications from the subject. It implements an interface defining the update method.
* Interface: Defines the update method that observers implement to receive notifications.

### Benefits:
Decouples objects: The subject doesn't need to know specific details about its observers.
Promotes loose coupling and easier maintenance.
Allows for adding or removing observers dynamically.

Basic Example:

1. Define an Observer Interface:

```csharp
public interface IObserver
{
void OnNotify(object data); // Update method for observers
}
```
  
```csharp
public class Subject : MonoBehaviour
{
private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(object data)
    {
        foreach (IObserver observer in observers)
        {
            observer.OnNotify(data);
        }
    }

    // Subject logic that triggers Notify() when state changes
}
```

```csharp
public class PlayerHealthDisplay : MonoBehaviour, IObserver
{
public Text healthText;

    void Start()
    {
        // Find the subject (e.g., through GetComponent or reference)
        Subject subject = FindObjectOfType<Subject>();
        subject.RegisterObserver(this); // Register as an observer
    }

    public void OnNotify(object data)
    {
        if (data is int health) // Check if data is player health
        {
            healthText.text = $"Health: {health}";
        }
    }
}
```

### Event Handling 

```csharp
/// <summary><para>Represents the method that will handle an event that has no event data.</para></summary>
/// <param name="sender">The object that raised the event.</param>
public delegate void EventHandler(object sender, EventArgs e);
```

