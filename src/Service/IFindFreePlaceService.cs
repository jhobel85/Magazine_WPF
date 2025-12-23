using Magazine_WPF.Model;

namespace Magazine_WPF.Service
{
    public interface IFindFreePlaceService
    {
        int FindAndOccupy(IMagazine magazine);
    }
}
