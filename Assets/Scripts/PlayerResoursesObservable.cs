using System;
using System.Collections.Generic;

public class PlayerResoursesObservable
{
    public List<IPlayerResoursesObserver> observers = new List<IPlayerResoursesObserver>();
    public float obsorvableValue;

    public PlayerResoursesObservable(float obsorvableValue)
    {
        this.obsorvableValue = obsorvableValue;
    }

    public void Update(float obsorvableValue)
    {
        this.obsorvableValue = obsorvableValue;
        foreach (var observer in observers)
              observer.Update();
    }
}

public interface IPlayerResoursesObserver
{
    void SetOnUpdateAction(Action onUpdate);
    void Update();
}

public class PlayerResoursesObserver : IPlayerResoursesObserver
{
    PlayerResoursesObservable playerResoursesObservable;
    public float obsorvableValue;
    private Action onUpdate;
    
    public PlayerResoursesObserver(PlayerResoursesObservable playerResoursesObservable)
    {
        this.playerResoursesObservable = playerResoursesObservable;
        playerResoursesObservable.observers.Add(this);
    }

    public void SetOnUpdateAction(Action onUpdate)
    {
         this.onUpdate = onUpdate;
    }

    public void Update()
    {
        obsorvableValue = playerResoursesObservable.obsorvableValue;
        onUpdate.Invoke();
    }
}