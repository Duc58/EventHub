# EventHub

A simple Event system manager in Unity. How to use:

1. Subscribe/Unsubscribe when and where you want:
```C#
public class A: MonoBehaviour {
  private void Start() {
       EventHub.Subscribe<ShowAdsEvent>(OnAdsWillShow);
  }

  private void OnDisable() {
       EventHub.Unsubscribe<ShowAdsEvent>(OnAdsWillShow);
  }

  private void OnAdsWillShow(ShowAdsEvent event) { 
      // do something
  }
}
```
2. Fire event:
```C#
public class B {
  EventHub.Fire<ShowAdsEvent>();
}
```
