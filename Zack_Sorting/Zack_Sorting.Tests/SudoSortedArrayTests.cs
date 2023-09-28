
using System;

namespace SSA;

[TestClass]
public class SudoSortedArrayTests
{
	[TestMethod]
	[Timeout(60)]
	public void Presorted()
	{
		int[] sortedArrayControl = { 0, 1, 2, 3 };
		int[] mixedArray = { 0, 1, 2, 3 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_RepeatingAddresss()
	{
		int[] sortedArrayControl = { 0, 1, 1, 2 };
		int[] mixedArray = { 0, 1, 1, 2 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_Offset()
	{
		int[] sortedArrayControl = { 2, 3, 4, 5 };
		int[] mixedArray = { 2, 3, 4, 5 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_NegativeOffset()
	{
		int[] sortedArrayControl = { -2, 3, 4, 5 };
		int[] mixedArray = { -2, 3, 4, 5 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_SkippedAddress()
	{
		int[] sortedArrayControl = { 0, 1, 3 };
		int[] mixedArray = { 0, 1, 3 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted()
	{
		int[] sortedArrayControl = { 0, 1 };
		int[] mixedArray = { 1, 0 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_RepeatingAddresss_v1()
	{
		int[] sortedArrayControl = { 1, 1, 2 };
		int[] mixedArray = { 1, 2, 1 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_RepeatingAddresss_v2()
	{
		int[] sortedArrayControl = { 1, 2, 2 };
		int[] mixedArray = { 2, 2, 1 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_RepeatedRepeatingAddresss()
	{
		int[] sortedArrayControl = { 1, 2, 2, 2, 2 };
		int[] mixedArray = { 2, 2, 2, 2, 1 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_MultipleRepeatingAddresss_v1()
	{
		int[] sortedArrayControl = { 1, 1, 2, 2 };
		int[] mixedArray = { 1, 2, 1, 2 };

		AddressedObject[] IndexableArray = new AddressedObject[mixedArray.Length];
		for (int i = 0; i < mixedArray.Length; i++)
		{
			IndexableArray[i] = new AddressedObject(mixedArray[i]);
		}

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = new(IndexableArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_MultipleRepeatingAddresss_v2()
	{
		int[] sortedArrayControl = { 1, 1, 2, 2 };
		int[] mixedArray = { 2, 2, 1, 1 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Unsorted_MultipleRepeatingAddresss_v3()
	{
		int[] sortedArrayControl = { 1, 1, 2, 3, 3 };
		int[] mixedArray = { 3, 2, 3, 1, 1 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Random_MultipleRepeatingAddresss()
	{
		int[] sortedArrayControl = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19, 20, 20 };
		int[] mixedArray = { 13, 2, 9, 7, 10, 11, 19, 14, 8, 6, 4, 17, 3, 12, 1, 15, 5, 18, 20, 16, 13, 2, 9, 7, 10, 11, 19, 14, 8, 6, 4, 17, 3, 12, 1, 15, 5, 18, 20, 16 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void SingleValue_SetSameValue()
	{
		int[] sortedArrayControl = { 1 };
		int[] mixedArray = { 1 };
		var value = new AddressedObject(1);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[0] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[0], value);
		Assert.AreEqual<int>(sortedArrayControl[0], SudoSortedIndexableArray[0].Address);
	}

	[TestMethod]
	[Timeout(60)]
	public void SingleValue_SetDifferentValue()
	{
		int[] sortedArrayControl = { 100 };
		int[] mixedArray = { 1 };
		var value = new AddressedObject(100);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[0] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[0], value);
		Assert.AreEqual<int>(sortedArrayControl[0], SudoSortedIndexableArray[0].Address);
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_SetExistingValue_SortedInsideRange()
	{
		int[] sortedArrayControl = { 1, 2, 2, 3 };
		int[] mixedArray = { 1, 1, 2, 3 };
		var value = new AddressedObject(2);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[1] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[1], value);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_SetNotExistingValue_SortedInsideRange()
	{
		int[] sortedArrayControl = { 1, 2, 3, 4 };
		int[] mixedArray = { 1, 3, 3, 4 };
		var value = new AddressedObject(2);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[1] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[1], value);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Presorted_SetNotExistingValue_UnsortedInsideRange()
	{
		int[] sortedArrayControl = { 1, 2, 3, 4 };
		int[] mixedArray = { 1, 3, 3, 4 };
		var value = new AddressedObject(2);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[2] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[2], value);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void PreSorted_SetNotExistingValue_SortedOutsideRange()
	{
		int[] sortedArrayControl = { 1, 1, 10 };
		int[] mixedArray = { 1, 1, 3 };
		var value = new AddressedObject(10);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[2] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[2], value);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void PreSorted_SetNotExistingValue_UnsortedOutsideRange()
	{
		int[] sortedArrayControl = { 1, 3, 10 };
		int[] mixedArray = { 1, 1, 3 };
		var value = new AddressedObject(10);
		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		SudoSortedIndexableArray[0] = value;

		Assert.AreEqual(SudoSortedIndexableArray.AddressedObjects[0], value);

		for (int i = 0; i < sortedArrayControl.Length; i++)
		{
			Assert.AreEqual<int>(sortedArrayControl[i], SudoSortedIndexableArray[i].Address);
		}
	}

	[TestMethod]
	[Timeout(60)]
	public void Get_IndexOfIAddressedObject()
	{
		int[] mixedArray = { 5, 6, 7, 7, 7, 1, 2, 3, 4 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		Assert.IsNull(SudoSortedIndexableArray.GetIndexOfFirstIAddressedObjectByAddress(10));
		Assert.AreEqual(6, SudoSortedIndexableArray.GetIndexOfFirstIAddressedObjectByAddress(7));
	}

	[TestMethod]
	[Timeout(60)]
	public void Get_IAddressedObject()
	{
		int[] mixedArray = { 5, 6, 7, 7, 7, 1, 2, 3, 4 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		Assert.AreEqual(default, SudoSortedIndexableArray.GetAnyOrDefaultIAddressedObjectByAddress(10));
		Assert.AreEqual(7, SudoSortedIndexableArray.GetAnyOrDefaultIAddressedObjectByAddress(7)?.Address);
	}

	[TestMethod]
	[Timeout(60)]
	public void Count_IAddressedObjects()
	{
		int[] mixedArray = { 5, 6, 7, 7, 7, 1, 2, 3, 4 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		Assert.AreEqual(3, SudoSortedIndexableArray.CountIAddressedObjectsByAddress(7));
		Assert.AreEqual(0, SudoSortedIndexableArray.CountIAddressedObjectsByAddress(10));
	}

	[TestMethod]
	[Timeout(60)]
	public void Get_IAddressedObjects()
	{
		int[] mixedArray = { 5, 6, 7, 7, 7, 1, 2, 3, 4 };

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = InitiateSudoSortedArray(mixedArray);

		Assert.AreEqual(3, SudoSortedIndexableArray.GetIAddressedObjectsByAddress(7).Length);
	}

	[TestMethod]
	[Timeout(60)]
	public void InitializeSudoRandomArray_WithSetRange_AllAddressesInRange_NoError()
	{
		int[] mixedArray = { 55, 63, 76, 78, 78, 12, 29, 35, 46, 0, 100 };

		_ = InitiateSudoSortedArrayWithRange(mixedArray, 0, 100);
	}

	[TestMethod]
	[ExpectedException(typeof(ArgumentOutOfRangeException), "Objects Address outside of given range.")]
	[Timeout(60)]
	public void InitializeSudoRandomArray_WithSetRange_AddressesOutOfRange_ThrowsError()
	{
		int[] mixedArray = { 5, 6, 7, 7, 7, 1, 2, 3, 4 };

		_ = InitiateSudoSortedArrayWithRange(mixedArray, 2, 7);
	}

	private static SudoSortedArray<AddressedObject> InitiateSudoSortedArray(int[] mixedArray)
	{
		AddressedObject[] IndexableArray = new AddressedObject[mixedArray.Length];
		for (int i = 0; i < mixedArray.Length; i++)
		{
			IndexableArray[i] = new AddressedObject(mixedArray[i]);
		}

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = new(IndexableArray);
		return SudoSortedIndexableArray;
	}

	private static SudoSortedArray<AddressedObject> InitiateSudoSortedArrayWithRange(int[] mixedArray, int min, int max)
	{
		AddressedObject[] IndexableArray = new AddressedObject[mixedArray.Length];
		for (int i = 0; i < mixedArray.Length; i++)
		{
			IndexableArray[i] = new AddressedObject(mixedArray[i]);
		}

		SudoSortedArray<AddressedObject> SudoSortedIndexableArray = new(IndexableArray, min, max);
		return SudoSortedIndexableArray;
	}

	public class AddressedObject : IAddressed
	{
		public AddressedObject(int Address)
		{
			this.Address = Address;
		}
		public int Address { get; private set; }

	}
}
