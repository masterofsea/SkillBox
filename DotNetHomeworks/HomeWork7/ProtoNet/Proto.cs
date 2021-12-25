using System;
using System.Collections.Generic;
using System.Threading;

namespace ProtoNet
{
    public static class Proto
    {
        public static dynamic Undefined => UndefinedType.Instance;

        private static readonly ThreadLocal<Stack<ProtoObject>> Context = new ThreadLocal<Stack<ProtoObject>>(() => new Stack<ProtoObject>());

        public static dynamic CurrentContext => Context.Value.Count > 0 ? Context.Value.Peek() : null;

        internal static IDisposable CreateContext(ProtoObject context)
        {
            Context.Value.Push(context);
            return new ProtoContext(() => Context.Value.Pop());
        }

        private class ProtoContext : IDisposable
        {
            private readonly Action _callback;

            public ProtoContext(Action callback)
            {
                _callback = callback;
            }

            public void Dispose()
            {
                _callback();
            }
        }
    }


    public sealed class UndefinedType
    {
        private static volatile UndefinedType _instance;
        private static readonly object SyncRoot = new object();

        public static UndefinedType Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    _instance ??= new UndefinedType();
                }

                return _instance;
            }
        }

        public override string ToString()
        {
            return "undefined";
        }
    }
}