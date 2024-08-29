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
        CanCraftObjectsBuffer entry = new CanCraftObjectsBuffer
        {
            objectID = itemId,
            amount = outputAmount,
            entityAmountToConsume = 0
        };
        if (canCraftBuffer.Contains(entry))
        {
            Debug.Log($"[{MagnetismPotionMod.NAME}]: Crafter already contained itemId {itemId} so not adding it again");
        }
        else
        {
            canCraftBuffer.Add(entry);
            Debug.Log($"[{MagnetismPotionMod.NAME}]: Adding itemId {itemId} to crafter");
        }
    }
}
