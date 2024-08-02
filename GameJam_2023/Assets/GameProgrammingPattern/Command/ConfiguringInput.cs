using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study.GPP
{

    #region raw
    public class ConfiguringInput : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                fireGun();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                dropBom();
            }
        }

        //
        public void fireGun() { }
        public void dropBom() { }

    }
    #endregion

    #region command pattern

    //exemple for player..
    public class Entity
    {
        public void fireGun() { }
        public void dropBomb() { }
    }
    public interface ICommand
    {
        void Execute(Entity entity);
    }

    #region alternative to interface...
    public class Command
    {
        protected virtual void Execute()
        {

        }
    }
    #endregion

    public class FireGunCommand : ICommand
    {
        public void Execute(Entity entity)
        {
            entity.fireGun();
        }


    }
    public class DropBombCommand : ICommand
    {
        public void Execute(Entity entity)
        {
            entity.dropBomb();
        }


    }

    public class ConfigureInput_better : MonoBehaviour
    {
        private FireGunCommand button_A;
        private DropBombCommand button_B;

        public Entity entity;

        private void Update()
        {
            ICommand command = ProcessInput();

            if (command != null)
            {
                command.Execute(entity);
            }
        }

        public ICommand ProcessInput()
        {
            if (Input.GetKeyDown(KeyCode.A)) return button_A;
            else if (Input.GetKeyDown(KeyCode.B)) return button_B;
            return null;
        }

    }
    #endregion

    #region Undo and Redo

    public interface IUndoCommand
    {
        void Execute();
        void Undo();

    }

    public class Unit : MonoBehaviour
    {
        public Vector2 pos;

        public void MoveTo(Vector2 pos)
        {
            this.pos = pos;
        }
    }
    class MoveUnitCommand : IUndoCommand
    {
        public Unit unit;
        public Vector2 pos;

        //
        public Vector2 pos_before;

        public MoveUnitCommand(Unit unit, Vector2 pos)
        {
            this.unit = unit;
            this.pos = pos;

            pos_before = Vector2.zero;
        }

        public void Execute()
        {
            //remember the last pos so we can revert it...
            pos_before = unit.transform.position;

            unit.MoveTo(pos);
        }

        public void Undo()
        {
            unit.MoveTo(pos_before);
        }

    }

    public class UndoAndRed : MonoBehaviour
    {
        List<MoveUnitCommand> move_commands = new List<MoveUnitCommand>();
        protected int command_pointer;

        public void Update()
        {
            handleInput();
        }

        IUndoCommand handleInput()
        {
            Unit unit = getSelectedUnit();

            #region exemple old..
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                var destY = unit.pos.y - 1;

                return new MoveUnitCommand(unit, new Vector2(unit.pos.x, destY));
            }


            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                var destY = unit.pos.y + 1;

                return new MoveUnitCommand(unit, new Vector2(unit.pos.x, destY));

            }
            #endregion

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //execute.
                //lo aggiungiamo alla lista e lo puntiamo...
                var command = new MoveUnitCommand(unit, new Vector2(unit.pos.x, +1));
                command.Execute();

                move_commands.Add(command);
                command_pointer++;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //undo...
                //revert del corrent e torniamo indietro con il pointer...
                move_commands[command_pointer].Undo();
                command_pointer--;

                //se viene scelto un nuovo comando dopo undo di  uno, si eliminata tutta la lista...
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                //redu...
                //avanziamo col pointer e eseguiamo
                command_pointer++;
                move_commands[command_pointer].Execute();
            }

            return null;

            Unit getSelectedUnit()
            {
                return null; // ritorna unity selezionata...
            }
        }


    }

    #endregion

}
