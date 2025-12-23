using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Magazine_Conveyor
{
    public class Magazine : INotifyPropertyChanged, IMagazine
    {
        private bool isRotary;
        public static readonly int PLACES_AVAILABLE_DEFAULT = 50;
        public static readonly int NEEDED_PLACES_DEFAULT = 3;
        private int positionCount;
        private int neededPlaces;
        private List<Position> positions;
        private static Magazine? instance;

        public event PropertyChangedEventHandler? PropertyChanged;

        public static Magazine GetInstance(int positionsCount, bool isRotary = false, bool createAlwaysNew = true)
        {
            if (instance == null || createAlwaysNew)
            {
                bool positionsVisibleByDefault = true;
                instance = new Magazine(positionsCount, isRotary)
                {
                    Positions = Enumerable.Range(0, positionsCount).Select(i => new Position(positionsVisibleByDefault)).ToList()
                };
            }
            return instance;
        }

        private Magazine(int positionsCount, bool isRotary = false)
        {
            this.positionCount = positionsCount;
            this.isRotary = isRotary;
            this.positions = [];
        }

        public List<Position> Positions { get => positions; private set => positions = value; } // = Enumerable.Range(0, POSITION_MAX_COUNT).Select(i => new Position()).ToList();                            
        public bool IsRotary
        {
            get => isRotary; set
            {
                if (isRotary == value)
                    return;
                isRotary = value;
                //Inform View
                OnPropertyChanged(nameof(IsRotary));
            }
        }
        public int PositionCount
        {
            get => positionCount; set
            {
                if (positionCount == value)
                    return;
                positionCount = value;
                //Inform View
                OnPropertyChanged(nameof(PositionCount));
            }
        }
        public int NeededPlaces
        {
            get => neededPlaces; set
            {
                if (NeededPlaces == value)
                    return;
                neededPlaces = value;
                //Inform View
                OnPropertyChanged(nameof(NeededPlaces));
            }
        }

        //Inform View
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /**
         * Based on selected PositionCount make each Position visible or not.
         */
        public void UpdatePositionsVisibility()
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                bool select = i <= PositionCount - 1;
                Positions[i].IsVisible = select;
            }
        }

        /**
         * Get information about all current positions (places) whether the place is occupied or not.
         */
        public bool[] Places
        {
            get
            {
                bool isRot = IsRotary;
                var neeededPlaces = NeededPlaces;
                bool[] currPlaces = [];
                var visiblePos = Positions.Where(j => j.IsVisible);
                IEnumerable<bool> occupied = visiblePos.Select(i => i.IsChecked);
                return currPlaces = occupied.ToArray();
            }
        }

        /**         
 * Return -1 if no suitable position found.
 * Return first sutiable position based on FindFreePlace() and mark the Positions as occupied.         
 * Place == Index
 */
        public void UpdatePossitionsOccupancy(int freePlace)
        {
            if (freePlace >= 0)
            {
                var lastPlace = freePlace + NeededPlaces;

                for (int i = freePlace; i < lastPlace; i++)
                {
                    int j = i;
                    var lastIndex = Positions.Count - 1;
                    if (i > lastIndex && IsRotary)
                    {
                        j = i - lastIndex - 1;
                    }

                    Positions[j].IsChecked = true;
                }
            }
        }

        /// <summary>
        /// Function to return the free place in the magazine level
        /// </summary>
        /// <param name="places">Array of bools of dimension "n". True means occupied position, false means available</param>
        /// <param name="isRotary">Flag whether the level is rotary (last position is neighbour of the first one)</param>
        /// <param name="neededPlaces">Number of places needed</param>
        /// <returns>Index of the first position found (zero based) or -1 if no position is found</returns>
        public int FindFreePlace(bool[] places, bool isRotary, int neededPlaces)
        {
            int ret = -1;
            //MessageBox.Show("places " + string.Join(";", places) + ", isRotary " + isRotary + ", neededPlaces " + neededPlaces);
            int i = 0;
            bool previous = true;
            bool current = true;
            int placesFound = 0;
            bool endAlreadyReached = false;
            if (places.Length > 0 && neededPlaces > 0 && neededPlaces <= places.Length)
            {
                do
                {
                    previous = current;
                    current = places[i];

                    if (ret == -1 && !current)
                    {
                        ret = i;
                        placesFound = 1;
                    }
                    else if (!current && !previous)
                    {
                        placesFound++;
                    }
                    else
                    {
                        placesFound = 0;
                        ret = -1;
                    }
                    i++;
                    if (i == places.Length && isRotary && !endAlreadyReached)
                    {
                        i = 0;
                        endAlreadyReached = true;
                    }
                } while (placesFound < neededPlaces && i < places.Length);
            }

            // If not enought free places available
            if (placesFound != neededPlaces)
            {
                ret = -1;
            }
            return ret;
        }
    }

}
