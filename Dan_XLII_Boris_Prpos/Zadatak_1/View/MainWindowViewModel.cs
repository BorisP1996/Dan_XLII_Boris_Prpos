using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            ListEmploye = GetAllEmployes();
        }

        private List<vwEmploye> listEmploye;
        public List<vwEmploye> ListEmploye
        {
            get
            {
                return listEmploye;
            }
            set
            {
                listEmploye=value;
                OnPropertyChanged("ListEmploye");
            }
        }
        private vwEmploye employe;
        public vwEmploye Employe
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
        public List<vwEmploye> GetAllEmployes()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<vwEmploye> list = new List<vwEmploye>();
                    list = (from x in context.vwEmployes select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        private ICommand deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (deleteUser == null)
                {
                    deleteUser = new RelayCommand(param => DeleteUserExecute(), param => CanDeleteUserExecute());
                }
                return deleteUser;
            }
        }
        public void DeleteUserExecute()
        {
            try
            {
                if (Employe != null)
                {
                    using (Entity context = new Entity())
                    {
                        //taking registration number from selected user 
                        string IdNumber = Employe.IdNumber;
                        //inserting message box that will be shown when delete button is pressed
                        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete employe?", "Delete Confirmation", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            //finding user and card that needs to be deleted, finding them with registration number 
                            tblEmploye employeToDelete = (from r in context.tblEmployes where r.IdNumber == IdNumber select r).First();
                            //removing from database=> both user and his ID card
                            context.tblEmployes.Remove(employeToDelete);
                          
                            // saving changes in database
                            context.SaveChanges();
                            // writing action into file
                            //Write.Writer.WriteDelete(userToDelete);
                            //// refreshing => getting new state from database
                            ListEmploye = GetAllEmployes();
                        }
                        // in case "No" is clicked in line 77
                        else
                        {
                            MessageBox.Show("Deleting proccess is stoped");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteUserExecute()
        {
            if (Employe == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand addUser;
        public ICommand AddUser
        {
            get
            {
                if (addUser == null)
                {
                    addUser = new RelayCommand(param => AddUserExecute(), param => CanAddUserExecute());

                }
                return addUser;
            }
        }

        private void AddUserExecute()
        {
            try
            {
                AddEmploye addEmploye = new AddEmploye();
                //opening new window
                addEmploye.ShowDialog();
                //checking for updates
                //if ((addEmploye.DataContext as AddEmployeViewModel).IsUpdateEmploye == true)
                //{
                    //refresing list => including added user
                    ListEmploye = GetAllEmployes();
                    //sorting refreshed list
                    main.DataGridUsers.Items.SortDescriptions.Add(new SortDescription("DateOfExpire", ListSortDirection.Ascending));
               //}

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddUserExecute()
        {
            return true;
        }

    }
}
