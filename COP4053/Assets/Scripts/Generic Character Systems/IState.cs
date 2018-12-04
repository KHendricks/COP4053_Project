using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T> {

    void OnEnter(T owner);

    void Update(T owner);

    void OnExit(T owner);
}
