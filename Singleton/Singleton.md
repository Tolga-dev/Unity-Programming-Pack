## Singleton

The Singleton Design Pattern is a creational pattern that ensures a class has only
one instance and provides a global access point to it. It can be 
useful in some scenarios.

### Benefits:
* Ensures only one instance of a specific class exists.
* Provides a central access point for other scripts to interact with the GameManager.
### Drawbacks:

* Creates tight coupling between scripts that rely on the Singleton.
* Makes unit testing more difficult because the instance is not easily mockable.
* Introduces a global state that can be hard to manage in complex projects.
### Alternatives:

* E1
  * Just singleton
* E2
  * Singleton of component
* E3
  * Singleton of generic
```csharp
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    Debug.LogError("GameManager instance not found!");
                }
            }
            return _instance;
        }
    }

    // Other game manager related properties and methods
}
```