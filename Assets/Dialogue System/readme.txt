To use Dialogue System.

1. Create actors 
	Go to actors folder --> Right Click --> Create --> Dialogue --> Actor

2. Choose a Name and Color for the Actor, audio settings are optional and will default if empty

3. Create Dialogue
	Go to Conversatiosn folder --> Right Click --> Create --> Dialogue --> Conversation

4. Add elements to the array, selecting an actor for each element and creating text.

5. Nest the DialogueTrigger prefab on a character and assign the dialogue asset. Add a collider to the game object and check isTrigger. Dialogue will trigger when the player enters the collider.