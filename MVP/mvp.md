


* E1
  * Health = Model
  * HealthBar = View
  * HealthPresenter = Presenter

## Components:


* Model: Represents the data of your application. It holds the core functionality and doesn't contain any game object references or Unity-specific logic.
* View: Handles the visual representation of the data. It uses Unity components like UI elements or prefabs to display information received from the Presenter.
* Presenter: The mediator between Model and View. It retrieves data from the Model, formats it for the View, and handles user input from the View to update the Model. It doesn't contain any game object references or UI logic.





