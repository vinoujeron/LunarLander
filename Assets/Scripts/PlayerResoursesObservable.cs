using System;
using System.Collections.Generic;

public class PlayerResoursesObservable
{
     public List<IPlayerResoursesObserver> observers = new List<IPlayerResoursesObserver>();
     private bool _isNoFuel;
     public void Update()
     {
          if (_isNoFuel)
               return;
          
          foreach (var observer in observers)
          {
               observer.Update();
          }
          _isNoFuel = true;
     }
}

public interface IPlayerResoursesObserver
{
     void SetOnUpdateAction(Action onUpdate);
     void Update();
}

public class PlayerResoursesObserver : IPlayerResoursesObserver
{
     private Action onUpdate;
     
     public PlayerResoursesObserver(PlayerResoursesObservable observable)
     {
          observable.observers.Add(this);
     }

     public void SetOnUpdateAction(Action _onUpdate)
     {
          onUpdate = _onUpdate;
     }
     
     public void Update()
     {
          onUpdate.Invoke();
     }
}
