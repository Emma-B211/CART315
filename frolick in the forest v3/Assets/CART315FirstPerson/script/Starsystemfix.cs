
using UnityEngine;

public class StarSystemFix : MonoBehaviour
{
    public ParticleSystem stars;

    void Start()
    {
        if (stars == null)
            stars = GetComponent<ParticleSystem>();

        if (stars == null)
        {
            Debug.LogError("No Particle System found!");
            return;
        }

        var main = stars.main;
        main.loop = true;
        main.playOnAwake = true;
        main.startLifetime = 100f;
        main.startSpeed = 0f;
        main.startSize = 0.15f;
        main.startColor = Color.white;
        main.maxParticles = 1000;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = stars.emission;
        emission.enabled = true;
        emission.rateOverTime = 50f;

        var shape = stars.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 10f;
        shape.radiusThickness = 1f;

        var renderer = stars.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
            renderer.renderMode = ParticleSystemRenderMode.Billboard;
        }

        stars.Clear();
        stars.Play();

        Debug.Log("Star Particle System started!");
    }
}
