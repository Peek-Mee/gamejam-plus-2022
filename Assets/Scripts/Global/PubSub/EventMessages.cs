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
    public struct PlayerDialogueMessage
    {
        public int Id { get; private set; }
        public float DisposeAfter { get; private set; }
        public PlayerDialogueMessage(int id, float disposeAfter)
        {
            Id = id;
            DisposeAfter = disposeAfter;
        }
    }


    public struct PlayerOutDialogueMessage
    {

    }


}
