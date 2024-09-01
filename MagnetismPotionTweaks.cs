using CoreLib.Submodules.ModEntity.Atributes;
using MagnetismPotion;
using PugMod;
using Unity.Entities;
using UnityEngine;

[EntityModification]
public static class MagnetismPotionTweaks
{
    private static readonly string ModName = MagnetismPotionMod.NAME;

    [EntityModification(ObjectID.DistilleryTable)]
    private static void EditDistilleryTable(Entity entity, GameObject authoring, EntityManager entityManager)
    {
        var canCraftBuffer = entityManager.GetBuffer<CanCraftObjectsBuffer>(entity);

        addBufferEntrySafe(canCraftBuffer, API.Authoring.GetObjectID("MagnetismPotion:PotionMagnetism"), 5);
    }

    private static void addBufferEntrySafe(DynamicBuffer<CanCraftObjectsBuffer> canCraftBuffer, ObjectID itemId, int outputAmount)
    {
        CanCraftObjectsBuffer newEntry = new CanCraftObjectsBuffer
        {
            objectID = itemId,
            amount = outputAmount,
            entityAmountToConsume = 0
        };

        // For some reason, DynamicBuffer does not implement GetEnumerator, so we have to for-i loop it
        var lastIndex = canCraftBuffer.Length - 1;
        if (canCraftBuffer[lastIndex].objectID == ObjectID.None) // Contains nothing
        {
            Debug.Log($"[{ModName}]: Adding itemId {itemId} to crafter (only entry?)");
            canCraftBuffer[lastIndex] = newEntry;
        }
        else
        {
            // Check for existing entries
            for (int i = 0; i < canCraftBuffer.Length; i++)
            {
                if (canCraftBuffer[i].objectID == itemId)
                {
                    Debug.Log($"[{ModName}]: Crafter already contained itemId {itemId} so not adding it again");
                    return;
                }
            }
            Debug.Log($"[{ModName}]: Appending itemId {itemId} to crafter");
            canCraftBuffer.Add(newEntry);
        }
    }
}
