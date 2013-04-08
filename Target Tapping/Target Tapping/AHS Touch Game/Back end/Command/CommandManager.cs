using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargetTapping.Back_end.Command
{
    public class CommandManager
    {
        //CommandManager is a singleton
        private static readonly CommandManager instance = new CommandManager();

        private LinkedList<CommandInterface> historyList;
        private LinkedList<CommandInterface> redoList;

        //Constructor
        private CommandManager()
        {
            this.historyList = new LinkedList<CommandInterface>();
            this.redoList = new LinkedList<CommandInterface>();

        }

        //invoke command and add it to history list
        public void invokeCommand(CommandInterface command)
        {
            command.execute();

            if (command.isReversible())
            {
                this.historyList.AddFirst(command);
            }
            else
            {
                this.historyList.Clear();
            }
            if (this.redoList.Count() > 0)
            {
                this.redoList.Clear();
            }

        }
        //undo
        public void undo()
        {
            if (this.historyList.Count() > 0)
            {
                CommandInterface command = this.historyList.First();
                this.historyList.RemoveFirst();
                command.unexecute();
                this.redoList.AddFirst(command);
            }
        }

        //redo
        public void redo()
        {

            if (this.redoList.Count() > 0)
            {
                CommandInterface command = this.redoList.First();
                this.redoList.RemoveFirst();
                command.execute();
                this.historyList.AddFirst(command);
            }

        }

        public static CommandManager getInstance()
        {
            return instance;
        }

    }
}
