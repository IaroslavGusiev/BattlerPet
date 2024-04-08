using UnityEngine;

namespace Code.Services
{
    public interface IRandom
    {
        int Seed { get; }
        
        void NewSeed();
        
        void NewSeed(int seed);

        float GetFloat(); /// A random float number between 0.0f (inclusive) and 1.0f (inclusive)
        
        int GetInt();
        
        float Range(float min, float max);
        
        int Range(int min, int max);
        
        Vector2 GetInsideCircle(float radius);
        
        Vector3 GetInsideSphere(float radius);
        
        Quaternion GetRotation();
        
        Quaternion GetRotationOnSurface(Vector3 surface);
    }
}