using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM;

namespace UndoManager
{

    public class UndoManager : Services.IUndoManager
    {
        private static UndoManager _undoManagerInstance = null;
        private Stack<DelegateUndoCommand> _redoStack;
        private Stack<DelegateUndoCommand> _undoStack;

        private Queue<DelegateUndoCommand> _recordedQueue;
        private bool _record = false;

        private UndoManager()
        {
            _redoStack = new Stack<DelegateUndoCommand>();
            _undoStack = new Stack<DelegateUndoCommand>();
            _recordedQueue = new Queue<DelegateUndoCommand>();
        }

        public static UndoManager Instance
        {
            get
            {
                if (_undoManagerInstance == null)
                {
                    _undoManagerInstance = new UndoManager();
                }
                return _undoManagerInstance;
            }
        }

        private ICommand _doCommand;
        private ICommand _undoCommand;

        public void Execute(DelegateUndoCommand undoableCommand)
        {
            if (undoableCommand != null)
            {
                if (_record)
                {
                    _recordedQueue.Enqueue(undoableCommand);
                }

                _undoStack.Push(undoableCommand);
                _redoStack.Clear(); // This is to align with standard undo-redo functionality in all MS apps .. check Word, Excel etc ..
                undoableCommand.Execute(null); // TODO : What do we do with the parameter. We do nothing .. the way we've implemeted, we dont need this parameter
            }
        }

        public ICommand UndoCommand
        {
            get
            {
                if (_undoCommand == null)
                {
                    _undoCommand = new DelegateCommand(
                        () =>
                        {
                            if (_undoStack.Count > 0)
                            {
                                var undoableCommand = _undoStack.Pop();
                                _redoStack.Push(undoableCommand);
                                undoableCommand.Undo();
                            }
                        },
                        () =>
                        {
                            return _undoStack.Count > 0;
                        }
                        );
                }
                return _undoCommand;
            }
        }

        public ICommand ReDoCommand
        {
            get
            {
                if (_doCommand == null)
                {
                    _doCommand = new DelegateCommand(
                        () =>
                        {
                            if (_redoStack.Count > 0)
                            {
                                var undoableCommand = _redoStack.Pop();
                                _undoStack.Push(undoableCommand);
                                undoableCommand.Execute(null); // TODO : What do we do about the parameter. We do nothing .. the way we've implemeted, we dont need this parameter
                            }
                        },
                        () =>
                        {
                            return _redoStack.Count > 0;
                        }
                        );
                }
                return _doCommand;
            }
        }

        public void ClearUndoStack()
        {
            Debug.WriteLine("ClearUndoStack");
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public void Record()
        {
            _record = true;
        }
        public void StopRecording()
        {
            _record = false;
        }
        public void PlayRecording()
        {
            int count = _recordedQueue.Count();
            for (int i = 0; i < count; i++ )
            {
                var command = _recordedQueue.Dequeue();
                command.Execute(null);
            }
        }
    }
}
