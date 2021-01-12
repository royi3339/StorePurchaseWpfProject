using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace BE
{
    [Serializable]
    public class DuplicateIdException : Exception
    {
        private string v;
        private int hostingUnitKey;

        public DuplicateIdException()
        {
        }

        public DuplicateIdException(string message) : base(message)
        {
        }

        public DuplicateIdException(string v, int hostingUnitKey)
        {
            this.v = v;
            this.hostingUnitKey = hostingUnitKey;
        }

        public DuplicateIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class MissingIdException : Exception
    {
        private string v;
        private int Key;

        public MissingIdException()
        {
        }

        public MissingIdException(string message) : base(message)
        {
        }

        public MissingIdException(string v, int Key)
        {
            this.v = v;
            this.Key = Key;
        }

        public MissingIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MissingIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class NoDaysException : Exception
    {
        public NoDaysException()
        {
        }

        public NoDaysException(string message) : base(message)
        {
        }

        public NoDaysException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoDaysException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


    [Serializable]
    public class openOrdersException : Exception
    {
        public openOrdersException()
        {
        }

        public openOrdersException(string message) : base(message)
        {
        }

        public openOrdersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected openOrdersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class textExeption : Exception
    {
        public textExeption()
        {
        }

        public textExeption(string message) : base(message)
        {

        }

        public textExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected textExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class yearExeption : Exception
    {
        public yearExeption()
        {
        }

        public yearExeption(string message) : base(message)
        {
        }

        public yearExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected yearExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class wrongPasswordException : Exception
    {
        public wrongPasswordException()
        {
        }

        public wrongPasswordException(string message) : base(message)
        {
        }

        public wrongPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected wrongPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class orderStatusException : Exception
    {
        public orderStatusException()
        {
        }

        public orderStatusException(string message) : base(message)
        {
        }

        public orderStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected orderStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}