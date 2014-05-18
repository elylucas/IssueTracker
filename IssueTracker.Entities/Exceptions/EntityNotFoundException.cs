using System;

namespace IssueTracker.Entities.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
            
        }
    }
}