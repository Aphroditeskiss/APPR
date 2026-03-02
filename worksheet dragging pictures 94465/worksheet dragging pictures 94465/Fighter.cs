using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worksheet_dragging_pictures_94465
{
    internal class Fighter
    {
        private string name;
        private bool onboard;
        private int location;

        public Fighter(string c_name)
        {
            name = c_name;
            onboard = false;
        }

        public void SetOnBoard(bool isOnboard, int newLocation)
        {
            onboard = isOnboard;
            location = newLocation;
        }

        public string GetName()
        {
            return name;
        }

        public bool GetIsOnboardValue()
        {
            return onboard;
        }

        public int GetLocation()
        {
            return location;
        }
    }
}
