using UnityEngine;

namespace GJ2022.Global.PubSub
{
    public class EventMessages 
    {
   
    }
    public struct OrbObtainedMessage
    {
        public string OrbId { get; private set; }
        public OrbObtainedMessage(string orbId)
        {
            OrbId = orbId;
        }
    }
    public struct PlayerNearbyMessage
    {
        public string OrbId { get; private set; }
        public PlayerNearbyMessage(string orbId)
        {
            OrbId = orbId;
        }
    }

    public struct PlayerLeaveMessage
    {
        public string OrbId { get; private set; }
        public PlayerLeaveMessage(string orbId)
        {
            OrbId = orbId;
        }
    }

}
