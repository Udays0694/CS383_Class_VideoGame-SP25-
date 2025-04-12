//For my test plan, it will consist of a few basic questions: 
  //  1.What is the minimum value of this variable that can be used, and what occurs? 
  //  2. What is the maximum value of this variable that can be used, and what occurs? 
  //  3. What effect will these have with enemies or other interactions within the game? 
//Based on these simple premises,we can use this to create the first few tests, being: Testing Health, Speed, Damage, and Player instances. 
//I chose some of these specifically since they interact with all other instances of a teammate’s actions. 
//Meaning that whatever they worked on usually affected some part of the player and its values. 
//Knowing this, when testing the health we found that the health would drop below 0, and if not on zero exactly, would not trigger the Game Over screen, 
//rather it would let the player continue, regardless of if they were negative or not. If healing was acquired and the Player’s health did go to 0 exactly, the game over screen would occur.