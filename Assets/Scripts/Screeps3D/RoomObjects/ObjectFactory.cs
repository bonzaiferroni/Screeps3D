namespace Screeps3D {
    public class ObjectFactory {
        public RoomObject Get(string type) {
            
            switch (type) {
                case (Constants.TYPE_CREEP):
                    return new Creep();
                case (Constants.TYPE_EXTENSION):
                    return new Extension();
                case (Constants.TYPE_SPAWN):
                    return new Spawn();
                case (Constants.TYPE_STORAGE):
                    return new Storage();
                case (Constants.TYPE_TOWER):
                    return new Tower();
                case (Constants.TYPE_CONTROLLER):
                    return new Controller();
                case (Constants.TYPE_TERMINAL):
                    return new Terminal();
                case (Constants.TYPE_CONTAINER):
                    return new Container();
                case (Constants.TYPE_LINK):
                    return new Link();
                case (Constants.TYPE_RAMPART):
                    return new Rampart();
                default:
                    return new RoomObject();
            }
        }
    }
}