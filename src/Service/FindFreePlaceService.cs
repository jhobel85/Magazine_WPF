using Magazine_WPF.Model;

namespace Magazine_WPF.Service
{
    /// <summary>
    /// Stateless service that finds and marks a free place for a given magazine.
    /// </summary>
    public class FindFreePlaceService : IFindFreePlaceService
    {
        public int FindAndOccupy(IMagazine magazine)
        {
            var places = magazine.Places;
            var rot = magazine.IsRotary;
            var neededPlaces = magazine.NeededPlaces;

            var freePlace = magazine.FindFreePlace(places, rot, neededPlaces);
            magazine.UpdatePossitionsOccupancy(freePlace);
            return freePlace;
        }
    }
}
