namespace Screeps3D.RoomObjects
{
    public class ObjectFactory
    {
        public RoomObject Get(string type)
        {
            switch (type)
            {
                case Constants.TYPE_CREEP:
                    return new Creep();
                case Constants.TYPE_EXTENSION:
                    return new Extension();
                case Constants.TYPE_SPAWN:
                    return new Spawn();
                case Constants.TYPE_STORAGE:
                    return new Storage();
                case Constants.TYPE_TOWER:
                    return new Tower();
                case Constants.TYPE_CONTROLLER:
                    return new Controller();
                case Constants.TYPE_TERMINAL:
                    return new Terminal();
                case Constants.TYPE_CONTAINER:
                    return new Container();
                case Constants.TYPE_LINK:
                    return new Link();
                case Constants.TYPE_RAMPART:
                    return new Rampart();
                case Constants.TYPE_CONSTRUCTION:
                    return new ConstructionSite();
                case Constants.TYPE_LAB:
                    return new Lab();
                case Constants.TYPE_CONSTRUCTED_WALL:
                    return new ConstructedWall();
                case Constants.TYPE_NUKER:
                    return new Nuker();
                case Constants.TYPE_MINERAL:
                    return new Mineral();
                case Constants.TYPE_POWER_SPAWN:
                    return new PowerSpawn();
                case Constants.TYPE_SOURCE:
                    return new Source();
                default:
                    return new RoomObject();
            }
        }
    }
}