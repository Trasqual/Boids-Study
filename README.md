# Boids Study with Unity (Flocking, Runner Platform, RTS Movement)
 
This project includes 3 branches for 3 different implementations of boids in Unity. This study took a few days, it started with my research for a project at work since I didn't have much time the code leaves alot to desire. However I believe this will help people understand the basis of how boids work and how useful they are.


### Main Branch: Flocking

This is the basis for the other branches where I used the information I gathered to create the foundation of a modular boid system. This branch includes the general behaviours of the boids such as Cohesion, Seperation  and Alignment.

When you play the project, a certain number of boids are spawned going in random directions. Their movement is limited buy a box and they disappear and spawn from the other side of the box if they hit a limit. After a while we start to see the boids start forming different sizes of groups and they start to align with eachother.

#### Click the Image Below to Watch the Showcase Video:
[![IMAGE ALT TEXT](http://img.youtube.com/vi/YzHbQMlTONI/0.jpg)](http://www.youtube.com/watch?v=YzHbQMlTONI "Showcase Video")

### Runner-Test Branch: Runner Platform for Hyper Casual Games

This was why I started researching boids. I needed to make a hyper-casual runner project for work that included multiple playered controlled characters running on a platform while avoiding obstacles and jumping over gaps. Once I figured the basis in the main branch I tought it would be super easy to adjust the scripts to fit a runner game... I have never been so wrong in my life. After fiddling for a while I realised the general steering behaviour had to be seperated from the user input because in a runner platform you need your characters to move forward constantly while fluently moving right or left with player input. However the steering behaviour normalizes and then limits the input to a max steering value thus the seperation of input and steering. I also had to add a jumping behaviour, gravity, avoidance and grouping. I also added a noise script so each character moved alittle differently instead of a robot like behaviour where each character moved exactly the same. AT work I ended up using a little different approach than this.

#### Click the Image Below to Watch the Showcase Video:
[![IMAGE ALT TEXT](http://img.youtube.com/vi/FXnF902Gz6s/0.jpg)](http://www.youtube.com/watch?v=FXnF902Gz6s "Showcase Video")

### RTS-Test Branch: RTS Style Group Movement with Mouse Click

After finishing the runner tests I took a break but I had this crippling feeling in my head that I had to try boids in an rts style so I started working on it and I am quite proud of the results. I already had the main components from the runner test so it only took me a few hours to tweek some of the code, do a little refactoring and add a basic mouse input movement. I also added a small grid system to the boid group movement so when the boids reach the target they form a lose rectangular formation.
Results were much better than what I expected. The boids don't have any pathfinding algorithm, nor do they have rigidbodies so sometimes they can go inside a ramp etc. I think you could easily use rb.Moveposition instead of transform movement and get quite nice results so it might be worth a shot. Unfortunately I currently don't want to work more on this project so I won't be trying it just yet.

#### Click the Image Below to Watch the Showcase Video:
[![IMAGE ALT TEXT](http://img.youtube.com/vi/ya1TOOiYJwo/0.jpg)](http://www.youtube.com/watch?v=ya1TOOiYJwo "Showcase Video")

## Logic Behind the Systems:

* Managers: Basically we have a BoidManager which has a BoidSpawner and a BoidGroupMovement. The Boid Manager is responsible for keeping track of the boids in a list. BoidGroupMovement is responsible of making the boids move as a group because everyboid needs to know where the other boids are so they can use their cohesion, seperation etc algorithms.

* Scripts on the Boids:
-Boid: This is the actual script that moves the boid and so it has the steering method too.

-BoidHelper: This is the base class for general boid behaviours. (BoidBehaviour might have been a better name but.. oh well)

-BoidCohesion: This is where a boid tries to steer towards the center of nearby boids.

-BoidSeperation: This is where a boid tries to steer a certain distance away from enarby boids.

-BoidAlignment: This is where a boid tries to steer to the average direction of nearby boids.

-BoidAvoidance: This is where a boid makes a spherecast to steer away from the obstacles infront. 

-BoidNoise: This adds a little random noise to each boid so they don't move exactly the same.

-BoidGrouper: This slightly helps boids to stay grouped together.

-BoidSettings: This has all the settings including weights of all behaviour. (Generally it seems that keeping Cohesion and Seperation weights same and having a high avoidance value gives the best result).

Hope this helps, Enjoy!
