using UnityEditor;

[CustomPropertyDrawer(typeof(AudioArrayStorage))]
public class CollectionStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer
{ }

[CustomPropertyDrawer(typeof(AudioDictionary))]
public class AudioDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer
{ }

[CustomPropertyDrawer(typeof(CollectibleIntDictionary))]
public class CollectibleIntDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }