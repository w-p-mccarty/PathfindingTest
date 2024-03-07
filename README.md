# Pathfinding Test
#### Author: William McCarty

---


## Project Description

---
In this Unity project, users can interactively click on different areas within a 3D space defined by a plane. The program 
seamlessly executes an A* pathfinding algorithm, ensuring efficient navigation through the environment. Additionally, it 
includes path smoothing functionality and enables objects to be rearranged within the editor during runtime, allowing for 
dynamic updates to the pathfinding process.

## Pathfinding Implementation

---
The system employs the A* algorithm, beginning from the start node. We iterate through the open list until it's empty or 
we reach the end node. For each element retrieved from the open list, ordered by lowest cost, we examine the surrounding
nodes using the grid coordinates of the 2D grid. These neighboring nodes are then added to the open list with their initial 
costs calculated by summing the distance to the end goal and to the start point. Furthermore, we set the parent of the 
discovered nodes to be the coordinates of the node currently being processed. This parent-child relationship enables us to
trace back to the start node once the process is completed. Subsequently, we remove the processed node from the open list
and add it to the closed list.

Once we either reach the end node or exhaust all options in the open list, we initiate a backtracking process from the end
node, following the parent coordinates until we reach the start node. Upon reaching the start node, we reverse the path
to obtain the valid walkable path. This ensures that the path is correctly traced and ready for use within the application.


Given the grid-like structure of the NavGrid, A* appeared as the ideal solution for pathfinding. Its suitability lies not 
only in efficiently navigating grids but also in its flexibility to accommodate additional costs for various considerations.
This enables us to assign weights to factors influencing the desirability of traveling through certain areas, providing
a more nuanced and informed pathfinding solution.


## 2D Grid

---
Upon the initial pathfinding attempt, the 2D grid is dynamically generated. Once created, the grid remains static unless
the application is restarted. You have the flexibility to adjust the grid dimensions by selecting "NavGridPlane" in the
editor and modifying the "Grid Size X" and "Grid Size Y" parameters in the inspector. Additionally, you can increase the 
density of nodes within the navigation grid by adjusting the "Node Size" property, providing finer granularity for pathfinding
precision.


After the initial grid construction, we continually update the nodes based on the current scene state. Initially, we identify
the player's location and designate that node as the start point. Subsequently, we locate the point in the world space where
the user clicked on the NavGrid and designate the corresponding node as the end point. Before commencing pathfinding, we
iterate through each node to determine if it intersects with any game objects assigned to a layer mask marked as "Blocked".
Nodes overlapping with such objects are flagged as "Blocked," while the rest remain "Walkable." This dynamic updating 
ensures that the grid accurately reflects the current scene state, guaranteeing the player's movement along the intended path.


In the initial project setup, assigning coordinates to each node emerged as the most logical approach. These coordinates
serve as a reference point for locating parent nodes during pathfinding. However, for future iterations, optimizing this
process by replacing coordinates with direct references to parent nodes could reduce lookup costs and enhance efficiency.

Additionally, constructing the NavGrid just once was a pragmatic decision to avoid repetitive grid reconstruction. Yet, 
for enhanced flexibility, implementing an update mechanism to dynamically modify the NavGrid in real-time could be beneficial
in future iterations.

Lastly, enabling the ability to reprocess node types for each pathfinding call allows for dynamic object rearrangement
and provides real-time feedback. This feature facilitates observing changes in the environment and reflects them
instantly in the pathfinding process, enhancing overall responsiveness and adaptability.

## Path Smoothing

---

Once the pathfinding algorithm returns the set of nodes for the player to follow, we feed these points into a smoothing
function that employs the Catmull-Rom Equation. This function utilizes the center positions of the nodes as control points,
generating an interpolating spline that forms a smooth path for the player to traverse. The degree of smoothness in the
interpolation is determined by the number of smooth segments between each control point. This parameter can be adjusted
by selecting the player object in the editor and modifying the "Smooth Segments" property.


In exploring various smoothing techniques, the Catmull-Rom Equation emerged as the most suitable choice for achieving the
desired outcomes. By incorporating this technique and ensuring the inclusion of the center node, the likelihood of object 
clipping during traversal is significantly reduced. However, for future enhancements, it could be worthwhile to explore 
alternative, more fluid smoothing methods to streamline the process and expedite calculations. By investigating such
techniques, we can further optimize the path-smoothing process while maintaining or even improving the quality of the
generated paths.


## Running Application


---
If you are wanting to visualize and manipulate the path itself please follow (A) other withwise follow (B).


#### Option A
1. Open Unity Editor to Game Scene

2. Hit Play in Editor
3. If you want to see path player is traversing when clicked, select Player gameobject in scene and in inspector toggle debug.
4. If you want to see the A* path, open nodes, closed nodes, and blocked nodes, select NavGridPlane.  In inspector toggle
debug and either Show NavGrid Nodes As Cubes or Wired.  Once you click a location the nodes will be color coded: White = 
Walkable(not discovered); Black = Blocked(not walkable); green = in open list; magenta = in closed list; cyan = path found; 
red = end node; blue = start node.
5. While the game is running you are free to move any obstacles in the Obstacles folder of the hierarchy in the scene to 
adjust the pathfinding behavior.
6. If you are wanting to adjust the grid and/or the Node size you need to make modifications on the NavGridPlane object
before starting any pathfinding.  The grid is constructed on the first time a path is requested.


#### Option B
1. I created a .exe you can run to simply play around with the default settings.
2. The build is located in the Build/Text.exe
