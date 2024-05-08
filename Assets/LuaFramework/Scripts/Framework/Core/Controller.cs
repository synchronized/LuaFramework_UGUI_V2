/* 
 LuaFramework Code By Jarjin lee
*/

using System;
using System.Collections.Generic;

public class Controller : IController {
    protected IDictionary<string, Type> m_commandMap;

    protected static volatile IController m_instance;
    protected readonly object m_syncRoot = new object();
    protected static readonly object m_staticSyncRoot = new object();

    protected Controller() {
        InitializeController();
    }

    static Controller() {
    }

    public static IController Instance {
        get {
            if (m_instance == null) {
                lock (m_staticSyncRoot) {
                    if (m_instance == null) m_instance = new Controller();
                }
            }
            return m_instance;
        }
    }

    protected virtual void InitializeController() {
        m_commandMap = new Dictionary<string, Type>();
    }

    public virtual void ExecuteCommand(IMessage note) {
        Type commandType = null;
        lock (m_syncRoot) {
            if (m_commandMap.ContainsKey(note.Name)) {
                commandType = m_commandMap[note.Name];
            }
        }
        if (commandType != null) {  //Controller
            object commandInstance = Activator.CreateInstance(commandType);
            if (commandInstance is ICommand) {
                ((ICommand)commandInstance).Execute(note);
            }
        }
    }

    public virtual void RegisterCommand(string commandName, Type commandType) {
        lock (m_syncRoot) {
            m_commandMap[commandName] = commandType;
        }
    }

    public virtual bool HasCommand(string commandName) {
        lock (m_syncRoot) {
            return m_commandMap.ContainsKey(commandName);
        }
    }

    public virtual void RemoveCommand(string commandName) {
        lock (m_syncRoot) {
            if (m_commandMap.ContainsKey(commandName)) {
                m_commandMap.Remove(commandName);
            }
        }
    }
}

