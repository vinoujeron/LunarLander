using UnityEngine;
using UnityEngine.UI;

public class SetFuelText : MonoBehaviour
{
    [SerializeField] private PlayerResourses playerResourses = null;
    [SerializeField] private Text value = null;

    private PlayerResoursesObserver _playerResoursesObserver;

    private void Start()
    {
        _playerResoursesObserver = new PlayerResoursesObserver(playerResourses.playerResoursesObservable);
        _playerResoursesObserver.SetOnUpdateAction(() => { value.text = _playerResoursesObserver.obsorvableValue.ToString("N0"); });
        _playerResoursesObserver.Update();
    }
}
