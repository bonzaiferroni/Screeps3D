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
                    return new Extension();
                default:
                    return new RoomObject();
            }
        }
    }
}