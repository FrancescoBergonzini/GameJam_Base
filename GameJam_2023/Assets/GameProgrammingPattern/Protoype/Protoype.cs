using Study.Prototype.prototype;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Study
{
    public class Protoype : MonoBehaviour
    {
        Study.Prototype.spawnfunction.Monster spawnGhost()
        {
            return new Study.Prototype.spawnfunction.Ghost();
        }

        private void Start()
        {
            Study.Prototype.spawnfunction.Spawner ghostSpawner = new Study.Prototype.spawnfunction.Spawner(spawnGhost);
        }
    }
}

namespace Study.Prototype.raw
{
    #region raw
    class Monster
    {

    }

    class Ghost : Monster
    {

    }

    class Demon : Monster
    {

    }

    class Sorcerer : Monster
    {

    }

    class Spawner
    {
        protected virtual Monster SpawnMonster()
        {
            throw new System.Exception();
        }
    }

    class GhostSpawner : Spawner
    {
        protected override Monster SpawnMonster()
        {
            return new Ghost();
        }
    }
    #endregion
}

namespace Study.Prototype.prototype
{
    class Monster
    {
        public virtual Monster clone() { throw new System.Exception(); }
    }

    class Ghost : Monster
    {
        int _health;
        int _speed;

        public Ghost(int health, int speed)
        {
            _health = health;
            _speed = speed;
        }

        public override Monster clone()
        {
            return new Ghost(_health, _speed); 
        }
    }


    //all in one...
    class Spawner
    {
        Monster _prototype;

        public Spawner(Monster prototype)
        {
            _prototype = prototype;
        }

        Monster spawnMonster()
        {
            return _prototype.clone();
        }
    }
    
    //test use...
    /*
    private void Start()
    {
        Monster ghostPrototype = new Ghost(12, 5);
        Spawner ghostspawner = new Spawner(ghostPrototype);

        ghostPrototype.clone();
    }
    */
}

namespace Study.Prototype.spawnfunction
{
    public delegate Monster SpawnCallback();

    public class Monster
    {

    }


    public class Ghost : Monster
    {

    }

    class Spawner
    {
        SpawnCallback _spawn;

        public Spawner(SpawnCallback spawn)
        {
            _spawn = spawn;
        }

        Monster spawnMonster() { return _spawn(); }
    }

    //esempio uso
    /*
    Monster spawnGhost()
    {
        return new Ghost();
    }

    private void Start()
    {
        Spawner ghostSpawner = new Spawner(spawnGhost);
    }
    */
}

namespace Study.Prototype.template
{
    public class Monster
    {
        public string Name { get; set; }
    }

    // Esempi di classi derivate
    public class Ghost : Monster
    {
        public Ghost()
        {
            Name = "Ghost";
        }
    }

    public class Demon : Monster
    {
        public Demon()
        {
            Name = "Demon";
        }
    }
    public class Spawner
    {
        protected virtual Monster SpawnMonster() { throw new System.Exception("This method should be overridden."); }
    }


    class SpawnerFor<T> : Spawner where T : Monster, new()
    {
        protected override Monster SpawnMonster()
        {
            return new T();
        }
    }

    //utilizzo
    //Spawner ghostSpawner = new SpawnerFor<Ghost>();
}

