using System;
using System.Runtime.Serialization;

namespace RoofStock.Data.Util
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }       
    }
}