using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace m8.common.Collections;

/// <summary>
///  A type that provide features similar to an array, but does not do bound checks for 
///  performance reasons.
/// </summary>
public unsafe class UnsafeArray<T> : IDisposable where T : unmanaged
{
    private T* _array;
    private int _length;
    private bool _disposed;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="length">Size of the array</param>
    public UnsafeArray(int length)
    {
        _array = (T*)NativeMemory.Alloc((nuint)length, (nuint)sizeof(T));
        _length = length;
        _disposed = false;
    }

    /// <summary>
    ///  Fill the array with a value.
    /// </summary>
    public void Fill(T value)
    {
        for (int i = 0; i < _length; ++i)
        {
            *(_array + i) = value;
        }
    }

    /// <summary>
    ///  Accessor to the elements of the array
    /// </summary>
    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(0 <= index && index < _length);
            Debug.Assert(!_disposed);
            return *(_array +  index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            Debug.Assert(0 <= index && index < _length);
            Debug.Assert(!_disposed);
            *(_array + index) = value;
        }
    }

    /// <summary>
    ///  Dispose the classes ressources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (_array != null)
            {
                NativeMemory.Free(_array);
                _array = null;
            }

            _disposed = true;
        }
    }

    /// <summary>
    ///  Destructor
    /// </summary>
    ~UnsafeArray()
    {
        Dispose(false);
    }
}
