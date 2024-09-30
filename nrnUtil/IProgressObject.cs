using System;

namespace nrnUtil
{
	public interface IProgressObject
    {
        void SetProgressMaxValue(int value);
        void IncreaseProgressValue(int value);
    }
	
}