using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System;

namespace Vagabond.API.Exceptions
{
    public class DestinationNotFoundException : Exception
    {
        public DestinationNotFoundException(string message) : base(message) 
        {
        }
    }
}
