# Project setup
- Git pull
- go to Assets/Libs/readme.md
  - download all packages from list from unity asset store through unity editor -> package manager
  - move all packages to Assets/Libs/ folder (Unity imports packages as folders into Assets/ by default)
# Project configuration
## Client only configuration setup
- File -> build settings
  - Select Windows/mac/linux platform
  - Add scenes to build
    - Login
    - Barrack
    - Tavern
    - Town
    - Training Yard
- For each added scene, you can find it Assets/scenes
  - Open scene in editor
  - Find GameService object on the scene in hierarchy
    - Make sure that it has right services configuration in the inspector, it should be %SceneName%**Mock**Config
- Build and run solution
## Client local configuration setup
- File -> build settings
  - Select Windows/mac/linux platform
  - Add scenes to build
    - Login
    - Barrack
    - Tavern
    - Town
    - Training Yard
- For each added scene, you can find it Assets/scenes
  - Open scene in editor
  - Find GameService object on the scene in hierarchy
    - Make sure that it has right services configuration in the inspector, it should be %SceneName%**Local**Config
- Build and run solution
## Training server local configuration setup
- File -> build settings
  - Select dedicated platform
  - Add scenes to build
    - Training Yard
- For each added scene, you can find it Assets/scenes
  - Open scene in editor
  - Find GameService object on the scene in hierarchy
    - Make sure that it has right services configuration in the inspector, it should be %SceneName%**Local**Config
- Build and run solution
