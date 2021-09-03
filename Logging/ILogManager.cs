using System;

namespace RogueGame.Logging
{
    public interface ILogManager
    {
        void EventLog(string message);
        void RegisterEventListener(Action<string> listener);
        void UnregisterEventListener(Action<string> listener);
    }
}