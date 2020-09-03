# EventHub

How to use:

1. Subscribe/Unsubscribe when and where you want:

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

2. Fire event:

public class B {
  EventHub.Fire<ShowAdsEvent>();
}
