using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.ViewModel;

namespace Zadatak_1.View
{
    class AddEmployeViewModel : ViewModelBase
    {
        AddEmploye addEmploye;
        Entity context = new Entity();

        public AddEmployeViewModel(AddEmploye addEmployeOpen)
        {
            addEmploye = addEmployeOpen;
            LocationList = GetLocations();
            GenderList = GetGenders();
            //Location = new tblLOCATION();
           // Gender = new tblGender();
            Employe = new tblEmploye();
            Sector = new tblSector();
          
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
        private tblEmploye employe;
        public tblEmploye Employe
        {
            get
            {
                return employe;
            }
            set
            {
                employe = value;
                OnPropertyChanged("Employe");
            }
        }
        private tblSector sector;
        public tblSector Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }
        private List<tblGender> genderList;
        public List<tblGender> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }
        private tblGender gender;
        public tblGender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
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
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save==null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }
        private void SaveExecute()
        {
            tblEmploye newEmploye = new tblEmploye();
            newEmploye.UserName = Employe.UserName;
            newEmploye.Surname = Employe.Surname;
            newEmploye.IdNumber = Employe.IdNumber;
            newEmploye.Number = Employe.Number;
            newEmploye.DateOfBirth = "1996-25-10";
            newEmploye.JMBG = Employe.JMBG;
            //for sector
            string newSector = Sector.SectorName;
            tblSector newSectorObject = new tblSector();
            newSectorObject.SectorName = newSector;
            List<tblSector> sectorList = context.tblSectors.ToList();
            List<string> sectorNames = new List<string>();
            for (int i = 0; i < sectorList.Count; i++)
            {
                sectorNames.Add(sectorList[i].SectorName);
            }
                if (sectorNames.Contains(newSectorObject.SectorName))
                {
                tblSector sectorToFind = new tblSector();
                sectorToFind = (from r in context.tblSectors where r.SectorName == newSectorObject.SectorName select r).First();
                newEmploye.SectorID = sectorToFind.SectorID;
                    context.SaveChanges();
                }
                else
                {
                    
                    context.tblSectors.Add(newSectorObject);
                    context.SaveChanges();
                    newEmploye.SectorID = newSectorObject.SectorID;
                    context.SaveChanges();
                }
            
            newEmploye.GenderID = Gender.GenderID;
            string adress = Location.Adress;
            if (Location.Adress=="Adresa1")
            {
                Location.LocationID = 1;
            }
            if (Location.Adress=="Adresa2")
            {
                Location.LocationID = 1;
            }
            if (Location.Adress == "Adresa3")
            {
                Location.LocationID = 3;
            }
            if (Location.Adress == "Adresa4")
            {
                Location.LocationID = 4;
            }
            if (Location.Adress == "Adresa5")
            {
                Location.LocationID = 5;
            }
            newEmploye.LocationID = Location.LocationID;
            context.tblEmployes.Add(newEmploye);
            context.SaveChanges();
            addEmploye.Close();

        }
        private bool CanSaveExecute()
        {
            return true;
        }
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());

                }
                return close;
            }
        }
        private void CloseExecute()
        {
            try
            {
                //close window
                addEmploye.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            //it is always possible to click close
            return true;
        }
        public List<tblGender> GetGenders()
        {
            List<tblGender> listOfGenders = new List<tblGender>();
            listOfGenders = context.tblGenders.ToList();
            return listOfGenders;
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
