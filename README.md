# Screeps3D
A 3D client for the MMORTS Screeps.com

## Goal 
To build a 3D client for Screeps.

## Progress
At the moment everything is the simplest working solution. Only a single room can be loaded per session (close and restart to load a new room). Shard0 is the only working shard.

I think it has a fairly solid foundation so that if anyone wants to contribute to it, it is at a good point. There is a websocket/http client that could be fleshed out more but has all the basic functionality. There is a solid system for rendering rooms/objects. At the moment the project is organized into two systems:

* ScreepsAPI - HTTP/Websocket client for communicating with the server
* Screeps3D - Login, WorldView, RoomView, etc.

It would be ideal to keep these two separate, so that the ScreepsAPI can be exported as a package for use in other screeps/unity3D projects. 

Here are the major areas that I'd like to tackle next: 
* Get it working with private servers
* Gameplay options like hotkeys, camera controls, console colors, etc.
* Finish model set for room objects (Missing nuker, links, and terminal)
* Rendering roads
* Rendering creeps (new model with player icon downloaded and assigned as texture)
* Rendering creep, tower, link, etc. actions (particle systems are an excellent way to make these visually appealing)
* Creep Say (I'm imagining a floating text that appears above their heads and drifts up to eventually disappear)
* Figure out the best way to subscribe/unsubscribe from rooms, fog of war, etc.
