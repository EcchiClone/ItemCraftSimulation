using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;
using UnityEngine;

public class ResourceManager
{
    // Resources Management
	Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
            return resource as T;

        return null;
    }
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : Object
    {
        if (_resources.TryGetValue(key, out Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;
        //if (key.Contains(".psd"))
        //    loadKey = $"{key}[{key.Replace(".psd", "")}]";

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) =>
        {
            _resources.Add(key, op.Result);
            callback?.Invoke(op.Result);
        };
    }
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback, Action callbackOnEnd = null) where T : Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
                callbackOnEnd?.Invoke();
            }
        };
    }
    public bool Exists(string key) => _resources.ContainsKey(key);
    public void LoadSpriteFromTexture(string textureKey, string spriteName, Action<Sprite> callback)
    {
        Addressables.LoadAssetsAsync<Sprite>(textureKey, null).Completed += handle =>
        {
            if (handle.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[ResourceManager] Failed to load sprites from {textureKey}");
                callback?.Invoke(null);
                return;
            }

            foreach (var sprite in handle.Result)
            {
                if (sprite.name == spriteName)
                {
                    _resources[$"{textureKey}[{spriteName}]"] = sprite;
                    callback?.Invoke(sprite);
                    return;
                }
            }

            Debug.LogWarning($"[ResourceManager] Sprite with name {spriteName} not found in {textureKey}");
            callback?.Invoke(null);
        };
    }

}