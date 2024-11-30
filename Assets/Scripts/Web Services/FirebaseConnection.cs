using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseConnection : MonoBehaviour
{
    private DatabaseReference _databaseReference;

    // Firebase bağlantı durumunu kontrol etmek için event'ler
    public event Action OnFirebaseInitialized;
    public event Action<string> OnFirebaseError;

    private void Awake()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Firebase başlatıldı
                FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;
                _databaseReference = FirebaseDatabase.GetInstance(firebaseApp).RootReference;
                Debug.Log("Firebase Realtime Database bağlantısı başarılı.");
                OnFirebaseInitialized?.Invoke();
            }
            else
            {
                // Firebase başlatılamadı
                Debug.LogError($"Firebase bağımlılıkları çözülemedi: {task.Result}");
                OnFirebaseError?.Invoke(task.Result.ToString());
            }
        });
    }

    // Veritabanına veri yazmak için bir yöntem
    public void WriteData(string path, object data, Action onSuccess = null, Action<string> onError = null)
    {
        _databaseReference.Child(path).SetValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log($"Veri başarıyla yazıldı: {path}");
                onSuccess?.Invoke();
            }
            else
            {
                Debug.LogError($"Veri yazılamadı: {path}. Hata: {task.Exception}");
                onError?.Invoke(task.Exception?.Message);
            }
        });
    }

    // Veritabanından veri okumak için bir yöntem
    public void ReadData(string path, Action<DataSnapshot> onSuccess, Action<string> onError)
    {
        _databaseReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log($"Veri başarıyla okundu: {path}");
                onSuccess?.Invoke(task.Result);
            }
            else
            {
                Debug.LogError($"Veri okunamadı: {path}. Hata: {task.Exception}");
                onError?.Invoke(task.Exception?.Message);
            }
        });
    }
}
