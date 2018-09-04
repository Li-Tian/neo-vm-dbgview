using System;
using System.Collections.Generic;
using NoDbgViewTR;

namespace Neo.VM
{
    public class InteropService
    {
        private Dictionary<string, Func<ExecutionEngine, bool>> dictionary = new Dictionary<string, Func<ExecutionEngine, bool>>();

        public InteropService()
        {
            TR.Enter();
            Register("System.ExecutionEngine.GetScriptContainer", GetScriptContainer);
            Register("System.ExecutionEngine.GetExecutingScriptHash", GetExecutingScriptHash);
            Register("System.ExecutionEngine.GetCallingScriptHash", GetCallingScriptHash);
            Register("System.ExecutionEngine.GetEntryScriptHash", GetEntryScriptHash);
            TR.Exit();
        }

        protected void Register(string method, Func<ExecutionEngine, bool> handler)
        {
            TR.Enter();
            dictionary[method] = handler;
            TR.Exit();
        }

        internal bool Invoke(string method, ExecutionEngine engine)
        {
            TR.Enter();
            if (!dictionary.TryGetValue(method, out Func<ExecutionEngine, bool> func)) return TR.Exit(false);
            return TR.Exit(func(engine));
        }

        private static bool GetScriptContainer(ExecutionEngine engine)
        {
            TR.Enter();
            engine.EvaluationStack.Push(StackItem.FromInterface(engine.ScriptContainer));
            return TR.Exit(true);
        }

        private static bool GetExecutingScriptHash(ExecutionEngine engine)
        {
            TR.Enter();
            engine.EvaluationStack.Push(engine.CurrentContext.ScriptHash);
            return TR.Exit(true);
        }

        private static bool GetCallingScriptHash(ExecutionEngine engine)
        {
            TR.Enter();
            engine.EvaluationStack.Push(engine.CallingContext.ScriptHash);
            return TR.Exit(true);
        }

        private static bool GetEntryScriptHash(ExecutionEngine engine)
        {
            TR.Enter();
            engine.EvaluationStack.Push(engine.EntryContext.ScriptHash);
            return TR.Exit(true);
        }
    }
}
