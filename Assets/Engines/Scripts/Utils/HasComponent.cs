using UnityEngine;

public static class GameObjectExtensions {

	public static bool HasComponent<T> (this GameObject gameObject) {
		return gameObject.TryGetComponent(out T _);
	}

	public static bool HasComponent<T> (this Component component) {
		return component.TryGetComponent(out T _);
	}

}