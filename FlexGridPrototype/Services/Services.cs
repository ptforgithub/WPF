using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     public interface IUndoManager
    {
        void ClearUndoStack();
        void Execute(MVVM.DelegateUndoCommand undoableCommand);
        System.Windows.Input.ICommand ReDoCommand { get; }
        System.Windows.Input.ICommand UndoCommand { get; }
        void Record();
        void StopRecording();
        void PlayRecording();
    }

}
