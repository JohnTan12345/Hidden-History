# Hidden History

## Description
The app name is known as Hidden History. A mobile AR treasure hunting game that is inspired by unearthing paintings made by Singaporean artists from the past. The artifacts/elements are items that contribute to the making of an artwork. The idea behind this project is to promote Singapore’s art heritage by tapping into AR and gamifying the experience to show more than just images and text. Making it educational, informative, and interactive.

The UI was inspired by AFK mobile games that usually require auto-farming. Icons used are straightforward & not ambiguous. So, users can pick up the purpose of the button on the get-go.

Most of the paintings featured in our AR game included famous Singaporean artists like Georgette Chen, who was one of Singapore's pioneer artists who established the Nanyang style of painting, and Liu Kang, who made significant contributions to this specific direction of style.

We decided to gamify the experience to include elements that can be dug up. Each element can be considered an artifact as they are commonly associated with the makings of a painting.

For example, a painting usually requires different forms of mediums. We decided on using 3D models of a colour palette, crayon box, puzzle piece, and paintbrushes. That was how the ideation of the buried artifacts came about. What kind of treasure hunting game doesn’t involve digging? 

The paintings and their descriptive contents were sourced from https://www.roots.gov.sg/

## Target Audience
The target audience for this application is made for everyone. From young teens to elderly Singaporeans. Anyone keen to learn more about Singapore’s Art heritage and their creators can use the app. International tourists included.

## Displayed Content
User authentication is a simple email & password input field with a button to log in or sign up. Proper error-handling messages are also put in place. Users can switch between login or sign up by clicking the text prompt below the login/sign up button.

“X” images that are found on the floor can be scanned to trigger the game’s digging mechanic. 
There are 3D dirt pile models that spawn when the image is scanned. After successful digging, it’ll be replaced with the actual 3D artifact. This appears briefly before being removed and added to the artifact panel UI.

Artifact panel UI is made up of different parts, mostly text and image components. The artifacts that are dug up contribute to the revealing of a hidden painting. Players can read the painting title & its description.

Players will be greeted with shadow silhouettes of the artifacts they are meant to dig up from the alternate X images found during game progression. Collect an artifact, and the shadow is replaced with the visible 2D sprite of the artifact dug up. 

The actual painting will only be revealed once all the necessary artifacts have been dug up. The main UI interface also shows the player the main objective of the game while keeping track of the total number of elements they have collected.

Elements are equivalent to the artifacts found; it’s just a different naming term.
The tutorial provides players with text & image info guides.


## Key Game Controls:
### Setup & Authentication
1.	Start the App: When the app is launched, the player is automatically directed to the Login/Signup Page.
2.	Authenticate: The player must complete the user authentication process (either logging into an existing account or signing up for a new one).
3.	Start Game Prompt: Upon successful login, the player is redirected to a greeting prompt where they can officially begin the game.

### Locating and Scanning Artifacts
1.	Enter AR Environment: The player starts the game and enters the live Augmented Reality (AR) environment.
2.	Locate 'X' Marker: Walk around and find one of the six different types of 'X' images anywhere on the ground.
3.	Scan and Reveal Mound: Walk over and scan the 'X' image with the phone camera to reveal a 3D hidden dirt mound overlayed onto the environment.

### Digging and Collecting Artifacts
1.	Initiate Dig: A shovel button will pop up on the screen when pointing at the ‘X’ image on the floor.
2.	Dig for Artifact: Repeatedly tap the shovel button to begin digging up the artifact concealed within the mound.
3.	Receive Feedback: The player will receive visual and audio feedback upon the successful completion of the dig and the retrieval of the artifact.
4.	Continue Exploration: The player can choose to find a different 'X' marker or stick with the same one as they continue their progress.

### Tracking Progress and Unlocking Paintings
1.	View Artifact Panel: At any time, tap the magnifying glass button on the bottom right of the screen to open the Artifact Panel page.
2.	 Collection for a Page: Each of the six pages in the panel requires a total of 4 unique artifacts to be uncovered.
3.	 Reveal Painting: Once four artifacts are collected for a specific page, a button to reveal the hidden painting will become accessible.
4.	 Game Completion: To complete the entire game, the player must dig up all artifacts and reveal all six hidden paintings.

