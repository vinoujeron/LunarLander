using System;
using System.Collections.Generic;

public interface IObserver
{
    void SetOnUpdateAction(Action onUpdate);
    void Update();
}

public class PlayerResoursesObserver : IObserver
{
    public float obsorvableValue;
    private PlayerResoursesObservable playerResoursesObservable;
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

public class PlayerResoursesObservable
{
    public List<PlayerResoursesObserver> observers = new List<PlayerResoursesObserver>();
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

public class WinObserver : IObserver
{
    private WinLogic winLogic; // Observable
    private Action onUpdate;

    public WinObserver(WinLogic winLogic)
    {
        winLogic.winObservers.Add(this);
    }
    public void SetOnUpdateAction(Action onUpdate)
    {
        this.onUpdate = onUpdate;
    }

    public void Update()
    {
        onUpdate.Invoke();
    }
}
