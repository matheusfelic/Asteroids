using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class PlayerTest
{
    [UnityTest]
    public IEnumerator Player_IsInvulnerableForSpecifiedTime_PlayMode()
    {
        GameObject playerObject = Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        Player player = playerObject.AddComponent<Player>();

        Assert.IsTrue(player.isInvulnerable);
        yield return new WaitForSeconds(player.invulnerabilityTime - 1f);
        Assert.IsTrue(player.isInvulnerable);
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(player.isInvulnerable);
    }

    [UnityTest]
    public IEnumerator Player_IsNotThrustingInitially_PlayMode()
    {
        GameObject playerObject = Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        Player player = playerObject.AddComponent<Player>();

        Assert.IsFalse(player.thrusting);
        yield return null;
        Assert.IsFalse(player.thrusting);
    }

    [UnityTest]
    public IEnumerator Player_TurnsOnCollisionsAfterInvulnerability_PlayMode()
    {
        GameObject playerObject = Object.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        Player player = playerObject.AddComponent<Player>();

        FieldInfo collisionHandlerField = typeof(Player).GetField("collisionHandler", BindingFlags.NonPublic | BindingFlags.Instance);
        IPlayerCollisionHandler collisionHandler = (IPlayerCollisionHandler)collisionHandlerField.GetValue(player);

        Assert.IsFalse(collisionHandler.AreCollisionsEnabled());
        yield return new WaitForSeconds(player.invulnerabilityTime + 1f);
        Assert.IsTrue(collisionHandler.AreCollisionsEnabled());
    }
}
