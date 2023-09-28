using System;

namespace SSA;

/*
 *This is a thought exercize thinking through how to create a sorting algorithm that is as fast as possible and seems even faster
 *This is a non-comparitive sorting algorithm that takes an array and puts each "Address" into an "Address Book" this is similar to a dictionary.
 *Once everything is added to the address book it is then referenced to pull the sorted item out.
 *
 *This sudo sorted array acts like it is always sorted.
 *I have also set this up to be able to handle replacing items in the array,
 *essentialy deleting whatever is at the index specified and adding in whatever object was specified auto sorting it.
 *
 *Where n is the number of items in the array and r is the range of those items.
 *The initialization takes O(4n+r) and the sorting is O(2n) total for O(6n+r) in all cases.
 *
 *The sorting being O(2n) total means that I have allowed it to be split up per index access
 *this ranges from O(1) for already sorted items to worst case O(2n) if all items get sorted to find the one accesed.
 *
 *Size complexity is O(n+r)
 *This is not a stable sorting algorithm
 *
 *In practice this is dramaticaly slowed down due to the initialization of the Address book and its potential size
 */
/*
*Todo: 
*Handle arrays with null values
*Add in a Stable sort option (need to think about how things are replaced as well)
*Add in a set range option so that any values outside that range throw an error (removing a large amount of hidden work)
*Build in a way to pre initialize as much as possible (eager initialization)
*Build in a way to reuse the Address Book and Array allocations (reallocation)
*Handle large ranges
*Parralelize as much as possible
*Implement IEnumerable for linq
*/
public class SudoSortedArray<T> where T : IAddressed
{
	public int Min { get; private set; }
	public int Max { get; private set; }
	public int Length { get; private set; }
	public T[] AddressedObjects { get; private set; }
	private AddressedRecord[] AddressBook { get; set; } = Array.Empty<AddressedRecord>();


	public SudoSortedArray(T[] array)
	{
		AddressedObjects = array;
		InitializeSudoSortedArray();
	}

	public SudoSortedArray(T[] array, int min, int max)
	{
		Min = min;
		Max = max;
		CheckRange();
		AddressedObjects = array;
		Length = array.Length;
		InitializeIndexArray();
	}

	private void InitializeSudoSortedArray()
	{
		(Min, Max, Length) = GetMinMaxLength(AddressedObjects);
		CheckRange();
		InitializeIndexArray();
	}
	private void CheckRange()
	{
		if ((long)Max - Min != Max - Min)
			throw new ArgumentException("The range of Addresses is too large!");
	}

	private void InitializeIndexArray()
	{
		AddressBook = new AddressedRecord[Max - Min + 1];

		//count each of the unique Addresses given
		foreach (var item in AddressedObjects)
		{
			if (item.Address < Min || item.Address > Max)
			{
				throw new ArgumentOutOfRangeException("Objects Address outside of given range.");
			}
			(AddressBook[item.Address - Min] ??= new AddressedRecord()).Count++;
		}
		InitializeAddresses();
	}

	private void InitializeAddresses(int? startingAddress = null, int? endingAddress = null)
	{
		int index = 0;

		if (startingAddress is not null)
		{
			for (int i = 0; i < startingAddress; i++)
			{
				AddressedRecord item = AddressBook[i];
				if (item is null) continue;
				index += item.Count;
			}
		}
		startingAddress ??= 0;
		endingAddress ??= AddressBook.Length;

		//count each Address and record the range of Addresses
		for (int i = startingAddress.Value; i < endingAddress; i++)
		{
			AddressedRecord item = AddressBook[i];

			if (item is null) continue;
			item.StartingIndex = index; //inclusive
			index += item.Count;
			item.EndingIndex = index - 1; //inclusive
			item.RoamingIndexPointer = item.StartingIndex;
		}
	}

	public T this[int i]
	{
		get => Index(i);
		set => SetIndex(i, value);
	}

	private T Index(int index)
	{
		Sort(index);
		return AddressedObjects[index];
	}
	private void SetIndex(int index, T value)
	{
		Sort(index);
		Replace(index, value);
	}

