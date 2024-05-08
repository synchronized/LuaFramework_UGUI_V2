/* 
 LuaFramework Code By Jarjin leeibution 3.0 License 
*/
using System;
using System.Collections.Generic;

public interface IController
{
    void RegisterCommand(string messageName, Type commandType);

    void ExecuteCommand(IMessage message);

	void RemoveCommand(string messageName);

	bool HasCommand(string messageName);
}
