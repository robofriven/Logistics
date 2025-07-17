using UnityEngine;

/// <summary>
/// Defines the contract for any object that can hold other GameObjects as inventory.
/// </summary>
public interface IInventory
{
    /// <summary>
    /// The maximum number of items this inventory can hold.
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// The current number of items in the inventory.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// A convenience property that is true when Count equals Capacity.
    /// </summary>
    bool IsFull { get; }

    /// <summary>
    /// Attempts to add an item to the inventory.
    /// </summary>
    /// <param name="item">The GameObject to add.</param>
    /// <returns>True if the item was added successfully, false otherwise.</returns>
    bool TryAddItem(GameObject item);

    /// <summary>
    /// Attempts to remove an item from the inventory.
    /// </summary>
    /// <param name="item">The GameObject that was removed.</param>
    /// <returns>True if an item was removed successfully, false otherwise.</returns>
    bool TryRemoveItem(out GameObject item);
}