	private void Replace(int index, T value)
	{
		//if the Addressing is the same just replace the item
		if (AddressedObjects[index].Address == value.Address)
		{
			AddressedObjects[index] = value;
			return;
		}

		//Handling when an item is replaced outside of the handled Address range
		//Unfortionatly this hides a large amount of background work.
		//Consider making this throw an execption or handling it in a better way.
		if (value.Address > Max || value.Address < Min)
		{
			AddressedObjects[index] = value;
			InitializeSudoSortedArray();
			return;
		}

		//if the new items address is in the handled range
		int currentIndex = AddressedObjects[index].Address - Min;
		AddressBook[currentIndex].Count--;
		int targetIndex = value.Address - Min;
		(AddressBook[targetIndex] ??= new AddressedRecord()).Count++;

		AddressedObjects[index] = value;

		InitializeAddresses(
			Math.Min(targetIndex, currentIndex),
			Math.Max(targetIndex, currentIndex));

		AddressedObjects[index] = value;
		InitializeIndexArray();
	}

	public void Sort()
	{
		for (int i = 0; i < Length; i++)
		{
			Sort(i);
		}
	}

	private void Sort(int index)
	{
		T currentItem = AddressedObjects[index];
		int currentAddress = currentItem.Address - Min;

		//in a closed finite system of Addresses when you know where each of them go. If you pick one up (leaving an "empty" space)
		//placing it where it needs to go then pick that one up and reapeat
		//the empty space of the first one will never be left blank and will be filled with the correct Address.
		while (!CheckIfSorted(index, AddressedObjects[index].Address - Min))
		{
			FindTargetIndex(currentAddress, out int targetIndex, out int targetAddress);
			(currentItem, AddressedObjects[targetIndex]) = (AddressedObjects[targetIndex], currentItem); //Inline swap

			currentAddress = targetAddress;
		}
		return;
	}

	private void FindTargetIndex(int currentAddress, out int targetIndex, out int targetAddress)
	{
		targetIndex = AddressBook[currentAddress].RoamingIndexPointer++;
		targetAddress = AddressedObjects[targetIndex].Address - Min;

		//looking for a target index in the matching Address range that is not already sorted
		//since there is an unsorted item there will be an unsorted target
		while (CheckIfSorted(targetIndex, targetAddress))
		{
			targetIndex = AddressBook[currentAddress].RoamingIndexPointer++;
			targetAddress = AddressedObjects[targetIndex].Address - Min;
		}
	}

	internal bool CheckIfSorted(int targetIndex, int targetAddress)
	{
		if (targetIndex >= AddressBook[targetAddress].StartingIndex && targetIndex <= AddressBook[targetAddress].EndingIndex)
		{
			return true;
		}
		return false;
	}

	private static (int min, int max, int length) GetMinMaxLength(T[] array)
	{
		int length = array.Length;
		int min = int.MaxValue;
		int max = int.MinValue;
		if (length > 0)
		{
			for (int i = 0; i < length; i++)
			{
				int index = array[i].Address;
				if (min > index) min = index;
				if (max < index) max = index;
			}
		}
		return (min, max, length);
	}

	public int? GetIndexOfFirstIAddressedObjectByAddress(int Address)
	{
		if (Address < Min || Address > Max) return null;
		return AddressBook[Address - Min]?.StartingIndex;
	}

	public int CountIAddressedObjectsByAddress(int Address)
	{
		if (Address < Min || Address > Max) return 0;
		return AddressBook[Address - Min]?.Count ?? 0;
	}

	public T? GetAnyOrDefaultIAddressedObjectByAddress(int Address)
	{
		int? index = GetIndexOfFirstIAddressedObjectByAddress(Address);
		if (index is null) return default;
		return this[index.Value];
	}

	public T[] GetIAddressedObjectsByAddress(int Address)
	{
		if (Address < Min || Address > Max) return Array.Empty<T>();
		AddressedRecord AddressedRecord = AddressBook[Address - Min];
		if (AddressedRecord is null) return Array.Empty<T>();
		T[] result = new T[AddressedRecord.Count];
		for (int i = 0; i < AddressedRecord.Count; i++)
		{
			result[i] = this[i];
		}
		return result;
	}

	internal class AddressedRecord
	{
		public int Count { get; set; }
		public int StartingIndex { get; set; }
		public int EndingIndex { get; set; }
		private int roamingIndexPointer;
		public int RoamingIndexPointer
		{
			get
			{
				if (roamingIndexPointer > EndingIndex)
				{
					roamingIndexPointer = StartingIndex;
				};
				return roamingIndexPointer;
			}
			set => roamingIndexPointer = value;
		}
	}
}