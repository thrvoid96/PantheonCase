using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Actions
{
    public static class ActionFactory
    {
        private static Dictionary<ActionType, Type> actionsByType;
        private static bool isInitialized => actionsByType != null;

        private static void InitializeFactory()
        {
            if (isInitialized) return;

            var actionTypes = Assembly.GetAssembly(typeof(BaseAction)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BaseAction)));

            actionsByType = new Dictionary<ActionType, Type>();

            foreach (var type in actionTypes)
            {
                var temp = Activator.CreateInstance(type) as BaseAction;
                actionsByType.Add(temp.GetActionType(),type);
            }
        }

        public static BaseAction GetAction(ActionType actionType)
        {
            InitializeFactory();

            if (actionsByType.ContainsKey(actionType))
            {
                Type type = actionsByType[actionType];
                var action = Activator.CreateInstance(type) as BaseAction;
                return action;
            }

            return null;
        }

        internal static IEnumerable<ActionType> GetActionTypes()
        {
            InitializeFactory();
            return actionsByType.Keys;
        }
    }
}
