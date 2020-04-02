using UnityEngine;
using NUnit.Framework;


namespace MLAgents.Tests
{
    public class RayPerceptionTests : MonoBehaviour
    {
        [Test]
        public void TestPerception3D()
        {
            var angles = new[] {0f, 90f, 180f};
            var tags = new[] {"test", "test_1"};

            var go = new GameObject("MyGameObject");
#pragma warning disable CS0618 // Type or member is obsolete
            var rayPer3D = go.AddComponent<RayPerception3D>();
#pragma warning restore CS0618 // Type or member is obsolete
            var result = rayPer3D.Perceive(1f, angles, tags);
            Assert.IsTrue(result.Count == angles.Length * (tags.Length + 2));
        }

        [Test]
        public void TestPerception2D()
        {
            var angles = new[] {0f, 90f, 180f};
            var tags = new[] {"test", "test_1"};

            var go = new GameObject("MyGameObject");
#pragma warning disable CS0618 // Type or member is obsolete
            var rayPer2D = go.AddComponent<RayPerception2D>();
#pragma warning restore CS0618 // Type or member is obsolete
            var result = rayPer2D.Perceive(1f, angles,
                tags);
            Assert.IsTrue(result.Count == angles.Length * (tags.Length + 2));
        }
    }
}
