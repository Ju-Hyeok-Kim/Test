using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bizentro.RPA.Framework
{
    public class History
    {
        private List<Command> _commands;
        private int _currentPosition = -1;
        
		public int RedoCommandCount
        {
            get
            {
                if (this._currentPosition < 0)
                    return this._commands.Count;
                else
                    return (this._commands.Count - 1) - this._currentPosition;
            }
        }
        
        public int UndoCommandCount
        {
            get
            {
                return this._currentPosition + 1;
            }
        }

        public History()
        {
            this._commands = new List<Command>();
        }

        public void AddCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            int redoPos = this._currentPosition + 1;

            this._commands.Add(command);
            this._currentPosition = _commands.Count - 1;
        }

        public void AddCommand(CommandType commandType, List<List<object>> cmd)
        {
            this._commands.Add(new Command(commandType, cmd));
            this._currentPosition = this._commands.Count - 1;
        }

        public void IncreaseCommandPosition()
        {
            ++this._currentPosition;
        }

        public void DecreaseCommandPosition()
        {
            --this._currentPosition;
        }

        public void DeleteCurrentCommand()
        {
            this._commands.Remove(this._commands[this._currentPosition]);
            this.DecreaseCommandPosition();
        }
        
        public void Clear()
        {
            this._commands.Clear();
            this._currentPosition = -1;
        }
        
        private Command GetPreviousCommand()
        {
            Command result = null;
            if (this._currentPosition >= 0)
                result = this._commands[_currentPosition];
            return result;
        }
        
        private Command GetNextCommand()
        {
            Command result = null;
            if (this._currentPosition < _commands.Count - 1)
                result = this._commands[_currentPosition + 1];
            return result;
        }

        public Command PerformUndo()
        {
            if (UndoCommandCount <= 0)
                return null;

            Command cmd = this.GetPreviousCommand();
            return cmd;
        }

        public Command PerformRedo()
        {
            if (RedoCommandCount <= 0)
                return null;

            Command cmd = this.GetNextCommand();
            return cmd;
        }
    }
}