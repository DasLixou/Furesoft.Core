﻿using System;
using System.Collections.Generic;
using System.Linq;
using Furesoft.Core.Activation;

namespace Furesoft.Core.CLI
{
	/// <summary>
	/// A Class to build CommandLine Applications easily
	/// </summary>
	public class App
	{
		private Dictionary<string, ICliCommand> _commands = new Dictionary<string, ICliCommand>();

		public void AddCommand(ICliCommand cmd)
		{
			_commands.Add(cmd.Name, cmd);
		}

		/// <summary>
		/// Start The Application
		/// </summary>
		/// <returns>The Return Code</returns>
		public int Run()
		{
			//collect all command processors
			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(_ => _.GetTypes());

			foreach (var t in types)
			{
				if (t.IsInterface || t.IsAbstract)
				{
					continue;
				}
				else if (typeof(ICliCommand).IsAssignableFrom(t))
				{
					var instance = DefaultActivator.Instance.CreateInstance<ICliCommand>(t, Array.Empty<Type>());
					_commands.Add(instance.Name, instance);
				}
			}

			var args = Environment.GetCommandLineArgs();

			if (args.Length == 1)
			{
				PrintAllCommands();
				return -1;
			}

			if (args.Length == 2 && (args[1] == "--interactive" || args[1] == "-i"))
			{
				while (true)
				{
					Console.Write(">> ");
					var input = Console.ReadLine();
					ProcessCommand(input.Split(' ', StringSplitOptions.RemoveEmptyEntries));
				}
			}
			else
			{
				return ProcessCommand(args);
			}
		}

		public int EvaluateLine(string cmd)
		{
			return ProcessCommand(cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries)); ;
		}

		private int ProcessCommand(string[] args)
		{
			var name = args[1];

			//find correct processor and invoke it with new argumentvector
			if (_commands.ContainsKey(name))
			{
				return _commands[name].Invoke(new CommandlineArguments(args));
			}
			else if (name == "help")
			{
				PrintAllCommands();
			}
			else
			{
				// Print list of commands with helptext
				PrintAllCommands();
			}

			return -1;
		}

		private void PrintAllCommands()
		{
			Console.WriteLine("Command\t\t\tDescription\t\t\tExample");
			Console.WriteLine("-----------------------------");
			foreach (var cmd in _commands)
			{
				Console.WriteLine(cmd.Key + "\t" + cmd.Value.Description + "\t" + cmd.Value.HelpText);
			}
		}
	}
}