### Support and Exit Options
1. View Tutorial: Tap the tutorial button in the top right-hand corner of the screen at any point for guidance.
2. Pause Game: Click the home button on the bottom left-hand of the screen to return to the start game prompt if a break is needed.

### Completing the game
1. Re-View Paintings: Choose to view the unlocked paintings again.
2. Restart Game: Choose to reset all progress and restart the game.
3. Exit: Choose to exit the game and return to the main menu.

## Platform & Hardware
- For optimal use, players are required to have either an Android or IOS phone to run the app. The app is also supported on Windows PC desktops. However, we recommend that players use their mobile devices for the best player experience.

## Game Solution
- Just keep digging all 6 images until you dug up all 21 artifacts to end the game.

# Limitations & Bugs
- If the player scans over two images at the same time, the object from the first image would no longer disappear. (game will run as per normal, just that the dirt pile will be there until it is eventually dug up)
- There is a slight flickering effect when you open the artifact page for the first time
- In the mobile build, when you use the AR feature, it will constantly spawn multiple objects on the painting without disappearing even though the image stays the same. This is due to the AR feature sometimes mistaking the image for another image

# References & Credits
All icons used in the application are supplied by Icons8.
Icons used: White Outlined
- https://icons8.com/icon/83326/home
- https://icons8.com/icon/eHvteXE5z1zQ/spade 
- https://icons8.com/icon/20319/user-manual 
- https://icons8.com/icon/59878/search 
## Images:
Scroll backdrop: https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSDi3AduYHkZhuxyUcs6UXCtZXYv0Nl5uiniQ&s
The 2D sprites used to represent the visible artifacts/elements were created with the help of Adobe Firefly AI Image Gen.
Paintings & their respective contents:
- https://www.roots.gov.sg/Collection-Landing/listing/1026159  [Watermelons] 
- https://www.roots.gov.sg/Collection-Landing/listing/1029439 [Sunflower & Buddha]
- https://www.roots.gov.sg/Collection-Landing/listing/1322068 [See Hear Speak]
- https://www.roots.gov.sg/Collection-Landing/listing/1034154 [East Coast Vendor]
- https://www.roots.gov.sg/Collection-Landing/listing/1027839 [Family Portrait]
- https://www.roots.gov.sg/Collection-Landing/listing/1032869 [Nepal Landscape]
Some Images were edited with Remove Background (Remove Background from Image for Free – remove.bg) & resized in Adobe Illustrator.
## 3D Models: Taken from Sketchfab
- Melon: "3D Melon - 01" (https://skfb.ly/o7O7R) by SanForge Studio is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
- Palette: "Suitcase and Paint Palett" (https://skfb.ly/on7ML) by P_4_N_D_A is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
- Crayons: "CC0 - Crayons" (https://skfb.ly/opBAZ) by plaggy is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
- Paintbrush: "Paintbrush" (https://skfb.ly/oEGRv) by emiliajasova is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
- PuzzlePiece: "M_ Puzzle_ Piece_ Floor" (https://skfb.ly/oFnn6) by hangmandigital is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
- Dirt Pile: "Photoscan Low-poly Construction Sand Pile" (https://skfb.ly/oAPz6) by Martin Topolski is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/). 
## SFX: Taken from Epidemic Sound, Sound Effect Tracks
- Game Over: Synth, Muted, End, Complete
- Dig Completed: Success, Access Granted, Reward 03
- Digging: Shovel, Spade, Dig into Ground x6
- Google Gemini for gen ai Images used in the Hidden History Website.

## Original / Custom Assets:
All UI in the app is made from scratch in Unity.
The “X” Images, app logo, and artifact shadow silhouettes were made from scratch with Adobe Illustrator.
The painting frame was modelled from scratch with Maya and textured with 3D Substance Painter; textures were sourced from the Substance community.
