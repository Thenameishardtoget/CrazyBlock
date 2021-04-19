using System;

namespace Frame
{
    public interface IBridge
    {
        bool IsDirty();
        [Obsolete("This is a frame function, please replace by 'SetDirty'")]
        void __Save();
    }
}