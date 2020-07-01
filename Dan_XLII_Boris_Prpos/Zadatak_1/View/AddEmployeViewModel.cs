using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    class AddEmployeViewModel : ViewModelBase
    {
        AddEmploye addEmploye;

        public AddEmployeViewModel(AddEmploye addEmployeOpen)
        {
            addEmploye = addEmployeOpen;
            LocationList = GetLocations();
          
        }
        private bool isUpdateEmploye;
        public bool IsUpdateEmploye
        {
            get
            {
                return isUpdateEmploye;
            }
            set
            {
                isUpdateEmploye = value;
            }
        }
        private List<tblLOCATION> locationList;
        public List<tblLOCATION> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }
        private tblLOCATION location;
        public tblLOCATION Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        public List<tblLOCATION> GetLocations()
        {
            string path = @"../../Locations.txt";
            StreamReader sr = new StreamReader(path);
            string line = "";
            List<string> locations = new List<string>();
            while ((line=sr.ReadLine())!=null)
            {
                locations.Add(line);
            }
            sr.Close();

            List<tblLOCATION> locationList = new List<tblLOCATION>();
            Entity context = new Entity();
            for (int i = 0; i < locations.Count; i++)
            {
                tblLOCATION oneLocation = new tblLOCATION();
                List<string> adressList = new List<string>();
                adressList = locations[i].Split(',').ToList();
                oneLocation.Adress = adressList[0];
                oneLocation.Place = adressList[1];
                oneLocation.States = adressList[2];
                locationList.Add(oneLocation);
                //context.tblLOCATIONS.Add(oneLocation);
            }
            if (context.tblLOCATIONS.Count()==0)
            {
                for (int i = 0; i < locationList.Count; i++)
                {
                    context.tblLOCATIONS.Add(locationList[i]);
                }
            }
            context.SaveChanges();
            return locationList;
        }
    }
}
