using CoreLib.Submodules.ModEntity.Atributes;
using MagnetismPotion;
using PugMod;
using System.Linq;
using Unity.Entities;
using UnityEngine;

[EntityModification]
public static class MagnetismPotionTweaks
{
    [EntityModification(ObjectID.DistilleryTable)]
    private static void EditDistilleryTable(Entity entity, GameObject authoring, EntityManager entityManager)
    {
        var canCraftBuffer = entityManager.GetBuffer<CanCraftObjectsBuffer>(entity);

        addBufferEntry(canCraftBuffer, API.Authoring.GetObjectID("MagnetismPotion:PotionMagnetism"), 5);
    }

    private static void addBufferEntry(DynamicBuffer<CanCraftObjectsBuffer> canCraftBuffer, ObjectID itemId, int outputAmount)
    {
        Debug.Log($"[{MagnetismPotionMod.NAME}]: Adding itemId {itemId} to crafter");
        canCraftBuffer.Add(new CanCraftObjectsBuffer
        {
            objectID = itemId,
            amount = outputAmount,
            entityAmountToConsume = 0
        });
    }
}
