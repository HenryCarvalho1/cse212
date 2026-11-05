using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Attempt to dequeue from an entirely empty queue.
    // Expected Result: Throws an InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. The exception throwing logic works properly.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        
        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", exception.Message);
    }

    [TestMethod]
    // Scenario: Enqueue multiple unique items with the highest priority item placed explicitly at the very end of the queue, then dequeue twice to verify removal.
    // Expected Result: Returns the last item first, removes it from the list, and then returns the second highest priority item.
    // Defect(s) Found: The loop condition `index < _queue.Count - 1` skipped the last item entirely, and the item was never actually removed from the internal list.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("LowPriority", 1);
        priorityQueue.Enqueue("MedPriority", 2);
        priorityQueue.Enqueue("HighPriority", 5); // Placed at the very end

        var firstResult = priorityQueue.Dequeue();
        Assert.AreEqual("HighPriority", firstResult);

        var secondResult = priorityQueue.Dequeue();
        Assert.AreEqual("MedPriority", secondResult);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where more than one item shares the exact same highest priority.
    // Expected Result: The item added first (closest to the front) should be dequeued first according to the FIFO requirement.
    // Defect(s) Found: The loop condition used `>=` instead of `>`, which caused the queue to incorrectly return the newest/last item added rather than the first.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstHigh", 10); // Added first
        priorityQueue.Enqueue("SecondHigh", 10);
        priorityQueue.Enqueue("ThirdHigh", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("FirstHigh", result);
    }
}