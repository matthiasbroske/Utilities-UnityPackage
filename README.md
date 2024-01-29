# Utilities

## About
A collection of basic utilities to assist with game development in Unity.

## Installation
1. In Unity, open ```Window -> Package Manager```. 
2. Click the ```+``` button
3. Select ```Add package from git URL```
4. Paste ```git@github.com:matthiasbroske/Utilities-UnityPackage.git``` for the latest package
    - If you want to access a particular release or branch, you can append ```#<tag or branch>``` at the end, e.g. ```git@github.com:matthiasbroske/Utilities-UnityPackage.git#main```

## Contents

### Editor Scripts
- [`ProjectLayoutManager`](Editor/Scripts/ProjectLayoutManager.cs): Editor script for automatically generating the folder layout for a new project, as well as adding empty folders to source control.
  - Select ```Assets -> Project Layout -> 1. Create Default Folders``` and follow the prompts to generate your projects initial file structure.
  - Select ```Assets -> Project Layout -> 2. Keep Empty Folders``` to ensure any empty folders are trackable by your source control. 
  - Select ```Assets -> Project Layout -> 3. Clean Kept Folders``` to remove ```.keep``` files from non-empty folders.

### Runtime Scripts
- [`PersistentVariableReference`](Runtime/Scripts/Utilities/Persistent%20Variables/PersistentVariableReference.cs): A ScriptableObject reference to a variable saved using PlayerPrefs, perfect for persistently saving settings and the like. Supports `float`, `int`, `string` and `bool` types.
  - [`PersistentFloatSliderInitializer`](Runtime/Scripts/Utilities/Persistent%20Variables/PersistentFloatSliderInitializer.cs): Initializes a slider to the value of a `PersistentFloatReference` and links them such that any changes to the slider automatically update the persistent variable and vice-versa.
  - [`PersistentBoolToggleInitializer`](Runtime/Scripts/Utilities/Persistent%20Variables/PersistentBoolToggleInitializer.cs): Initializes a toggle to the value of a `PersistentBoolReference` and links them such that any changes to the toggle automatically update the persistent variable and vice-versa.
- [`Easings`](Runtime/Scripts/Utilities/Easings.cs): A collection of commonly used easing functions.

